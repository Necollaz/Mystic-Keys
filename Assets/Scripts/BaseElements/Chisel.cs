using System;
using UnityEngine;

[RequireComponent(typeof(ChiselAnimator))]
[RequireComponent(typeof(Rigidbody))]
public class Chisel : MonoBehaviour
{
    private ChiselAnimator _chiselAnimator;
    private Rigidbody _rigidbody;

    public event Action<Chisel> PullOutComplete;

    public int GroupIndex { get; set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _chiselAnimator = GetComponent<ChiselAnimator>();
        _rigidbody.isKinematic = true;
        _chiselAnimator.PullOutComplete += OnPullOutComplete;
    }

    public void PullOut()
    {
        _rigidbody.isKinematic = false;
        _chiselAnimator.PlayAnimation();
    }

    private void OnPullOutComplete()
    {
        _chiselAnimator.PullOutComplete -= OnPullOutComplete;
        gameObject.SetActive(false);
        PullOutComplete?.Invoke(this);
    }
}