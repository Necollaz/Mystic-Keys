using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseElements.FolderBeam;
using BaseElements.FolderChisel;
using LayersAndGroup;

namespace Spawners.SpawnerBeam
{
    public class SpawnerBeams : BaseSpawner<Beam>
    {
        [SerializeField] private SpawnerChisels _chiselSpawner;
        [SerializeField] private List<BeamGroupPoints> _beamChiselPairs = new List<BeamGroupPoints>();
        
        public override void Awake()
        {
            base.Awake();
        }
        
        public override void Create()
        {
            CreateByPoints();
            LinkBeamsToChisels();
        }
        
        private void LinkBeamsToChisels()
        {
            List<Beam> beams = SpawnedInstances;
            List<Chisel> chisels = _chiselSpawner.SpawnedInstances;
            
            foreach (BeamGroupPoints pair in _beamChiselPairs)
            {
                Beam beam = beams.Find(b => IsSameTransform(b.transform, pair.BeamSpawnPoint));
                
                if (beam != null)
                {
                    beam.AllChiselsRemoved += HandleDestruction;
                    
                    foreach (Transform chiselSpawnPoint in pair.ChiselSpawnPoints)
                    {
                        Chisel chisel = chisels.Find(c => IsSameTransform(c.transform, chiselSpawnPoint));
                        
                        if (chisel != null)
                        {
                            beam.AttachChisel(chisel);
                            chisel.PullOutComplete += beam.DetachChisel;
                        }
                    }
                }
            }
        }
        
        private bool IsSameTransform(Transform a, Transform b)
        {
            return Vector3.Distance(a.position, b.position) < 0.01f && Quaternion.Angle(a.rotation, b.rotation) < 1f;
        }
        
        private void HandleDestruction(Beam beam)
        {
            StartCoroutine(Return(beam));
        }
        
        private IEnumerator Return(Beam beam)
        {
            yield return null;
            
            Pool.Return(beam);
            beam.AllChiselsRemoved -= HandleDestruction;
        }
    }
}