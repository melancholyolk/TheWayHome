using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
/// <summary>
/// 通过滚轮输入
/// 挂在锁上
/// 通过结束
/// </summary>
public class RollInput : MyInput
{
    public Transform[] rolls;
    public string name;
    public AudioClip open;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SendAnswer();
        }
    }


    protected override void SendAnswer()
    {
        string rest = "";
        foreach (var VARIABLE in rolls)
        {
            float angle = VARIABLE.GetComponent<MouseRollControl>().currentAngel;
            print(angle);
            angle %= 360;
            if (angle > 180)
                angle -= 360;
            if (angle < -180)
                angle += 360;
            rest += angle + ";";
        }

        if (transform.parent.GetComponent<DeCode>().Check(rest))
        {
            StartCoroutine(Success());
        }
    }

    /// <summary>
    /// 解密成功后调用
    /// </summary>
    protected override IEnumerator Success()
    {
        if (GetComponent<Animator>() != null)
            GetComponent<Animator>().enabled = true;

        gameObject.GetComponent<AudioSource>().PlayOneShot(open);
        foreach (var VARIABLE in rolls)
        {
            VARIABLE.GetComponent<Collider>().enabled = false;
        }
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}