using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Mygame;
public class UserGUI : MonoBehaviour {
	private IUserAction action;
	// Use this for initialization
	void Start () {
		action = SSDirector.getInstance().currentSceneController as IUserAction;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	private void OnGUI()
	{
		float width = Screen.width / 12;
		float hight = Screen.height / 12;
		GUI.Label(new Rect(width * 6 - 50, hight , 150, 50), "方的牧，圆的魔");
		if (action.is_win == 1)
		{
			GUI.Box(new Rect(width * 6 - 150, hight * 2 + 80, 300, 150), "Result");
			GUI.Label(new Rect(width * 6 - 30, hight * 2 + 110, 100, 50), "You Lose!!!");
			if (GUI.Button(new Rect(width * 6 - 50, hight * 2 + 150, 100, 50), "ReStart"))
			{
				action.Restart();
			}
		}
		if (action.is_win == 2)
		{
			GUI.Box(new Rect(width * 6 - 150, hight * 2 + 80, 300, 150), "Result");
			GUI.Label(new Rect(width * 6 - 30, hight * 2 + 110, 100, 50), "You win!!!");
			if (GUI.Button(new Rect(width * 6 - 50, hight * 2 + 150, 100, 50), "ReStart"))
			{
				action.Restart();
			}

		}
		if (action.is_win == 0)
		{
			GUI.backgroundColor = Color.red;
			if (GUI.Button(new Rect(width * 6 - 50, hight * 2, 100, 50), "Go"))
			{
				action.Boat_Go();
			}
		}
		GUI.backgroundColor = Color.clear;
		for (int i = 0; i < 6; i++) {
			if(GUI.Button(new Rect(width*6-268-i*64,hight*6+50,64,50), ""))
			{
				action.Left_button(i);
			}
		}
		for (int i = 0; i < 6; i++)
		{
			if (GUI.Button(new Rect(width * 6 + 210+ i * 64, hight * 6 + 50, 64, 50), ""))
			{
				action.Right_button(i);
			}
		}
		for(int i = 0; i < 2; i++)
		{
			if (GUI.Button(new Rect(width * 6-90 - i * 64, hight * 6 + 75, 64, 50), ""))
			{
				action.Left_Boat_button(i);
			}
		}
		for (int i = 0; i < 2; i++)
		{
			if (GUI.Button(new Rect(width * 6 +30 + i * 64, hight * 6 + 75, 64, 50), ""))
			{
				action.Right_Boat_button(i);
			}
		}
	}
}
