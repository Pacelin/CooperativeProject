using System;
using UnityEngine;

public class HandInventory : Inventory
{
    public override bool IsFull => HoldedPickup != null;
    public override bool IsEmpty => HoldedPickup == null;

    public Vector3 HandPosition => _handPoint.position;
    public Vector3 HandDirection => _handPoint.forward;

    public Pickup HoldedPickup { get; private set; }

    [SerializeField] private Transform _handPoint;
    [SerializeField] private FixedJoint _joint;
    [SerializeField] private KeyCode _dropKey = KeyCode.Q;

    private LayerMask _lastLayer;

    private void Update()
    {
        if (HoldedPickup == null) return;

        if (Input.GetKeyDown(_dropKey))
            HoldedPickup.OnDrop(this);
    }

    public override void AddPickup(Pickup pickup)
    {
        if (IsFull)
            throw new Exception("Hand Inventory is full");

        HoldedPickup = pickup;
        _lastLayer = pickup.gameObject.layer;
        pickup.transform.position = _handPoint.TransformPoint(pickup.PickupInHandOffset);
        pickup.transform.rotation = _handPoint.rotation * Quaternion.Euler(pickup.PickupInHandRotation);
        _joint.connectedBody = pickup.Rigidbody;
        pickup.gameObject.layer = LayerMask.NameToLayer("Pickup");
    }

    public override void RemovePickup(Pickup pickup)
    {
        if (HoldedPickup == pickup) Clear();
    }

    public override void RemovePickup(int index) => Clear();
    public override Pickup GetPickup(int index) => HoldedPickup;
    public override bool ContainsPickup(Pickup pickup) => HoldedPickup == pickup;
    public override void Clear() 
    {
        if (HoldedPickup == null) return;
        
        _joint.connectedBody = null;
        HoldedPickup.gameObject.layer = _lastLayer;
        HoldedPickup = null;
    }
}