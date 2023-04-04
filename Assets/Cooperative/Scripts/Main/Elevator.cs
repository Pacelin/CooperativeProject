using TMPro;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Transform PlayerPoint;
    [SerializeField] private Animator _liftAnimator;
    [SerializeField] private bool _opened;
    [SerializeField] private TMP_Text _authorText;

    private void Awake() => _liftAnimator.SetBool("opened", _opened);
    public void Open() => _liftAnimator.SetBool("opened", true);
    public void Close() => _liftAnimator.SetBool("opened", false);

    public void SetAuthor(string name)
    {
        _authorText.text = name;
    }
}