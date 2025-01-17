using System.Collections;
using UnityEngine;

namespace Animations
{
    [RequireComponent(typeof(ControllerAnimations))]
    public class LockboxAnimator : MonoBehaviour
    {
        private ControllerAnimations _animationController;
        
        private void Awake()
        {
            _animationController = GetComponent<ControllerAnimations>();
        }
        
        public void Reset()
        {
            StopAllCoroutines();
            _animationController.ResetParameters();
        }
        
        public IEnumerator Open()
        {
            _animationController.ResetParameters();
            
            _animationController.SetBool(AnimationData.Params.OpenLockbox, true);
            
            yield return Wait();
            
            _animationController.SetBool(AnimationData.Params.OpenLockbox, false);
            _animationController.SetBool(AnimationData.Params.IdleOpenLockbox, true);
        }
        
        public IEnumerator Close()
        {
            _animationController.ResetParameters();
            
            _animationController.SetBool(AnimationData.Params.IdleOpenLockbox, false);
            _animationController.SetBool(AnimationData.Params.CloseLockbox, true);
            
            yield return Wait();
            
            _animationController.SetBool(AnimationData.Params.CloseLockbox, false);
            _animationController.SetBool(AnimationData.Params.DisappearanceLockbox, true);
            
            yield return Wait();
            
            _animationController.SetBool(AnimationData.Params.DisappearanceLockbox, false);
        }
        
        public IEnumerator Appearance()
        {
            _animationController.ResetParameters();
            
            _animationController.SetBool(AnimationData.Params.AppearanceLockbox, true);
            
            yield return Wait();
            
            _animationController.SetBool(AnimationData.Params.AppearanceLockbox, false);
        }
        
        private WaitForSeconds Wait()
        {
            float animationLength = _animationController.GetAnimationLength();
            
            return new WaitForSeconds(animationLength);
        }
    }
}