using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class physicsManager : SSActionManager, ISSActionCallback, IActionManager{
	public SceneController sceneController;//场记  
	public DiskFactory diskFactory;//游戏工厂   
	public Emit EmitDisk;
	public GameObject Disk;
	int count = 0;
	// Use this for initialization
	void Start () {
		sceneController = (SceneController)SSDirector.getInstance().currentScenceController;
		diskFactory = DiskFactory.getInstance();
		sceneController.t_phy = this;//设置动作管理器  
	}

	// Update is called once per frame
	public void Update () {
		base.Update();
	}
	public void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Competeted,int intParam = 0, string strParam = null, Object objectParam = null)              //回调函数回收飞碟记录是否失分  
	{
		
	}
	public void playDisk()
	{
		EmitDisk = Emit.GetSSAction();
		Disk = diskFactory.getDisk(sceneController.round);
		this.RunAction(Disk, EmitDisk, this);
	}
}
