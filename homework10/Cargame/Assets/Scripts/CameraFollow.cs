using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CameraFollow : MonoBehaviour
{

	public Transform target;
	public float distanceH = 12f;
	public float distanceV = 6f;
	public float smoothSpeed = 10f;

	// Use this for initialization
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
	}

	void LateUpdate()
	{
		Vector3 nextpos = target.forward * -1 * distanceH + target.up * distanceV + target.position;
		this.transform.position = Vector3.Lerp(this.transform.position, nextpos, smoothSpeed * Time.deltaTime); //平滑插值
		this.transform.LookAt(target);
	}
}
