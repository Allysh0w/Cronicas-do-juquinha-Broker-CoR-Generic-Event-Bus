using UnityEngine;

public class DamageApplierHandler : IChainHandler<CombatEvent>
{

    bool IChainHandler<CombatEvent>.Handle(ref CombatEvent @event)
    {
        if(@event.combatTargetType == CombatTargetType.Player)
        {
            var hp = @event.target.GetComponent<PlayerHealth>();
            if(hp != null && @event.damage > 0)
            {
                hp.ChangeHealth(-@event.damage);
                ///check hp here if wnats to stop the chain
            }

               
        } else if(@event.combatTargetType == CombatTargetType.Enemy)
        {
            var hp = @event.target.GetComponent<EnemyHealth>();
            if(hp != null && @event.damage > 0)
            {
                hp.ChangeHealth(-@event.damage);
                // same here
            }

        }

        return true; 
    }
}
