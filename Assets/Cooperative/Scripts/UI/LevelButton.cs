using UnityEngine;
using TMPro;

public class LevelButton : ImageButton
{
    public LevelInfo Info => _info;

    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _numberText;

    private LevelInfo _info;

    public void SetInfo(LevelInfo info, int number)
    {
        _info = info;
        _nameText.text = info.LevelName;
        _numberText.text = number.ToString();
    }
}