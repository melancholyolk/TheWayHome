using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 更改房间设置
/// </summary>
public class WallChange : MonoBehaviour
{
    public Material hide_material;
    public Material show_material;

    public GameObject[] props;
    public GameObject[] show_wall;
    public GameObject[] hide_wall;
    public GameObject ground;
    public SpriteRenderer sprite;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            foreach (GameObject obj in props)
            {
                obj.layer = 7;
            }
            foreach (GameObject obj in hide_wall)
            {
                obj.GetComponent<MeshRenderer>().material = hide_material;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            foreach (GameObject obj in props)
            {
                obj.layer = 10;
            }
            foreach (GameObject obj in hide_wall)
            {
                obj.GetComponent<MeshRenderer>().material = show_material;
            }
        }
    }

    IEnumerator FadeOut()
    {
        float rate = 1;
        float time = 0;
        while(rate > 0)
        {
            time += Time.deltaTime * 3;
            rate = Mathf.Lerp(1,0,time);
            sprite.color = new Color(0,0,0,rate);
            yield return new WaitForSeconds(Time.deltaTime);
        }

    }

    IEnumerator FadeIn()
    {
        float rate = 0;
        float time = 0;
        while (rate < 1)
        {
            time += Time.deltaTime*3;
            rate = Mathf.Lerp(0, 1, time);
            sprite.color = new Color(0, 0, 0, rate);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
