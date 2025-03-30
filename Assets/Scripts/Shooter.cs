using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")] 
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float baseFiringRate = 0.2f;

    [Header("AI")]
    [SerializeField] float firingRateVariance = 0f;
    [SerializeField] float minFiringRate = 0.1f;
    [SerializeField] bool useAI;

    [HideInInspector] public bool isFiring;

    Coroutine firingCoroutine;
    AudioPlayer audioPlayer;

    void Awake()
    {
        audioPlayer = FindAnyObjectByType<AudioPlayer>();
    }

    void Start()
    {
        if(useAI){
            isFiring = true;
        }
    }

    void Update()
    {
        Fire();
    }

    void Fire(){
        if(isFiring && firingCoroutine == null) firingCoroutine = StartCoroutine(FireCotinuously());
        else if(!isFiring && firingCoroutine != null) {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    IEnumerator FireCotinuously(){
        while(true){
            GameObject instance = Instantiate(projectilePrefab, 
                                            transform.position, Quaternion.identity);

            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            if(rb != null){
                rb.linearVelocity = transform.up * projectileSpeed;
            }
            Destroy(instance, projectileLifetime);

            float timeToNextProjectile = Random.Range(baseFiringRate - firingRateVariance, 
                                            baseFiringRate + firingRateVariance);
            timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minFiringRate, float.MaxValue);

            audioPlayer.PlayShootingClip();

            yield return new WaitForSeconds(timeToNextProjectile);
        }
    }
}
