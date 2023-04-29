using System.Collections;
using System.Collections.Generic;
using Microsoft.SqlServer.Server;
using UnityEngine;

namespace Pacelin
{
	public class Trigger : MonoBehaviour
	{
        public bool Enabled { get => _enabled; set => _enabled = value; }

		[Header("Trigger Settings")]
		[SerializeField] private bool _enabled = true;
		[SerializeField] private bool _triggerOnce = true;
		[SerializeField] private TriggerAction[] _actions;

		protected void Notify()
		{
			if (!Enabled) return;
			if (_triggerOnce) Enabled = false;

			foreach (var action in _actions)
				action.OnTrigger();
		}
	}
}
