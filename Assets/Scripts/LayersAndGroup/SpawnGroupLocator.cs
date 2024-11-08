using UnityEngine;

public class SpawnGroupLocator : MonoBehaviour
{
    public SubGroup[] SubGroup;

    public int FindGroupIndex(Transform keySpawnPoint)
    {
        for (int i = 0; i < SubGroup.Length; i++)
        {
            if (SubGroup[i].KeySpawnPoint == keySpawnPoint)
            {
                return i;
            }
        }

        return -1;
    }
}