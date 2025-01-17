using System;
using System.Collections;
using UnityEngine;
using BaseElements.FolderChisel;

namespace BaseElements.FolderBeam
{
    [RequireComponent(typeof(Rigidbody))]
    public class Beam : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _destructionEffect;
        [SerializeField] private Transform _hingeAxisTransform;
        
        private HingeJointService _hingeJointService;
        private Rigidbody _rigidbody;
        
        public event Action<Beam> AllChiselsRemoved;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.useGravity = true;
            _hingeJointService = new HingeJointService(transform, _hingeAxisTransform);
            _hingeJointService.AllHingesRemoved += OnAllHingesRemoved;
        }
        
        private void OnDestroy()
        {
            _hingeJointService.AllHingesRemoved -= OnAllHingesRemoved;
            _hingeJointService.Clear();
        }
        
        public void AttachChisel(Chisel chisel)
        {
            _hingeJointService.Add(chisel);
        }
        
        public void DetachChisel(Chisel chisel)
        {
            _hingeJointService.Remove(chisel);
        }
        
        private void OnAllHingesRemoved()
        {
            StartCoroutine(HandleDestructionEffect());
            AllChiselsRemoved?.Invoke(this);
        }
        
        private IEnumerator HandleDestructionEffect()
        {
            Instantiate(_destructionEffect, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
            
            yield break;
        }
    }
}