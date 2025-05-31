using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    private Player player; // Referência ao script Player
    private Animator anim; // Referência ao componente Animator

    void Start()
    {
        player = GetComponent<Player>(); // Pega o componente Player
        anim = GetComponent<Animator>(); // Pega o componente Animator
    }

    void Update()
    {
        OnMove(); // Chama a função de movimentação
        OnRun();  // Chama a função de corrida
    }

    #region Movement

    void OnMove()
    {
        if (player.direction.sqrMagnitude > 0) // Verifica se está se movendo (Vector2 diferente de zero)
        {
            if (player.isRolling) // Se estiver rolando
            {
                anim.SetTrigger("isRoll"); // Aciona animação de rolar
            }
            else
            {
                anim.SetInteger("transition", 1); // Animação de andar
            }
        }
        else
        {
            anim.SetInteger("transition", 0); // Animação de idle (parado)
        }

        if (player.direction.x > 0) // Indo para a direita
        {
            transform.eulerAngles = new Vector2(0, 0); // Mantém a rotação normal
        }

        if (player.direction.x < 0) // Indo para a esquerda
        {
            transform.eulerAngles = new Vector2(0, 180); // Espelha horizontalmente
        }

        if (player.isCutting) // Se estiver cortando
        {
            anim.SetInteger("transition", 3); // Animação de cortar
        }

        if (player.isDigging) // Se estiver cavando
        {
            anim.SetInteger("transition", 4); // Animação de cavar
        }

        if (player.isWatering) // Se estiver regando
        {
            anim.SetInteger("transition", 5); // Animação de regar
        }
    }

    void OnRun()
    {
        if (player.isRunning) // Se estiver correndo
        {
            anim.SetInteger("transition", 2); // Animação de correr
        }
    }

    #endregion
}
