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
        {
            var pickup = HoldedPickup;

            if (pickup.CanDrop)
            {
                RemovePickup(pickup);
                pickup.OnDrop();
            }
            else
            {
                pickup.OnDropFailed();
            }
        }
    }

    public override void AddPickup(Pickup pickup)
    {
        if (IsFull())
            throw new Exception("Hand Inventory is full");

        HoldedPickup = pickup;
        pickup.DisablePhysics();
        pickup.transform.position = _handPoint.position;
        pickup.transform.forward = _handPoint.forward;
    }

    public override void RemovePickup(Pickup pickup)
    {
        if (HoldedPickup != pickup) return;

        HoldedPickup = null;
        pickup.EnablePhysics();
    }

    public override bool IsFull() => HoldedPickup != null;
}
