using UnityEngine;

public class InitializationExample : CooperativeInitialization
{
    [Header("My Settings")]
    [SerializeField] private float _speedMultiplier;
    
    private float _defaultMovementSpeed;
    
    public override void InitializeScene()
    {
        _defaultMovementSpeed = Player.FPSController.walkSpeed;
        Player.FPSController.walkSpeed = _defaultMovementSpeed * _speedMultiplier;
    }
    public override void DeinitializeScene()
    {
        Player.FPSController.walkSpeed = _defaultMovementSpeed;
    }
}