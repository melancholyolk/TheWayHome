using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ToyControl : MonoBehaviour
{
    private Camera decodeCamera;
    public bool is_chosed = false;
    public ToyManager toy_manager;
    public Vector2 ori_pos;
    public ToyPanel toy_panel;
    public int tag;
    public bool is_complete = false;
    private void Start()
    {
        decodeCamera = GameObject.FindWithTag("DecodeCamera").GetComponent<Camera>();
    }
    // Update is called once per frame
    void Update()
    {
        if (is_complete)
        {
            return;
        }
        if (is_chosed)
        {
            Vector2 Pos = decodeCamera.ScreenToWorldPoint(Input.mousePosition);//转换成世界坐标
            transform.position = new Vector2(Pos.x, Pos.y);
        }
        if (Input.GetMouseButtonUp(0) && is_chosed)
        {
            Judge();
            this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
        }
        if (Input.GetMouseButtonDown(0) && !is_chosed)
        {
            if(Vector2.Distance(decodeCamera.ScreenToWorldPoint(Input.mousePosition),this.transform.position) < 1.5f)
            {
                OnPointDown();
            }
        }
    }
    public void Init(GameObject obj,ToyManager mng,Sprite spr)
    {
        this.GetComponent<SpriteRenderer>().sprite = spr;
        this.GetComponent<SpriteRenderer>().sortingOrder = 1;
        toy_manager = mng;
        toy_panel = obj.GetComponent<ToyPanel>();
        toy_panel.SetUsed(tag);
        this.transform.position = obj.transform.position;
    }
    public void OnPointDown()
    {
        is_chosed = true;
        this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
    }
    private void Judge()
    {
        for(int i = 0;i < toy_manager.toy_point.Length; i++)
        {
            float dis = Vector2.Distance(toy_manager.toy_point[i].position,this.transform.position);
            if(dis < 2f && !toy_manager.toy_point[i].GetComponent<ToyPanel>().is_used)
            {
                toy_panel.SetNotUsed();
                is_chosed = false;
                toy_panel = toy_manager.toy_point[i].GetComponent<ToyPanel>();
                toy_panel.SetUsed(tag);
                this.transform.position = toy_panel.transform.position;
                break;
            }
        }
        is_chosed = false;
        this.transform.position = toy_panel.transform.position;
    }
    public void Clear()
    {
        toy_panel.SetNotUsed();
        Destroy(this.gameObject);
    }
}
