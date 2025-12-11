using TMPro;
using UnityEngine;

// All stats UI needs to be updated using an <UIEvents> events sent by EventBus pub-sub.
public class StatsUI : MonoBehaviour
{
   public GameObject[] statsSlots;
   public CanvasGroup statsCanvas;
   private bool statsOpen = false;

    void Start()
    {
        UpdateAllStats();
    }

    public void Update()
    {
        if (Input.GetButtonDown("ToggleStatsCanvas"))
        {
            if (statsOpen)
            {
                Time.timeScale = 1;
                UpdateAllStats();
                statsCanvas.alpha = 0;
                statsOpen = false;
            } else
            {
                Time.timeScale = 0;
                UpdateAllStats();
                statsCanvas.alpha = 1;
                statsOpen = true;
            }
        }
    }
   public void UpdateMaxHP()
    {
        statsSlots[0].GetComponentInChildren<TMP_Text>().text = "Max HP: " + StatsManager.Instance.maxHealth;
    }
    public void UpdateDamage()
    {
        statsSlots[1].GetComponentInChildren<TMP_Text>().text = "Damage: " + StatsManager.Instance.damage;
    }
    public void UpdateSpeed()
    {
        statsSlots[2].GetComponentInChildren<TMP_Text>().text = "Speed: " + StatsManager.Instance.speed;
    }

    public void UpdateLucky()
    {
        statsSlots[3].GetComponentInChildren<TMP_Text>().text = "Luck: 50%";
    }

    public void UpdateAllStats()
    {
        UpdateDamage();
        UpdateMaxHP();
        UpdateSpeed();
        UpdateLucky();
    }

}
