using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolFollowAction : SSAction
{
    private float speed = 2f;            //跟随玩家的速度
    private GameObject player;           //玩家
    private MonsterData data;             //侦查兵数据
	private SenceController my_scene;

	private PatrolFollowAction() { }
    public static PatrolFollowAction GetSSAction()
    {
        PatrolFollowAction action = CreateInstance<PatrolFollowAction>();
		action.my_scene= SSDirector.getInstance().currentScenceController as SenceController;
		action.player = action.my_scene.hero;
        return action;
    }

    public override void Update()
    {
        if (transform.localEulerAngles.x != 0 || transform.localEulerAngles.z != 0)
        {
            transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
        }
        if (transform.position.y != 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
         
        Follow();
        //如果侦察兵没有跟随对象，或者需要跟随的玩家不在侦查兵的区域内
        if ( data.wall_sign != my_scene.wall_sign)
        {
            this.destroy = true;
            this.callback.SSActionEvent(this,1,this.gameobject);
        }
    }
    public override void Start()
    {
        data = this.gameobject.GetComponent<MonsterData>();
		this.gameobject.GetComponent<Animator>().SetBool("isrun", true);
	}
    void Follow()
    {
        transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        this.transform.LookAt(player.transform.position);
		if(Vector3.Distance(transform.position, player.transform.position) < 1)
		{
			my_scene.Gameover();
		}
    }
}
