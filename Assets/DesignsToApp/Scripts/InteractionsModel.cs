using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Rendering.Universal.Internal;
using UnityEngine.SceneManagement;

public class InteractionsModel : MonoBehaviour
{
    [SerializeField] CharacterType characterType;
    [SerializeField] String dateInfo;
    [SerializeField] GameObject gameObjectInfo;

    Animator animator;
    String[] animName;

    void Start()
    {
        animator = this.GetComponent<Animator>();
        gameObjectInfo = this.gameObject.transform.Find("Info").gameObject;
        if (characterType.ToString() != "Info")
        {
            animName = FindObjectOfType<AnimationsToModel>().GetAnimationsSelect(characterType);
        }

    }

    void Update()
    {

        if (gameObjectInfo.activeInHierarchy == true){
            gameObjectInfo.transform.LookAt(Camera.main.transform);
        }

    }


    private void OnMouseUp()
    {
        if (characterType.ToString() != "Info" && dateInfo == "")
        {
            animator.SetTrigger(animName[0]);
        }
        else if (characterType.ToString() != "Info" && dateInfo != null)
        {
            animator.SetTrigger(animName[0]);
            gameObjectInfo.SetActive(false);
        }
        else
        {
            gameObjectInfo.SetActive(false);
        }
    }

    private void OnMouseDown()
    {
        if (characterType.ToString() != "Info" && dateInfo == "")
        {
            animator.SetTrigger(animName[1]);
        }
        else if (characterType.ToString() != "Info" && dateInfo != null)
        {
            animator.SetTrigger(animName[1]);
            gameObjectInfo.SetActive(true);
            GameObject textInfo = gameObjectInfo.transform.GetChild(0).gameObject;
            textInfo.transform.GetChild(0).GetComponent<TextMeshPro>().text = dateInfo;
        }
        else
        {
            gameObjectInfo.SetActive(true);
            GameObject textInfo = gameObjectInfo.transform.GetChild(0).gameObject;
            textInfo.transform.GetChild(0).GetComponent<TextMeshPro>().text = dateInfo;
        }
    }

}
