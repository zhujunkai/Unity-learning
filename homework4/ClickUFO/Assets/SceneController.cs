using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour, ISceneController, IUserAction
{
	public SSActionManager actionManager { get; set; }
	public int round = 0;//轮数  
	public float Score = 0;//分数  
	public Text ScoreText;//分数文本  
	public Text RoundText;//轮数文本  
	public Text GameText;//倒计时文本  
	public Text FinalText;//结束文本  
	public Text TargetText;
	public bool next { get; set; }
	public int game = 0;//记录游戏进行情况  
	public int num = 0;//每轮的飞碟数量  
	GameObject disk;
	GameObject explosion;
	public int CoolTimes = 3; //准备时间  
							  // Use this for initialization  
	void Awake()
	//创建导演实例并载入资源  
	{
		SSDirector director = SSDirector.getInstance();
		DiskFactory DF = DiskFactory.getInstance();
		DF.sceneControler = this;
		director.setFPS(60);
		director.currentScenceController = this;
		director.currentScenceController.LoadResources();
	}
	void Start()
	{
		round = 1;
		next = false;
	}
	public void LoadResources()
	{
		//explosion = Instantiate(Resources.Load("prefabs/Explosion"), new Vector3(-40, 0, 0), Quaternion.identity) as GameObject;
	}
	void Update()
	{
		TargetText.text = "Target:" + 7;
		ScoreText.text = "Score:" + Score.ToString();
		RoundText.text = "Round:" + round.ToString();
		if (Input.GetMouseButtonDown(0) && game == 1)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
				if (hit.transform.tag == "Disk")
				{
					//explosion.transform.position = hit.collider.gameObject.transform.position;
					//explosion.GetComponent<Renderer>().material = hit.collider.gameObject.GetComponent<Renderer>().material;
					//explosion.GetComponent<ParticleSystem>().Play();
					hit.collider.gameObject.SetActive(false);
					print("Hit!!!");
					if (hit.collider.gameObject.transform.localScale==DiskData.size[0]) Score += 1;
					else if (hit.collider.gameObject.transform.localScale == DiskData.size[1]) Score += 2;
					else Score += 3;
				}
			}
		}
		if (num == 11 && Score >=  7)
		//每轮总共发射10个，如果得分达到一定要求进入下一轮，否则GameOver  
		{
			if (round == 3)
			{
				game = 2;
				Win();
			}
			else
			{
				round++;
				game = 0;
				next = true;
				num = 0;
			}
		}
		else if (num == 11 && Score < 7&&game==1)
		{
			game = 2;//游戏结束  
			GameOver();
		}
	}
	public IEnumerator waitForOneSecond(int i)
	{
		GameText.text = "Round "+i;
		yield return new WaitForSeconds(1);
		while (CoolTimes >= 0 && game == 3)
		{
			GameText.text = CoolTimes.ToString();
			print("还剩" + CoolTimes);
			yield return new WaitForSeconds(1);
			CoolTimes--;
		}
		GameText.text = "Start";
		yield return new WaitForSeconds(1);
		GameText.text = "";
		game = 1;//游戏开始  
	}
	public void GameOver()
	{
		FinalText.text = "Game Over!!!";
	}
	public void Win()
	{
		FinalText.text = "Win!!!";
	}
	public void StartGame()
	{
		round = 1;
		num = 0;
		next = false;
		if (game == 0)
		{
			game = 3;//进入倒计时状态  
			StartCoroutine(waitForOneSecond(1));
		}
	}
	public void ReLoad()
	{
		DiskFactory.getInstance().remove_all();
		SceneManager.LoadScene("things");
		game = 0;
	}
	public void NextRound()
	{
		num = 0;
		DiskFactory.getInstance().free_all();
		next = false;
		Score = 0;
		if (game == 0)
		{
			game = 3;//进入倒计时状态  
			StartCoroutine(waitForOneSecond(round));
		}
	}
}
