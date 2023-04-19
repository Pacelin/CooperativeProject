using UnityEngine;

public class ElevatorButton : Interactable
{
    [SerializeField] private ScenesLoader _loader;
    [SerializeField] private AudioSource _selfAudioSource;

    public override void OnInteractDown(Interactor interactor)
    {
        _loader.GoToNextScene();
        _selfAudioSource.Play();
    }

    public override void OnInteractUp(Interactor interactor) { }
    public override void OnTryInteract(Interactor interactor) { }
}