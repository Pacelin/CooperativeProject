using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pacelin
{
	public abstract class TriggerAction : MonoBehaviour
	{
        public bool Enabled { get => _enabled; set => _enabled = value; }

		[Header("Action Settings")]
        [SerializeField] private bool _enabled = true;
		[SerializeField] private float _delay = 0;
        [SerializeField] private bool _actionOnce = true;

        private bool _locked;

        public void OnTrigger() 
        {
            if (!Enabled || _locked) return;
            StartCoroutine(TriggerCoroutine());
        }

        private IEnumerator TriggerCoroutine()
        {
            _locked = true;
            yield return new WaitForSeconds(_delay);
            
            yield return ApplyAction();
            
            if (_actionOnce) Enabled = false;
            _locked = false;
        }

        protected abstract IEnumerator ApplyAction();
	}
}
