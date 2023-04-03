using UnityEngine;

public abstract class Inventory : MonoBehaviour
{
    public abstract void AddPickup(Pickup pickup);
    public abstract void RemovePickup(Pickup pickup);
    public abstract bool ContainsPickup(Pickup pickup);
    public abstract bool IsFull();
}