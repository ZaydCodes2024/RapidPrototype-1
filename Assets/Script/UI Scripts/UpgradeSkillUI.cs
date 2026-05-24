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
        UpdateVisual();
    }
    private void UpdateVisual()
    {
        foreach (Transform child in skillContainer)
        {
            if (child == template) continue;
            Destroy(child.gameObject);
        }

        foreach (UpgradeSkillSO upgradeSkillSO in UpgradeSkillManager.Instance.GetUpgradeSkillList())
        {
            Transform skillTemplate = Instantiate(template, skillContainer);
            skillTemplate.gameObject.SetActive(true);
            skillTemplate.GetComponent<UpgradeSkillSingleUI>().SetUpgradeSkillSO(upgradeSkillSO);
        }
    }
}
