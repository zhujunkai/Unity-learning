using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonUI : MonoBehaviour {
	public GameObject UGUIbar;
	public GameObject IMGUIbar;
	private Rect Button1;
	private Rect Button2;
	// Use this for initialization
	void Start () {
		Button1 = new Rect(30, 50, 60, 20);
		Button2 = new Rect(130, 50, 60, 20);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	private void OnGUI()
	{
		if (GUI.Button(Button1, "加血"))
		{
			UGUIbar.GetComponent<HealthBar>().Add(10);
			IMGUIbar.GetComponent<IMGUIBar>().Add(10);
		}
		if (GUI.Button(Button2, "减血"))
		{
			UGUIbar.GetComponent<HealthBar>().Reduce(10);
			IMGUIbar.GetComponent<IMGUIBar>().Reduce(10);
		}
	}
}
