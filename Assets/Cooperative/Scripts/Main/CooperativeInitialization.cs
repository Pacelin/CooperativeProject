using UnityEngine;

public abstract class CooperativeInitialization : MonoBehaviour
{
    [Header("Cooperative Settings")]
    public string AuthorName;
    [Space]
    public Transform StartElevetorPoint;
    public Transform FinishElevatorPoint;

    public abstract void InitializeScene();
    public abstract void DeinitializeScene();
}
