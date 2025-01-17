using Spawners;
using UnityEngine;

namespace Levels
{
    [RequireComponent(typeof(LaunchingSpawners))]
    public class Level : MonoBehaviour
    {
        public LaunchingSpawners LaunchingSpawners { get; private set; }
        
        private void Awake()
        {
            LaunchingSpawners = GetComponent<LaunchingSpawners>();
        }
    }
}