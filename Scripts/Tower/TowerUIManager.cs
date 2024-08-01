using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerUIManager : MonoBehaviour
{
    [SerializeField] private Transform _towerButtonContainer;
    [SerializeField] private Button _towerButtonPrefab;
    [SerializeField] private TowerPlacementManager _towerPlacementManager;
    [SerializeField] private List<AbstractTower> availableTowers;

    void Start()
    {
        CreateTowerButtons();
    }

    private void CreateTowerButtons()
    {
        availableTowers = TowerCreationManager.Instance.GetTowerList();

        foreach (AbstractTower tower in availableTowers)
        {
            Button button = Instantiate(_towerButtonPrefab, _towerButtonContainer);
            TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();

            if (buttonText != null)
            {
                buttonText.text = tower.gameObject.name;
            }

            button.onClick.AddListener(() => OnTowerButtonClicked(tower.GetType().Name));
        }
    }

    private void OnTowerButtonClicked(string towerType)
    {
        Debug.Log($"Button clicked for tower: {towerType}");
        GameObject createdTower = TowerCreationManager.Instance.CreateTower(towerType, Vector3.zero);
        if (createdTower != null)
        {
            _towerPlacementManager.SelectTower(createdTower);
        }
    }
}