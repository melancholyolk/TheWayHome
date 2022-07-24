using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperationControl
{
    public bool is_picking = false;
    public bool is_setting = false;
    public bool is_decoding = false;
    public bool is_dialog = false;
    public bool ui_isOpening = false;
    public bool is_playingCG = false;

    private static OperationControl _instance;
    public static OperationControl Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new OperationControl();
            }
            return _instance;
        }
    }
    public bool CanOperate()
    {
        if (!is_decoding && !is_picking && !ui_isOpening && !is_setting && !is_playingCG)
        {
            return true;
        }
        return false;
    }
}
