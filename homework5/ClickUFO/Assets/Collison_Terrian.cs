using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collison_Terrian : MonoBehaviour {

	// Use this for initialization
	public GameObject t_Disk;
	public DiskFactory t_factory;
	private void Start()
	{
		t_factory = DiskFactory.getInstance();
	}
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Disk")
		{
			t_Disk = collision.gameObject;
			t_factory.freeDisk(t_Disk);
		}
	}
}
