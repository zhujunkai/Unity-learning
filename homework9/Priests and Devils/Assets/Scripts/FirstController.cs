using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Com.Mygame;
public class FirstController : MonoBehaviour,ISceneController,IUserAction {
	public CCActionManager actionManager;
	public GameObject move1;
	public GameObject move2;
	SSDirector ssDirector;
	GameObject Light;
	GameObject Boat;
	GameObject The_Left_Bank;
	GameObject The_Right_Bank;
	GameObject The_Lose_Canvas;
	GameObject The_Win_Canvas;
	AI my_ai;
	bool AIisControlling;
	int timerecorder;

//以下向量需改
	public GameObject[] Devil_And_Priest = new GameObject[6];//0-2魔鬼和3-5牧师

	bool[] left_bank_people = new bool[6]; 
	bool[] right_bank_people = new bool[6];
	int[] boat_people = new int[2];
	bool boat_is_left;
	public bool moving;
	public bool game_is_running;

	int left_Priest_num;
	int left_Devil_num;
	int right_Priest_num;
	int right_Devil_num;

	public int is_win { get; set; }
	public string Statelabel { get; set; }

	// Use this for initialization

	void Start () {
		ssDirector = SSDirector.getInstance();
		ssDirector.currentSceneController = this;
		ssDirector.currentSceneController.LoadResources();
		actionManager = new CCActionManager();
		actionManager.Start();
		my_ai = new AI();
		AIisControlling = false;
		timerecorder = 0;
		Statelabel = "";
	}
	
	// Update is called once per frame
	void Update () {
		actionManager.Update();
	}
	public void LoadResources()
	{
		Instantiate<GameObject>(Resources.Load<GameObject>("prefabs/River"));
		Light =Instantiate<GameObject>(Resources.Load<GameObject>("prefabs/Directional Light"));
		Boat = Instantiate<GameObject>(Resources.Load<GameObject>("prefabs/Boat"), new Vector3(-1.5f, -1.5f, 0), Quaternion.identity);
		The_Left_Bank= Instantiate<GameObject>(Resources.Load<GameObject>("prefabs/Left_bank"));
		The_Right_Bank= Instantiate<GameObject>(Resources.Load<GameObject>("prefabs/Right_bank"));
		for (int i = 0; i < 3; i++)
		{
			Devil_And_Priest[i+3] = Instantiate<GameObject>(Resources.Load<GameObject>("prefabs/Priest"),new Vector3(-7-i,-0.25f,0),Quaternion.identity);
			Devil_And_Priest[i]= Instantiate<GameObject>(Resources.Load<GameObject>("prefabs/Devil"), new Vector3(-4 - i, -0.25f, 0), Quaternion.identity);
			left_bank_people[i] = true;
			left_bank_people[i + 3] = true;
			right_bank_people[i] = false;
			right_bank_people[i + 3] = false;
		}
		boat_people[0] = -1;
		boat_people[1] = -1;
		boat_is_left = true;
		moving = false;
		game_is_running = true;
		left_Priest_num =3;
		left_Devil_num=3;
		right_Priest_num=0;
		right_Devil_num=0;
		is_win = 0;
	}
	public void Boat_Go()
	{
		if (!moving)
		{
			if (boat_people[0] != -1 || boat_people[1] != -1)
			{
				moving = true;
				Move_boat();
			}
		}
	}
	public void Left_button(int place)
	{
		if (left_bank_people[place]&&boat_is_left && (!moving)&& game_is_running)
		{
			if (boat_people[0] == -1)
			{
				moving = true;
				actionManager.RunAction(Devil_And_Priest[place], actionManager.left_people_to_boat[place], actionManager);
				boat_people[0] = place;
				left_bank_people[place] = false;
			}
			else if(boat_people[1]==-1)
			{
				moving = true;
				actionManager.RunAction(Devil_And_Priest[place], actionManager.left_people_to_boat[place+6], actionManager);
				boat_people[1] = place;
				left_bank_people[place] = false;
			}
		}
	}
	public void Right_button(int place)
	{
		if (right_bank_people[place] && (!boat_is_left)&&(!moving) && game_is_running)
		{
			if (boat_people[0] == -1)
			{
				moving = true;
				actionManager.RunAction(Devil_And_Priest[place], actionManager.right_people_to_boat[place], actionManager);
				boat_people[0] = place;
				right_bank_people[place] = false;
			}
			else if (boat_people[1] == -1)
			{
				moving = true;
				actionManager.RunAction(Devil_And_Priest[place], actionManager.right_people_to_boat[place+6], actionManager);
				boat_people[1] = place;
				right_bank_people[place] = false;
			}
		}
	}
	public void Left_Boat_button(int place)
	{
		if (boat_people[place] != -1&& boat_is_left && (!moving) && game_is_running)
		{
			moving = true;
			int position = boat_people[place];
			actionManager.RunAction(Devil_And_Priest[position], actionManager.boat_people_to_left[position+place*6], actionManager);
			left_bank_people[position] = true;
			boat_people[place] = -1;
		}
	}
	public void Right_Boat_button(int place)
	{
		if (boat_people[1-place] != -1&&(!boat_is_left) && (!moving) && game_is_running)
		{
			moving = true;
			int position = boat_people[1 - place];
			actionManager.RunAction(Devil_And_Priest[position], actionManager.boat_people_to_right[position+(1-place)*6], actionManager);
			right_bank_people[position] = true;
			boat_people[1 - place] = -1;
			if (right_Priest_num == 3 && right_Devil_num == 3&&boat_people[0]==-1&&boat_people[1]==-1)
			{
				Win();
			}
		}
	}
	void Move_boat()
	{
		if (boat_is_left)
		{
			if (boat_people[0] != -1) actionManager.RunAction(Devil_And_Priest[boat_people[0]], actionManager.to_right_boat[0], actionManager);
			if (boat_people[1] != -1) actionManager.RunAction(Devil_And_Priest[boat_people[1]], actionManager.to_right_boat[1], actionManager);
			actionManager.RunAction(Boat, actionManager.boat_moving[1], actionManager);
			boat_is_left = false;
			for(int i = 0; i < 2; i++)
			{
				if (boat_people[i] != -1){
					if (boat_people[i] < 3)
					{
						left_Devil_num--;
						right_Devil_num++;
					}
					else
					{
						left_Priest_num--;
						right_Priest_num++;
					}
				}
			}
			bank_judge();
		}
		else
		{
			if (boat_people[0] != -1) actionManager.RunAction(Devil_And_Priest[boat_people[0]], actionManager.to_left_boat[0], actionManager);
			if (boat_people[1] != -1) actionManager.RunAction(Devil_And_Priest[boat_people[1]], actionManager.to_left_boat[1], actionManager);
			actionManager.RunAction(Boat, actionManager.boat_moving[0], actionManager);
			boat_is_left = true;
			for (int i = 0; i < 2; i++)
			{
				if (boat_people[i] != -1)
				{
					if (boat_people[i] < 3)
					{
						left_Devil_num++;
						right_Devil_num--;
					}
					else
					{
						left_Priest_num++;
						right_Priest_num--;
					}
				}
			}
			bank_judge();
		}
	}
	void bank_judge()
	{
		left_bank_judge();
		right_bank_judge();
	}
	void left_bank_judge()
	{
		if (left_Devil_num > left_Priest_num&&left_Priest_num!=0)
		{
			GameOver();
		}
	}
	void right_bank_judge()
	{
		if (right_Devil_num > right_Priest_num&&right_Priest_num!=0)
		{
			GameOver();
		}
	}
	public void Pause()
	{

	}
	public void Resume()
	{

	}
	void Win()
	{
		is_win = 2;
		game_is_running = false;
	}
	public void GameOver()
	{
		is_win = 1;
		game_is_running = false;
	}
	public void Restart()
	{
		Boat.transform.position = new Vector3(-1.5f, -1.5f, 0);
		for (int i = 0; i < 3; i++)
		{
			Devil_And_Priest[i + 3].transform.position = new Vector3(-7 - i, -0.25f, 0);
			Devil_And_Priest[i].transform.position = new Vector3(-4 - i, -0.25f, 0);
			left_bank_people[i] = true;
			left_bank_people[i + 3] = true;
			right_bank_people[i] = false;
			right_bank_people[i + 3] = false;
		}
		boat_people[0] = -1;
		boat_people[1] = -1;
		boat_is_left = true;
		moving = false;
		left_Priest_num = 3;
		left_Devil_num = 3;
		right_Priest_num = 0;
		right_Devil_num = 0;
		game_is_running = true;
		is_win = 0;
	}
	public void next()
	{
		if (!moving)
		{
			if (boat_people[0] != -1 || boat_people[1] != -1)
			{
				for (int i = 0; i < 2; i++)
				{
					Left_Boat_button(i);
					Right_Boat_button(i);
					if (i == 0) moving = false;
				}
				if (right_Priest_num == 3 && right_Devil_num == 3 && boat_people[0] == -1 && boat_people[1] == -1)
				{
					Win();
				}
			}
			else
			{
				AIState aIState = new AIState(left_Priest_num, left_Devil_num, !boat_is_left);
				LinkedList<AIState> resultList = my_ai.findnextState(aIState);
				int Statecount = 1;
				Statelabel += Statecount+": "+(resultList.First.Value.boatstate?"isRight":"isLeft ")+"  LeftP:"+ resultList.First.Value.lp+"  LeftD:"+ resultList.First.Value.ld+"\n";
				resultList.RemoveFirst();
				AIState result = resultList.First.Value;
				if (result != null)
				{
					if (boat_is_left)
					{
						if (aIState.lp > result.lp)
						{
							for (int i = 0; i < aIState.lp - result.lp; i++)
							{
								for (int j = 0; j < 3; j++)
								{
									if (left_bank_people[3 + j])
									{
										Left_button(3 + j);
										moving = false;
										break;
									}
								}
							}
						}
						if (aIState.ld > result.ld)
						{
							for (int i = 0; i < aIState.ld - result.ld; i++)
							{
								for (int j = 0; j < 3; j++)
								{
									if (left_bank_people[j] && boat_people[1] != j && boat_people[0] != j)
									{
										Left_button(j);
										moving = false;
										break;
									}
								}
							}
						}
					}
					else
					{
						if (aIState.lp < result.lp)
						{
							for (int i = 0; i < result.lp - aIState.lp; i++)
							{
								for (int j = 0; j < 3; j++)
								{
									if (right_bank_people[3 + j])
									{
										Right_button(3 + j);
										moving = false;
										break;
									}
								}
							}
						}
						if (aIState.ld < result.ld)
						{
							for (int i = 0; i < result.ld - aIState.ld; i++)
							{
								for (int j = 0; j < 3; j++)
								{
									if (right_bank_people[j] && boat_people[1] != j && boat_people[0] != j)
									{
										Right_button(j);
										moving = false;
										break;
									}
								}
							}
						}
					}
				}
				moving = true;
				AIisControlling = true;
				timerecorder = 0;
				while (resultList.Count > 0)
				{
					Statecount++;
					Statelabel += Statecount + ": "+(resultList.First.Value.boatstate ? "isRight" : "isLeft ") + "  LeftP:" + resultList.First.Value.lp + "  LeftD:" + resultList.First.Value.ld + "\n";
					resultList.RemoveFirst();
				}
			}	
		}
	}
	void FixedUpdate()
	{
		if (AIisControlling)
		{
			moving = true;
			timerecorder++;
			if (timerecorder >= 50)
			{
				AIisControlling = false;
				moving = false;
				Boat_Go();
			}
		}
	}
}
