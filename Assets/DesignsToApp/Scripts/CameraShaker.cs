using System;
using System.Collections;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    //[SerializeField] private Transform camera;
    [SerializeField] private bool start = false;
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float duration = 1f;

    private static event Action Shake;


    void Update()
    {
        if (start)
        {
            start = false;
            StartCoroutine(Shaking());
        }
    }


    public static void Invoke()
    {
        Shake?.Invoke();
    }

    private void OnEnable() => Shake += CameraShake;
    private void OnDisable() => Shake -= CameraShake;

    public void CameraShake()
    {
        StartCoroutine(Shaking());
    }

    IEnumerator Shaking()
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime/duration);
            transform.position = startPosition + UnityEngine.Random.insideUnitSphere * strength;
            yield return null;
        }
        transform.position = startPosition;
    }

}
