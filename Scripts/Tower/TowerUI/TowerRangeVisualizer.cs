using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class TowerRangeVisualizer : MonoBehaviour, ITowerRangeUpdater
{
    private float _towerRange;
    private LineRenderer _lineRenderer;
    private bool _showRange = false;

    [SerializeField] private int segments = 64;
    [SerializeField] private float lineWidth = 0.1f;
    [SerializeField] private Color lineColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);

    private void Awake()
    {
        InitializeLineRenderer();
    }

    private void OnEnable()
    {
        TowerPlacementManager.OnTowerSelected += HandleTowerRange;
    }

    private void OnDisable()
    {
        TowerPlacementManager.OnTowerSelected -= HandleTowerRange;
    }

    private void HandleTowerRange(GameObject tower)
    {
        if (tower == gameObject)
        {
            HandleRangeVisualization();
            DrawRangeCircle();
            ToggleRangeVisualization(true);
        }
    }

    public void InitializeLineRenderer()
    {
        _lineRenderer = gameObject.GetComponent<LineRenderer>();
        _lineRenderer.enabled = true;
        _lineRenderer.startWidth = lineWidth;
        _lineRenderer.endWidth = lineWidth;
        _lineRenderer.positionCount = segments + 1;
        _lineRenderer.material = new Material(Shader.Find("Unlit/Color"));
        _lineRenderer.startColor = lineColor;
        _lineRenderer.endColor = lineColor;
        _lineRenderer.useWorldSpace = false;
        _lineRenderer.enabled = _showRange;
    }

    private void HandleRangeVisualization()
    {
        TowerTypeSO towerData = gameObject.GetComponent<AbstractBaseTower>().GetTowerData();
        if (towerData != null)
        {
            _towerRange = towerData.towerRange;
        }
    }

    private void DrawRangeCircle()
    {
        float deltaTheta = (2f * Mathf.PI) / segments;
        float theta = 0f;

        for (int i = 0; i <= segments; i++)
        {
            float x = _towerRange * Mathf.Cos(theta);
            float z = _towerRange * Mathf.Sin(theta);
            Vector3 pos = new Vector3(x, 0f, z);
            _lineRenderer.SetPosition(i, pos);
            theta += deltaTheta;
        }
    }

    public void ToggleRangeVisualization(bool show)
    {
        _showRange = show;
        _lineRenderer.enabled = _showRange;
    }

    public void UpdateTowerRangeVisualization(float newTowerRange)
    {
        _towerRange = newTowerRange;
        DrawRangeCircle();
    }
}