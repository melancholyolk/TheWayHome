using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GameState", order = 2)]
public class GameState : ScriptableObject
{
    public enum Chapter
    {
	    None,
	    Chapter0,
	    Chapter1,
	    Chapter2,
    }
	[NonSerialized]
    public Chapter state;
}
