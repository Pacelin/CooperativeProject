using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Pacelin
{
	public class TintView : MonoBehaviour
	{
		public static TintView Instance => _instance;
		private static TintView _instance;

		[SerializeField] private float _timeIn;
		[SerializeField] private float _timeOut;
		[SerializeField] private float _timeDelay;
		[Space]
		[SerializeField] private TMP_Text _textView;
		[SerializeField] private CanvasGroup _group;

		private Queue<Tint> _tintsQueue = new Queue<Tint>();

		private void Awake()
		{
			_instance = this;
		}

		private void Start() =>
			StartCoroutine(Showing());

		public void ShowTint(Tint tint) =>
			_tintsQueue.Enqueue(tint);

		private IEnumerator Showing()
		{
			while (true)
			{
				yield return new WaitUntil(() => _tintsQueue.Count > 0);
				
				var tint = _tintsQueue.Dequeue();
				_textView.text = tint.Text;

				for (float t = 0; t < _timeIn; t += Time.deltaTime)
				{
					_group.alpha = Mathf.Lerp(0, 1, t / _timeIn);
					yield return null;
				}
				_group.alpha = 1;
				
				yield return new WaitForSeconds(tint.Duration);

				for (float t = 0; t < _timeOut; t += Time.deltaTime)
				{
					_group.alpha = Mathf.Lerp(1, 0, t / _timeOut);
					yield return null;
				}
				_group.alpha = 0;
				
				yield return new WaitForSeconds(_timeDelay);
			}
		}
	}
}
