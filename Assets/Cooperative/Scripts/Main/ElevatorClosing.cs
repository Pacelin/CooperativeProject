using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ElevatorClosing : MonoBehaviour
{
	[SerializeField] private Elevator _elevator;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
			_elevator.Close();
	}
}
