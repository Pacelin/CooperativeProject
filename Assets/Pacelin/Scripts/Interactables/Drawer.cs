using System.Collections;
using System.Linq;
using UnityEngine;

namespace Pacelin
{
	public class Drawer : LockableInteractable
	{
		public event System.Action<Drawer, bool> OnOpenedChanged;

		[SerializeField] private bool _opened;
		[Space]
		[SerializeField] private Vector3 _closedPosition;
		[SerializeField] private Vector3 _openedPosition;
		[Space]
		[SerializeField] private AnimationCurve _openingCurve;
		[SerializeField] private AnimationCurve _closingCurve;
		[Space]
		[SerializeField] private AudioSource _openingAudioSource;
		[SerializeField] private AudioSource _closingAudioSource;

		private bool _lockAnimation;

		private void Awake()
		{
			if (_opened)
				transform.localPosition = _openedPosition;
			else
				transform.localPosition = _closedPosition;
		}

		private IEnumerator Opening()
		{
			_lockAnimation = true;
			_opened = true;
			OnOpenedChanged?.Invoke(this, true);
			_openingAudioSource.Play();

			for (float t = 0; t < _openingCurve.keys.Last().time; t += Time.deltaTime)
			{
				transform.localPosition = Vector3.Lerp(_closedPosition, _openedPosition, _openingCurve.Evaluate(t));
				yield return null;
			}
			transform.localPosition = _openedPosition;

			_lockAnimation = false;
		}

		private IEnumerator Closing()
		{
			_lockAnimation = true;
			_opened = false;
			OnOpenedChanged?.Invoke(this, false);
			_closingAudioSource.Play();

			for (float t = 0; t < _closingCurve.keys.Last().time; t += Time.deltaTime)
			{
				transform.localPosition = Vector3.Lerp(_closedPosition, _openedPosition, _closingCurve.Evaluate(t));
				yield return null;
			}
			transform.localPosition = _closedPosition;

			_lockAnimation = false;
		}

        public override void OnInteract(Interactor interactor)
        {
			if (_lockAnimation) return;

			if (_opened)
				StartCoroutine(Closing());
			else
				StartCoroutine(Opening());
        }

        public override void OnUnlock() { }
    }
}