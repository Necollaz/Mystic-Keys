using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Beam : MonoBehaviour
{
    [SerializeField] private ParticleSystem _destructionEffect;

    private Rigidbody _rigidbody;

    public int GroupIndex { get; set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
}