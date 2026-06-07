using System;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSkillUI : MonoBehaviour
{
    [SerializeField] private Transform skillContainer;
    [SerializeField] private Transform template;
    private void Awake()
    {
        template.gameObject.SetActive(false);
    }
    private void Start()
    {
        GameManager.Instance.OnUpgradePhaseStarted += GameManager_OnUpgradePhaseStarted;
    }

    private void GameManager_OnUpgradePhaseStarted(object sender, EventArgs e)
    {
        Cursor.lockState = CursorLockMode.None;
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        gameObject.SetActive(true);

        foreach (Transform child in skillContainer)
        {
            if (child == template) continue;
            Destroy(child.gameObject);
        }

        foreach (UpgradeSkillSO upgradeSkillSO in UpgradeSkillManager.Instance.GetUpgradeSkillList())
        {
            Transform skillTemplate = Instantiate(template, skillContainer);
            skillTemplate.gameObject.SetActive(true);
            UpgradeSkillSingleUI upgradeUI = skillTemplate.GetComponent<UpgradeSkillSingleUI>();
            upgradeUI.SetUpgradeSkillSO(upgradeSkillSO);
            upgradeUI.onSelectButtonPressed += OnSkillSelected;
        }
    }
    private void OnSkillSelected(UpgradeSkillSO skill)
    {
        // Apply upgrade here
        // UpgradeSkillManager.Instance.ApplyUpgrade(skill);
        Debug.Log("Upgrade Selected");

        gameObject.SetActive(false);

        GameManager.Instance.UpgradeChosen();
    }
}
