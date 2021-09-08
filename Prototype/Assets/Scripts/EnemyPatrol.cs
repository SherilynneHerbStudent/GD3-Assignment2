using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    private Animator _animator;

    private NavMeshAgent _navMeshAgent;

    public GameObject Player;

    public float AttackDistance = 10.0f;

    public float FollowDistance = 20.0f;

    [Range(0.0f, 1.0f)]
    public float AttackProbability = 0.5f;

    public Transform[] patrolPoints;

    private int currentControlPointIndex = 0;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _animator.SetBool("isWalking", true);
        MoveToNextPatrolPoint();
    }
    
    

    void MoveToNextPatrolPoint()
    {
        if(patrolPoints.Length > 0)
        {
            _navMeshAgent.destination = patrolPoints[currentControlPointIndex].position;

            currentControlPointIndex++;
            currentControlPointIndex %= patrolPoints.Length;
        }
    }

    private void Update()
    {
        if (_navMeshAgent.enabled)
        {
            float dist = Vector3.Distance(Player.transform.position, this.transform.position);

            bool chase = true;
            bool patrol = false;
            bool follow = (dist < FollowDistance);

            if (follow)
            {
                float random = Random.Range(0.0f, 1.0f);
                if (random > (1.0f - AttackProbability) && dist < AttackDistance)
                {
                    chase = true;
                }

                _navMeshAgent.SetDestination(Player.transform.position);
            }

            patrol = !follow && !chase && patrolPoints.Length > 0;

            if ((!follow || chase) && !patrol)
                _navMeshAgent.SetDestination(transform.position);

            if (patrol)
            {
                if (!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance < 0.5f)
                    MoveToNextPatrolPoint();
            }

            
            //_animator.SetBool("isHitting", true);
            //_animator.SetBool("isChasing", follow || patrol);
        }
    }
}
