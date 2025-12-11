using UnityEngine;

// nobody knows about the input manager
public class InputManager : MonoBehaviour
{

    [Header("Input Broker")]
    ///public InputEventBus inputBroker;

    public static InputManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void Update()
    {
        if(InputEventBus.Instance == null) return;
        
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (horizontal != 0 || vertical != 0)
        {
            var direction = new Vector2(horizontal, vertical);
            InputEvent movementEvent =  new InputEvent(InputEnum.Move, direction);

            //inputBroker.Publish(movementEvent);
            InputEventBus.Instance.Publish(movementEvent);
        }

        if (Input.GetButtonDown("Slash"))
        {
            InputEvent inputSlashEvent = new InputEvent(InputEnum.Slash);
            //inputBroker.Publish(inputSlashEvent);
            InputEventBus.Instance.Publish(inputSlashEvent);
        }

        if (Input.GetButtonDown("Submit"))
        {
            InputEvent inputDialogueEvent = new InputEvent(InputEnum.TalkDialogue);
            //inputBroker.Publish(inputDialogueEvent);
            InputEventBus.Instance.Publish(inputDialogueEvent);
        }

        if (Input.GetButtonDown("ToggleStatsCanvas"))
        {
            InputEvent inputToggleStatusEvent = new InputEvent(InputEnum.ToggleStatusUI);
            //inputBroker.Publish(inputToggleStatusEvent);
            InputEventBus.Instance.Publish(inputToggleStatusEvent);
        }

        if(Input.GetButtonDown("ToggleSkillCanvas"))
        {
            InputEvent inputToggleSkillTree =  new InputEvent(InputEnum.ToggleSkillTree);
            //inputBroker.Publish(inputToggleSkillTree);
            InputEventBus.Instance.Publish(inputToggleSkillTree);
        }
      
    }



}
