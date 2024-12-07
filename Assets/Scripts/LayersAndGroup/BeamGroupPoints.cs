using System;
using UnityEngine;

namespace LayersAndGroup
{
    [Serializable]
    public class BeamGroupPoints
    {
        public Transform BeamSpawnPoint;
        public Transform[] ChiselSpawnPoints;
    }
}