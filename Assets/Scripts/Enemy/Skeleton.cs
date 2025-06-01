using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;



public class Skeleton : MonoBehaviour
{
    [Header("Stats")] // Define no inspector da Unity
    public float currentHealth;
    public float totalHealth;
    public Image helthBar;
    public bool isDead;

    [Header("Components")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private AnimationControll animControll;

    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = totalHealth;
        player = FindObjectOfType<Player>();
        agent.updateRotation = false; // Não rotaciona o eixo Z
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            agent.SetDestination(player.transform.position); // Inimigo anda em direção ao player

            if (Vector2.Distance(transform.position, player.transform.position) <= agent.stoppingDistance)
            {
                animControll.PlayAnim(2);
            }
            else
            {
                animControll.PlayAnim(1);
            }

            float positionX = player.transform.position.x - transform.position.x;

            if (positionX > 0)
            {
                transform.eulerAngles = new Vector2(0, 0);
            }
            else
            {
                transform.eulerAngles = new Vector2(0, 180);
            }
        }
    }
}
