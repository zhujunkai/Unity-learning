using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCSequenceAction : SSAction,ISSActionCallback {
	public List<SSAction> sequece;
	public int repeat = -1;
	public int start = 0;

	public static  CCSequenceAction GetSSAction(int repeat,int start,List<SSAction> sequece)
	{
		CCSequenceAction action = ScriptableObject.CreateInstance<CCSequenceAction>();
		action.repeat = repeat;
		action.start = start;
		action.sequece = sequece;
		return action;
	}

	public void SSActionEvent(SSAction source, int intParam = 0, GameObject objectParam = null)
	{
		source.destroy = false;
		this.start++;
		if (this.start > sequece.Count)
		{
			this.start = 0;
			if (repeat > 0) repeat--;
			if (repeat == 0) {
				this.destroy = true;
				this.callback.SSActionEvent(this);
			}
		}
		else
		{
			sequece[start].Start();
		}
	}
	// Use this for initialization
	public override void Start () {
		foreach(SSAction action in sequece)
		{
			action.gameobject = this.gameobject;
			action.transform = this.transform;
			action.callback = this;
			action.Start();
		}
		start = 0;
		sequece[0].Start();
	}

	// Update is called once per frame
	public override void Update () {
		if (sequece.Count == 0) return;
		if (start < sequece.Count)
		{
			sequece[start].Update();
		}
	}

	void OnDestory()
	{

	}
}
