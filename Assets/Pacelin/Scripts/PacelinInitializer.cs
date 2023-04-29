using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pacelin
{
	public class PacelinInitializer : Initializer
	{
		[Header("Pacelin Settings")]
		[SerializeField] private float _playerSpeed;
		[SerializeField] private InventorySwitching _customInventory;

		private float _defaultSpeed;

		public sealed override void InitializeScene()
		{
			Player.FPSController.enableJump = false;
			Player.FPSController.enableSprint = false;

			_defaultSpeed = Player.FPSController.walkSpeed;
			Player.FPSController.walkSpeed = _playerSpeed;
			Player.Inventory.enabled = false;
			Player.Interactor.Inventory = _customInventory;
		}
		public sealed override void DeinitializeScene() 
		{
			Player.FPSController.enableJump = true;
			Player.FPSController.enableSprint = true;
			
			Player.FPSController.walkSpeed = _defaultSpeed;
			Player.Inventory.enabled = true;
			Player.Interactor.Inventory = Player.Inventory;
			Player.Inventory.Joint.connectedBody = null;
		}
	}
}
