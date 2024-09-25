using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAOEEffectStrategy
{
    void CreateAOEEffect(Vector3 position, float radius);
}