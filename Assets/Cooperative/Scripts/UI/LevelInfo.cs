using UnityEngine;

[CreateAssetMenu(fileName = "New LevelInfo", menuName = "Level Info")]
public class LevelInfo : ScriptableObject
{
	public string LevelName;
	[TextArea(1, 5)] public string Authors;
	[TextArea(3, 7)] public string Description;
	[Space]
	public int SceneBuildIndex;
}