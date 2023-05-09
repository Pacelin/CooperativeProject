using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Pacelin
{
	public class PauseUI : MonoBehaviour
	{
		[SerializeField] private float _openTime;
		[SerializeField] private float _closeTime;
		[SerializeField] private KeyCode _pauseKey = KeyCode.Escape;
		[Space]
		[SerializeField] private CanvasGroup _group;
		[Space]
		[SerializeField] private TextButton _returnButton;
		[SerializeField] private TextButton _mainMenuButton;
		[Space]
		[SerializeField] private Slider _masterSlider;
		[SerializeField] private Slider _musicSlider;
		[SerializeField] private Slider _soundsSlider;
		[Space]
		[SerializeField] private AudioMixer _mixer;
		
		private float _lastTimeScale;
		private bool _opened;
		private bool _lock;
		private CursorLockMode _lastCursorMode;
		private bool _lastCursorVisibility;

		private void Awake()
		{
			_mixer.GetFloat("master_volume", out var master);
			_mixer.GetFloat("music_volume", out var music);
			_mixer.GetFloat("sounds_volume", out var sounds);
			_masterSlider.SetValueWithoutNotify(master);
			_musicSlider.SetValueWithoutNotify(music);
			_soundsSlider.SetValueWithoutNotify(sounds);
			_opened = false;
			_lock = false;
		}

		private void OnEnable()
		{
			_returnButton.OnClick += OnReturnClick;
			_mainMenuButton.OnClick += OnMainMenuClick;
			_masterSlider.onValueChanged.AddListener(OnMasterValueChanged);
			_musicSlider.onValueChanged.AddListener(OnMusicValueChanged);
			_soundsSlider.onValueChanged.AddListener(OnSoundsValueChanged);
		}

		private void OnDisable()
		{
			_returnButton.OnClick -= OnReturnClick;
			_mainMenuButton.OnClick -= OnMainMenuClick;
			_masterSlider.onValueChanged.RemoveListener(OnMasterValueChanged);
			_musicSlider.onValueChanged.RemoveListener(OnMusicValueChanged);
			_soundsSlider.onValueChanged.RemoveListener(OnSoundsValueChanged);
		}

		private void Update()
		{
			if (_lock) return;
			if (Input.GetKeyDown(_pauseKey))
			{
				if (_opened)
					StartCoroutine(Close());
				else
					StartCoroutine(Open());
			}
		}

		private void OnMasterValueChanged(float value) =>
			_mixer.SetFloat("master_volume", value);
		private void OnMusicValueChanged(float value) =>
			_mixer.SetFloat("music_volume", value);
		private void OnSoundsValueChanged(float value) =>
			_mixer.SetFloat("sounds_volume", value);

		private void OnReturnClick()
		{
			if (_lock) return;
			StartCoroutine(Close());
		}

		private void OnMainMenuClick()
		{
			Time.timeScale = _lastTimeScale;
			SceneManager.LoadScene(0, LoadSceneMode.Single);
		}

		private IEnumerator Close()
		{
			_lock = true;
			Cursor.lockState = _lastCursorMode;
			Cursor.visible = _lastCursorVisibility;
			for (float t = 0; t < _closeTime; t += Time.unscaledDeltaTime)
			{
				_group.alpha = Mathf.Lerp(1, 0, t / _closeTime);
				yield return null;
			}
			_group.alpha = 0;
			Time.timeScale = _lastTimeScale;
			_group.interactable = false;
			_group.blocksRaycasts = false;
			_opened = false;
			_lock = false;
			Player.FPSController.enabled = true;
		}

		private IEnumerator Open()
		{
			_lock = true;
			_lastTimeScale = Time.timeScale;
			Time.timeScale = 0; 
			_lastCursorMode = Cursor.lockState;
			_lastCursorVisibility = Cursor.visible;
			
			for (float t = 0; t < _openTime; t += Time.unscaledDeltaTime)
			{
				_group.alpha = Mathf.Lerp(0, 1, t / _closeTime);
				yield return null;
			}
			_group.alpha = 1;
			_group.interactable = true;
			_group.blocksRaycasts = true;
			_opened = true;
			_lock = false;
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			Player.FPSController.enabled = false;
		}
	}
}
