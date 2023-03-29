using UnityEngine;

public class Player : MonoBehaviour
{
    public static Camera PlayerCamera => _instance._playerCamera;
    public static FirstPersonController FPSController => _instance._personController;
    public static Interactor Interactor => _instance._interactor;

    private static Player _instance;

    [SerializeField] private Camera _playerCamera;
    [SerializeField] private FirstPersonController _personController;
    [SerializeField] private Interactor _interactor;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }
}
