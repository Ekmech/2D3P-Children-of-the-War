using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Soldiers : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;

    [Header("SoldiersSettings")]
    [SerializeField] float catchRange = 2f;
    [SerializeField] float chaseRange = 5f;
    [SerializeField] float turnSpeed = 15f;
    [SerializeField] float patrolRadius = 6f;
    [SerializeField] float patrolWaitTime = 2f;
    [SerializeField] float searchSpeed = 3.5f;
    [SerializeField] float chaseSpeed = 4.5f;

    private bool isSearched = false;
    enum State
    {
        Idle,
        Search,
        Chase,
        Catch
    }
    [SerializeField] private State currentState = State.Idle;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
    }
    void Update()
    {
        StateCheck();
        StateExecute();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
        switch (currentState)
        {
            case State.Search:
                Gizmos.color = Color.blue;
                Vector3 targetPos = new Vector3(agent.destination.x, transform.position.y, agent.destination.z);
                Gizmos.DrawLine(transform.position, targetPos);
                break;
            case State.Chase:
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(transform.position, player.position);
                break;
            case State.Catch:
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, player.position);
                break;
        }
    }
    private void StateCheck()
    {
        float distanceToTarget = Vector3.Distance(player.position, transform.position);
        if (distanceToTarget <= chaseRange && distanceToTarget > catchRange)
        {
            currentState = State.Chase;
        }
        else if (distanceToTarget <= catchRange)
        {
            currentState = State.Catch;
        }
        else
        {
            currentState = State.Search;
        }
    }
    private void StateExecute()
    {
        switch (currentState)
        {
            case State.Idle:
                break;
            case State.Search:
                if (!isSearched && agent.remainingDistance <= 0.1f || !agent.hasPath && !isSearched)
                {
                    Vector3 agentTarget = new Vector3(agent.destination.x, transform.position.y, agent.destination.z);
                    agent.enabled = false;
                    transform.position = agentTarget;
                    agent.enabled = true;

                    Invoke("Search", patrolWaitTime);
                    isSearched = true;
                }
                break;
            case State.Chase:
                Chase();
                break;
            case State.Catch:
                Catch();
                break;
        }
    }
    private void Search()
    {
        agent.isStopped = false;
        agent.speed = searchSpeed;
        isSearched = false;
        agent.SetDestination(GetRandomPosition());
    }
    private void Chase()
    {
        if (!player)
        {
            return;
        }
        agent.isStopped = false;
        agent.speed = chaseSpeed;
        agent.SetDestination(player.position);
    }
    private void Catch()
    {
        if (!player)
        {
            return;
        }
        agent.velocity = Vector3.zero;
        agent.isStopped = false;
        LookTheTarget(player.position);
    }
    private void LookTheTarget(Vector3 target)
    {
        Vector3 lookPos = new Vector3(target.x, transform.position.y, target.z);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookPos - transform.position), turnSpeed * Time.deltaTime);
    }
    private Vector3 GetRandomPosition()
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * patrolRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, patrolRadius, 1);
        return hit.position;
    }
}