using UnityEngine;

[CreateAssetMenu(fileName = "KeySpawnConfig", menuName = "ScriptableObjects/KeySpawnConfig")]
public class KeySpawnConfig : ScriptableObject
{
    public KeyLayer[] KeyLayers;
}