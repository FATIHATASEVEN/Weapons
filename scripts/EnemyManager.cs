using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyManager : MonoBehaviour
{
    public int EnemyHealth = 200;
    public NavMeshAgent enemyAgent;
    public Transform player;
    public LayerMask grounlayer;
    public LayerMask playerlayer;

    public Vector3 walkPoint;
    public float walkpointrange;
    public bool walkpointset;

    public float sightRange, attackRange;
    public bool EnemysightRange, EnemyAttackRange;

    public float attackdelay;
    public bool isAttacking;
    public Transform attackpoint;
    public GameObject projectile;
    public float projectileForse;
    public Animator enemyAnimator;

    private GameManager gameManager;

    void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;
        enemyAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        EnemysightRange = Physics.CheckSphere(transform.position, sightRange, playerlayer);
        EnemyAttackRange = Physics.CheckSphere(transform.position, attackRange, playerlayer);

        if(!EnemysightRange && !EnemyAttackRange)
        {
            Patrolling();
            enemyAnimator.SetBool("Patrolling", true);
            enemyAnimator.SetBool("PlayerAttacking", false);
            enemyAnimator.SetBool("PlayerDetecting", false);
        }
        else if(EnemysightRange && !EnemyAttackRange)
        {
            DeteckPlayer();
            enemyAnimator.SetBool("Patrolling", false);
            enemyAnimator.SetBool("PlayerAttacking", false);
            enemyAnimator.SetBool("PlayerDetecting", true);
        }
        else if(EnemysightRange&&EnemyAttackRange)
        {
            AttackPlayer();
            enemyAnimator.SetBool("Patrolling", false);
            enemyAnimator.SetBool("PlayerAttacking", true);
            enemyAnimator.SetBool("PlayerDetecting", false);

        }
    }
    void Patrolling()
    {
        if(walkpointset==false)
        {
            float randomPosZ = Random.Range(-walkpointrange, walkpointrange);
            float randomPosX = Random.Range(-walkpointrange, walkpointrange);

            walkPoint = new Vector3(transform.position.x + randomPosX, transform.position.y,transform.position.z + randomPosZ);
            if(Physics.Raycast(walkPoint,-transform.up , 2f , grounlayer))
            {
                walkpointset = true;
            }
           
        }
        if (walkpointset == true)
        {
            enemyAgent.SetDestination(walkPoint);
        }
        Vector3 distanceTowalkpoint = transform.position - walkPoint;
        if (distanceTowalkpoint.magnitude < 1f)
        {
            walkpointset = false;
        }
    }

    void DeteckPlayer()
    {
        enemyAgent.SetDestination(player.position);
        transform.LookAt(player);

    }
    void AttackPlayer()
    {
        enemyAgent.SetDestination(transform.position);
        transform.LookAt(player);

        if(isAttacking==false)
        {
            Rigidbody rb=Instantiate(projectile,attackpoint.position,Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * projectileForse, ForceMode.Impulse);

            isAttacking = true;
            Invoke("ResetAttack", attackdelay);
        }
    }

    void ResetAttack()
    {
        isAttacking = false;
    }

    public  void EnemyTakeDamage(int DamagaAmount) 
    {
        EnemyHealth -= DamagaAmount;
        if(EnemyHealth<=0)
        {
            EnemyDeath();
        }
    }
    void EnemyDeath()
    {
        Destroy(gameObject);
        gameManager = FindObjectOfType<GameManager>();
        gameManager.AddKill();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sightRange);

    }
}
