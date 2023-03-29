using UnityEngine;

public abstract class InteractorPointListener : MonoBehaviour
{
    public abstract bool NeedMarkInteractable();
    public abstract void OnNewGameObjectLooked(GameObject obj);
}