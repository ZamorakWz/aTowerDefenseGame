using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITowerFactory
{
    AbstractBaseTower CreateTower(Vector3 position);
}
