using UnityEngine;

public class InactiveMarkerSpawner : MonoBehaviour
{
    [SerializeField] private ParticleSystem _inactivePrefab;

    public void CreateInactiveMarker(Transform point)
    {
        Instantiate(_inactivePrefab, point.position, point.rotation);
    }
}
