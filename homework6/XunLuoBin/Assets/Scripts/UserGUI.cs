using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUserAction
{
	void MovePlayer(float x, float z);
	bool GetGameover();
	int GetScore();
	void Restart();
}

public class UserGUI : MonoBehaviour {

	private IUserAction action;
	private GUIStyle text_style = new GUIStyle();
	// Use this for initialization
	void Start () {
		action = SSDirector.getInstance().currentScenceController as IUserAction;
	}
	
	// Update is called once per frame
	void Update () {
		//获取方向键的偏移量
		float translation = -Input.GetAxis("Vertical");
		float rotation =  -Input.GetAxis("Horizontal");
		action.MovePlayer(translation, rotation);
	}
	private void OnGUI()
	{
		GUI.Label(new Rect(10, 5, 200, 50), "分数:", text_style);
		GUI.Label(new Rect(55, 5, 200, 50), action.GetScore().ToString(), text_style);
		if (action.GetGameover())
		{
			GUI.Label(new Rect(Screen.width / 2 - 50, Screen.width / 2 - 250, 100, 100), "游戏结束", text_style);
			if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.width / 2 - 150, 100, 50), "重新开始"))
			{
				action.Restart();
				return;
			}
		}
	}
}
