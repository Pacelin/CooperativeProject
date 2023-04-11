using System;
using UnityEngine;

public class InteractorRay : MonoBehaviour
{
    public event Action<GameObject> OnLookedObjectChanged;

    public bool Enabled => _enabled;
    public GameObject LookAtGameObject { get; private set; }
    public float RayDistance { get => _rayDistance; set => _rayDistance = value; }

    [SerializeField] private bool _enabled;
    [SerializeField] private float _rayDistance;
    [SerializeField] private LayerMask _rayMask;
    [SerializeField] private Transform _rayPoint;

    public void Enable()
    {
        _enabled = true;
    }
    
    public void Disable()
    {
        _enabled = false;
        SetLookedGameObject(null);
    }

    private void FixedUpdate()
    {
        if (!_enabled) return;

        GameObject obj = null;
        if (Physics.Raycast(_rayPoint.position, _rayPoint.forward, out var hit, _rayDistance, _rayMask))
            obj = hit.transform.gameObject;
        
        SetLookedGameObject(obj);
    }

    private void SetLookedGameObject(GameObject obj)
    {
        if (obj == LookAtGameObject) return;
        
        LookAtGameObject = obj;
        OnLookedObjectChanged?.Invoke(obj);
    }
}