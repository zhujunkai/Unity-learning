using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myFactory : MonoBehaviour
{
    private GameObject my_collider = null;
	private GameObject t_Monster = null;								//巡逻兵
	private List<GameObject> used = new List<GameObject>();        //正在被使用的巡逻兵
    private Vector3[] vec = new Vector3[9];                         //保存每个巡逻兵的初始位置
	GameObject[] Monster = new GameObject[4];
	GameObject[] m_collider = new GameObject[4];


	public SenceController sceneControler;                    //场景控制器

    public List<GameObject> GetMonster()
    {
		sceneControler = SSDirector.getInstance().currentScenceController as SenceController;

		int[] pos_x = { 5, -5 };
        int[] pos_z = { 5, -5 };
        int index = 0;
        //生成不同的巡逻兵初始位置
        for(int i=0;i < 2;i++)
        {
            for(int j=0;j < 2;j++)
            {
				Monster[i + j * 2] = Instantiate<GameObject>(Resources.Load<GameObject>("prefabs/Monster"));
				Monster[i + j * 2].transform.position = new Vector3(pos_x[i], 0.1f, pos_z[j]);
				m_collider[i + j * 2] = Instantiate<GameObject>(Resources.Load<GameObject>("prefabs/m_collider"));
				m_collider[i + j * 2].transform.position = new Vector3(pos_x[i], 0f, pos_z[j]);
				Monster[i + j * 2].AddComponent<MonsterData>();
				Monster[i + j * 2].GetComponent<MonsterData>().sign = i + j * 2;
				Monster[i + j * 2].GetComponent<MonsterData>().wall_sign = i + j * 2;
				Monster[i + j * 2].GetComponent<MonsterData>().player = sceneControler.hero;
				Monster[i + j * 2].GetComponent<MonsterData>().start_position = new Vector3(pos_x[i], 0f, pos_z[j]);
				m_collider[i + j * 2].AddComponent<MCollider>();
				m_collider[i + j * 2].GetComponent<MCollider>().sign = i + j * 2;
			}
		}
        for(int i=0; i < 4; i++)
        {
			t_Monster = Monster[i];
            used.Add(Monster[i]);
        }   
        return used;
    }
    public void StopPatrol()
    {
        //切换所有侦查兵的动画
        for (int i = 0; i < used.Count; i++)
        {
            used[i].gameObject.GetComponent<Animator>().SetBool("run", false);
        }
    }
}
