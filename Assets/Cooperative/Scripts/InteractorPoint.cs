using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractorPoint : MonoBehaviour
{
    public bool Enabled => _enabled;
    public GameObject LookAtGameObject => _currentGameObject;

    [SerializeField] private float _interactDistance;
    [SerializeField] private LayerMask _interactMask;
    [SerializeField] private Transform _rayPoint;
    [SerializeField] private List<InteractorPointListener> _listeners;

    [Header("References")]
    [SerializeField] private Image _crosshair;
    [Space]
    [SerializeField] private float _crosshairDefaultScale;
    [SerializeField] private float _crosshairInteractScale;
    [Space]
    [SerializeField] private Color _crosshairDefaultColor;
    [SerializeField] private Color _crosshairInteractColor;

    private bool _enabled;
    private bool _interactable;
    private GameObject _currentGameObject;

    private void Awake() => Enable();

    private void FixedUpdate()
    {
        if (!enabled) return;

        GameObject obj = null;
        if (Physics.Raycast(_rayPoint.position, _rayPoint.forward, out var hit, _interactDistance, _interactMask))
            obj = hit.transform.gameObject;

        SetGameObjectLooked(obj);
    }

    public void AddInteractorPointListener(InteractorPointListener listener) => _listeners.Add(listener);
    public void RemoveInteractorPointListener(InteractorPointListener listener) => _listeners.Remove(listener);
    
    public void Enable()
    {
        _crosshair.color = _crosshairDefaultColor;
        _enabled = true;
    }
    
    public void Disable()
    {
        _crosshair.color = Color.clear;
        _enabled = false;
        SetGameObjectLooked(null);
    }

    private void SetGameObjectLooked(GameObject obj)
    {
        if (obj == _currentGameObject) return;

        bool markInteractable = false;
        foreach (var listener in _listeners)
        {
            listener.OnNewGameObjectLooked(obj);
            if (listener.NeedMarkInteractable())
                markInteractable = true;
        }

        _currentGameObject = obj;
        MarkInteractable(markInteractable);
    }

    private void MarkInteractable(bool interactable)
    {
        if (!enabled || _interactable == interactable) return;

        if (interactable)
        {
            _crosshair.transform.localScale = new Vector3(_crosshairInteractScale, _crosshairInteractScale, 1);
            _crosshair.color = _crosshairInteractColor;
        }
        else
        {
            _crosshair.transform.localScale = new Vector3(_crosshairDefaultScale, _crosshairDefaultScale, 1);
            _crosshair.color = _crosshairDefaultColor;
        }

        _interactable = interactable;
    }
}