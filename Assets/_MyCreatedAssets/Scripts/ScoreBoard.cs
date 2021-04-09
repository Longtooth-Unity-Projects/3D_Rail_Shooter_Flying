using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    private int playerScore = 0;

    //cached references
    TMP_Text scoreText;

    private void Start()
    {
        scoreText = GetComponent<TMP_Text>();
        scoreText.text = playerScore.ToString();
    }

    public void adjustScore(int amountToAdjust)
    {
        playerScore += amountToAdjust;
        scoreText.text = playerScore.ToString();
    }
}
