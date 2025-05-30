using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueControl : MonoBehaviour
{
    [System.Serializable]
    public enum idiom
    {
        pt,
        ing,
        spa
    }

    public idiom language;

    [Header("Components")]
    public GameObject dialogueObj; // Janela de diálogo
    public Image profileSprite; // Imagem do NPC
    public Text speechText; // Texto de diálogo do NPC
    public Text actorNameText; // Nome do NPC

    [Header("Settings")]
    public float typingSpeed; // Velocidade da fala

    // Variáveis de controle
    [HideInInspector] public bool isShowing; // Para saber  se a janela está aberta
    private int index; // Par saber quantos textos tem em um diálogo
    private string[] sentences;

    public static DialogueControl instance;

    private void Awake() // Na hierarquia de scripts, 'Awake' é o primeiro a ser chamado
    {
        instance = this;
    }

    void Start()
    {

    }

    void Update()
    {

    }

    IEnumerator TypeSentence() // Coroutine que escreve uma frase letra por letra, com um intervalo entre cada letra
    {
        foreach (char letter in sentences[index].ToCharArray()) // Para cada caractere (letter) da frase atual (convertemos a string em array de caracteres)
        {
            speechText.text += letter; // Adiciona a letra atual ao texto exibido na interface
            yield return new WaitForSeconds(typingSpeed); // Espera por um curto intervalo de tempo antes de adicionar a próxima letra
        }
    }

    public void NextSentence() // Pula para a próxima frase
    {
        if (speechText.text == sentences[index]) // Se a frase apareceu por completo
        {
            if (index == sentences.Length - 1)
            {
                index++;
                speechText.text = "";
                StartCoroutine(TypeSentence());
            }
            else // Quando termina os textos
            {
                speechText.text = "";
                index = 0;
                dialogueObj.SetActive(false);
                sentences = null;
                isShowing = false;
            }
        }
    }

    public void Speech(string[] txt) // Chama a fala do NPC
    {
        if (!isShowing) // Para enquanto uma fala continuar, não executar o 'Speech' novamente
        {
            dialogueObj.SetActive(true);
            sentences = txt;
            StartCoroutine(TypeSentence());
            isShowing = true;
        }
    }

}
