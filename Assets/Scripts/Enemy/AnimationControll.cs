using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControll : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask playerLayer;



    private PlayerAnim playerAnim;
    private Animator anim;
    private Skeleton skeleton;


    private void Start()
    {
        anim = GetComponent<Animator>();
        playerAnim = FindObjectOfType<PlayerAnim>();
        skeleton = GetComponentInParent<Skeleton>(); // Pega elementos do objeto pai
    }

    public void PlayAnim(int value)
    {
        anim.SetInteger("transition", value);
    }

    public void Attack()
    {
        if (!skeleton.isDead)
        {
            Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, radius, playerLayer); // Detecção de colisão somente ao bater no player

            if (hit != null)
            {
                playerAnim.OnHit();
                Debug.Log("Player tomou dano");
            }
            else
            {
                Debug.Log("Inimigo errou o ataque");
            }
        }
    }

    public void OnHit()
    {
        if (skeleton.currentHealth <= 0)
        {
            anim.SetTrigger("death");
            skeleton.isDead = true;

            Destroy(skeleton.gameObject, 1.5f);
        }
        else
        {
            anim.SetTrigger("hit");
            skeleton.currentHealth -= 3; // Dano do Player

            // Para diminuir a barra de vida
            skeleton.helthBar.fillAmount = skeleton.currentHealth / skeleton.totalHealth;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, radius);
    }
}
