using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IMGUIBar : MonoBehaviour {
	private Rect t_position;
	public float MaxHP = 100.0f;
	public float HP = 100.0f;
	// Use this for initialization
	void Start () {
		t_position=new Rect(20, 20, 200, 20);
		if (HP > MaxHP) HP = MaxHP;
		if (HP < 0) HP = 0;
	}
	private void OnGUI()
	{
		GUI.HorizontalScrollbar(t_position,0.0f,HP,0f,MaxHP);
	}
	public void Add(float num)
	{
		if (HP + num < MaxHP)
		{
			HP += num;
		}
		else
		{
			HP = MaxHP;
		}
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
	}
}
