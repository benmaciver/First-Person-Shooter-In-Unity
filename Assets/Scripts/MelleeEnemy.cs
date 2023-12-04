using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : MonoBehaviour, GameObjectController
{
    public float damage = 10f;
    public float health = 100f;
    public float attackRange = 3f;
    private NavMeshAgent agent;
    private GameObject player;
    private Animator anim;
    private bool dead;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        agent.speed = 3.5f;
        dead = false;
    }

    void Update()
    {
        if (!dead)
        {
            UpdateAgentDestination();
            CheckAttackRange();

            if (health <= 0 && !dead)
            {
                HandleDeath();
            }
        }
        else
        {
            HandleDeadState();
        }
    }

    void UpdateAgentDestination()
    {
        agent.SetDestination(player.transform.position);
    }

    void CheckAttackRange()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < attackRange)
        {
            agent.isStopped = true;
            anim.SetBool("Attacking", true);
            //attackPlayer();
        }
        else
        {
            agent.isStopped = false;
            anim.SetBool("Attacking", false);
        }
    }

    void HandleDeath()
    {
        anim.SetBool("Dead", true);
        dead = true;
        anim.SetBool("Attacking", false);

        // Assuming the "Death" animation length is 2 seconds. Adjust the time accordingly.
        Invoke(nameof(DestroyEnemy), 5f);
    }

    void HandleDeadState()
    {
        agent.isStopped = true;
        anim.SetBool("Dead", false);
    }

    void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    void damagePlayer()
    {
        player.GetComponent<PlayerController>().TakeDamage(damage);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    void attackPlayer()
    {
        player.GetComponent<PlayerController>().TakeDamage(damage);
    }
}
