using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSkillSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private Image iconImage;
    [SerializeField] private Button selectButton;
    public event Action<UpgradeSkillSO> OnSelectButtonPressed;
    private UpgradeSkillSO _upgradeSkillSO;
    private void Awake()
    {
        selectButton.onClick.AddListener( () =>
        {
            OnSelectButtonPressed?.Invoke(_upgradeSkillSO);
        });
    }
    public void SetUpgradeSkillSO(UpgradeSkillSO upgradeSkillSO)
    {
        titleText.text = upgradeSkillSO.title;
        description.text = upgradeSkillSO.description;
        iconImage.sprite = upgradeSkillSO.icon;
        _upgradeSkillSO = upgradeSkillSO;
    }
}
