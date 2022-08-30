using Decode;
using Mirror;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : NetworkBehaviour
{
	[SyncVar(hook = nameof(GameStart))] 
	public bool btn_isdown = false;
	public int chose_num = 0;
	public Button btn;
	public GameObject[] btns;
	public GameObject canvas;
	public GameObject bg;
	public Animator fadeout;

	void GameStart(bool newbtn,bool oldbtn)
	{
		GameState.state = GameState.Chapter.Chapter0;
		fadeout.enabled = true;
		btn.gameObject.SetActive(false);
		Invoke(nameof(DestoryChild),0.5f);
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
			btn.interactable = true;
		}
		else
		{
			btn.interactable = false;
		}
	}

	public void CmdBtnClick()
	{
		btn_isdown = true;
	}

	
	private void DestoryChild()
	{
		Destroy(bg);
		
		foreach (GameObject obj in GameObject.FindGameObjectsWithTag("PlayerChose"))
		{
			obj.GetComponent<PlayerChose>().is_ready = true;
		}

		foreach (GameObject obj in btns)
		{
			Destroy(obj);
		}
		
		Destroy(gameObject);
	}
}