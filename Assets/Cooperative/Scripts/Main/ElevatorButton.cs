using UnityEngine;

public class ElevatorButton : Interactable
{
    [SerializeField] private ScenesLoader _loader;

    public override void OnInteractDown(Interactor interactor)
    {
        _loader.GoToNextScene();
    }

    public override void OnInteractUp(Interactor interactor) { }
    public override void OnTryInteract(Interactor interactor) { }
}