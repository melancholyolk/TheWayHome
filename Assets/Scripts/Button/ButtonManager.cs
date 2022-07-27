using Decode;
using Mirror;
using UnityEngine;
using UnityEngine.Video;

public class ButtonManager : NetworkBehaviour
{
	[SyncVar] 
	public bool btn_isdown = false;
	public int chose_num = 0;
	public GameObject btn;
	public GameObject[] btns;
	public GameObject canvas;
	public GameObject bg;
	public GameState gameState;
	// Update is called once per frame
	void Update()
	{
		if (btn_isdown)
		{
			btn_isdown = false;
			gameState.state = GameState.Chapter.Chapter0;
			Invoke("DestoryChild", 1);
		}
	}

	public void ButtonDown()
	{
		chose_num++;
		JudgeState();
	}

	public void ButtonUp()
	{
		chose_num--;
		JudgeState();
	}

	private void JudgeState()
	{
		if (chose_num == 2 && isServer)
		{
			btn.SetActive(true);
		}
		else
		{
			btn.SetActive(false);
		}
	}

	public void CmdBtnClick()
	{
		btn_isdown = true;
	}

	
	private void DestoryChild()
	{
		Destroy(bg);
		btn.SetActive(false);
		foreach (GameObject obj in GameObject.FindGameObjectsWithTag("PlayerChose"))
		{
			obj.GetComponent<PlayerChose>().is_ready = true;
		}

		foreach (GameObject obj in btns)
		{
			Destroy(obj);
		}
		
		Destroy(this.gameObject);
	}
}