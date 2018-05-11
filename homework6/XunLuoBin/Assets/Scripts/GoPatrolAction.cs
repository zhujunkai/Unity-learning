using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoPatrolAction : SSAction
{
	private enum Dirction { EAST, NORTH, WEST, SOUTH };
	private float pos_x, pos_z;                 //移动前的初始x和z方向坐标
	private float move_speed = 1.2f;            //移动速度
	private bool move_sign = true;              //是否到达目的地
	private Dirction dirction = Dirction.EAST;  //移动的方向
	private MonsterData data;                    //巡逻兵的数据
	private Vector3 monster_start;
	private SenceController my_scene;



	private GoPatrolAction() { }
	public static GoPatrolAction GetSSAction()
	{
		GoPatrolAction action = CreateInstance<GoPatrolAction>();
		action.pos_x = 0;
		action.pos_z = 0;
		return action;
	}
	public override void Update()
	{
		//防止碰撞发生后的旋转
		if (transform.localEulerAngles.x != 0 || transform.localEulerAngles.z != 0)
		{
			transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
		}
		if (transform.position.y != 0)
		{
			transform.position = new Vector3(transform.position.x, 0, transform.position.z);
		}
		//巡逻兵移动
		Gopatrol();
		//如果巡逻兵需要跟随玩家并且玩家就在侦察兵所在的区域，侦查动作结束
		if ( data.wall_sign == my_scene.wall_sign)
		{
			this.destroy = true;
			this.callback.SSActionEvent(this, 0, this.gameobject);
		}
	}
	public override void Start()
	{
		data = this.gameobject.GetComponent<MonsterData>();
		monster_start = data.start_position;
		dirction =(Dirction)((int)Random.Range(0, 4));
		my_scene = SSDirector.getInstance().currentScenceController as SenceController;
		this.gameobject.GetComponent<Animator>().SetBool("isrun", false);
	}

	void Gopatrol()
	{
		if (move_sign)
		{
			//不需要转向则设定一个目的地，按照矩形移动
			switch (dirction)
			{
				case Dirction.EAST:
					pos_x = monster_start.x + Random.Range(2, 5);
					pos_z = monster_start.z + Random.Range(2, 5);
					break;
				case Dirction.NORTH:
					pos_x = monster_start.x - Random.Range(2, 5);
					pos_z = monster_start.z + Random.Range(2, 5);
					break;
				case Dirction.WEST:
					pos_x = monster_start.x + Random.Range(2, 5);
					pos_z = monster_start.z - Random.Range(2, 5);
					break;
				case Dirction.SOUTH:
					pos_x = monster_start.x - Random.Range(2, 5);
					pos_z = monster_start.z - Random.Range(2, 5);
					break;
			}
			move_sign = false;
		}
		this.transform.LookAt(new Vector3(pos_x, 0, pos_z));
		float distance = Vector3.Distance(transform.position, new Vector3(pos_x, 0, pos_z));
		//当前位置与目的地距离浮点数的比较
		if (distance > 0.9)
		{
			transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(pos_x, 0, pos_z), move_speed * Time.deltaTime);
		}
		else
		{
			dirction = dirction + 1;
			if (dirction > Dirction.SOUTH)
			{
				dirction = Dirction.EAST;
			}
			move_sign = true;
		}
	}
}
