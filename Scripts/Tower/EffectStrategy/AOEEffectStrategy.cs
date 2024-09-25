using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEEffectStrategy : IAOEEffectStrategy
{
    private ParticleSystem _explosionParticlePrefab;

    public AOEEffectStrategy(ParticleSystem effectPrefab)
    {
        this._explosionParticlePrefab = effectPrefab;
    }

    public void CreateAOEEffect(Vector3 position, float radius)
    {
        ParticleSystem effect = Object.Instantiate(_explosionParticlePrefab, position,Quaternion.identity);
        var main = effect.main;
        main.startSize = radius * 2;
        effect.Play();

        Object.Destroy(effect.gameObject, main.duration);
    }
}