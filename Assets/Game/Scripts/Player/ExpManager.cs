using UnityEngine;
using UnityEngine.UI;
using TMPro;

// This class needs to be refactored because it's managing UI, delegates and UI updates
// since we have a generic eventBus now, we don't need to use delegates anymore
public class ExpManager : MonoBehaviour
{
   
   public int level;
   public int currentExp;
   public int expToLvl = 10;
   public float expGrowthMultiplier = 1.2f; //20 % more exp each lvl
   public Slider expSlider;
   public TMP_Text currentExpText;
   public TMP_Text currentLvlText;

   public delegate void PlayerLvlUp(int points);
   public static event PlayerLvlUp OnPlayerLvlUp;


   void Start()
    {
        UpdateUI();
    }

    private void OnEnable()
    {
        EnemyHealth.OnMonsterDefeated += GainExperience; 
    }

    private void OnDisable()
    {
        EnemyHealth.OnMonsterDefeated -= GainExperience;
    }

    public void GainExperience(int amount)
    {
        currentExp += amount;
        if(currentExp >= expToLvl)
        {
            LevelUp();
        }

        UpdateUI();
    }

    private void LevelUp()
    {
        level++;
        OnPlayerLvlUp(1);
        StatsManager.Instance.lvl = level;
        currentExp -= expToLvl;
        expToLvl = Mathf.RoundToInt(expToLvl * expGrowthMultiplier);

    }

    public void UpdateUI()
    {
        expSlider.maxValue = expToLvl;
        expSlider.value = currentExp;
        currentExpText.text = $"Exp: {currentExp} / {expToLvl} ";
        currentLvlText.text = $"Lvl: {level}";
    }
}
