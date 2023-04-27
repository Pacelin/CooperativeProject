using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ElevatorClosing : MonoBehaviour
{
	[SerializeField] private Elevator _elevator;

	private void OnTriggerEnter(Collider other)
	{
		if (_elevator.Opened && other.CompareTag("Player"))
			_elevator.Close();
	}
}
