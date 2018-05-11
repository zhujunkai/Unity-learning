using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCollider : MonoBehaviour {

	public int sign = 0;
	SenceController sceneController;
	private void Start()
	{
	}
	void OnTriggerEnter(Collider collider)
	{
		//标记玩家进入自己的区域
		if (collider.gameObject.tag == "Player")
		{
			sceneController = SSDirector.getInstance().currentScenceController as SenceController;
			sceneController.wall_sign= sign;
		}
	}
}
