using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(TMP_Text))]
public class TextButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
	public event System.Action OnClick;

	[SerializeField] private Color _defaultColor = Color.white;
	[SerializeField] private Color _hoverColor = Color.white;
	[SerializeField] private Color _downColor = Color.white;
	[Space]
	[SerializeField] private Vector3 _defaultScale = Vector3.one;
	[SerializeField] private Vector3 _hoverScale = Vector3.one * 1.1f;
	[SerializeField] private Vector3 _downScale = Vector3.one * 0.9f;
	
	private TMP_Text _text;
	private bool _isDown;

	private void Awake()
	{
		_text = GetComponent<TMP_Text>();
		_text.color = _defaultColor;
	}

    public void OnPointerDown(PointerEventData eventData) =>
		SetState(_downColor, _downScale, true);
    public void OnPointerEnter(PointerEventData eventData) =>
		SetState(_hoverColor, _hoverScale, false);
    public void OnPointerExit(PointerEventData eventData) =>
		SetState(_defaultColor, _defaultScale, false);
    public void OnPointerUp(PointerEventData eventData) =>
		TryClick().SetState(_hoverColor, _hoverScale, false);

	private void SetState(Color color, Vector3 scale, bool isDown)
	{
		_isDown = isDown;
		_text.color = color;
		transform.localScale = scale;
	}
	private TextButton TryClick()
	{
		if (_isDown) OnClick?.Invoke();
		return this;
	}
}
