using UnityEngine;

public class CombatAttackTrigger : MonoBehaviour
{

    // it's better to use scriptable objects here 
    [Header("Attack Detection")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float weaponRange = 1f;
    [SerializeField] private LayerMask targetLayer;

    [Header("Attack Settings")]
    [SerializeField] private int baseDamage = 10;
    [SerializeField] private CombatTargetType attackerType;

    [Header("Knockback Settings")]
    [SerializeField] private bool applyKnockback = true;
    [SerializeField] private float knockbackForce = 5f;
    [SerializeField] private float stunTime = 0.2f;


    public void TriggerAttack()
    {
        if (attackPoint == null)
        {
            Debug.Log("Attack point not assigned =>");
            return;
        }

        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, weaponRange, targetLayer);
        if (hits.Length > 0)
        {
            Debug.Log($"{gameObject.name} hit {hits.Length} target(s)");

            foreach (var hit in hits)
            {
                if (hit.gameObject != gameObject)
                {
                    var combatEvent = new CombatEvent
                    {
                        attacker = gameObject,
                        target = hit.gameObject,
                        damage = baseDamage,
                        combatTargetType = attackerType == CombatTargetType.Player ? CombatTargetType.Enemy : CombatTargetType.Player,
                        attackPoint = attackPoint,
                        knockback = new Knockback
                        {
                            applyKnockback = applyKnockback,
                            force = knockbackForce,
                            stunTime = stunTime
                        }

                    };

                    CombatManager.Instance.ProcessCombatEvent(combatEvent);
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = attackerType == CombatTargetType.Player ? Color.blue : Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, weaponRange);
    }



}
