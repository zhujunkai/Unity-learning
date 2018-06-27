using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollide : MonoBehaviour {
	public float damage = 1;
	private void OnCollisionEnter(Collision collision)
	{
		// 速度越大，损坏越大
		damage += this.gameObject.GetComponent<Rigidbody>().velocity.magnitude;
	}
}
