using System.Collections;
using UnityEngine;

public class BrokerChainPlayerMovement : MonoBehaviour, IEventListener<InputEvent>, IKnockbackable
{
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private int facingDirection = 1; // change to -1 depends on the player direction spawned for the first time 
    [SerializeField] private float animationAttackCooldown = 1;
    private float timer;

    private bool isKnockedBack;

    void OnEnable()
    {      
        if(InputEventBus.Instance != null)
        {
            InputEventBus.Instance.Register(this);
        }
    }

    void OnDisable()
    {
        if (InputEventBus.Instance != null)
        {
            InputEventBus.Instance.Unregister(this);
        }
        
    }

    void IEventListener<InputEvent>.OnEvent(InputEvent @event)
    {
        // the magic happens here. For example: PubSub => subscription => consumer application 
        if (@event.Type == InputEnum.Move && !isKnockedBack && @event.MoveDirection.HasValue)
        {        
            Vector2 direction = @event.MoveDirection.Value;
            Move(direction);
        }

        if(@event.Type == InputEnum.Slash)
        {
            PlayAttackAnimation();
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }

    private void Move(Vector2 direction)
    {
        float horizontal = direction.x;
        float vertical = direction.y;

        if (horizontal > 0 && transform.localScale.x < 0 ||
        horizontal < 0 && transform.localScale.x > 0)
        {
            Flip();
        }

        SyncAnim(horizontal, vertical);
        rb.linearVelocity = new Vector2(horizontal, vertical) * StatsManager.Instance.speed;

    }

    private void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    private void SyncAnim(float horizontal, float vertical)
    {
        anim.SetFloat("horizontal", Mathf.Abs(horizontal));
        anim.SetFloat("vertical", Mathf.Abs(vertical));
    }

    private void PlayAttackAnimation()
    {
        if(timer <= 0)
        {
            anim.SetBool("isAttacking", true);
            timer = animationAttackCooldown;
        }
    }

    public void FinishAttackAnimation()
    {
        anim.SetBool("isAttacking", false);
    }

    ////////////////////// interface knockbackable implementations =>
    void IKnockbackable.ApplyKnockback(Vector2 direction, float force)
    {
        rb.linearVelocity = direction * force;
    }

    void IKnockbackable.SetKnockbackState(bool state)
    {    
        // implement here hit animation 
         isKnockedBack = state;
        // if (state)
        // {
        //     anim.SetTrigger("Hit");
        // }
        return;
        
    }

    Rigidbody2D IKnockbackable.GetRigidbody2D() => rb;

    bool IKnockbackable.IsKnockedBack => isKnockedBack;


}
