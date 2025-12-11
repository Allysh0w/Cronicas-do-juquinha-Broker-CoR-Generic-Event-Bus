using UnityEngine;

public class EnemyMovementBrokerChain : MonoBehaviour, IKnockbackable
{

    private Rigidbody2D rb;
    private Animator anim;
    private bool isKnockedBack;
    private Transform player;
    public float speed;
    private int facingDirection = -1;
    private EnemyState enemyState;
    public float attackRange = 2;
    public float attackCooldown = 2;
    private float attackCooldownTimer;
    public float playerDetectRange = 5;
    public Transform detectionPoint;
    public LayerMask playerLayer;

    bool IKnockbackable.IsKnockedBack => isKnockedBack;

    void IKnockbackable.ApplyKnockback(Vector2 direction, float force)
    {
        rb.linearVelocity = direction * force;
    }

    Rigidbody2D IKnockbackable.GetRigidbody2D() => rb;

    void IKnockbackable.SetKnockbackState(bool state)
    {
        if(state)
            ChangeState(EnemyState.Knockback);
        else
            ChangeState(EnemyState.Idle);
    }


    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ChangeState(EnemyState.Idle);
    }

    public void Update()
    {
        if (enemyState != EnemyState.Knockback)
        {
            CheckForPlayer();

            if (attackCooldownTimer > 0)
            {
                attackCooldownTimer -= Time.deltaTime;
            }

            if (enemyState == EnemyState.Chasing)
            {
                Chase();
            }
            else if (enemyState == EnemyState.Attacking)
            {
                rb.linearVelocity = Vector2.zero;
            }
        }
    }

    public void ChangeState(EnemyState state)
    {
        if(anim == null) return;
        enemyState = state;
        anim.SetBool("isIdle", state == EnemyState.Idle);
        anim.SetBool("isChasing", state == EnemyState.Chasing);
        anim.SetBool("isAttacking", state == EnemyState.Attacking);
    }

    private void Flip()
    {
        Debug.Log("Flip ==>");
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);

    }

    private void Chase()
    {

        if (player.position.x > transform.position.x && facingDirection == -1 ||
            player.position.x < transform.position.x && facingDirection == 1)
        {
            Flip();
        }
        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = direction * speed;
    }

    private void CheckForPlayer()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(detectionPoint.position, playerDetectRange, playerLayer);

        if (hits.Length > 0)
        {
            player = hits[0].transform;
            // if the player is in attack range AND cooldown is ready
            if (Vector2.Distance(transform.position, player.position) <= attackRange && attackCooldownTimer <= 0)
            {
                attackCooldownTimer = attackCooldown;
                ChangeState(EnemyState.Attacking);
            }
            else if (Vector2.Distance(transform.position, player.position) > attackRange && enemyState != EnemyState.Attacking)
            {
                ChangeState(EnemyState.Chasing);
            }
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
            ChangeState(EnemyState.Idle);
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(detectionPoint.position, playerDetectRange);
    }

}


