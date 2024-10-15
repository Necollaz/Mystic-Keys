using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ControllerAnimations))]
public class KeyAnimator : MonoBehaviour
{
    [SerializeField] private ParticleSystem _removeKey;

    private ControllerAnimations _animationController;

    private void Awake()
    {
        _animationController = GetComponent<ControllerAnimations>();
    }

    public IEnumerator TryTurn()
    {
        _animationController.TryTurnKey(true);
        yield return null;

        float animationLength = _animationController.GetAnimationLength();
        yield return new WaitForSeconds(animationLength);

        _animationController.TryTurnKey(false);
    }

    public IEnumerator Turn()
    {
        _animationController.TurnKey(true);
        yield return null;

        float animationLength = _animationController.GetAnimationLength();
        yield return new WaitForSeconds(animationLength);

        _animationController.TurnKey(false);
        ParticleSystem removeKeyParticle = Instantiate(_removeKey, transform.position, Quaternion.identity);

        removeKeyParticle.Play();
        yield return new WaitForSeconds(_removeKey.main.duration);
    }
}