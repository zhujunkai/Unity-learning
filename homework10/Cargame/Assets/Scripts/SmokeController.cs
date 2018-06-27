using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class SmokeController : MonoBehaviour
{
	public float engineRevs;            // 引擎负荷
	public float exhaustRate;           // 尾气变化率
	public float damage;                // 车辆损坏情况
	public GameObject car;              // 车辆
	public CarController carController; // 车辆控制器
	public CarCollide carCollide;       // 车辆碰撞检测

	ParticleSystem exhaust;             // 尾气粒子系统

	void Start()
	{
		exhaust = GetComponent<ParticleSystem>();
		car = this.transform.parent.parent.gameObject;
		carController = car.GetComponent<CarController>();
		carCollide = car.gameObject.GetComponent<CarCollide>();
		exhaustRate = 5000;
		exhaust.Play();
	}

	void Update()
	{
		engineRevs = carController.Revs;
		damage = carCollide.damage;
		// 动态修改 emissionRate。引擎负荷越大，粒子产生率越大。
		exhaust.emissionRate = Mathf.Pow(engineRevs, 5) * exhaustRate + 30;
		Debug.Log(damage.ToString());
		if (damage > 50)
		{
			exhaust.startColor = new Color(0.3f,0.2f,1,0.2f);
		}
		else
		{
			float temp = (damage-1) / 50;
			exhaust.startColor = new Color(1-0.7f*temp, 1-0.8f*temp,1,0.2f);
		}
	}
}
