using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsUI : Menu
{
	[Header("Menus")]
	[SerializeField] private MenuUI _mainMenu;
	[Header("Elements")]
	[SerializeField] private TextButton _backButton;
	[Space]
	[SerializeField] private Slider _masterSlider;
	[SerializeField] private Slider _musicSlider;
	[SerializeField] private Slider _soundsSlider;
	[Space]
	[SerializeField] private AudioMixer _mixer;

	private void Awake()
	{
		_mixer.GetFloat("master_volume", out var master);
		_mixer.GetFloat("music_volume", out var music);
		_mixer.GetFloat("sounds_volume", out var sounds);
		_masterSlider.SetValueWithoutNotify(master);
		_musicSlider.SetValueWithoutNotify(music);
		_soundsSlider.SetValueWithoutNotify(sounds);
	}

	private void OnEnable()
	{
		_backButton.OnClick += OnBackClicked;
		_masterSlider.onValueChanged.AddListener(OnMasterValueChanged);
		_musicSlider.onValueChanged.AddListener(OnMusicValueChanged);
		_soundsSlider.onValueChanged.AddListener(OnSoundsValueChanged);
	}

    private void OnDisable()
	{
		_backButton.OnClick -= OnBackClicked;
		_masterSlider.onValueChanged.RemoveListener(OnMasterValueChanged);
		_musicSlider.onValueChanged.RemoveListener(OnMusicValueChanged);
		_soundsSlider.onValueChanged.RemoveListener(OnSoundsValueChanged);
	}

	private void OnMasterValueChanged(float value) =>
		_mixer.SetFloat("master_volume", value);
	private void OnMusicValueChanged(float value) =>
		_mixer.SetFloat("music_volume", value);
	private void OnSoundsValueChanged(float value) =>
		_mixer.SetFloat("sounds_volume", value);

    private void OnBackClicked() => SwitchTo(_mainMenu);

}