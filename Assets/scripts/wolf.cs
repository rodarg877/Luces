using UnityEngine;
using UnityEngine.AI;

public class WolfPatrol : MonoBehaviour
{
    public float patrolRadius = 20f;
    public float patrolDelay = 3f;

    private NavMeshAgent agent;
    private Vector3 startPosition;
    private float timer;
    private Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

     
        startPosition = transform.position;

        // 🔹 Llamamos al destino en la primera actualización
        Invoke(nameof(SetNewDestination), 0.1f);
    }

    void Update()
    {

        float speed = agent.velocity.magnitude;
        animator.SetFloat("Speed", speed);

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            timer += Time.deltaTime;

            if (timer >= patrolDelay)
            {
                animator.SetTrigger("Dig");
                SetNewDestination();
                timer = 0f;
            }
        }
    }

    void SetNewDestination()
    {
        if (!agent.enabled || !agent.isOnNavMesh) return;

        Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
        randomDirection += startPosition;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, patrolRadius, NavMesh.AllAreas))
        {
            // 🔹 Evitamos destinos muy cercanos
            if (Vector3.Distance(hit.position, transform.position) > 2f)
            {
                agent.SetDestination(hit.position);
            }
            else
            {
                Invoke(nameof(SetNewDestination), 0.2f); // Reintenta si el punto es muy cercano
            }
        }
    }
}
