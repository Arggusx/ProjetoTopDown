using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    [Header("Attacking Settings")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask enemyLayer;

    private bool isHitting;
    private float timeCount;
    private float recoveryTime = 1.5f;
    private Player player; // Referência ao script Player
    private Animator anim; // Referência ao componente Animator
    private bool isAttacking;

    private Casting cast;

    void Start()
    {
        player = GetComponent<Player>(); // Pega o componente Player
        anim = GetComponent<Animator>(); // Pega o componente Animator

        cast = FindObjectOfType<Casting>();
    }

    void Update()
    {
        OnMove(); // Chama a função de movimentação
        OnRun();  // Chama a função de corrida

        if (isHitting)
        {
            timeCount += Time.deltaTime; // Contador
        }

        if (timeCount >= recoveryTime)
        {
            isHitting = false; // Toma mais um dano
            timeCount = 0f; // Zera para reiniciar a contágem
        }
    }

    #region Movement

    void OnMove()
    {
        if (player.direction.sqrMagnitude > 0) // Verifica se está se movendo (Vector2 diferente de zero)
        {
            if (player.isRolling) // Se estiver rolando
            {
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("roll")) // Se a animação não estiver sendo executada
                {
                    anim.SetTrigger("isRoll"); // Aciona animação de rolar
                }
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

        if (player.isAttacking)
        {
            anim.SetInteger("transition", 6);
        }
    }

    void OnRun()
    {
        if (player.isRunning && player.direction.sqrMagnitude > 0) // Se estiver correndo
        {
            anim.SetInteger("transition", 2); // Animação de correr
        }
    }
    #endregion

    #region Attack

    public void OnAttack()
    {
        // Armazena a posição do colisor da espada, raio e a layer de quem vai ser atacado
        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, radius, enemyLayer);

        if (hit != null)
        {
            hit.gameObject.GetComponentInChildren<AnimationControll>().OnHit(); // Pega elementos do objeto filho
            Debug.Log("Atacou o inimigo");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, radius);
    }

    #endregion

    public void OnCasting()
    {
        cast.OnCasting();
    }

    // Quando o jogador clica no botão referente a pesca
    public void OnCastingStarded()
    {
        anim.SetTrigger("isCasting");
        player.isPaused = true;
    }

    // Chamado quando a animação de pesca termina
    public void OnCastingEnded()
    {
        cast.OnCasting();
        player.isPaused = false;
    }

    public void OnHammeringStarted() // Ao começar a contágem de construção, começa a animação
    {
        anim.SetBool("hammering", true);
    }

    public void OnHammeringEnded() // Ao terminar de construir, para a animação
    {
        anim.SetBool("hammering", false);
    }

    public void OnHit()
    {
        if (!isHitting) // Player toma dano
        {
            anim.SetTrigger("hit");
            isHitting = true; // Para de tomar dano
            player.isRunning = false;
        }
    }

}
