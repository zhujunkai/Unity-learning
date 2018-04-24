using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emit : SSAction {

	bool enableEmit = true;//使力作用一次，不想产生变加速运动  
	Vector3 force;//力  
	float startX;//起始位置  
	float startY;//起始位置 
	public SceneController sceneControler = (SceneController)SSDirector.getInstance().currentScenceController;//引入场记  
																											  // Use this for initialization  
	public override void Start()
	{
		startX = 10 - Random.value * 20; 
		startY = 5 - Random.value * 5;
		this.transform.position = new Vector3(startX, startY, 0);
		force = new Vector3(6 * Random.Range(-1, 1), 3 * Random.Range(0, 0.5f), 30 + 5 * sceneControler.round);//根据轮数设置力的大小  
	}
	public static Emit GetSSAction()
	{
		Emit action = ScriptableObject.CreateInstance<Emit>();
		return action;
	}

	public override void Update()
	{
		if (!this.destroy)
		{
			if (enableEmit)
			{
				gameobject.GetComponent<Rigidbody>().velocity = Vector3.zero;
				gameobject.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
				enableEmit = false;
			}
		} 
	}

	public void Destory()//回调函数  
	{
		this.destroy = true;
		this.callback.SSActionEvent(this);
	}
	// Update is called once per frame  
}
