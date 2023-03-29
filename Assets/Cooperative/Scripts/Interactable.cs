using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class Interactable : MonoBehaviour
{
    public bool CanInteract;
    public bool HideInteractAbility;

    public abstract void OnInteractorEnter();
    public abstract void OnInteractDown();
    public abstract void OnInteractUp();
    public abstract void OnInteractorExit();
}