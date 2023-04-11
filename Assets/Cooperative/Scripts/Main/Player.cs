using UnityEngine;

public class Player : MonoBehaviour
{
    public static bool IsInitialized => _instance != null;
    public static Camera PlayerCamera => _instance._playerCamera;
    public static Camera OverlayCamera => _instance._overlayCamera;
    public static FirstPersonController FPSController => _instance._personController;
    public static Interactor Interactor => _instance._interactor;
    public static HandInventory Inventory => _instance._inventory;

    private static Player _instance;

    [SerializeField] private Camera _playerCamera;
    [SerializeField] private Camera _overlayCamera;
    [SerializeField] private FirstPersonController _personController;
    [SerializeField] private Interactor _interactor;
    [SerializeField] private HandInventory _inventory;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }
}
