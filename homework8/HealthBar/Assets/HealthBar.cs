using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
	public Slider HPslider;    //添加血条Slider的引用
	public float HP;
	// Use this for initialization
	void Start () {
		HPslider.value = HPslider.maxValue = HP;
	}
	public void Add(float num)
	{
		if (HP + num < HPslider.maxValue)
		{
			HP += num;
		}
		else
		{
			HP = HPslider.maxValue;
		}
		HPslider.value = HP;
	}
	public void Reduce(float num)
	{
		if (HP - num > 0)
		{
			HP -= num;
		}
		else
		{
			HP = 0;
		}
		HPslider.value = HP;
	}
}
