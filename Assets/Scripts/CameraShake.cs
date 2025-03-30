using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] float shakeDuration = 1f;
    [SerializeField] float shakeMagnitude = 0.5f;

    Vector3 inititalPosition;

    void Start()
    {
        inititalPosition = transform.position;
    }

    public void Play(){
        StartCoroutine(Shake());
    }

    IEnumerator Shake(){
        float elapsedTime = 0;
        while(elapsedTime < shakeDuration){
            transform.position = inititalPosition + (Vector3)Random.insideUnitCircle * shakeMagnitude;
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.position = inititalPosition;
    }
}
