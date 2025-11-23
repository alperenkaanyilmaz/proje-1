using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    public enum EnemyType { Watcher, Runner }
    public EnemyType type = EnemyType.Watcher;

    [Header("Common")]
    public Transform[] patrolPoints;
    public float detectRange = 10f;
    public float fieldOfView = 100f;
    public float attackRange = 1.6f;
    public float loseRange = 18f;

    [Header("Audio/Noise")]
    public float hearRange = 6f;

    NavMeshAgent agent;
    Transform player;
    int currentPatrol = 0;
    Vector3 lastKnownPlayerPos;
    bool playerSeen = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        var p = GameObject.FindGameObjectWithTag("Player");
        if (p) player = p.transform;
        if (patrolPoints != null && patrolPoints.Length > 0) agent.SetDestination(patrolPoints[0].position);
    }

    void Update()
    {
        if (player == null) return;

        float dist = Vector3.Distance(transform.position, player.position);
        bool canHear = NoiseSystem.Instance != null && NoiseSystem.Instance.IsNoiseNear(transform.position, hearRange);

        if (!playerSeen)
        {
            if (dist <= detectRange && IsInFOV())
            {
                playerSeen = true;
                lastKnownPlayerPos = player.position;
            }
            else if (canHear)
            {
                playerSeen = true;
                lastKnownPlayerPos = NoiseSystem.Instance.GetLoudestNoisePosition(transform.position, hearRange);
            }
        }

        if (playerSeen)
        {
            agent.SetDestination(lastKnownPlayerPos);
            if (dist <= attackRange)
            {
                Attack();
            }
            if (dist > loseRange)
            {
                // give up
                playerSeen = false;
                ResumePatrol();
            }
            lastKnownPlayerPos = player.position;
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        if (patrolPoints == null || patrolPoints.Length == 0) return;
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            currentPatrol = (currentPatrol + 1) % patrolPoints.Length;
            agent.SetDestination(patrolPoints[currentPatrol].position);
        }
    }

    bool IsInFOV()
    {
        Vector3 dir = (player.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, dir);
        if (angle <= fieldOfView * 0.5f)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + Vector3.up * 1.3f, dir, out hit, detectRange))
            {
                if (hit.collider.CompareTag("Player")) return true;
            }
        }
        return false;
    }

    void Attack()
    {
        var dmg = player.GetComponent<PlayerDamage>();
        if (dmg != null) dmg.TakeDamage(25f * Time.deltaTime);
    }

    void ResumePatrol()
    {
        if (patrolPoints != null && patrolPoints.Length > 0)
            agent.SetDestination(patrolPoints[currentPatrol].position);
    }

    public void OnNoiseHeard(Vector3 pos)
    {
        playerSeen = true;
        lastKnownPlayerPos = pos;
    }
}
