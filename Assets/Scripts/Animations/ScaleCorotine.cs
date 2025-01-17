using System.Collections;
using UnityEngine;

namespace Animations
{
    public class ScaleCorotine
    {
        private Transform _targetTransform;
        
        public ScaleCorotine(Transform transform)
        {
            _targetTransform = transform;
        }
        
        public IEnumerator ScaleOverTime(Vector3 startScale, Vector3 endScale, float duration)
        {
            float elapsed = 0f;
            
            while (elapsed < duration)
            {
                _targetTransform.localScale = Vector3.Lerp(startScale, endScale, elapsed / duration);
                elapsed += Time.deltaTime;
                
                yield return null;
            }
            
            _targetTransform.localScale = Vector3.zero;
        }
    }
}