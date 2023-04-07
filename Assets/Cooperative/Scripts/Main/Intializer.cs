using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Initializer : MonoBehaviour
{
    [Header("Settings")]
    public string AuthorName;
    [Space]
    public Transform StartElevatorPoint;
    public Transform FinishElevatorPoint;

    private void Awake()
    {
        if (!Player.IsInitialized)
            SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public abstract void InitializeScene();
    public abstract void DeinitializeScene();

    private void OnDrawGizmos()
    {
        if (StartElevatorPoint != null)
            DrawElevatorGizmos(StartElevatorPoint, new Color(0, 1, 0, 0.2f));

        if (FinishElevatorPoint != null)
            DrawElevatorGizmos(FinishElevatorPoint, new Color(1, 0, 0, 0.2f));
    }

    private void DrawElevatorGizmos(Transform transform, Color color)
    {
        Gizmos.color = color;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawCube(Vector3.up * 2.5f, new Vector3(4, 5, 4));

        Gizmos.color = new Color(1, 1, 1, 0.6f);
        Gizmos.DrawWireCube(Vector3.forward * 2f + Vector3.up * 1.5f, new Vector3(2, 3, 0));
    }
}
