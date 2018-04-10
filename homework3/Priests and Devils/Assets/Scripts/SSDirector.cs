using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Mygame;
namespace Com.Mygame { 
	public interface IUserAction
	{
		void Left_button(int place);
		void Right_button(int place);
		void Left_Boat_button(int place);
		void Right_Boat_button(int place);
		void Boat_Go();
		void GameOver();
		void Restart();
		int is_win { get; set; }
	}
	public interface ISceneController
	{
		void LoadResources();
		void Pause();
		void Resume();
	}
	public class SSDirector : System.Object {

		private static SSDirector _instance;
		public ISceneController currentSceneController { get; set; }
		public bool running { get; set; }
		public static SSDirector getInstance()
		{
			if (_instance == null)
			{
				_instance = new SSDirector();
			}
			return _instance;
		}
	}
}
