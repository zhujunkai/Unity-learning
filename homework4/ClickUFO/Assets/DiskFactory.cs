using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DiskFactory : System.Object
{
	private static DiskFactory _instance;
	public SceneController sceneControler { get; set; }
	public List<GameObject> used;
	public List<GameObject> free;
	// Use this for initialization  

	public static DiskFactory getInstance()
	{
		if (_instance == null)
		{
			_instance = new DiskFactory();
			_instance.used = new List<GameObject>();
			_instance.free = new List<GameObject>();
		}
		return _instance;
	}
	public GameObject getDisk(int round)
	{
		GameObject newDisk;
		if (free.Count == 0)
		{
			newDisk = GameObject.Instantiate(Resources.Load("prefabs/Disk1")) as GameObject;
			Debug.Log("111");
		}
		else
		{
			newDisk = free[0];
			newDisk.SetActive(true);
			free.Remove(free[0]);
		}
		switch (sceneControler.num%3)
		//根据轮数制定飞碟的颜色和大小  
		{
			case 0:
				newDisk.transform.localScale = DiskData.size[0];
				newDisk.GetComponent<Renderer>().material.color = DiskData.color[0];
				newDisk.GetComponent<SphereCollider>().radius = 0.6f;
				break;
			case 1:
				newDisk.transform.localScale = DiskData.size[1];
				newDisk.GetComponent<Renderer>().material.color = DiskData.color[1];
				newDisk.GetComponent<SphereCollider>().radius = 0.5f;
				break;
			case 2:
				newDisk.transform.localScale = DiskData.size[2];
				newDisk.GetComponent<Renderer>().material.color = DiskData.color[2];
				newDisk.GetComponent<SphereCollider>().radius = 0.4f;
				break;
		}
		used.Add(newDisk);
		return newDisk;
	}
	public void freeDisk(GameObject disk1)
	{
		for (int i = 0; i < used.Count; i++)
		{
			if (used[i] == disk1)
			{
				used.Remove(disk1);
				free.Add(disk1);
			}
		}
		return;
	}
	public void free_all()
	{
		while (used.Count != 0)
		{
			GameObject a = used[0];
			a.SetActive(false);
			free.Add(a);
			used.Remove(a);
		}
	}
	public void remove_all()
	{
		while (used.Count != 0)
		{
			GameObject a = used[0];
			used.Remove(a);
		}
		while (free.Count != 0)
		{
			GameObject a = free[0];
			free.Remove(a);
		}
	}
}