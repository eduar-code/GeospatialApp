using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsToModel : MonoBehaviour
{
    [SerializeField] Animations[] animations;



    [System.Serializable]
    private class Animations
    {
        public CharacterType characterType;
        public String[] nameAnimation;
    }

    public String[] GetAnimationsSelect(CharacterType characterType)
    {
        return GetAnimations(characterType).nameAnimation;
    }

    private Animations GetAnimations(CharacterType characterType)
    {
        foreach (Animations anim in animations)
        {
            if (anim.characterType == characterType)
            {
                return anim;
            }
        }
        return null;
    }
}
