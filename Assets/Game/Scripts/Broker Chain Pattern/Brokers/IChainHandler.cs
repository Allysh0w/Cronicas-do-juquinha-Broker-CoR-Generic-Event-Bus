using UnityEngine;

public interface IChainHandler<TEvent>
{   
    bool Handle(ref TEvent @event);
}
