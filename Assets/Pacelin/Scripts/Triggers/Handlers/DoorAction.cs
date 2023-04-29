using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pacelin
{
	public class DoorAction : TriggerAction
	{
		[Header("Door Settings")]
		[SerializeField] private Door _door;
		[SerializeField] private DoorActionType _doorActionType;

		protected override IEnumerator ApplyAction()
		{
			if (_doorActionType == DoorActionType.OpenDoor ||
				_doorActionType == DoorActionType.OpenDoorAndLock)
				_door.Open();
			else
				_door.Close();

			if (_doorActionType == DoorActionType.OpenDoorAndLock ||
				_doorActionType == DoorActionType.CloseDoorAndLock)
			{
				_door.CanInteract = false;
				_door.HideInteraction = true;
			}
			yield break;
		}
	}

	public enum DoorActionType
	{
		OpenDoor,
		OpenDoorAndLock,
		CloseDoor,
		CloseDoorAndLock
	}
}
