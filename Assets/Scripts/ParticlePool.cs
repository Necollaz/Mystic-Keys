using System.Collections.Generic;
using UnityEngine;

public class ParticlePool : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particlePrefab;
    [SerializeField] private int _poolSize = 10;

    private Queue<ParticleSystem> _availableParticles = new Queue<ParticleSystem>();

    private void Awake()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            ParticleSystem instance = Instantiate(_particlePrefab, transform);
            instance.gameObject.SetActive(false);
            _availableParticles.Enqueue(instance);
        }
    }

    public ParticleSystem Get()
    {
        if (_availableParticles.Count > 0)
        {
            ParticleSystem particle = _availableParticles.Dequeue();
            particle.gameObject.SetActive(true);
            return particle;
        }
        else
        {
            ParticleSystem newParticle = Instantiate(_particlePrefab, transform);
            return newParticle;
        }
    }

    public void Return(ParticleSystem particle)
    {
        particle.Stop();
        particle.gameObject.SetActive(false);
        _availableParticles.Enqueue(particle);
    }
}
