using UnityEngine;

public class Lift : MonoBehaviour
{
    public Transform PlayerPoint;
    [SerializeField] private Animator _liftAnimator;
    [SerializeField] private bool _opened;

    private Vector3 _startPoint;
    private Vector3 _finishPoint;

    private void Awake() => _liftAnimator.SetBool("opened", _opened);
    public void Open() => _liftAnimator.SetBool("opened", true);
    public void Close() => _liftAnimator.SetBool("opened", false);

    public void MoveToStartPoint()
    {
        transform.position = _startPoint;
    }

    public void MoveToFinishPoint()
    {
        transform.position = _finishPoint;
    }

    public void SetStartPoint(Vector3 point) => _startPoint = point;
    public void SetFinishPoint(Vector3 point) => _finishPoint = point;
    public void SetAuthor(string name)
    {
        
    }
}