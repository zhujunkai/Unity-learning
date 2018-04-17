using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUserAction
{
	void StartGame();//开始游戏   
	void ReLoad();//重新开始游戏  
	void NextRound();//重新开始游戏  
	bool next { get; set; }
}

public class UserGUI : MonoBehaviour
{
	private IUserAction action;
	// Use this for initialization  
	void Start()
	{
		action = SSDirector.getInstance().currentScenceController as IUserAction;
	}
	void OnGUI()
	{
		GUIStyle fontstyle1 = new GUIStyle();
		fontstyle1.fontSize = 50;
		fontstyle1.normal.textColor = new Color(255, 255, 255);
		if (GUI.Button(new Rect(60, 60, 120, 40), "STARTGAME"))
		{
			action.StartGame();
		}
		if (GUI.Button(new Rect(200, 60, 120, 40), "RELOAD"))
		{
			action.ReLoad();
		}
		if (action.next)
		{
			if (GUI.Button(new Rect(200,180, 120, 40), "NextRound"))
			{
				action.NextRound();
			}
		}
	}
	// Update is called once per frame  
	void Update()
	{
		//  
	}
}