using UnityEngine;

public class Interactor : InteractorPointListener
{
    [SerializeField] private KeyCode _interactKey = KeyCode.E;

    private Interactable _currentInteractable;
    private bool _down;

    private void Update()
    {
        if (_currentInteractable == null) return;

        if (Input.GetKeyDown(_interactKey))
        {
            _currentInteractable.OnInteractDown();
            _down = true;
        }
        else if (_down && Input.GetKeyUp(_interactKey))
        {
            _currentInteractable.OnInteractUp();
        }
    }

    public override void OnNewGameObjectLooked(GameObject obj) =>
        SetInteractable(obj?.GetComponent<Interactable>());

    public override bool NeedMarkInteractable() =>
        _currentInteractable != null && !_currentInteractable.HideInteractAbility;
    
    private void SetInteractable(Interactable interactable)
    {
        if (interactable == _currentInteractable) return;

        if (interactable == null || !interactable.enabled || !interactable.CanInteract)
            interactable = null;
        
        _currentInteractable?.OnInteractorExit();
        _currentInteractable = interactable;
        _down = false;
        _currentInteractable?.OnInteractorEnter();
    }
}