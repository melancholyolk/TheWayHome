using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageController : MonoBehaviour
{
    public ImageFrame[] frames;
    public int cur_num = 0;
    public int count = 0;

    public AudioClip open;

    // Start is called before the first frame update
    void Start()
    {
        frames[cur_num].FrameChoosed(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (cur_num < frames.Length - 1)
            {
                frames[cur_num].FrameChoosed(false);
                cur_num++;
                frames[cur_num].FrameChoosed(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (cur_num > 0)
            {
                frames[cur_num].FrameChoosed(false);
                cur_num--;
                frames[cur_num].FrameChoosed(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            frames[cur_num].FrameRotate();
        }
    }

    public void Judge()
    {
        count = 0;
        for (int i = 0; i < frames.Length; i++)
        {
            if (frames[i].is_right)
            {
                count++;
            }
        }

        if (count == frames.Length)
        {
            this.transform.parent.SendMessage("Complete");
            transform.parent.GetComponent<AudioSource>().PlayOneShot(open);
            Destroy(gameObject, 2);
        }
    }
}