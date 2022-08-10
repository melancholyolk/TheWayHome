using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    private static InputManager _instance;
    public static InputManager Instance{
        get{
            if(_instance == null)
            {
                _instance = new InputManager();
			}
            return _instance;
		}
        set{
            Debug.LogError("²»ÄÜÐÞ¸Ä");
		}
    }
    public enum KeyBoard
    {
        Interact,
        Decode,
        CloseDecode,
        OpenProp,
        OpenBook,
        TurnForward,
        TurnBackward,
        MoveUp,
        MoveDown,
        MoveRight,
        MoveLeft
	}
    private Dictionary<KeyBoard, KeyCode> key;
    public KeyCode GetKeycode(KeyBoard kb)
    {
        return key[kb];
	}
}
