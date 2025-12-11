using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    // we dont need this class anymore since but I'll keep it only because Attack fn is called via animation event
    // and only to compare with the old code

    public int damage;
    public Transform attackPoint;
    public float weaponRange;
    public LayerMask playerLayer;
    public float knockbackForce;
    public float stunTime;
    private CombatAttackTrigger attackTrigger;



    void Start()
    {
        attackTrigger = GetComponent<CombatAttackTrigger>();
    }

    public void Attack()
    {
        attackTrigger.TriggerAttack();
    }
}
