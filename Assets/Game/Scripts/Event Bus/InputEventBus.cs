using UnityEngine;

// MonoBehaviour Singleton (same as InputManager)
public class InputEventBus : MonoBehaviour
{
    public static InputEventBus Instance { get; private set; }
    
    private GameEventBroker<InputEvent> eventBroker = new GameEventBroker<InputEvent>();
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("InputBroker initialized");
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void Register(IEventListener<InputEvent> listener)
    {
        eventBroker.Register(listener);
        Debug.Log($"Registered: {listener.GetType().Name}");
    }
    
    public void Unregister(IEventListener<InputEvent> listener)
    {
        eventBroker.Unregister(listener);
    }
    
    public void Publish(InputEvent evt)
    {
        eventBroker.Publish(evt);
    }
}
// public class InputBroker : GameEventBroker<InputEvent>
// {
//     // add filters later 
//     // ideas: events filter, broker event priority, event validation etc
// }
