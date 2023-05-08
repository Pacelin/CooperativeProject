using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelInfoUI : Menu
{
	[SerializeField] private ImageButton _playButton;
	[SerializeField] private TMP_Text _levelNameText;
	[SerializeField] private TMP_Text _authorsText;
	[SerializeField] private TMP_Text _descriptionText;

	private LevelInfo _info;

	private void OnEnable()
	{
		_playButton.OnClick += OnPlayClick;
	}

    private void OnDisable()
	{
		_playButton.OnClick -= OnPlayClick;
	}

	public void ShowInfo(LevelInfo info)
	{
		if (Opened && _info == info) return;

		_info = info;
		StartCoroutine(ShowInfo());
	}

	private IEnumerator ShowInfo()
	{
		if (Opened)
		{
			Close();
			yield return new WaitForSeconds(_closeTime);
		}

		_levelNameText.text = _info.LevelName;
		_authorsText.text = _info.Authors;
		_descriptionText.text = _info.Description;
		Open();
	}

    private void OnPlayClick(ImageButton button)
    {
		if (_info == null || !Opened) return;
		ScenesLoader.FirstSceneBuildIndex = _info.SceneBuildIndex;
		SceneManager.LoadScene("Main Scene", LoadSceneMode.Single);
    }
}