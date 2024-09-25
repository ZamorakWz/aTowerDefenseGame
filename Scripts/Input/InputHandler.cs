using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using Zenject;

public class InputHandler : MonoBehaviour
{
    [Inject] private TowerDataPanelManager towerDataPanelManager;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!IsPointerOverUIElement())
            {
                HandleInput(Input.mousePosition);
            }
        }
        else if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began && !IsPointerOverUIElement())
            {
                HandleInput(touch.position);
            }
        }
    }

    private bool IsPointerOverUIElement()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    private void HandleInput(Vector3 screenPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        int layerMask = LayerMask.GetMask("TowerInteraction");

        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, layerMask))
        {
            GameObject hitObject = hitInfo.collider.gameObject;
            ProcessHitObject(hitObject);
        }
        else
        {
            towerDataPanelManager.CloseAllPanels();
        }
    }

    private void ProcessHitObject(GameObject hitObject)
    {
        switch (hitObject.tag)
        {
            case "Tower":
                TowerDataUI towerDataUI = hitObject.GetComponent<TowerDataUI>();
                if (towerDataUI != null)
                {
                    towerDataPanelManager.ShowTowerPanel(towerDataUI);
                }
                break;
            case "BuffDebuff":
                //BuffDebuffPickup buffDebuff = hitObject.GetComponent<BuffDebuffPickup>();
                //if (buffDebuff != null)
                //{
                //    buffDebuff.OnPickedUp();
                //}
                break;
            default:
                towerDataPanelManager.CloseAllPanels();
                break;
        }
    }
}