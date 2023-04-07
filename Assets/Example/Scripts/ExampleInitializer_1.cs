using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ENTER_YOUR_NAME_HERE
{
	public class ExampleInitializer_1 : Initializer
	{
		public sealed override void InitializeScene()
		{
			Player.FPSController.sprintSpeed *= 2;
		}
		public sealed override void DeinitializeScene() 
		{
			Player.FPSController.sprintSpeed /= 2;
		}
	}
}
