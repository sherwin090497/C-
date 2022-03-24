using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score_Player : MonoBehaviour
{
    public static Score_Player instance;
    public Text text;
    int score;
    int finalScore;

    Animator Star_Animation;
    const string One_Star = "One";
    const string Two_Star = "Two";
    const string Three_Star = "Three";

    void Start()
    {
        Star_Animation = GetComponent<Animator>();
        if (instance == null)
        {
            instance = this;
        }
    }

    public void ChangeScore(int value)
    {
        score += value;
        text.text = "Score:  " + score.ToString();
    }

    void Update()
    {
        score = Player_Movement.points;
        text.text = "Score:  " + score.ToString();

        if (score <= 80)
        {
            Star_Animation.SetTrigger(One_Star);
        }
        else if (score > 80 && score < 140)
        {
            Star_Animation.SetTrigger(Two_Star);
        }
        else
        {
            Star_Animation.SetTrigger(Three_Star);
        }

        score = 0;
    }

}
