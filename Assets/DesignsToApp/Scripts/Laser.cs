using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Laser : MonoBehaviour
{

    [SerializeField] private Transform muzzlePoint;
    [SerializeField] private ParticleSystem muzzleParticle;
    [SerializeField] private ParticleSystem hitParticle;
    [SerializeField] private GameObject downPanel;
    [SerializeField] private GameObject topPanel;
    [SerializeField] private AudioSource laserSound;
    [SerializeField] private TextMeshProUGUI nameCharacter;
     [SerializeField] private float damage;
   

    LineRenderer bean;

    void Awake()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        muzzlePoint = gameObject.transform.GetChild(1);
        bean = gameObject.GetComponent<LineRenderer>();
        bean.enabled = false;
    }


    private void FixedUpdate()
    {
        Ray ray = new Ray(muzzlePoint.position, muzzlePoint.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.name == "Spaceship")
            {
                ActDesacPanelgun(true);
                bean.SetPosition(0, muzzlePoint.position);
                bean.SetPosition(1, hit.transform.position);
                hitParticle.transform.position = hit.transform.position;

                if (bean.enabled && hit.collider.TryGetComponent(out Damageable damageable))
                {
                    damageable.ApplyDamage(damage * Time.fixedDeltaTime);
                }
            }
            nameCharacter.text = hit.transform.name;
            nameCharacter.color = Color.yellow;
        }
        else
        {
            ActDesacPanelgun(false);
            nameCharacter.text = "Looking";
            nameCharacter.color = Color.white;
        }
    }

    public void Shoot(bool val)
    {

        if (val)
        {
            bean.enabled = true;
            muzzleParticle.Play();
            hitParticle.Play();
            laserSound.Play();
        }
        else
        {
            bean.enabled = false;
            muzzleParticle.Stop();
            hitParticle.Stop();
            laserSound.Stop();
        }

    }

    private void ActDesacPanelgun(bool val)
    {
        if (val)
        {
            downPanel.GetComponent<Image>().color = new Color(255, 0, 0, 212);
            topPanel.GetComponent<Image>().color = new Color(255, 0, 0, 212);
            downPanel.transform.GetChild(0).gameObject.SetActive(true);
            downPanel.transform.GetChild(1).gameObject.SetActive(false);
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            downPanel.GetComponent<Image>().color = new Color(255, 255, 255, 212);
            topPanel.GetComponent<Image>().color = new Color(255, 255, 255, 212);
            downPanel.transform.GetChild(0).gameObject.SetActive(false);
            downPanel.transform.GetChild(1).gameObject.SetActive(true);
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            Shoot(false);

        }
    }
}
