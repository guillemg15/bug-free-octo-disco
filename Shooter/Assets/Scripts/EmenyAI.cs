using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 5;
    [SerializeField] float turnSpeed = 5;

    NavMeshAgent navMeshAgent;
    float disctanceToTarget = Mathf.Infinity;
    bool isProvoked = false;
    EnemyHealth health;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        health = GetComponent<EnemyHealth>();
    }

    void Update()
    {
        if(health.IsDead())
        {
            enabled = false;
            navMeshAgent.enabled = false;
        }
        disctanceToTarget = Vector3.Distance(target.position, transform.position);
        if (isProvoked)
        {
            EngageTarget();
        }
        else if (disctanceToTarget <= chaseRange)//Calcular diferencia de distancia entre el jugador y el enemigo
        {
            isProvoked = true;
        }
    }

    public void OnDamageTaken()
    {
        isProvoked = true;
    }

    private void EngageTarget()
    {
        FaceTarget();
        if(disctanceToTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }

        if(disctanceToTarget <= navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
    }

    private void ChaseTarget()
    {
        GetComponent<Animator>().SetBool("Attack", false);
        GetComponent<Animator>().SetTrigger("Move");
        navMeshAgent.SetDestination(target.position);
    }

    private void AttackTarget()
    {
        GetComponent<Animator>().SetBool("Attack", true);
        //Debug.Log(name + "Esta Atacando" + target.name); Hecho para hacer pruebas
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation ( new Vector3 (direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    void OnDrawGizmosSelected() //Parar ver el rango de seguimiento
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
