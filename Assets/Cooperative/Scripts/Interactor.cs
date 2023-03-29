using UnityEngine;
using UnityEngine.UI;

public class Interactor : MonoBehaviour
{
    public float InteractDistance { get => _interactDistance; set => _interactDistance = value; }

    [SerializeField] private KeyCode _interactKey = KeyCode.E;
    [Header("Settings")]
    [SerializeField] private Transform _rayPoint;
    [SerializeField] private float _interactDistance;
    [Space]
    [SerializeField] private Image _crosshair;
    [Space]
    [SerializeField] private float _crosshairDefaultScale;
    [SerializeField] private float _crosshairInteractScale;
    [Space]
    [SerializeField] private Color _crosshairDefaultColor;
    [SerializeField] private Color _crosshairInteractColor;

    private bool _show = true;
    private Interactable _currentInteractable;
    private bool _down = false;

    private void FixedUpdate()
    {
        if (_show)
        {
            int layerMask = ~LayerMask.GetMask("Player", "Ignore Raycast");
            if (Physics.Raycast(_rayPoint.position, _rayPoint.forward, out var hit, _interactDistance, layerMask))
                SetInteractable(hit.transform.GetComponent<Interactable>());
            else
                SetInteractable(null);
        }
    }

    private void Update()
    {
        if (!_show || _currentInteractable == null) return;

        if (Input.GetKeyDown(_interactKey))
        {
            _currentInteractable.OnInteractDown();
            _down = true;
        }
        else if (_down && Input.GetKeyUp(_interactKey))
        {
            _currentInteractable.OnInteractUp();
            if (_currentInteractable.InteractOnce)
                _currentInteractable.enabled = false;
        }
    }

    public void Show()
    {
        _crosshair.color = _crosshairDefaultColor;
        _show = true;
    }

    public void Hide()
    {
        SetInteractable(null);
        _crosshair.color = Color.clear;
        _show = false;
    }

    private void SetInteractable(Interactable interactable)
    {
        if (interactable == null || !interactable.enabled)
        {
            if (_currentInteractable != null)
            {
                _down = false;
                _currentInteractable.OnInteractorExit();
            }
            _currentInteractable = null;

            _crosshair.transform.localScale = new Vector3(_crosshairDefaultScale, _crosshairDefaultScale, 1);
            _crosshair.color = _crosshairDefaultColor;
        }
        else
        {
            if (_currentInteractable == interactable) return;

            if (_currentInteractable != null)
            {
                _down = false;
                _currentInteractable.OnInteractorExit();
            }
            _currentInteractable = interactable;
            _currentInteractable.OnInteractorEnter();

            _crosshair.transform.localScale = new Vector3(_crosshairInteractScale, _crosshairInteractScale, 1);
            _crosshair.color = _crosshairInteractColor;
        }
    }
}
