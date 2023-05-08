using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : Menu
{
	[Header("Menus")]
	[SerializeField] private SettingsUI _settingsMenu;
	[SerializeField] private LevelsUI _levelsMenu;

	[Header("Buttons")]
	[SerializeField] private TextButton _playButton;
	[SerializeField] private TextButton _levelsButton;
	[SerializeField] private TextButton _settingsButton;
	[SerializeField] private TextButton _exitButton;
	[Space]
	[SerializeField] private int _firstSceneBuildIndex;

	private void OnEnable()
	{
		_playButton.OnClick += OnPlayClick;
		_levelsButton.OnClick += OnChooseLevelClick;
		_settingsButton.OnClick += OnSettingsClick;
		_exitButton.OnClick += OnExitClick;
	}

    private void OnDisable()
	{
		_playButton.OnClick -= OnPlayClick;
		_levelsButton.OnClick -= OnChooseLevelClick;
		_settingsButton.OnClick -= OnSettingsClick;
		_exitButton.OnClick -= OnExitClick;
	}

    private void OnPlayClick()
    {
		var level = _levelsMenu.Levels.FirstOrDefault(level => level.SceneBuildIndex == _firstSceneBuildIndex); 
		if (level == null)
		{
			SceneManager.LoadScene(_firstSceneBuildIndex, LoadSceneMode.Single);
		}
		else
		{
			ScenesLoader.FirstSceneBuildIndex = _levelsMenu.Levels.First().SceneBuildIndex;
			SceneManager.LoadScene("Main Scene", LoadSceneMode.Single);
		}
    }   
    
	private void OnChooseLevelClick() => SwitchTo(_levelsMenu);
	private void OnSettingsClick() => SwitchTo(_settingsMenu);
	private void OnExitClick() => Application.Quit();
}