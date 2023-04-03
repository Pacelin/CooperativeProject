using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class Interactable : MonoBehaviour
{
    [Header("Interactable Settings")]
    public bool CanInteract;
    public bool HideInteraction;
    [Space]
    [SerializeField] protected Color _intractorColor = new Color(1, 1, 1, 0.8f);
    [SerializeField] protected float _interactorScale = 1.1f;
    [Space]
    [SerializeField] protected Collider _selfCollider;

    public virtual void OnInteractorEnter(Interactor interactor)
    {
        if (!HideInteraction)
            interactor.Crosshair.Set(_intractorColor, _interactorScale);
    }

    public virtual void OnInteractorExit(Interactor interactor)
    {
        if (!HideInteraction)
            interactor.Crosshair.SetDefault();
    }

    public abstract void OnTryInteract(Interactor interactor);
    public abstract void OnInteractDown(Interactor interactor);
    public abstract void OnInteractUp(Interactor interactor);
}