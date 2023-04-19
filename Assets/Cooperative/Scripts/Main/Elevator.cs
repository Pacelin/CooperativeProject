using TMPro;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Transform PlayerPoint;
    [SerializeField] private AudioSource _selfAudioSource;
    [SerializeField] private AudioClip _openClip;
    [SerializeField] private AudioClip _closeClip;
    [SerializeField] private Animator _liftAnimator;
    [SerializeField] private TMP_Text _authorText;

    public void Open() 
    {
        _liftAnimator.SetTrigger("init");
        _liftAnimator.SetBool("opened", true);
        _selfAudioSource.clip = _openClip;
        _selfAudioSource.Play();
    }
    public void Close() 
    {
        _liftAnimator.SetTrigger("init");
        _liftAnimator.SetBool("opened", false);
        _selfAudioSource.clip = _closeClip;
        _selfAudioSource.Play();
    }

    public void SetAuthor(string name)
    {
        _authorText.text = name;
    }
}