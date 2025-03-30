using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip ShootingClip;
    [SerializeField] [Range(0f, 1f)] float shootVolume = 1f;

    [Header("Damaging")]
    [SerializeField] AudioClip DamageClip;
    [SerializeField] [Range(0f, 1f)] float damageVolume = 1f;

    static AudioPlayer instance;

    void Awake()
    {
        ManageSingleton();
    }

    void ManageSingleton(){
        if(instance != null){
            gameObject.SetActive(false);
            Destroy(gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayShootingClip(){
        PlayClip(ShootingClip, shootVolume);
    }

    public void PlayDamageClip(){
        PlayClip(DamageClip, damageVolume);
    }

    void PlayClip(AudioClip clip, float volume){
        if(clip != null){
            Vector3 camPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clip, camPos);
        }
    }
}
