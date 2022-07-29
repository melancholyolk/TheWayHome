using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class GameState
{
	[Serializable]
    public enum Chapter
    {
	    None,
	    Choosing,
	    Chapter0,
	    Chapter1,
	    Chapter2,
    }

    public static Chapter state = Chapter.None;
}
