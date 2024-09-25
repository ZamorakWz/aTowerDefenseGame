using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Zenject;
using System.Collections.Generic;

public class TowerDataUI : MonoBehaviour
{
    [SerializeField] private Canvas towerCanvas;
    [SerializeField] private TextMeshProUGUI topicText;
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private TextMeshProUGUI rangeText;
    [SerializeField] private TextMeshProUGUI fireRateText;

    [SerializeField] private Button damageUpgradeButton;
    [SerializeField] private Button rangeUpgradeButton;
    [SerializeField] private Button fireRateUpgradeButton;

    [SerializeField] private TextMeshProUGUI strategyText;
    [SerializeField] private TMP_Dropdown strategyDropdown;
    [SerializeField] private TextMeshProUGUI youCantMakeTargetSelectionText;

    private AbstractBaseTower tower;
    private float upgradeCost;
    private float damageUpgradeCost;
    private float rangeUpgradeCost;
    private float fireRateUpgradeCost;

    [Inject] private TowerDataPanelManager towerDataPanelManager;

    private void Awake()
    {
        tower = GetComponent<AbstractBaseTower>();
        HidePanel();
    }

    private void OnEnable()
    {
        TowerPlacementManager.OnTowerPlaced += HandleUpdateUI;
    }

    private void OnDisable()
    {
        TowerPlacementManager.OnTowerPlaced -= HandleUpdateUI;
    }

    public void ShowPanel()
    {
        UpdateTowerUI();
        if (towerCanvas != null)
        {
            towerCanvas.gameObject.SetActive(true);
        }
    }

    public void HidePanel()
    {
        if (towerCanvas != null)
        {
            towerCanvas.gameObject.SetActive(false);
        }
    }

    private void UpdateTowerUI()
    {
        if (tower.isTowerPlaced)
        {
            topicText.text = $"{tower.GetTowerData().towerName} Upgrade Panel";

            damageUpgradeCost = tower.GetUpgradeCost(tower.damageUpgradeLevel);
            rangeUpgradeCost = tower.GetUpgradeCost(tower.rangeUpgradeLevel);
            fireRateUpgradeCost = tower.GetUpgradeCost(tower.fireRateUpgradeLevel);

            damageText.text = $"Damage:{tower.towerDamage.ToString("F1")}({damageUpgradeCost} G)";
            rangeText.text = $"Range:{tower.towerRange.ToString("F1")}({rangeUpgradeCost} G)";
            fireRateText.text = $"Fire Rate:{tower.towerFireRate.ToString("F1")}({fireRateUpgradeCost} G)";

            UpdateButton(damageUpgradeButton, tower.damageUpgradeLevel);
            UpdateButton(rangeUpgradeButton, tower.rangeUpgradeLevel);
            UpdateButton(fireRateUpgradeButton, tower.fireRateUpgradeLevel);
        }
        else
        {
            HidePanel();
        }

        UpdateStrategyDropdown();
    }

    private void UpdateButton(Button button, int upgradeLevel)
    {
        upgradeCost = tower.GetUpgradeCost(upgradeLevel);
        button.interactable = GoldManager.Instance.GetCurrentGold() >= upgradeCost;
        button.GetComponentInChildren<TextMeshProUGUI>().text = "x1.1";
    }

    public void OnDamageUpgrade()
    {
        tower.UpgradeDamage();
        UpdateTowerUI();
    }

    public void OnRangeUpgrade()
    {
        tower.UpgradeRange();
        UpdateTowerUI();
    }

    public void OnFireRateUpgrade()
    {
        tower.UpgradeFireRate();
        UpdateTowerUI();
    }

    private void HandleUpdateUI(GameObject gameObject)
    {
        if (gameObject == this.gameObject)
        {
            UpdateTowerUI();
        }
    }

    private void UpdateStrategyDropdown()
    {
        strategyDropdown.ClearOptions();

        List<ITargetSelectionStrategy> strategies = tower.GetAvailableStrategies();

        if (strategies.Count <= 1)
        {
            strategyDropdown.gameObject.SetActive(false);
            strategyText.gameObject.SetActive(false);

            if (youCantMakeTargetSelectionText != null)
            {
                youCantMakeTargetSelectionText.gameObject.SetActive(true);
            }

            return;
        }

        strategyDropdown.gameObject.SetActive(true);

        List<string> strategyNames = new List<string>();
        foreach (var strategy in strategies)
        {
            strategyNames.Add(strategy.GetType().Name);
        }

        strategyDropdown.AddOptions(strategyNames);

        if (tower.targetSelectionStrategy == null)
        {
            tower.targetSelectionStrategy = strategies[0];
        }

        int currentStrategyIndex = 0;
        for (int i = 0; i < strategies.Count; i++)
        {
            if (strategies[i].GetType() == tower.targetSelectionStrategy.GetType())
            {
                currentStrategyIndex = i;
                break;
            }
        }

        strategyDropdown.value = currentStrategyIndex;
    }

    public void OnStrategyChanged(int index)
    {
        List<ITargetSelectionStrategy> strategies = tower.GetAvailableStrategies();
        if (index >= 0 && index < strategies.Count)
        {
            tower.ChangeTargetSelectionStrategy(strategies[index]);
        }
    }
}