using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotFarm : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Sprite hole;
    [SerializeField] private Sprite carrot;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Settings")]
    [SerializeField] private int digAmount;
    [SerializeField] private float waterAmount;
    [SerializeField] private bool detecting;

    private int inicialDigAmount;
    private float currentWater;

    PlayerItems playerItems;

    private bool dugHole;

    private void Start()
    {
        playerItems = FindObjectOfType<PlayerItems>();
        inicialDigAmount = digAmount;
    }

    void Update()
    {
        if (dugHole)
        {
            if (detecting)
            {
                currentWater += 0.1f;
            }

            if (currentWater >= waterAmount) // Atingiu o limite necessário para crescer
            {
                // Plantar cenoura
                spriteRenderer.sprite = carrot;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    spriteRenderer.sprite = hole;
                    playerItems.carrots++;
                    currentWater = 0f;
                }
            }
        }
    }

    public void OnHit()
    {

        digAmount--;
        if (digAmount <= inicialDigAmount / 2)
        {
            spriteRenderer.sprite = hole;
            dugHole = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dig"))
        {
            OnHit();
        }

        if (collision.CompareTag("Water"))
        {
            detecting = true;
        }
    }



    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Water"))
        {
            detecting = false;
        }
    }
}
