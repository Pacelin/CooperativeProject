using UnityEngine;

public class Player : MonoBehaviour
{
    public static Camera PlayerCamera => _instance._playerCamera;
    public static FirstPersonController FPSController => _instance._personController;
    public static InteractorPoint Interactor => _instance._interactor;
    public static PickupTaker PickupTaker => _instance._pickupTaker;
    public static HandInventory Inventory => _instance._inventory;

    private static Player _instance;

    [SerializeField] private Camera _playerCamera;
    [SerializeField] private FirstPersonController _personController;
    [SerializeField] private InteractorPoint _interactor;
    [SerializeField] private HandInventory _inventory;
    [SerializeField] private PickupTaker _pickupTaker;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }
}
