using UnityEditor;
using UnityEditor.Presets;
using UnityEngine;

namespace Pacelin
{
	public abstract class LockableInteractable : Interactable
	{
		public bool Locked => _locked;
		[Header("Lock Settings")]
		[SerializeField] private bool _locked;
		[Space]
		[SerializeField] private Pickup _key;
		[SerializeField] private bool _destroyKey;
		[Space]
		[SerializeField] private AudioSource _unlockAudioSource;
		[SerializeField] private AudioSource _lockedAudioSource;

		public void Unlock()
		{
			_unlockAudioSource?.Play();
			_locked = false;
			OnUnlock();
		}

		public abstract void OnInteract(Interactor interactor);
		public abstract void OnUnlock();

		public sealed override void OnInteractDown(Interactor interactor)
		{
			if (!Locked)
			{
				OnInteract(interactor);
				return;
			}

			if (_key != null && interactor.Inventory.ContainsPickup(_key))
			{
				if (_destroyKey)
				{
					interactor.Inventory.RemovePickup(_key);
					Destroy(_key.gameObject);
				}
				Unlock();
				return;	
			}

			_lockedAudioSource?.Play();
		}
		
		public sealed override void OnInteractUp(Interactor interactor) { }
		public sealed override void OnTryInteract(Interactor interactor) { }
	}
}