using System.Collections.Generic;
using UnityEngine;

public class CombatEvent 
{
  // public CombatEnum Type;
  // public object Data;
  public GameObject attacker;
  public GameObject target;
  public int damage;
  public CombatTargetType combatTargetType;
  public Transform attackPoint;
  public Knockback knockback; 
}

public class Knockback
{
    public bool applyKnockback;
    public float force;
    public float stunTime;
}

public class AttackData
{
    // public Transform AttackPoint;
    // public LayerMask TargetLayer;
    // public GameObject Target;
    // public CombatTargetType CombatTargetType;
    // public int Damage;
}
