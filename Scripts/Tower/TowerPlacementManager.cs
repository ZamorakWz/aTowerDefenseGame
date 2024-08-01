using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacementManager : MonoBehaviour
{
    private Camera _mainCamera;
    [SerializeField] private LayerMask _placementLayer;
    private GameObject _selectedTowerPrefab;
    private bool isCanPlaceHere;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    void Update()
    {
        if (_selectedTowerPrefab != null)
        {
            UpdatePrefabPlacement();

            if (Input.GetMouseButtonDown(0))
            {
                PlaceTower();
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            RemoveTower();
        }
    }

    public void SelectTower(GameObject tower)
    {
        if (_selectedTowerPrefab != null)
        {
            Destroy(_selectedTowerPrefab);
        }

        _selectedTowerPrefab = tower;
        Debug.Log("Selected tower prefab: " + _selectedTowerPrefab.gameObject.name);
    }

    private void UpdatePrefabPlacement()
    {
        if (TryGetPlacementPosition(out Vector3? hitPoint))
        {
            _selectedTowerPrefab.SetActive(true);
            _selectedTowerPrefab.transform.position = hitPoint.Value;
            isCanPlaceHere = CanPlaceHere(hitPoint.Value);
        }
        else
        {
            _selectedTowerPrefab.SetActive(false);
        }
    }

    private void PlaceTower()
    {
        if (isCanPlaceHere && TryGetPlacementPosition(out Vector3? hitPoint))
        {
            _selectedTowerPrefab.transform.position = hitPoint.Value;
            _selectedTowerPrefab = null;
        }
    }

    private void RemoveTower()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            GameObject towerToRemove = hit.collider.gameObject;
            if (towerToRemove.CompareTag("Tower"))
            {
                Destroy(towerToRemove);
            }
        }
    }

    private bool TryGetPlacementPosition(out Vector3? hitPoint)
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _placementLayer))
        {
            hitPoint = hit.point;
            return true;
        }
        hitPoint = null;
        return false;
    }

    public bool CanPlaceHere(Vector3 position)
    {
        if (GroundValidator.Instance == null || OverlapValidator.Instance == null)
        {
            Debug.LogError("GroundValidator or OverlapValidator is null!");
            return false;
        }

        bool isOnValidGround = GroundValidator.Instance.CheckGroundValidity(position);
        bool isNotOverlapping = OverlapValidator.Instance.CheckOverlapping(position, _selectedTowerPrefab);

        return isOnValidGround && isNotOverlapping;
    }
}