using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class Pickup : MonoBehaviour
{
    public bool CanTake;
    public bool CanDrop;
    public bool HidePickupAbility;

    public abstract void OnTake();
    public abstract void OnDrop();
    
    public abstract void DisablePhysics();
    public abstract void EnablePhysics();   

    public abstract void OnTakeFailed();
    public abstract void OnDropFailed();
}
