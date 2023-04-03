using UnityEngine;

public class Interactor : MonoBehaviour
{
    public Inventory Inventory { get => _handInventory; set => _handInventory = value; }
    public InteractorRay RayPoint => _ray;
    public InteractorCrosshair Crosshair => _crosshair;

    [SerializeField] private KeyCode _interactKey = KeyCode.E;
    [SerializeField] private Inventory _handInventory;
    [SerializeField] private InteractorRay _ray;
    [SerializeField] private InteractorCrosshair _crosshair;

    private Interactable _currentInteractable;
    private bool _down;

    private void OnEnable()
    {
        _ray.OnLookedObjectChanged += OnLookedObjectChanged;
    }

    private void OnDisable()
    {
        _ray.OnLookedObjectChanged -= OnLookedObjectChanged;
    }

    private void Update()
    {
        if (_currentInteractable == null) return;

        if (Input.GetKeyDown(_interactKey))
        {
            if (_currentInteractable.CanInteract)
            {
                _down = true;
                _currentInteractable.OnInteractDown(this);
            }
            else
            {
                _currentInteractable.OnTryInteract(this);
            }
        }
        else if (_down && Input.GetKeyUp(_interactKey))
        {
            _currentInteractable.OnInteractUp(this);
        }
    }

    public void OnLookedObjectChanged(GameObject obj)
    {
        Interactable interactable = obj?.GetComponent<Interactable>();
        if (interactable == _currentInteractable) return;

        _currentInteractable?.OnInteractorExit(this);

        _currentInteractable = interactable;
        _down = false;
        
        _currentInteractable?.OnInteractorEnter(this);
    }
}