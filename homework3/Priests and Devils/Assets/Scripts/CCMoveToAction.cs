using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Mygame;

public class CCMoveToAction : SSAction {
	public Vector3 target;
	public float speed;
	// Use this for initialization
	public static CCMoveToAction GetSSAction(Vector3 target,float speed){
		CCMoveToAction action = ScriptableObject.CreateInstance<CCMoveToAction>();
		action.target = target;
		action.speed = speed;
		return action;
	}
	public override void Start () {
		Debug.Log("MoveToAction, target is " + target);
	}
	
	// Update is called once per frame
	public override void Update () {
		this.transform.position = Vector3.MoveTowards(this.transform.position, target,speed*Time.deltaTime);
		if (this.transform.position == target)
		{
			FirstController temp = (FirstController)SSDirector.getInstance().currentSceneController;
			temp.moving = false;
			this.destory = true;
			this.callback.SSActionEvent(this);
		}
	}
}
