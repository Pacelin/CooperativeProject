using System;
using UnityEngine;

public class PickupTaker : InteractorPointListener
{
    public Action OnTryPickupInFullInventory;

    [SerializeField] private KeyCode _pickupKey = KeyCode.E;
    [SerializeField] private Inventory _inventory;

    private Pickup _currentPickup;
    
    private void Update()
    {
        if (_currentPickup == null) return;

        if (Input.GetKeyDown(_pickupKey))
        {
            if (_inventory.IsFull())
            {
                OnTryPickupInFullInventory?.Invoke();
            }
            else if (_currentPickup.CanTake)
            {
                _inventory.AddPickup(_currentPickup);
                _currentPickup.OnTake();
            }
            else
            {
                _currentPickup.OnTakeFailed();
            }
        }
    }

    public override void OnNewGameObjectLooked(GameObject obj) =>
        SetPickup(obj?.GetComponent<Pickup>());

    public override bool NeedMarkInteractable() =>
        _currentPickup != null && !_currentPickup.HidePickupAbility;
    
    private void SetPickup(Pickup pickup)
    {
        if (_currentPickup == pickup) return;

        if (pickup == null || !pickup.enabled)
            pickup = null;
        
        _currentPickup = pickup;
    }
}