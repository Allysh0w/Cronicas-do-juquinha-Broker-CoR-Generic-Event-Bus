using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance {get; private set;}


    private BrokerChain<CombatEvent> combatChain = new BrokerChain<CombatEvent>();

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeChain();

        } else
        {
            Destroy(gameObject);
        }
    }

    void InitializeChain()
    {
        combatChain.RegisterHandler(new DamageApplierHandler());
        combatChain.RegisterHandler(new KnockbackHandler(this));
        // attack -> knockBack (same as juquinha previous version)
        Debug.Log("Combat chain initialized ======>");
    }

    public void ProcessCombatEvent(CombatEvent evt)
    {
        combatChain.Process(evt);
    }
}
