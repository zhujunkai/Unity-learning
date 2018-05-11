using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUserAction
{
	void MovePlayer(float x, float z);
}

public class UserGUI : MonoBehaviour {

	private IUserAction action;

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
}
