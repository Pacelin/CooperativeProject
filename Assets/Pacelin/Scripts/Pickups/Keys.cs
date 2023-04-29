using System.Collections;
using UnityEngine;

namespace Pacelin
{
	public class Keys : Pickup
	{
		[Header("Keys Settings")]
		[SerializeField] private AudioSource _pickupAudioSource;

		public override void OnTake(Inventory inventory)
		{
			base.OnTake(inventory);
            _pickupAudioSource?.Play();
		}

		public override void OnTryInteract(Interactor interactor) { }
	}
}