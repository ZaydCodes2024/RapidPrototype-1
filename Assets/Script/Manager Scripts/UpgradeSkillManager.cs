using System.Collections.Generic;
using UnityEngine;

public class UpgradeSkillManager : MonoBehaviour
{
    public static UpgradeSkillManager Instance {get; private set;}
    [SerializeField] private UpgradeSkillListSO upgradeSkillListSO;
    private List<UpgradeSkillSO> upgradeSkillSOList;
    private int upgradeSkillMax = 3;
    private int index;
    private void Awake()
    {
        Instance = this;
        upgradeSkillSOList = new List<UpgradeSkillSO>();
        SetUpgradeSkillList(index);
    }
    
    private void SetUpgradeSkillList(int index)
    {
        while (index < upgradeSkillMax)
        {
            UpgradeSkillSO upgradeSkillSO = upgradeSkillListSO.upgradeSkillListSO[Random.Range(0, upgradeSkillListSO.upgradeSkillListSO.Count)];
            upgradeSkillSOList.Add(upgradeSkillSO);
            index++;
        }
        
    }
    public List<UpgradeSkillSO> GetUpgradeSkillList()
    {
        return upgradeSkillSOList;
    }
}
