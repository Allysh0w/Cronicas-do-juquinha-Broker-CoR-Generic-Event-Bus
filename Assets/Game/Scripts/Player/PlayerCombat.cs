using UnityEngine;

// We dont this class anymore. However, I'll keep it only to compare with the old version 
public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint;
    public LayerMask enemyLayer;
    private Animator anim;
    public float cooldown = 1;
    private float timer;
    private CombatAttackTrigger attackTrigger;


    void Start()
    {
        anim = GetComponent<Animator>();
        attackTrigger = GetComponent<CombatAttackTrigger>();
    }

    void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }

    public void Attack()
    {
        if(timer <= 0)
        {
            anim.SetBool("isAttacking", true);            
            timer = cooldown;   
        }
    }

    public void DealDamage()
    {
        attackTrigger.TriggerAttack();
    }

    public void FinishAttacking()
    {
        anim.SetBool("isAttacking", false);
    }

    private void OnDrawGizmosSelected()
    {
        
        if(StatsManager.Instance != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, StatsManager.Instance.weaponRange);  
        }
        
    }
}
