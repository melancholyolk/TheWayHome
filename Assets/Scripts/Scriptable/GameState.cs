using System;
using Scriptable;

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
    public static GameInfoSave currentSave;
}
