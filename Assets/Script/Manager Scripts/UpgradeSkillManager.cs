using System.Collections.Generic;
using UnityEngine;

public class UpgradeSkillManager : MonoBehaviour
{
    public static UpgradeSkillManager Instance {get; private set;}
    [SerializeField] private List<UpgradeSkillSO> upgradeSkillSOList;
    private void Awake()
    {
        Instance = this;
    }
    public List<UpgradeSkillSO> GetUpgradeSkillList()
    {
        return upgradeSkillSOList;
    }
}
