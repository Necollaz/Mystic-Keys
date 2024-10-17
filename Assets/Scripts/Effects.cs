using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class Effects
{
    private ParticlePool _particlePool;
    private MonoBehaviour _monoBehaviour;

    public Effects(ParticlePool particlePool, MonoBehaviour monoBehaviour)
    {
        _particlePool = particlePool;
        _monoBehaviour = monoBehaviour;
    }

    public void Play(Slot slot)
    {
        ParticleSystem effectInstance = _particlePool.Get();
        effectInstance.transform.position = slot.Transform.position;
        effectInstance.Play();
        _monoBehaviour.StartCoroutine(Return(effectInstance));
    }

    private IEnumerator Return(ParticleSystem particle)
    {
        yield return new WaitForSeconds(particle.main.duration + particle.main.startLifetime.constantMax);
        _particlePool.Return(particle);
    }
}