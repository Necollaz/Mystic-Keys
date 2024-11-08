using System;
using UnityEngine;

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

    private void OnDisable()
    {
        _chiselAnimator.PullOutComplete -= OnPullOutComplete;
    }

    public void PullOut()
    {
        _chiselAnimator.PlayAnimation();
    }

    private void OnPullOutComplete()
    {
        PullOutComplete?.Invoke(this);
    }
}