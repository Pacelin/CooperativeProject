using UnityEngine;

namespace Pacelin
{
	public class FloorMovingFX : MonoBehaviour
	{
		[SerializeField] private float _stepLength;
		[SerializeField] [MinMaxSlider(5, 20)] private Vector2Int _crispStepsRange; 
		[Space]
		[SerializeField] private AudioSource _stepAudioSource;
		[SerializeField] private AudioSource _floorCrispAudioSource;
		[SerializeField] private AudioClip[] _crispSounds;

		private Rigidbody _rb;
		private float _currentStepLength;
		private int _currentStepsCount;

		private int _currentCrispStepsCount;

		private void Start()
		{
			_rb = Player.FPSController.GetComponent<Rigidbody>();
			_currentCrispStepsCount = Random.Range(_crispStepsRange.x, _crispStepsRange.y + 1);
		}

		private void FixedUpdate()
		{
			if (_rb.velocity.magnitude == 0) return;

			_currentStepLength += _rb.velocity.magnitude * Time.deltaTime;

			if (_currentStepLength >= _stepLength)
			{
				_currentStepLength = 0;
				_stepAudioSource.Play();
				_currentStepsCount++;

				if (_currentStepsCount >= _currentCrispStepsCount)
				{
					_floorCrispAudioSource.clip = _crispSounds[Random.Range(0, _crispSounds.Length)];
					_floorCrispAudioSource.Play();
					_currentCrispStepsCount = Random.Range(_crispStepsRange.x, _crispStepsRange.y + 1);
					_currentStepsCount = 0;
				}
			}
		}
	}
}
