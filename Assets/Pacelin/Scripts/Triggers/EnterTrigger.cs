using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pacelin
{
	[RequireComponent(typeof(Collider))]
	public class EnterTrigger : Trigger
	{
		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Player"))
				Notify();
		}
	}
}
