﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCActionManager : SSActionManager, ISSActionCallback,IActionManager
{
	public SceneController sceneController;
	public DiskFactory diskFactory;
	public Emit2 EmitDisk;
	public GameObject Disk;
	int count = 0;
	// Use this for initialization  
	protected void Start()
	{
		sceneController = (SceneController)SSDirector.getInstance().currentScenceController;
		diskFactory = DiskFactory.getInstance();
		sceneController.t_CCA = this;
	}

	// Update is called once per frame  
	public void Update()
	{
		base.Update();
	}
	public void playDisk()
	{
		EmitDisk = Emit2.GetSSAction();
		Disk = diskFactory.getDisk(sceneController.round);
		this.RunAction(Disk, EmitDisk, this);
	}

	public void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Competeted,
		int intParam = 0, string strParam = null, Object objectParam = null)
	{
		//  
	}
}