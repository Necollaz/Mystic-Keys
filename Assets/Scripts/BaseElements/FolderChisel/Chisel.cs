using System;
using UnityEngine;
using Animations;

namespace BaseElements.FolderChisel
{
    [RequireComponent(typeof(ChiselAnimator))]
    public class Chisel : MonoBehaviour
    {
        private ChiselAnimator _chiselAnimator;
        
        public event Action<Chisel> PullOutComplete;
        
        public int GroupIndex { get; set; }
        
        private void Awake()
        {
            _chiselAnimator = GetComponent<ChiselAnimator>();
            
            _chiselAnimator.PullOutComplete += OnPullOutComplete;
        }
        
        public void PullOut()
        {
            _chiselAnimator.PlayAnimation();
        }
        
        private void OnPullOutComplete()
        {
            _chiselAnimator.PullOutComplete -= OnPullOutComplete;
            
            gameObject.SetActive(false);
            PullOutComplete?.Invoke(this);
        }
    }
}