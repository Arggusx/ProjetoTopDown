﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorSFX : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    public void PlaySFX(AudioClip clip)
    {
        audioSource.PlayOneShot(clip); // Toca algum efeito sonoro (ou música) somente uma vez
    }
}
