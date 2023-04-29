using UnityEngine;

namespace Pacelin
{
	[CreateAssetMenu(fileName = "New Tint", menuName = "Tint")]
	public class Tint : ScriptableObject
	{
		[Multiline] public string Text;
		public float Duration;
	}
}