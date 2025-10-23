using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using System.Threading.Tasks;

public class MonsterAI : MonoBehaviour
{
    [Header("Monster AI")]
    public NavMeshAgent agent;
    public Transform player;
    public Transform centerPoint;     // fallback to self if null
    public LayerMask playerLayer;
    public Animator animator;

    [Header("Audio")]
    public AudioSource chase;
    public float audioInSpeed = 2f;
    public float audioOutSSpeed = 4f;
    public float volume = 1f;
    private float targetVolume = 0f;

    [Header("Ranges")]
    public float itemRange = 12f;
    public float stalkRange = 8f;
    public float attackRange = 2.2f;

    [Header("Wanted")]
    public int wanted = 0;
    public float rangeIncrease = 1.05f; // multiplier per wanted level

    [Header("Debug")]
    public bool playerInItemRange;
    public bool playerInStalkRange;
    public bool playerInAttackRange;

    string currentAnim = "";

    void Awake()
    {
        if (!agent) agent = GetComponent<NavMeshAgent>();
        if (!animator) animator = GetComponent<Animator>();
    }

    void Start()
    {
        if (!player) player = GameObject.Find("Player")?.transform;
        if (!centerPoint) centerPoint = transform;
        ChangeAnimation("Run");
    }

    void Update()
    {
        if (!agent || !player) return;

        float itemR = itemRange * Mathf.Pow(rangeIncrease, wanted);
        float stalkR = stalkRange * Mathf.Pow(rangeIncrease, wanted);

        playerInItemRange = Physics.CheckSphere(transform.position, itemR, playerLayer);
        playerInStalkRange = Physics.CheckSphere(transform.position, stalkR, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        if (playerInItemRange && !playerInStalkRange && !playerInAttackRange)
        {
            Wandering(itemR);
            targetVolume = 0f;
        }

        else if (playerInItemRange && playerInStalkRange && !playerInAttackRange)
        {
            Stalking();
            targetVolume = volume;
            if (!chase.isPlaying)
            {
                chase.Play();
            }
        }

        else if (playerInItemRange && playerInStalkRange && playerInAttackRange)
        {
            AttackPlayer();
            targetVolume = volume;
            if (!chase.isPlaying)
            {
                chase.Play();
            }
        }

        float fadeSpeed = targetVolume > chase.volume ? audioInSpeed : audioOutSSpeed;
        chase.volume = Mathf.MoveTowards(chase.volume, targetVolume, fadeSpeed * Time.deltaTime);

        if (chase.volume <= 0.01f && chase.isPlaying)
        {
            chase.Stop();
        }

    }

    public void IncreaseWanted(int amount = 1)
    {
        wanted = Mathf.Max(0, wanted + amount);
    }

    void Wandering(float radius)
    {
        agent.speed = 3.0f;

        if (agent.remainingDistance > agent.stoppingDistance) return;

        Vector3 dest;
        if (RandomPointOnNavMesh(centerPoint.position, radius, out dest))
            agent.SetDestination(dest);
    }

    void Stalking()
    {
        agent.speed = 6.0f;
        agent.SetDestination(player.position);
        LookAtPlayer();
        ChangeAnimation("Run");
    }

    void AttackPlayer()
    {
        agent.speed = 3.5f;
        agent.SetDestination(player.position);
        LookAtPlayer();
        ChangeAnimation("Attack");
    }

    void LookAtPlayer()
    {
        Vector3 dir = player.position - transform.position;
        dir.y = 0f;
        if (dir.sqrMagnitude > 0.0001f)
        {
            var rot = Quaternion.LookRotation(dir.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 5f);
        }
    }

    bool RandomPointOnNavMesh(Vector3 center, float range, out Vector3 result, int attempts = 20)
    {
        result = center;

        if (!IsFinite(center) || range <= 0f) return false;

        for (int i = 0; i < attempts; i++)
        {
            float rx = UnityEngine.Random.Range(-range, range);
            float rz = UnityEngine.Random.Range(-range, range);
            Vector3 sample = new Vector3(center.x + rx, center.y, center.z + rz);

            if (!IsFinite(sample)) continue;

            if (NavMesh.SamplePosition(sample, out NavMeshHit hit, 2f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        return false;
    }

    bool IsFinite(Vector3 v) =>
        float.IsFinite(v.x) && float.IsFinite(v.y) && float.IsFinite(v.z);

    void ChangeAnimation(string anim, float crossfade = 0.2f)
    {
        if (animator && currentAnim != anim)
        {
            currentAnim = anim;
            animator.CrossFade(anim, crossfade);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green; Gizmos.DrawWireSphere(transform.position, itemRange);
        Gizmos.color = Color.blue; Gizmos.DrawWireSphere(transform.position, stalkRange);
        Gizmos.color = Color.red; Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
