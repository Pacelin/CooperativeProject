using System.Collections;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public bool Opened => _animator.GetBool("opened");

    [Header("Menu Settings")]
    [SerializeField] protected float _closeTime;
    [SerializeField] private Animator _animator;
    
    public void Open() => _animator.SetBool("opened", true);
    public void Close() => _animator.SetBool("opened", false);
    public void SwitchTo(Menu menu) => StartCoroutine(Switching(menu));

    private IEnumerator Switching(Menu menu)
    {
        Close();
        yield return new WaitForSeconds(_closeTime);
        menu.Open();
    }
}