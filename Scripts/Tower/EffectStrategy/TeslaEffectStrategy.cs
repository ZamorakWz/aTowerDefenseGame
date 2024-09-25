using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeslaEffectStrategy : ITeslaEffectStrategy
{
    private ParticleSystem _teslaParticlePrefab;

    public TeslaEffectStrategy(ParticleSystem teslaParticlePrefab)
    {
        this._teslaParticlePrefab = teslaParticlePrefab;
    }

    public void CreateTeslaEffect(Vector3 startPosition, Vector3 endPosition, float distance)
    {
        ParticleSystem effect = Object.Instantiate(_teslaParticlePrefab, startPosition, Quaternion.identity);

        if (endPosition != startPosition)
        {
            Vector3 direction = (endPosition - startPosition).normalized;
            effect.transform.rotation = Quaternion.LookRotation(direction);

            distance = Vector3.Distance(endPosition, startPosition);
            float length = distance;

            var shape = effect.shape;
            shape.length = distance;
        }

        var main = effect.main;

        float speedMultiplier = 10f;
        float maxSpeed = 20f;
        float calculatedSpeed = Mathf.Clamp(distance * speedMultiplier, 10f, maxSpeed);

        main.startSpeed = calculatedSpeed;
        main.startLifetime = distance / calculatedSpeed * 1f;

        effect.Play();

        Object.Destroy(effect.gameObject, main.duration);
    }
}