using UnityEngine;

public class NPC : MonoBehaviour
{
   public enum NPCState {Default, Idle, Patrol, Talk}
   public NPCState currentState = NPCState.Patrol;
   private NPCState defaultState;

   public NPCPatrol patrol;
   public NPCTalk npcTalk;

   void Start()
    {
        defaultState = currentState;

    }

    public void SwitchState(NPCState newState)
    {
        currentState = newState;
        patrol.enabled = newState == NPCState.Patrol;
        npcTalk.enabled = newState == NPCState.Talk;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            SwitchState(NPCState.Talk);
        }        
   }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SwitchState(defaultState);
            
        }
    }

}
