using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITeslaEffectStrategy
{
    void CreateTeslaEffect(Vector3 startPosition, Vector3 endPosition, float distance);
}