using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pacelin
{
	public class DrawerCombination : MonoBehaviour
	{
		[SerializeField] private Drawer[] _drawers;
		[SerializeField] private int[] _combination;
		[SerializeField] private GameObject _activateObject;

		private int _progress;

		private void OnEnable()
		{
			foreach(var drawer in _drawers)
				drawer.OnOpenedChanged += OnDrawerChanged;
		}

		private void OnDisable()
		{
			foreach(var drawer in _drawers)
				drawer.OnOpenedChanged -= OnDrawerChanged;
		}

		private void OnDrawerChanged(Drawer drawer, bool open)
		{
			if (!open)
			{
				_progress = 0;
				return;
			}

			if (drawer == _drawers[_combination[_progress]])
				_progress++;
			else
				_progress = 0;
			
			if (_progress >= _combination.Length)
			{
				_activateObject.SetActive(true);
				Destroy(this);
			}
		}
	}
}
