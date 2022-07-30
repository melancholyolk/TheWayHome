using System;
public static class GameState
{
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
