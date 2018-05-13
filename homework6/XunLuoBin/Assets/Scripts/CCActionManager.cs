using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCActionManager : SSActionManager, ISSActionCallback
{
	public SenceController sceneController;
	int count = 0;
	private GoPatrolAction go_patrol;                            //巡逻兵巡逻
	private PatrolFollowAction follow_patrol;
	GameEventManager my_eventmanager;

	// Use this for initialization  
	protected void Start()
	{
		sceneController = (SenceController)SSDirector.getInstance().currentScenceController;
		my_eventmanager = sceneController.my_eventmanager;
	}

	// Update is called once per frame  
	public void Update()
	{
		base.Update();
	}

	public void SSActionEvent(SSAction source,int intParam = 0, GameObject objectParam = null)
	{
		if (intParam == 0)
		{
			//侦查兵跟随玩家
			PatrolFollowAction follow = PatrolFollowAction.GetSSAction();
			this.RunAction(objectParam, follow, this);
		}
		else
		{
			//侦察兵按照初始位置开始继续巡逻
			GoPatrolAction move = GoPatrolAction.GetSSAction();
			this.RunAction(objectParam, move, this);
			//玩家逃脱
			my_eventmanager.PlayerEscape();
		}
	}

	public void GoPatrol(GameObject patrol)
	{
		go_patrol = GoPatrolAction.GetSSAction();
		//follow_patrol = PatrolFollowAction.GetSSAction();
		this.RunAction(patrol, go_patrol, this);
		//this.RunAction(patrol, follow_patrol, this);
	}
	//停止所有动作
	public void DestroyAllAction()
	{
		DestroyAll();
	}
}
