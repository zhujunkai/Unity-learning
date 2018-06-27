using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Vehicles.Car;

public class Bullet : NetworkBehaviour
{

	void OnCollisionEnter(Collision collision)
	{
		var hit = collision.gameObject;
		var hitPlayer = hit.GetComponent<CarUserControl>();
		if (hitPlayer != null)
		{
			var combat = hit.GetComponent<Combat>();
			combat.TakeDamage(30);

		}
		Destroy(gameObject);
	}
}
