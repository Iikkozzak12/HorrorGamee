using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Targets")]
    public Transform leftPoint;
    public Transform rightPoint;
    public Transform player;
    public Platform assignedPlatform;

    [Header("Detection")]
    public float followRange = 10f;      // max zasiêg œledzenia
    public float attackRange = 2.0f;     // zasiêg ataku
    public string attackBool = "isAttacking";
    public string attackTrigger = "AttackTrigger";
    public float damage = 20;
    [Header("Animation")]
    public string speedParam = "Speed";

    private NavMeshAgent agent;
    private Animator animator;

    private Transform currentTarget;
    private bool isAttacking = false;
    private bool isFollowingPlayer = false;

    private void Start()
    {
        agent = GetComponentInParent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        leftPoint = assignedPlatform.leftPoint;
        rightPoint = assignedPlatform.rightPoint;
        currentTarget = rightPoint;
        GoTo(currentTarget);
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            // Atak
            if (!isAttacking)
            {
                StartAttack();
            }

            // Wymuœ zatrzymanie animacji ruchu
            if (animator != null)
            {
                animator.SetFloat(speedParam, 0f);
            }

            return;
        }
        else if (distanceToPlayer <= followRange)
        {
            // ŒledŸ gracza
            if (!isFollowingPlayer)
            {
                isFollowingPlayer = true;
            }

            if (isAttacking)
            {
                EndAttack();
            }

            GoTo(player);
        }
        else
        {
            // Poza zasiêgiem – wróæ do patrolu
            if (isAttacking)
            {
                EndAttack();
            }

            if (isFollowingPlayer)
            {
                isFollowingPlayer = false;
                GoTo(currentTarget);
            }

            // Kontynuuj patrol
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                currentTarget = currentTarget == leftPoint ? rightPoint : leftPoint;
                GoTo(currentTarget);
            }
        }

        // Aktualizacja animacji ruchu
        if (!isAttacking && animator != null)
        {
            float speed = agent.velocity.magnitude;
            animator.SetFloat(speedParam, speed);
            animator.SetFloat("MotionSpeed", 1);
        }
    }

    private void GoTo(Transform target)
    {
        if (agent != null && target != null)
        {
            agent.isStopped = false;
            agent.SetDestination(target.position);
        }
    }

    private void StartAttack()
    {
        isAttacking = true;
        agent.isStopped = true;

        if (animator != null)
        {
            animator.SetBool(attackBool, true);
            animator.SetTrigger(attackTrigger);
            animator.SetFloat(speedParam, 0f);
        }
    }

    private void EndAttack()
    {
        isAttacking = false;
        agent.isStopped = false;

        if (animator != null)
        {
            animator.SetBool(attackBool, false);
            animator.SetFloat(speedParam, 0f);
        }
    }

    public void DealDamage()
    {
        if (Vector3.Distance(this.gameObject.transform.position, player.gameObject.transform.position) <= 2)
        {
            player.gameObject.GetComponent<Health>().TakeDamage(20);
        }
    }
}
