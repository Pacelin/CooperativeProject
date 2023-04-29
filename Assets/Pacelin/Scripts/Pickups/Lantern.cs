using System.Collections;
using UnityEngine;

namespace Pacelin
{
	public class Lantern : Pickup
	{
		[Header("Lantern Settings")]
		[SerializeField] private KeyCode _switchKey = KeyCode.F;
		[Space]
		[SerializeField] private Material _emissionMat;
		[SerializeField] private Light _light;
		[Space]
		[SerializeField] private AudioSource _turnOnAudioSource;
		[SerializeField] private AudioSource _turnOffAudioSource;
		[SerializeField] private AudioSource _turnedOnAudioSource;
		[SerializeField] private AudioSource _kickAudioSource;
		[Space]
		[SerializeField] private Tint _tint;

		private bool _picked = false;
		private bool _enabled = true;
		private bool _firstPickup = false;	

		private void OnCollisionEnter(Collision collision)
		{
			_kickAudioSource.Play();
		}

		private void OnEnable()
		{
			if (_enabled)
			{
				_emissionMat.EnableKeyword("_EMISSION");
				_turnOnAudioSource.Play();
				_turnedOnAudioSource.PlayDelayed(_turnOnAudioSource.clip.length);
			}
			else
			{
				_emissionMat.DisableKeyword("_EMISSION");
				_turnedOnAudioSource.Stop();
			}
		}

		public override void OnTake(Inventory inventory)
		{
			base.OnTake(inventory);
			_picked = true;

			if (!_firstPickup)
			{
				_firstPickup = true;
				TintView.Instance.ShowTint(_tint);
			}
		}
		
		public override void OnDrop(Inventory inventory)
		{
			base.OnDrop(inventory);
			_picked = false;
		}

		private void Update()
		{
			if (!_picked) return;

			if (Input.GetKeyDown(_switchKey)) Switch();
		}

		private void Switch()
		{
			_enabled = !_enabled;
			_light.enabled = _enabled;

			if (_enabled)
			{
				_emissionMat.EnableKeyword("_EMISSION");
				_turnOnAudioSource.Play();
				_turnedOnAudioSource.PlayDelayed(_turnOnAudioSource.clip.length);
			}
			else
			{
				_emissionMat.DisableKeyword("_EMISSION");
				_turnedOnAudioSource.Stop();
				_turnOffAudioSource.Play();
			}
		}

		public override void OnTryInteract(Interactor interactor) { }
	}
}