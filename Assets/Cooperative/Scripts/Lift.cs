using System.Numerics;
using UnityEngine;

public class Lift : MonoBehaviour
{
    [SerializeField] private Animator _liftAnimator;
    [SerializeField] private bool _opened;

    private void Awake() => _liftAnimator.SetBool("opened", _opened);
    public void Open() => _liftAnimator.SetBool("opened", true);
    public void Close() => _liftAnimator.SetBool("opened", false);
}