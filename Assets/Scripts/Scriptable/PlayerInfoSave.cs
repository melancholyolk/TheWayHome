using UnityEngine;

namespace Scriptable
{
	[CreateAssetMenu(fileName = "PlayerSave", menuName = "ScriptableObjects/PlayerInfoSave", order = 0)]
	public class PlayerInfoSave : ScriptableObject
	{
		[SerializeField]
		public PlayerInfo playerInfo;
	}
}