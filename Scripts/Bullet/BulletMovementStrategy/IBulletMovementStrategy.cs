using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBulletMovementStrategy
{
    void MoveBullet(
        Transform bulletTransform, 
        Vector3 targetPosition, 
        float bulletSpeed, 
        Action onComplete);
}