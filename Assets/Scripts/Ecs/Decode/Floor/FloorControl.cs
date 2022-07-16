using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorControl : MonoBehaviour
{
    public Transform ori_pos;

    private int[] num_list = {11, 12, 13, 18, 19, 24};

    private int next_num;

    private int cur = 0;

    private bool used = false;

    public GameObject obj;

    void Start()
    {
        next_num = num_list[0];
    }


    public void AddNum(int num)
    {
        if (used)
        {
            return;
        }

        if (num == next_num)
        {
            if (num == 24)
            {
                used = true;
                cur = 0;
                List<string> dialog = new List<string>();
                dialog.Add("好像有什么被打开了");
                GameObject.FindWithTag("Canvas").SendMessage("ShowDialog", dialog);
                GameObject.Find("TaskLoader").SendMessage("CompleteTask", "[1-1-2]");
                obj.transform.position -= new Vector3(0, 0, 20);
                obj.GetComponent<AudioSource>().Play();
                return;
            }

            cur++;
            next_num = num_list[cur];
        }
        else
        {
            cur = 0;
            next_num = num_list[0];
            CanvasManager.Instance.player.transform.position = ori_pos.position;
        }
    }
}