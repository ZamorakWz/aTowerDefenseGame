using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcBulletMoveStrategy : IBulletMovementStrategy
{
    public void MoveBullet(Transform bulletTransform, Vector3 targetPosition, float bulletSpeed, Action onComplete)
    {
        Vector3 direction = targetPosition - bulletTransform.position;
        float distance = direction.magnitude;
        float duration = distance / bulletSpeed;
        float jumpPower = distance / 4f; //Arc height is setting as quarter of the distance

        bulletTransform.DOJump(targetPosition, jumpPower, 1, duration)
            .SetEase(Ease.Linear)
            .OnComplete(() => onComplete?.Invoke());
    }
}