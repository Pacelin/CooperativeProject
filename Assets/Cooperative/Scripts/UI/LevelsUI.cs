using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelsUI : Menu
{
	public LevelInfo[] Levels => _levels.OrderBy(level => level.SceneBuildIndex).ToArray();

	[Header("Menus")]
	[SerializeField] private MenuUI _mainMenu;
	[SerializeField] private LevelInfoUI _levelInfoMenu;
	[Header("Elements")]
	[SerializeField] private TextButton _backButton;
	[SerializeField] private RectTransform _levelsList;
	[Space]
	[SerializeField] private LevelButton _levelButtonPrefab;
	[Space]
	[SerializeField] private LevelInfo[] _levels;

	private List<LevelButton> _levelButtons;

	private void Awake()
	{
		_levelButtons = new List<LevelButton>();
		var levels = Levels;
		var availableLevelBuildIndex = levels[0].SceneBuildIndex;
		if (PlayerPrefs.HasKey("last_level"))
			availableLevelBuildIndex = PlayerPrefs.GetInt("last_level");
		
		var availableLevels = levels.Where(level => level.SceneBuildIndex <= availableLevelBuildIndex);
		foreach (var level in availableLevels)
		{
			var levelButton = Instantiate(_levelButtonPrefab, Vector3.zero, Quaternion.identity, _levelsList);
			_levelButtons.Add(levelButton);
			levelButton.SetInfo(level, _levelButtons.Count);
		}
	}

	private void OnEnable()
	{
		_backButton.OnClick += OnBackClick;
		foreach(var levelButton in _levelButtons)
			levelButton.OnClick += OnLevelClicked;
	}

    private void OnDisable()
	{
		_backButton.OnClick -= OnBackClick;
		foreach(var levelButton in _levelButtons)
			levelButton.OnClick -= OnLevelClicked;
	}

    private void OnLevelClicked(ImageButton button)
    {
		var levelButton = (LevelButton) button;
		_levelInfoMenu.ShowInfo(levelButton.Info);
    }

	private void OnBackClick()
	{
		_levelInfoMenu.Close();
		SwitchTo(_mainMenu);
	}
}