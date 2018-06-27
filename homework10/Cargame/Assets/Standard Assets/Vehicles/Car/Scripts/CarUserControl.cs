using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof (CarController))]
    public class CarUserControl : NetworkBehaviour
	{
        private CarController m_Car; // the car controller we want to use
		public Transform m_cannonRot;
		public Transform m_muzzle;
		public GameObject m_shotPrefab;
		int count;
		[SyncVar]
		bool canFire;

		private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController>();
			count = 0;
			canFire = true;
        }


        private void FixedUpdate()
        {
			if (!isLocalPlayer)
				return;
			if (count >= 100)
			{
				count = 0;
				canFire = true;
			}
			if (!canFire)
			{
				count++;
			}
			if (Input.GetKey(KeyCode.Z))
			{
				m_cannonRot.transform.Rotate(Vector3.up, -Time.deltaTime * 100f);
			}
			if (Input.GetKey(KeyCode.X))
			{
				m_cannonRot.transform.Rotate(Vector3.up, Time.deltaTime * 100f);
			}
			if (Input.GetKeyDown(KeyCode.Space))
			{
				if (canFire)
				{
					canFire = false;
					CmdFire();
				}
			}
			// pass the input to the car!
			float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
#if !MOBILE_INPUT
            float handbrake = CrossPlatformInputManager.GetAxis("Jump");
            m_Car.Move(h, v, v, handbrake);
#else
            m_Car.Move(h, v, v, 0f);
#endif
        }
		[Command]
		void CmdFire()
		{
			GameObject go = GameObject.Instantiate(m_shotPrefab, m_muzzle.position+transform.forward*5+transform.up*-1, m_muzzle.rotation) as GameObject;
			go.GetComponent<Rigidbody>().velocity = gameObject.GetComponent<Rigidbody>().velocity+transform.forward * 40;
			NetworkServer.Spawn(go);
			GameObject.Destroy(go, 3f);
		}

	}
}
