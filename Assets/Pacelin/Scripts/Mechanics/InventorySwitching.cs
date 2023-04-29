using System.Collections.Generic;
using UnityEngine;

namespace Pacelin
{
    public class InventorySwitching : Inventory
    {
    	public Pickup HoldedPickup => 
			_currentPickupIndex >= 0 && _currentPickupIndex < _pickups.Count ?
			_pickups[_currentPickupIndex] : null;

		[SerializeField] private int _size;
		[SerializeField] private KeyCode _switchKey = KeyCode.Tab;
    	[SerializeField] private KeyCode _dropKey = KeyCode.Q;
    	[SerializeField] private Tint _switchTint;

		private List<Pickup> _pickups = new();
		private List<LayerMask> _layers = new();

        public override bool IsFull => _pickups.Count >= _size;
        public override bool IsEmpty => _pickups.Count == 0;

    	private Transform _handPoint;
    	private FixedJoint _joint;
    	private int _currentPickupIndex;
    	private bool _tintShowed = false;

		private void Start()
		{
			_handPoint = Player.Inventory.HandPoint;
			_joint = Player.Inventory.Joint;
			_currentPickupIndex = 0;
		}

		private void Update()
		{
			if (Input.GetKeyDown(_switchKey))
				Switch();

			if (HoldedPickup == null) return;

			if (Input.GetKeyDown(_dropKey))
				HoldedPickup.OnDrop(this);
		}

		public void PlaceInHand(Pickup pickup)
		{
			pickup.transform.position = _handPoint.TransformPoint(pickup.PickupInHandOffset);
			pickup.transform.rotation = _handPoint.rotation * Quaternion.Euler(pickup.PickupInHandRotation);
			_joint.connectedBody = pickup.Rigidbody;
		}

		public void SetPickupInHand(int index)
		{
			if (HoldedPickup != null)
				HoldedPickup.gameObject.SetActive(false);
			
			_currentPickupIndex = index;
			if (HoldedPickup != null)
			{
				HoldedPickup.gameObject.SetActive(true);
				PlaceInHand(HoldedPickup);
			}
		}

		public void Switch()
		{
			if (_pickups.Count == 1) return;
			var next = _pickups.Count == 0 ? 0 : (_currentPickupIndex + 1) % _pickups.Count;
			SetPickupInHand(next);
		}

        public override void AddPickup(Pickup pickup)
        {
			if (IsFull) return;

			_pickups.Add(pickup);
			_layers.Add(pickup.gameObject.layer);

			pickup.gameObject.layer = LayerMask.NameToLayer("Pickup");
			SetPickupInHand(_pickups.Count - 1);

			if (_tintShowed) return;
			if (_pickups.Count > 1)
			{
				TintView.Instance.ShowTint(_switchTint);
				_tintShowed = true;
			}
        }

        public override void Clear() { }

        public override bool ContainsPickup(Pickup pickup) => HoldedPickup == pickup;
        public override Pickup GetPickup(int index) => HoldedPickup;

        public override void RemovePickup(Pickup pickup)
        {
			if (HoldedPickup != pickup) return;
        
			HoldedPickup.gameObject.layer = _layers[_currentPickupIndex];
			_pickups.RemoveAt(_currentPickupIndex);
			_layers.RemoveAt(_currentPickupIndex);
        	_joint.connectedBody = null;

			SetPickupInHand(_pickups.Count - 1);
        }

        public override void RemovePickup(int index)
        {
			HoldedPickup.gameObject.layer = _layers[_currentPickupIndex];
			_pickups.RemoveAt(_currentPickupIndex);
			_layers.RemoveAt(_currentPickupIndex);
        	_joint.connectedBody = null;

			SetPickupInHand(_pickups.Count - 1);
        }
    }
}
