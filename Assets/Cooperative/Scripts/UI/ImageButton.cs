using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
	public event System.Action<ImageButton> OnClick;

	[SerializeField] private Color _defaultColor = Color.white;
	[SerializeField] private Color _hoverColor = Color.white;
	[SerializeField] private Color _downColor = Color.white;
	
	private Image _image;
	private bool _isDown;

	private void Awake()
	{
		_image = GetComponent<Image>();
		_image.color = _defaultColor;
	}

    public void OnPointerDown(PointerEventData eventData) =>
		SetState(_downColor, true);
    public void OnPointerEnter(PointerEventData eventData) =>
		SetState(_hoverColor, false);
    public void OnPointerExit(PointerEventData eventData) =>
		SetState(_defaultColor, false);
    public void OnPointerUp(PointerEventData eventData) =>
		TryClick().SetState(_hoverColor, false);

	private void SetState(Color color, bool isDown)
	{
		_isDown = isDown;
		_image.color = color;
	}
	private ImageButton TryClick()
	{
		if (_isDown) OnClick?.Invoke(this);
		return this;
	}
}
