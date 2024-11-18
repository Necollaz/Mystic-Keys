using System;
using System.Collections.Generic;
using UnityEngine;

public class HingeJointService
{
    private Transform _parentTransform;
    private Transform _hingeAxisTransform;
    private List<HingeJoint> _hingeJoints = new List<HingeJoint>();
    private Dictionary<Chisel, HingeJoint> _chiselJointMap = new Dictionary<Chisel, HingeJoint>();

    public event Action AllHingesRemoved;

    public HingeJointService(Transform parentTransform, Transform hingeAxisTransform)
    {
        _parentTransform = parentTransform ?? throw new ArgumentNullException(nameof(parentTransform));
        _hingeAxisTransform = hingeAxisTransform ?? throw new ArgumentNullException(nameof(hingeAxisTransform));
    }

    public void AddChisel(Chisel chisel)
    {
        if (chisel == null)
        {
            throw new ArgumentNullException(nameof(chisel));
        }

        if (_chiselJointMap.ContainsKey(chisel))
        {
            return;
        }

        HingeJoint hinge = _parentTransform.gameObject.AddComponent<HingeJoint>();
        hinge.anchor = _parentTransform.InverseTransformPoint(chisel.transform.position);
        hinge.axis = _parentTransform.InverseTransformDirection(_hingeAxisTransform.right);

        _hingeJoints.Add(hinge);
        _chiselJointMap.Add(chisel, hinge);
    }

    public void RemoveChisel(Chisel chisel)
    {
        if (chisel == null)
        {
            throw new ArgumentNullException(nameof(chisel));
        }

        if (_chiselJointMap.TryGetValue(chisel, out HingeJoint joint))
        {
            _hingeJoints.Remove(joint);
            GameObject.Destroy(joint);
            _chiselJointMap.Remove(chisel);

            if (_hingeJoints.Count == 0)
            {
                AllHingesRemoved?.Invoke();
            }
        }
    }

    public void ClearAll()
    {
        foreach (HingeJoint joint in _hingeJoints)
        {
            GameObject.Destroy(joint);
        }

        _hingeJoints.Clear();
        _chiselJointMap.Clear();
    }
}