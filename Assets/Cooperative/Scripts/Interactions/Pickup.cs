using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Pickup : Interactable
{
    [Header("Pickup Settings")]
    [SerializeField] private Rigidbody _selfRigidbody;

    public sealed override void OnInteractDown(Interactor interactor) => OnTake(interactor.Inventory);
    public sealed override void OnInteractUp(Interactor interactor) { }

    public virtual void OnTake(Inventory inventory)
    {
        if (_selfCollider != null)
        {
            _selfRigidbody.isKinematic = true;
            _selfRigidbody.detectCollisions = false;
        }
        if (_selfCollider != null)
            _selfCollider.enabled = true;

        inventory.AddPickup(this);
    }
    
    public virtual void OnDrop(Inventory inventory)
    {
        if (_selfCollider != null)
        {
            _selfRigidbody.isKinematic = false;
            _selfRigidbody.detectCollisions = true;
        }
        if (_selfCollider != null)
            _selfCollider.enabled = true;

        inventory.RemovePickup(this);
    }
}
