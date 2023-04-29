using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pacelin
{
	public class BreakableLightbulb : TriggerAction
	{
		[SerializeField] private Light _light;
		[SerializeField] private GameObject _emissionGameObject;
		[SerializeField] private ParticleSystem _explosionParticles;
		[SerializeField] private AudioSource _explosionAudio;
		private bool _exploded = false;

		public void Explode()
		{
			if (_exploded) return;
			_light.enabled = false;
			Destroy(_emissionGameObject);
			_explosionParticles.Play();
			_explosionAudio.Play();
		}

        protected override IEnumerator ApplyAction()
        {
            Explode();
			yield break;
        }
    }
}
