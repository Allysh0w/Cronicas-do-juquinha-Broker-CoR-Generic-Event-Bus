using UnityEngine;

using TMPro;
using UnityEngine.UI;

// the same here, we dont need this class anymore
// All attributes needs to come from stats controller and we need to build an eventBus pub-sub like...
// to send notification to the UI system, for example Eventbus<UIEvents> => sendNotification 
// Since the EventBus is generic, we can split it by domain
public class PlayerHealth : MonoBehaviour
{
    public TMP_Text healthText;
    public Animator  healthTextAnim;
    public Slider hpSlider;

    private void Start()
    {
        healthText.text = $"{StatsManager.Instance.currentHealth} / {StatsManager.Instance.maxHealth}";
        hpSlider.maxValue = StatsManager.Instance.maxHealth;
        hpSlider.value = StatsManager.Instance.currentHealth;
    }

    public void ChangeHealth(int amount)
    {
        StatsManager.Instance.currentHealth += amount;
        hpSlider.value = StatsManager.Instance.currentHealth;
        healthTextAnim.Play("PlayerHealthUpdate");        
        healthText.text = $"{StatsManager.Instance.currentHealth} / {StatsManager.Instance.maxHealth}";

        if(StatsManager.Instance.currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }

}
