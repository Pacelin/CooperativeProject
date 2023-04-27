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

        HoldedPickup.transform.position = _handPoint.position;
        HoldedPickup.transform.forward = _handPoint.forward;
    }

    public override void AddPickup(Pickup pickup)
    {
        if (IsFull)
            throw new Exception("Hand Inventory is full");

        HoldedPickup = pickup;
        _lastLayer = pickup.gameObject.layer;
        pickup.transform.position = _handPoint.position;
        pickup.transform.forward = _handPoint.forward;
        pickup.gameObject.layer = LayerMask.NameToLayer("Overlay");
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
        if (HoldedPickup != null)
        HoldedPickup.gameObject.layer = _lastLayer;
        HoldedPickup = null;
    }
}