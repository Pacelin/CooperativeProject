using UnityEngine;
using UnityEngine.UI;

public class InteractorCrosshair : MonoBehaviour
{
    public bool Visible { get; private set; }
    public Color Color => _image.color;

    [SerializeField] private Image _image;
    [Space]
    [SerializeField] private float _defaultScale;
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Sprite _defaultSprite;

    private void AWake()
    {
        Show();
        SetDefault();
    }

    public void Show() => Visible = _image.enabled = true;
    public void Hide() => Visible = _image.enabled = false;

    public void SetDefault()
    {
        _image.transform.localScale = Vector3.one * _defaultScale;
        _image.color = _defaultColor;
        _image.sprite = _defaultSprite;
    }
    
    public void Set(Color color, float scale)
    {
        _image.transform.localScale = Vector3.one * scale;
        _image.color = color;
    }

    public void SetSprite(Sprite sprite)
    {
        _image.sprite = sprite;
    }
}