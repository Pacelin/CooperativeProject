using System;
using UnityEngine;

public class HandInventory : Inventory
{
    public Vector3 HandPosition => _handPoint.position;
    public Vector3 HandDirection => _handPoint.forward;

    public Pickup HoldedPickup { get; private set; }
 
    [SerializeField] private Transform _handPoint;
    [SerializeField] private KeyCode _dropKey = KeyCode.Q;

    private void Update()
    {
        if (HoldedPickup == null) return;

        HoldedPickup.transform.position = _handPoint.position;
        HoldedPickup.transform.forward = _handPoint.forward;

        if (Input.GetKeyDown(_dropKey))
            HoldedPickup.OnDrop(this);
    }

    public override void AddPickup(Pickup pickup)
    {
        if (IsFull())
            throw new Exception("Hand Inventory is full");

        HoldedPickup = pickup;
        pickup.transform.position = _handPoint.position;
        pickup.transform.forward = _handPoint.forward;
    }

    public override void RemovePickup(Pickup pickup)
    {
        if (HoldedPickup != pickup) return;

        HoldedPickup = null;
    }

    public override void RemovePickup(int index) => HoldedPickup = null;

    public override Pickup GetPickup(int index) => HoldedPickup;

    public override bool ContainsPickup(Pickup pickup) => HoldedPickup == pickup;
    public override bool IsFull() => HoldedPickup != null;
}
