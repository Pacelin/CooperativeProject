using UnityEngine;

public abstract class CooperativeInitialization : MonoBehaviour
{
    public string AuthorName;
    public Vector3 StartLiftPosition;
    public Vector3 FinishLiftPosition;

    public abstract void InitializeScene();
    public abstract void DeinitializeScene();
}
