using System;
using UnityEngine;

public class HandInventory : Inventory
{
    public Transform HandPoint => _handPoint;
    
    public override bool IsFull => HoldedPickup != null;
    public override bool IsEmpty => HoldedPickup == null;

    public Vector3 HandPosition => _handPoint.position;
    public Vector3 HandDirection => _handPoint.forward;

    public Pickup HoldedPickup { get; private set; }

    [SerializeField] private Transform _handPoint;
    [SerializeField] private KeyCode _dropKey = KeyCode.Q;

    private LayerMask _lastLayer;

    private void Update()
    {
        if (HoldedPickup == null) return;

        if (Input.GetKeyDown(_dropKey))
            HoldedPickup.OnDrop(this);
    }

    private void LateUpdate()
    {
        if (HoldedPickup == null) return;

        HoldedPickup.transform.position = _handPoint.TransformPoint(HoldedPickup.PickupInHandOffset);
        HoldedPickup.transform.rotation = _handPoint.rotation * Quaternion.Euler(HoldedPickup.PickupInHandRotation);
    }

    public override void AddPickup(Pickup pickup)
    {
        if (IsFull)
            throw new Exception("Hand Inventory is full");

        HoldedPickup = pickup;
        _lastLayer = pickup.gameObject.layer;
        //pickup.transform.position 
        //pickup.transform.rotation = ;
        //_joint.connectedBody = pickup.Rigidbody;
        pickup.gameObject.layer = LayerMask.NameToLayer("Pickup");
        pickup.Rigidbody.isKinematic = true;
        HoldedPickup.Collider.enabled = false;
        HoldedPickup.Rigidbody.MovePosition(_handPoint.TransformPoint(HoldedPickup.PickupInHandOffset));
        HoldedPickup.Rigidbody.MoveRotation(_handPoint.rotation * Quaternion.Euler(HoldedPickup.PickupInHandRotation));
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
        
        //_joint.connectedBody = null;
        HoldedPickup.Rigidbody.isKinematic = false;
        HoldedPickup.Collider.enabled = true;
        HoldedPickup.gameObject.layer = _lastLayer;
        HoldedPickup = null;
    }
}