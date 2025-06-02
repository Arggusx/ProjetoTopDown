using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : MonoBehaviour
{
    [SerializeField] private AudioClip bgmMusic;

    private AudioManeger audioM;

    // Start is called before the first frame update
    void Start()
    {
        audioM = FindObjectOfType<AudioManeger>(); // Procura no projeto algum objeto que tenha o 'AudioManeger'

        audioM.PlayBGM(bgmMusic); // A música que vai estar na cena
    }

}
