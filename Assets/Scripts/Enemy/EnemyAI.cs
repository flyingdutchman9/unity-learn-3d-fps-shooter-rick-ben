using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target; // gdje se nalazi ono što tražimo
    [SerializeField] float chaseRange = 10f;
    [SerializeField] float attackRange = 2.7f;
    [SerializeField] float turnSpeed = 15f; // brzina kojom se okreće prema playeru

    NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;
    float distanceToPlayerTarget = Mathf.Infinity;
    private bool isZombieDead;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (!isZombieDead)
        {
            distanceToTarget = Vector3.Distance(target.transform.position, navMeshAgent.transform.position);

            if (isProvoked)
            {
                navMeshAgent.SetDestination(target.position);
                EngageTarget();
            }
            else if (distanceToTarget <= chaseRange)
            {
                isProvoked = true;
            }
        }
        else
        {
            
        }
    }

    internal void SetDeathActions()
    {
        if (!isZombieDead)
        {
            isZombieDead = true;
            navMeshAgent.enabled = false;
            GetComponent<Animator>().SetTrigger("die");
            transform.GetComponent<CapsuleCollider>().isTrigger = true;
            //transform.GetComponent<NavMeshAgent>().enabled = false;
            this.enabled = false; // ako je mrtav, isključimo skriptu...
        }
    }

    public void SetProvokedOnFire()
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

        // ganjaj metu dok ne dođeš do udaljenosti od 1 (postavljeno u inspectoru)
        if (distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }

        if (distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            AttackTarget();
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

    public bool IsPlayerInAttackRange()
    {
        float distanceToPlayerTarget = Vector3.Distance(target.transform.position, navMeshAgent.transform.position);
        return distanceToPlayerTarget <= attackRange;
    }

    private void ChaseTarget()
    {
        GetComponent<Animator>().SetBool("attack", false);
        GetComponent<Animator>().SetTrigger("move");
        navMeshAgent.acceleration = 12f;
    }

    private void AttackTarget()
    {
        //GetComponent<Animator>().ResetTrigger("move");
        GetComponent<Animator>().SetBool("attack", true);
        navMeshAgent.speed = 5.0f;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

}
