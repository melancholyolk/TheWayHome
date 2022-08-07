using UnityEngine;
using UnityEngine.Serialization;

namespace Scriptable
{
	[CreateAssetMenu(fileName = "GameSave", menuName = "ScriptableObjects/GameSave", order = 0)]
	public class GameInfoSave : ScriptableObject
	{
		[SerializeField]
		public PlayerInfo playerInfo1;
		[SerializeField]
		public PlayerInfo playerInfo2;
		[SerializeField]
		public GameState.Chapter process;
	}
}