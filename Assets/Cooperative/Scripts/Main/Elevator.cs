using TMPro;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Transform PlayerPoint;
    [SerializeField] private Animator _liftAnimator;
    [SerializeField] private TMP_Text _authorText;

    public void Open() 
    {
        _liftAnimator.SetTrigger("init");
        _liftAnimator.SetBool("opened", true);
    }
    public void Close() 
    {
        _liftAnimator.SetTrigger("init");
        _liftAnimator.SetBool("opened", false);
    }

    public void SetAuthor(string name)
    {
        _authorText.text = name;
    }
}