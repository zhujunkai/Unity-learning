using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreRecorder : MonoBehaviour
{
    public SenceController sceneController;
    public int score = 0;                            //分数

    // Use this for initialization
    void Start()
    {
        sceneController = (SenceController)SSDirector.getInstance().currentScenceController;
        sceneController.recorder = this;
    }
    public int GetScore()
    {
        return score;
    }
    public void AddScore()
    {
        score++;
    }
}

