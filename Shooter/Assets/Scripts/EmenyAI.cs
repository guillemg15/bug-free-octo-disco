using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EmenyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 5;

    NavMeshAgent navMeshAgent;
    float disctanceToTarget = Mathf.Infinity;
    bool isProvoked = false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
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

    private void EngageTarget()
    {
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
        navMeshAgent.SetDestination(target.position);
    }

    private void AttackTarget()
    {
        Debug.Log(name + "Esta Atacando" + target.name);
    }

    void OnDrawGizmosSelected() //Parar ver el rango de seguimiento
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
