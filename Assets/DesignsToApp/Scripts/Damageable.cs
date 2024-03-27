using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{

    [SerializeField] private float initialHealth;
    [SerializeField] private GameObject explosion;
    [SerializeField] private AudioSource audioExplosion;
    private float currentHealt;


    void Awake()
    {
        currentHealt = initialHealth;
        explosion.transform.localScale = gameObject.transform.localScale;
    }

    private void OnEnable()
    {
        currentHealt = initialHealth;
    }

    public void ApplyDamage(float damage)
    {
        if (currentHealt <= 0) return;
        currentHealt -= damage;

        if (currentHealt <= 0)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Debug.Log(explosion.transform.localScale);
            CameraShaker.Invoke();
            audioExplosion.Play();
            gameObject.GetComponent<Collider>().enabled = false;
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            StartCoroutine(EnableShip());
        }
    }

    IEnumerator EnableShip()
    {
        yield return new WaitForSeconds(5);
        gameObject.GetComponent<Collider>().enabled = true;
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        currentHealt = initialHealth;
    }

}
