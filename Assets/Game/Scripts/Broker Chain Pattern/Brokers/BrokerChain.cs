using System.Collections.Generic;
using UnityEngine;

public class BrokerChain<TEvent>
{
    private readonly List<IChainHandler<TEvent>> handlers = new List<IChainHandler<TEvent>>();

    public void RegisterHandler(IChainHandler<TEvent> handler)
    {
        handlers.Add(handler);
    }

    public void UnregisterHandler(IChainHandler<TEvent> handler)
    {
        handlers.Remove(handler);
    }

    public TEvent Process(TEvent @event)
    {
        foreach (var handler in handlers)
        {
            if(!handler.Handle(ref @event))
            {
                Debug.Log("Chain validated and stopped cause receive false from the handler");
                break;
            }            
        }
        return @event;
    }
}
