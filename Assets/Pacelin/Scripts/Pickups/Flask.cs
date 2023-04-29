using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pacelin
{
	public class Flask : Pickup
	{
		public FlaskFluid FluidType => _fluidType;
		[SerializeField] private FlaskFluid _fluidType;
		public override void OnTryInteract(Interactor interactor) { }
	}

	public enum FlaskFluid
	{
		Empty,
		AzoticAcid,
		SaltedAcid,
		Vodka
	}
}