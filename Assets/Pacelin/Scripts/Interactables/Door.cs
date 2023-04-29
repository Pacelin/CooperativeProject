using System.Collections;
using UnityEngine;

namespace Pacelin
{
	public class Door : LockableInteractable
	{
		[Header("Door Settings")]
		[SerializeField] private AudioSource _openAudioSource;
		[SerializeField] private AudioSource _closeAudioSource;
		[Space]
		[SerializeField] private AnimationCurve _openingCurve;
		[SerializeField] private AnimationCurve _closingCurve;
		[Space]
		[SerializeField] private float _openedRotationY;
		[SerializeField] private float _closedRotationY;
		[SerializeField] private bool _opened;

		private bool _lockOpening;

		private void Awake()
		{
			transform.localRotation = Quaternion.Euler(0, _opened ? _openedRotationY : _closedRotationY, 0);
		}
		
		public override void OnInteract(Interactor interactor)
		{
			if (_opened) Close();
			else Open();
		}

		public void Open()
		{
			if (_opened || _lockOpening) return;
			
			_opened = true;
			StartCoroutine(Rotate(_openAudioSource, _openingCurve));
		}
		
		public void Close()
		{
			if (!_opened || _lockOpening) return;
			
			_opened = false;
			StartCoroutine(Rotate(_closeAudioSource, _closingCurve));
		}

		private IEnumerator Rotate(AudioSource source, AnimationCurve curve)
		{
			_lockOpening = true;
			var clipLength = source.clip.length;
			source.Play();

			for (float t = 0; t < clipLength; t += Time.deltaTime)
			{
				var rotationZ = Mathf.LerpAngle(_closedRotationY, _openedRotationY, curve.Evaluate(t / clipLength)); 
				transform.localRotation = Quaternion.Euler(0, rotationZ, 0);

				yield return null;
			}
			transform.localRotation = Quaternion.Euler(0, _opened ? _openedRotationY : _closedRotationY, 0);
			_lockOpening = false;
		}

        public override void OnUnlock() { }
    }
}