using UnityEngine;

public class LockboxKeyDisplay : MonoBehaviour
{
    [SerializeField] private Key _keyPrefab;
    [SerializeField] private Transform[] _keySpawnPoints;

    private Lockbox _lockbox;

    private void Awake()
    {
        _lockbox = GetComponent<Lockbox>();
        if (_lockbox == null)
        {
            Debug.LogError("Lockbox component not found on the GameObject.");
            return;
        }

        _lockbox.KeyAdded += OnAddKeyVisual;
    }

    private void OnDestroy()
    {
        if (_lockbox != null)
        {
            _lockbox.KeyAdded -= OnAddKeyVisual;
        }
    }

    private void OnAddKeyVisual(Lockbox lockbox, int currentKeyCount)
    {
        if (currentKeyCount > _keySpawnPoints.Length)
        {
            return;
        }

        Transform spawnPoint = _keySpawnPoints[currentKeyCount - 1];
        Key keyInstance = Instantiate(_keyPrefab, spawnPoint.position, spawnPoint.rotation, spawnPoint);
        keyInstance.Initialize(lockbox.Color);

        Collider keyCollider = keyInstance.GetComponent<Collider>();

        if (keyCollider != null)
        {
            keyCollider.enabled = false;
        }
    }
}
