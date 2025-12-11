using UnityEngine;

// Listeners interface
public interface IEventListener<TEvent> 
{
   void OnEvent(TEvent @event);
}
