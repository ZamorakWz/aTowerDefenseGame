using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GroundValidator : MonoBehaviour
{
    public static GroundValidator Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public bool CheckGroundValidity(Vector3 position)
    {
        RaycastHit hit;
        LayerMask validGroundLayer = LayerMask.GetMask("TowerPlaceableGround");

        if (Physics.Raycast(position, Vector3.down, out hit, 5f, validGroundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}