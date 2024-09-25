using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePosition : MonoBehaviour
{
    public static BasePosition Instance {  get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Vector3 GetBasePosition()
    {
        return transform.position;
    }
}
