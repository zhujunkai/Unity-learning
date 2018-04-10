using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Mygame;

public class CCActionManager : SSActionManager,ISSActionCallback {
	public FirstController sceneController;
	private float object_speed;
	private float boat_speed;

	Vector3[] left_bank_position = new Vector3[6];
	Vector3[] right_bank_position = new Vector3[6];
	Vector3[] left_bank_up = new Vector3[6];
	Vector3[] right_bank_up = new Vector3[6];

	Vector3[] left_boat_up = new Vector3[2];
	Vector3[] right_boat_up = new Vector3[2];
	Vector3[] left_boat = new Vector3[2];
	Vector3[] right_boat = new Vector3[2];
	Vector3[] the_boat = new Vector3[2];

	public CCMoveToAction[] left_to_boat = new CCMoveToAction[2];
	public CCMoveToAction[] to_left_boat = new CCMoveToAction[2];
	public CCMoveToAction[] boat_to_left = new CCMoveToAction[6];
	public CCMoveToAction[] boat_to_right = new CCMoveToAction[6];
	public CCMoveToAction[] to_right_boat = new CCMoveToAction[2];
	public CCMoveToAction[] right_to_boat = new CCMoveToAction[2];
	public CCMoveToAction[] boat_moving = new CCMoveToAction[2];

	public CCMoveToAction[] left_up = new CCMoveToAction[6];
	public CCMoveToAction[] right_up = new CCMoveToAction[6];
	public CCMoveToAction[] left_to_boat_up = new CCMoveToAction[2];
	public CCMoveToAction[] right_to_boat_up = new CCMoveToAction[2];

	public CCSequenceAction[] left_people_to_boat = new CCSequenceAction[12];
	public CCSequenceAction[] right_people_to_boat = new CCSequenceAction[12];
	public CCSequenceAction[] boat_people_to_left = new CCSequenceAction[12];
	public CCSequenceAction[] boat_people_to_right = new CCSequenceAction[12];
	// Use this for initialization
	public new void Start () {
		for (int i = 0; i < 6; i++)
		{
			left_bank_position[i] = new Vector3(-4 - i, -0.25f, 0);
			right_bank_position[i] = new Vector3(4 + i, -0.25f, 0);
			left_bank_up[i] = new Vector3(-4 - i, 1, 0);
			right_bank_up[i] = new Vector3(4 + i, 1, 0);
		}
		for (int i = 0; i < 2; i++)
		{
			left_boat[i] = new Vector3(-1 - i, -0.75f, 0);
			right_boat[1 - i] = new Vector3(1 + i, -0.75f, 0);
			left_boat_up[i] = new Vector3(-1 - i, 1, 0);
			right_boat_up[1 - i] = new Vector3(1 + i, 1, 0);
		}
		the_boat[0] = new Vector3(-1.5f, -1.5f, 0);
		the_boat[1] = new Vector3(1.5f, -1.5f, 0);
		object_speed = 10.0f;
		boat_speed = 4.0f;
		sceneController = (FirstController)SSDirector.getInstance().currentSceneController;
		sceneController.actionManager = this;
		for(int i = 0; i < 2; i++)
		{
			left_to_boat[i] = CCMoveToAction.GetSSAction(left_boat[i], object_speed);
			right_to_boat[i] = CCMoveToAction.GetSSAction(right_boat[i], object_speed);
			to_left_boat[i] = CCMoveToAction.GetSSAction(left_boat[i], boat_speed);
			to_right_boat[i] = CCMoveToAction.GetSSAction(right_boat[i], boat_speed);
			boat_moving[i] = CCMoveToAction.GetSSAction(the_boat[i], boat_speed);
			left_to_boat_up[i]= CCMoveToAction.GetSSAction(left_boat_up[i], object_speed);
			right_to_boat_up[i]= CCMoveToAction.GetSSAction(right_boat_up[i], object_speed);
		}
		for(int i = 0; i < 6; i++)
		{
			boat_to_left[i]= CCMoveToAction.GetSSAction(left_bank_position[i], object_speed);
			boat_to_right[i] = CCMoveToAction.GetSSAction(right_bank_position[i], object_speed);
			left_up[i]= CCMoveToAction.GetSSAction(left_bank_up[i], object_speed);
			right_up[i] = CCMoveToAction.GetSSAction(right_bank_up[i], object_speed);
			left_people_to_boat[i] = CCSequenceAction.GetSSAction(0,0,new List<SSAction> { left_up[i], left_to_boat_up[0],left_to_boat[0]});
			left_people_to_boat[i+6] = CCSequenceAction.GetSSAction(0, 0, new List<SSAction> { left_up[i], left_to_boat_up[1], left_to_boat[1] });
			right_people_to_boat[i] = CCSequenceAction.GetSSAction(0, 0, new List<SSAction> { right_up[i], right_to_boat_up[0], right_to_boat[0] });
			right_people_to_boat[i + 6] = CCSequenceAction.GetSSAction(0, 0, new List<SSAction> { right_up[i], right_to_boat_up[1], right_to_boat[1] });
			boat_people_to_left[i] = CCSequenceAction.GetSSAction(0, 0, new List<SSAction> { left_to_boat_up[0], left_up[i],boat_to_left[i] });
			boat_people_to_left[i+6] = CCSequenceAction.GetSSAction(0, 0, new List<SSAction> { left_to_boat_up[1], left_up[i], boat_to_left[i] });
			boat_people_to_right[i] = CCSequenceAction.GetSSAction(0, 0, new List<SSAction> { right_to_boat_up[0], right_up[i], boat_to_right[i] });
			boat_people_to_right[i + 6] = CCSequenceAction.GetSSAction(0, 0, new List<SSAction> { right_to_boat_up[1], right_up[i], boat_to_right[i] });
		}
	}
	
	// Update is called once per frame
	public new void Update () {
		base.Update();
	}
	#region ISSActionCallback implementation
	public void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Competeted, int intParam = 0, string strParam = null, Object objectParam = null)
	{
		Debug.Log("change back Game_state");
		sceneController.game_is_running = true;
	}
	#endregion
}
