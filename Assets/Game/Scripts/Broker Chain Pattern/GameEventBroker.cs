using System.Collections.Generic;
using UnityEngine;


// Event Bus Pattern (Publish - Subscribe) // Just for studying... Thjis class here will handle only Input events
public class GameEventBroker<TEvent> 
{
   
   private readonly List<IEventListener<TEvent>> eventListeners = new List<IEventListener<TEvent>>();

   public void Register(IEventListener<TEvent> eventListener)
    {
        if (!eventListeners.Contains(eventListener))
        {
            eventListeners.Add(eventListener);
        }
    }

    public void Unregister(IEventListener<TEvent> eventListener)
    {
        eventListeners.Remove(eventListener);
    }

    public void Publish(TEvent @event)
    {
        foreach(var listener in eventListeners)
        {
            listener.OnEvent(@event);
        }
    }
 
}
