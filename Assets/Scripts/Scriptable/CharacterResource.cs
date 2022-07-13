using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CharacterResource", order = 1)]
public class CharacterResource : ScriptableObject
{
	public List<Sprite> characters;
}
