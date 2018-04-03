using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

	// Use this for initialization
	public Transform origin;
	public float speed=20 ;
	float ry, rz;

	void Start()
	{
		ry = Random.Range(1, 360);
		rz = Random.Range(1, 360);
	}

	void Update()
	{
		Vector3 axis = new Vector3(0, ry, rz);
		this.transform.RotateAround(origin.position, axis, speed * Time.deltaTime);
	}
}
