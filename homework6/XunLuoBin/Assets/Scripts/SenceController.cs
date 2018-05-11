using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SenceController : MonoBehaviour, ISceneController, IUserAction
{
	SSDirector ssDirector;
	public GameObject hero;
	bool game_over;
	int[] m_positionx = { 5, -5 };
	int[] m_positionz = { 5, -5 };
	GameObject[] m_collider = new GameObject[4];
	List<GameObject> Monster = new List<GameObject>();
	public CCActionManager action_manager;
	public int wall_sign;
	public myFactory my_factory;
	public ScoreRecorder recorder;

	// Use this for initialization
	void Start () {
		ssDirector = SSDirector.getInstance();
		ssDirector.currentScenceController = this;
		action_manager = gameObject.AddComponent<CCActionManager>() as CCActionManager;
		ssDirector.currentScenceController.LoadResources();
		game_over = false;
		wall_sign = 0;
		recorder = new ScoreRecorder();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void LoadResources()
	{
		hero = Instantiate<GameObject>(Resources.Load<GameObject>("prefabs/hero"));
		my_factory = new myFactory();
		Monster = my_factory.GetMonster();
		for (int i = 0; i < Monster.Count; i++)
		{
			action_manager.GoPatrol(Monster[i]);
		}
	}
	public void MovePlayer(float translation, float rotation)
	{
		if (!game_over)
		{
			if (translation != 0 || rotation != 0)
			{
				hero.GetComponent<Animator>().SetBool("IsRun", true);
			}
			else
			{
				hero.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
				hero.GetComponent<Animator>().SetBool("IsRun", false);
			}
			//移动和旋转
			Vector3 movement=new Vector3();
			movement.Set(rotation, 0f, translation);
			movement = movement.normalized * 35 * Time.deltaTime;
			hero.transform.LookAt(movement + hero.transform.position);
			hero.GetComponent<Rigidbody>().AddForce(movement,ForceMode.Impulse);
			//hero.transform.position=movement + hero.transform.position;
			//hero.transform.Translate(0, 0, translation * 2 * Time.deltaTime);
			//hero.transform.Rotate(0, rotation * 180 * Time.deltaTime, 0);
			//防止碰撞带来的移动
			if (hero.transform.localEulerAngles.x != 0 || hero.transform.localEulerAngles.z != 0)
			{
				hero.transform.localEulerAngles = new Vector3(0, hero.transform.localEulerAngles.y, 0);
			}
			if (hero.transform.position.y != 0)
			{
				hero.transform.position = new Vector3(hero.transform.position.x, 0, hero.transform.position.z);
			}
		}
	}
	void OnEnable()
	{
		GameEventManager.ScoreChange += AddScore;
		GameEventManager.GameoverChange += Gameover;
		//GameEventManager.CrystalChange += ReduceCrystalNumber;
	}
	void OnDisable()
	{
		GameEventManager.ScoreChange -= AddScore;
		GameEventManager.GameoverChange -= Gameover;
		//GameEventManager.CrystalChange -= ReduceCrystalNumber;
	}
	public void Gameover()
	{
		game_over = true;
		for(int i = 0; i < Monster.Count; i++)
		{
			Monster[i].GetComponent<Animator>().SetBool("isrun", false);
			Monster[i].GetComponent<Animator>().SetBool("iswalk", false);
		}
		action_manager.DestroyAllAction();
	}
	public bool GetGameover()
	{
		return game_over;
	}
	public void AddScore()
	{
		recorder.AddScore();
	}
	public int GetScore()
	{
		return recorder.GetScore();
	}
	public void Restart()
	{
		SceneManager.LoadScene("Resources/Scenes/mySence");
	}

}
