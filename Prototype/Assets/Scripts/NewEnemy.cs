using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

using UnityEngine.AI;

public class NewEnemy : MonoBehaviour
{
    public Animator enemyAnim;
    public GameObject enemyProjectile;
    public NavMeshAgent enemy;
    public Transform player;
    public LayerMask groundLayer, playerLayer;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float WalkPointRange;

    public float timeBetweenAttacks;
    public bool alreadyAttacked;

    public int manualAtkDistance = 1;
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    public Vector3 sightRangeOffset = new Vector3(0f, 0f, 3f), attackRangeOffset = new Vector3(0f, 0f, 3f);
    public GameObject playerAttackSpot;

    private float noMovementThreshold = 0.0001f;
    private const int noMovementFrames = 3;
    Vector3[] previousLocations = new Vector3[noMovementFrames];
    private bool isMoving;

    public GameObject ScaredUI;
    public GameObject HappyUI;

    public Transform enemyRespawnPoint;
    public thirdpersonmovement pm;

    private Vector3 EnemySP;
    // Start is called before the first frame update

    public bool IsMoving
    {
        get { return isMoving; }
    }
    private void Awake()
    {
       
        enemy = GetComponent<NavMeshAgent>();
        for (int i = 0; i < previousLocations.Length; i++)
        {
            previousLocations[i] = Vector3.zero;
        }
    }
    void Start()
    {
        enemyAnim.SetBool("isWalking", true);
        enemyAnim.SetBool("isChasing", false);
        enemyAnim.SetBool("isHitting", false);
    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position + sightRangeOffset, sightRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position + attackRangeOffset, attackRange, playerLayer);

        if (!playerInSightRange && !playerInAttackRange)
        {
            Patroling();
            
            enemyAnim.SetBool("isWalking", true);
           
        }
        else if (playerInSightRange && !playerInAttackRange)
        {
            Chasing();

            ScaredUI.SetActive(true);
            HappyUI.SetActive(false);
            enemyAnim.SetBool("isChasing", true);
            enemyAnim.SetBool("isHitting", false);
        }
        else if (playerInAttackRange && playerInAttackRange)
        {
            Attacking();


            enemyAnim.SetBool("isChasing", true);
            enemyAnim.SetBool("isHitting", true);

        }
        for (int i = 0; i < previousLocations.Length - 1; i++)
        {
            previousLocations[i] = previousLocations[i + 1];
        }
        previousLocations[previousLocations.Length - 1] = enemy.transform.position;

        for (int i = 0; i < previousLocations.Length - 1; i++)
        {
            if (Vector3.Distance(previousLocations[i], previousLocations[i + 1]) >= noMovementThreshold && (!playerInSightRange) && (!playerInAttackRange))
            {

                isMoving = true;
                break;
            }
            else
            {
                isMoving = false;
                SearchWalkPoint();
            }
        }

        if (pm.inBurrow == true)
        {
            ResetEnemy();
        }

    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            enemy.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;


    }
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-WalkPointRange, WalkPointRange);
        float randomX = Random.Range(-WalkPointRange, WalkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(walkPoint, -transform.up, 2f, groundLayer))
            walkPointSet = true;

    }

    private void Chasing()
    {
        enemy.SetDestination(player.transform.position);
    }
    private void Attacking()
    {
        enemy.SetDestination(player.transform.position - new Vector3(1f, 0, 1f));
        transform.LookAt(player);
        if (!alreadyAttacked)
        {
            enemy.SetDestination(transform.position);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        enemyAnim.SetBool("isHitting", false);

        alreadyAttacked = false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + sightRangeOffset, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position + attackRangeOffset, sightRange);
    }

    private void ResetEnemy()
    {
        enemy.transform.position = new Vector3(enemyRespawnPoint.position.x, enemyRespawnPoint.position.y, enemyRespawnPoint.position.z);
        playerInAttackRange = false;
        playerInSightRange = false;
        transform.position = Vector3.MoveTowards(transform.position, EnemySP, Time.deltaTime);
        ScaredUI.SetActive(false);
    }
}
