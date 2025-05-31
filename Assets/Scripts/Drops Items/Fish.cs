using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Se colidiu com o jogador
        {
            // Aumenta a quantidade de madeira do jogador
            collision.GetComponent<PlayerItems>().fishes++;

            // Destroi esse objeto de madeira
            Destroy(gameObject);
        }
    }
}
