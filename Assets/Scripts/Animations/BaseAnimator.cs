using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ControllerAnimations))]
public abstract class BaseAnimator : MonoBehaviour
{
    [SerializeField] private ParticleSystem _prefab;

    private ControllerAnimations _controllerAnimations;
    private ScaleCorotine _scaleCoroutine;
    private ParticleSystemPool _particlePool;

    public ControllerAnimations ControllerAnimations => _controllerAnimations;

    public virtual void Awake()
    {
        _controllerAnimations = GetComponent<ControllerAnimations>();
        _scaleCoroutine = new ScaleCorotine(transform);
        InitializeParticlePool();
    }

    public virtual void InitializeParticlePool()
    {
        _particlePool = new ParticleSystemPool(_prefab, 5, transform.parent);
    }

    public abstract void TriggerAnimation();

    public abstract float GetScaleDuration();

    public abstract void OnAnimationComplete();

    public void PlayAnimation()
    {
        StartCoroutine(AnimationCoroutine());
    }

    private IEnumerator AnimationCoroutine()
    {
        TriggerAnimation();

        float animationLength = _controllerAnimations.GetAnimationLength();

        yield return new WaitForSeconds(animationLength);

        Vector3 originalScale = transform.localScale;
        Vector3 increasedScale = originalScale * 1.2f;
        float scaleDuration = GetScaleDuration();

        yield return StartCoroutine(_scaleCoroutine.ScaleOverTime(originalScale, increasedScale, scaleDuration));
        yield return StartCoroutine(_scaleCoroutine.ScaleOverTime(increasedScale, originalScale, scaleDuration));

        PlayParticleEffect();
        OnAnimationComplete();
    }

    private void PlayParticleEffect()
    {
        ParticleSystem particle = _particlePool.Get();
        particle.transform.position = transform.position;
        particle.gameObject.SetActive(true);
        particle.Play();
        StartCoroutine(Return(particle));
    }

    private IEnumerator Return(ParticleSystem particle)
    {
        yield return new WaitUntil(() => !particle.IsAlive(true));

        particle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        particle.gameObject.SetActive(false);
        _particlePool.Return(particle);
    }
}