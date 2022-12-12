using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDemonAI : MonoBehaviour
{

    [SerializeField] Transform target; // gdje se nalazi ono što tražimo
    [SerializeField] float chaseRange = 10f;
    [SerializeField] float attackRange = 2.5f;
    [SerializeField] GameObject[] patrolDestinationObjects;
    [SerializeField] float turnSpeed = 5f;

    NavMeshAgent navMeshAgent;
    private const float walkingSpeed = 1.5f;
    private const float runningSpeed = 5f;

    float distanceToPlayerTarget = Mathf.Infinity;

    bool isHeadingToRandomDestination;
    bool isDead;
    bool isProvoked;
    int currentDestinationIndex = -1;

    void Start()
    {
        //patrolDestinationObjects = GameObject.FindGameObjectsWithTag("PatrolDestination");
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public bool IsPlayerInAttackRange()
    {
        float distanceToPlayerTarget = Vector3.Distance(target.transform.position, navMeshAgent.transform.position);
        return distanceToPlayerTarget <= attackRange;
    }

    void Update()
    {
        distanceToPlayerTarget = Vector3.Distance(target.transform.position, navMeshAgent.transform.position);

        if (isProvoked)
        {
            navMeshAgent.SetDestination(target.position);
            EngageTarget();
        }
        else if (distanceToPlayerTarget <= chaseRange)
        {
            SetProvokeActions();
        }

        if (!isProvoked && !isDead)
        {
            // Eneablati u budućnosti i vezati u Inspectoru gdje želimo da istražuje
            GoToRandomPlace();
        }
    }

    private void GoToRandomPlace()
    {
        bool hasReachedDestination = HasDemonReachedDestination();
        if (hasReachedDestination)
        {
            isHeadingToRandomDestination = false;
            currentDestinationIndex = UnityEngine.Random.Range(0, patrolDestinationObjects.Length);
        }

        if (!isHeadingToRandomDestination)
        {
            Transform patrolTransform = patrolDestinationObjects[currentDestinationIndex].transform;
            Vector3 newDestination = new Vector3(patrolTransform.position.x, patrolTransform.position.y, patrolTransform.position.z);

            navMeshAgent.SetDestination(newDestination);
            navMeshAgent.speed = walkingSpeed;
            isHeadingToRandomDestination = true;
            GetComponent<Animator>().SetBool(DemonAnimations.Walk, true);
        }
    }

    private bool HasDemonReachedDestination()
    {
        if (currentDestinationIndex < 0)
        {
            return true;
        }

        Transform currentPatrolTransform = patrolDestinationObjects[currentDestinationIndex].transform;
        float distanceToDestination = Vector3.Distance(currentPatrolTransform.position, navMeshAgent.transform.position);
        return distanceToDestination <= (navMeshAgent.stoppingDistance + 2); // No need to get to close
    }

    public void SetProvokeActions()
    {
        if (!isProvoked)
        {
            FaceTarget(true);
            isProvoked = true;
        }
    }

    private void EngageTarget()
    {
        FaceTarget();

        // ganjaj metu dok ne dođeš do udaljenosti (postavljene u inspectoru)
        // dodaj +1 za trčanje do određene točke, a zatim hodanje
        if (distanceToPlayerTarget >= navMeshAgent.stoppingDistance + 1)
        {
            ChaseTarget(true);
        }
        else if (distanceToPlayerTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget(false);
        }
       

        if (distanceToPlayerTarget < navMeshAgent.stoppingDistance + 1)
        {
            AttackTargetAnimation();
        }
        else if (distanceToPlayerTarget > navMeshAgent.stoppingDistance + 2)
        {
            StopAttackAnimation();
        }
    }

    void FaceTarget(bool force = false)
    {
        // Don't face target, if you are not close to it...it could make enemy walk backwards...
        if (distanceToPlayerTarget <= navMeshAgent.stoppingDistance + 2 || force)
        {
            // Oduzimamo razliku vektora između nas i neprijatelja.
            // normalized znači da nas udaljenost između neprijatelja i playera ne zanima,
            // a to nas ostavlja samo s podatkom o rotaciji playera
            Vector3 direction = (target.position - transform.position).normalized;
            // Radimo novi Quaternion i proslijeđujemo mu x i z koordinate (y koordinata nas ne zanima jer ne rotiramo u visinu)
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            // Parametri Slerpa: 1. naša trenutna rotacija, 2. rotacija koju tražimo, 3. brzina kojom se rotiramo
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
        }
    }

    private void ChaseTarget(bool run)
    {
        var animator = GetComponent<Animator>();
        if (run)
        {
            navMeshAgent.speed = runningSpeed;
            animator.SetBool(DemonAnimations.Run, true);
        }
        else
        {
            navMeshAgent.speed = walkingSpeed;
            animator.SetBool(DemonAnimations.Run, false);
        }
    }

    private void AttackTargetAnimation()
    {
        GetComponent<Animator>().SetBool(DemonAnimations.Attack, true);
    }

    private void StopAttackAnimation()
    {
        GetComponent<Animator>().SetBool(DemonAnimations.Attack, false);
    }

    public void SetDeathActions()
    {
        isDead = isProvoked = false;
        navMeshAgent.isStopped = true;
        var animator = GetComponent<Animator>();
        animator.SetBool(DemonAnimations.Attack, false);
        animator.SetBool(DemonAnimations.Run, false);
        animator.SetTrigger(DemonAnimations.Walk_Die);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

}
