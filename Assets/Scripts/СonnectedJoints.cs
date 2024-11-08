using System.Collections.Generic;
using UnityEngine;

public class СonnectedJoints : MonoBehaviour
{
    private Dictionary<Transform, FixedJoint> _joints = new Dictionary<Transform, FixedJoint>();

    public void AddJoint(Transform spawnPoint)
    {
        FixedJoint joint = gameObject.AddComponent<FixedJoint>();

        _joints.Add(spawnPoint, joint);
    }

    public void RemoveJoint(Transform spawnPoint)
    {
        if (_joints.ContainsKey(spawnPoint))
        {
            FixedJoint joint = _joints[spawnPoint];
            joint.gameObject.SetActive(false);
            _joints.Remove(spawnPoint);
        }
    }
}