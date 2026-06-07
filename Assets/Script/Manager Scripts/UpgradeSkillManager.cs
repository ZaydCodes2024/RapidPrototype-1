using System.Collections.Generic;
using UnityEngine;

public class UpgradeSkillManager : MonoBehaviour
{
    public static UpgradeSkillManager Instance {get; private set;}
    [SerializeField] private UpgradeSkillListSO upgradeSkillListSO;
    private List<UpgradeSkillSO> upgradeSkillSOList;
    private int upgradeSkillMax = 3;
    private void Awake()
    {
        Instance = this;
        upgradeSkillSOList = new List<UpgradeSkillSO>();
        SetUpgradeSkillList();
    }

    private void Start()
    {
        GameManager.Instance.OnUpgradeSelected += GameManager_OnUpgradeSelected;
    }

    private void GameManager_OnUpgradeSelected(object sender, System.EventArgs e)
    {
        SetUpgradeSkillList();
    }

    private void SetUpgradeSkillList()
    {
        upgradeSkillSOList.Clear();

        List<UpgradeSkillSO> availableSkills =
            new List<UpgradeSkillSO>(upgradeSkillListSO.upgradeSkillListSO);

        for (int i = 0; i < upgradeSkillMax && availableSkills.Count > 0; i++)
        {
            int randomIndex = Random.Range(0, availableSkills.Count);

            upgradeSkillSOList.Add(availableSkills[randomIndex]);

            availableSkills.RemoveAt(randomIndex);
        }
    }
    public List<UpgradeSkillSO> GetUpgradeSkillList()
    {
        return upgradeSkillSOList;
    }
}
