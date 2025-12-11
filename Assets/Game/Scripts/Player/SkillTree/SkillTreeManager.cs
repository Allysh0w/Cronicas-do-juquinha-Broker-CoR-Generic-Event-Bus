using UnityEngine;
using TMPro;

// the same here
public class SkillTreeManager : MonoBehaviour
{

    public SkillSlot[] skillSlots;
    public TMP_Text pointsText;
    public int availablePoints;
    public CanvasGroup skillCanvas;
    private bool skillsOpen;


    private void OnEnable()
    {
        SkillSlot.OnAbilityPointSpent += HandleAbilityPointSpent;
        SkillSlot.OnSkillMaxed += HandleSkillMaxed;
        ExpManager.OnPlayerLvlUp += UpdateAbilityPoints;
    }

    private void OnDisable()
    {
        SkillSlot.OnAbilityPointSpent -= HandleAbilityPointSpent;
        SkillSlot.OnSkillMaxed -= HandleSkillMaxed;
        ExpManager.OnPlayerLvlUp -= UpdateAbilityPoints;

    }

    private void Start()
    {
        foreach (SkillSlot slot in skillSlots)
        {
            slot.skillButton.onClick.AddListener(() => CheckAvailablePoints(slot));
        }
        UpdateAbilityPoints(0);
    }

    public void Update()
    {
        if (Input.GetButtonDown("ToggleSkillCanvas"))
        {
            if (skillsOpen)
            {
                Time.timeScale = 1;
                skillCanvas.alpha = 0;
                skillsOpen = false;
            } else
            {
                Time.timeScale = 0;
                skillCanvas.alpha = 1;
                skillsOpen = true;
            }
        }
    }

    private void HandleSkillMaxed(SkillSlot skillSlot)
    {
        foreach(SkillSlot slot in skillSlots)
        {
            if(!slot.isUnlocked && slot.CanUnlockSkill())
            {
                slot.Unlock();
            }
            
        }
    }

    private void CheckAvailablePoints(SkillSlot slot)
    {
        if(availablePoints > 0)
        {
            slot.TryUpgradeSkill();
        }
    }
    

    private void HandleAbilityPointSpent(SkillSlot skillSlot)
    {
        if(availablePoints > 0)
        {
            UpdateAbilityPoints(-1);
        }
    }


    public void UpdateAbilityPoints(int amount)
    {
        availablePoints += amount;
        pointsText.text = $"Skill Points: {availablePoints}";
    }
}
