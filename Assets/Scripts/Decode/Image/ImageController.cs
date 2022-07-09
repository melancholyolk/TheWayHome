using System.Collections;
using System.Collections.Generic;
using Decode;
using UnityEngine;

namespace Decode
{
	public class ImageController : DecodeWay
    {
        public ImageFrame[] frames;
        public int cur_num = 0;
        public int count = 0;
    	
        public AudioClip open;
    
        [Header("KeyInput")]
        public KeyCode left;
    
        public KeyCode right;
    
        public KeyCode rotate;
        // Start is called before the first frame update
        void Start()
        {
            frames[cur_num].FrameChoosed(true);
        }
    
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(left))
            {
                if (cur_num < frames.Length - 1)
                {
                    frames[cur_num].FrameChoosed(false);
                    cur_num++;
                    frames[cur_num].FrameChoosed(true);
                }
            }
    
            if (Input.GetKeyDown(right))
            {
                if (cur_num > 0)
                {
                    frames[cur_num].FrameChoosed(false);
                    cur_num--;
                    frames[cur_num].FrameChoosed(true);
                }
            }
    
            if (Input.GetKeyDown(rotate))
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
                OnComplete();
                GetComponent<AudioSource>().PlayOneShot(open);
                Destroy(gameObject, 2);
            }
        }

        public override void OnComplete()
        {
	        Debug.Log("解密完成");
        }
    }
	
}
