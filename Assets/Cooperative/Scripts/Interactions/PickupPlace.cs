using System;
using System.Linq;
using UnityEngine;

public class PickupPlace : Interactable
{
    public event Action<Pickup> OnPlacePickup;
    public event Action<Pickup> OnTakePickup;

    public bool CanPlace { get => _canPlace; set => _canPlace = value; }
    public bool CanTake { get => _canTake; set => _canTake = value; }
    public bool IsEmpty => _placedPickup == null;

    [Header("Pickup Place Settings")]
    [SerializeField] private bool _canPlace;
    [SerializeField] private bool _canTake;
    [SerializeField] protected Pickup[] _placeablePickups;

    protected Pickup _placedPickup;

    public override void OnInteractDown(Interactor interactor) 
    {
        if (this.IsEmpty) Place(interactor.Inventory);
        else Take(interactor.Inventory);
    }

    private void Take(Inventory inventory)
    {
        if (!inventory.IsEmpty || !this.CanTake) return;

        var pickup = _placedPickup;
        
        _placedPickup = null;
        inventory.AddPickup(pickup);
        OnTakePickup?.Invoke(pickup);
    }

    private void Place(Inventory inventory)
    {
        if (inventory.IsEmpty || !this.CanPlace || !_placeablePickups.Contains(inventory.GetPickup(0))) return;
        
        _placedPickup = inventory.GetPickup(0);
        inventory.RemovePickup(0);
        OnPlacePickup?.Invoke(_placedPickup);
    }

    private void Update()
    {
        if (_placedPickup == null) return;

        _placedPickup.transform.position = transform.position;
        _placedPickup.transform.rotation = transform.rotation;
    }

    public override void OnTryInteract(Interactor interactor) { }
    public override void OnInteractUp(Interactor interactor) { }
}