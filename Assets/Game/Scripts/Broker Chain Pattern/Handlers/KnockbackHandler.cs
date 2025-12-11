using System.Collections;
using UnityEngine;

public class KnockbackHandler : IChainHandler<CombatEvent>
{
    private MonoBehaviour coroutineRunner;

    public KnockbackHandler(MonoBehaviour runner) // needs a monobehaviour here because we need to trigger the coroutine
    {
        coroutineRunner =  runner;
    }

    bool IChainHandler<CombatEvent>.Handle(ref CombatEvent @event)
    {
        if(@event.knockback != null && @event.knockback.applyKnockback)
        {
            var knockable = @event.target.GetComponent<IKnockbackable>();
            if(knockable != null)
            {
                Vector2 direction = (@event.target.transform.position - @event.attacker.transform.position).normalized;
                coroutineRunner
                .StartCoroutine(
                    ApplyKnockback(
                        knockable, direction, @event.knockback.force, @event.knockback.stunTime));
            }
        }
        return true;
    }


    private IEnumerator ApplyKnockback(IKnockbackable target, Vector2 direction, float force, float stunTime)
    {
        target.SetKnockbackState(true);
        target.ApplyKnockback(direction, force);
        yield return new WaitForSeconds(stunTime);

        var rb = target.GetRigidbody2D();
        if(rb != null)
            rb.linearVelocity = Vector2.zero;
        
        target.SetKnockbackState(false);
        
    }
}
