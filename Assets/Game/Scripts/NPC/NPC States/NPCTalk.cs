using UnityEngine;


// same here
public class NPCTalk : MonoBehaviour
{
    
    private Rigidbody2D rb;
    public GameObject ballonSprite;
    public DialogueSO dialogSO;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic; 
        ballonSprite.SetActive(true);

    }

    private void OnDisable()
    {
        ballonSprite.SetActive(false);
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    private void Update()
    {
        if(Input.GetButtonDown("Submit"))
        {
            if(DialogueManager.Instance.isDialogueActive)
            {
                DialogueManager.Instance.AdvanceDialogue();
            } else
            {
                DialogueManager.Instance.StartDialogue(dialogSO);
            }
        }
    }
}
