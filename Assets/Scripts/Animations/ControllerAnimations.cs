using UnityEngine;

namespace Animations
{
    [RequireComponent(typeof(Animator))]
    public class ControllerAnimations : MonoBehaviour
    {
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>(); 
        }

        public void SetBool(int parameterHash, bool value)
        {
            _animator.SetBool(parameterHash, value);
        }

        public void ResetParameters()
        {
            _animator.SetBool(AnimationData.Params.OpenLockbox, false);
            _animator.SetBool(AnimationData.Params.IdleOpenLockbox, false);
            _animator.SetBool(AnimationData.Params.CloseLockbox, false);
            _animator.SetBool(AnimationData.Params.DisappearanceLockbox, false);
            _animator.SetBool(AnimationData.Params.AppearanceLockbox, false);
        }

        public float GetAnimationLength()
        {
            AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
            return stateInfo.length;
        }
    }
}