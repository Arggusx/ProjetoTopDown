using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControll : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private int xpValue;


    private PlayerAnim playerAnim;
    private Animator anim;
    private Skeleton skeleton;
    private PlayerItems playerItems;

    private Vector3 startPosition;
    private Quaternion startRotation;


    private void Start()
    {
        playerItems = FindObjectOfType<PlayerItems>();
        anim = GetComponent<Animator>();
        playerAnim = FindObjectOfType<PlayerAnim>();
        skeleton = GetComponentInParent<Skeleton>(); // Pega elementos do objeto pai
        startPosition = transform.position;
        startRotation = transform.rotation;
        skeleton.currentHealth = skeleton.totalHealth;
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
        if (skeleton.currentHealth <= 0 && !skeleton.isDead)
        {
            anim.SetTrigger("death"); // Toca a animação de morte
            skeleton.isDead = true;
            playerItems.XpLimit(10);
            StartCoroutine(DieCoroutine()); // Aguarda a animação antes de desativar
        }
        else
        {
            anim.SetTrigger("hit");
            skeleton.currentHealth -= 3; // Dano do player
            skeleton.helthBar.fillAmount = skeleton.currentHealth / skeleton.totalHealth;
        }
    }

    private IEnumerator DieCoroutine()
    {
        yield return new WaitForSeconds(1.5f); // Tempo da animação de morte
        Die();
    }

    void Die()
    {
        skeleton.gameObject.SetActive(false); // Desativa o inimigo
        Invoke(nameof(Respawn), 3f); // Renasce após 3 segundos
    }

    void Respawn()
    {
        skeleton.transform.position = startPosition;
        skeleton.transform.rotation = startRotation;
        skeleton.currentHealth = skeleton.totalHealth;
        skeleton.isDead = false;
        skeleton.helthBar.fillAmount = 1f;

        anim.ResetTrigger("death");
        anim.SetInteger("transition", 0);

        skeleton.gameObject.SetActive(true);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, radius);
    }
}
