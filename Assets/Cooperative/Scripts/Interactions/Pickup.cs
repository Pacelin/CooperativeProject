using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Pickup : Interactable
{
    public Rigidbody Rigidbody => _selfRigidbody;
    public Vector3 PickupInHandOffset => _pickupInHandOffset;
    public Vector3 PickupInHandRotation => _pickupInHandRotation;

    [Header("Pickup Settings")]
    [SerializeField] protected Rigidbody _selfRigidbody;
    [Space]
    [SerializeField] protected Vector3 _pickupInHandOffset = Vector3.zero;
    [SerializeField] protected Vector3 _pickupInHandRotation = Vector3.zero;

    protected virtual void Awake()
    {
        _selfRigidbody.solverIterations = 50;
    }

    public sealed override void OnInteractDown(Interactor interactor)
    {
        if (interactor.Inventory.IsFull) return;
        OnTake(interactor.Inventory);
    }
    public sealed override void OnInteractUp(Interactor interactor) { }

    public virtual void OnTake(Inventory inventory)
    {
        inventory.AddPickup(this);
    }
    
    public virtual void OnDrop(Inventory inventory)
    {
        inventory.RemovePickup(this);
    }
}
