﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Sprite[] lives;
    public Image livesImageDisplay;

    public int score;
    public Text scoreText;

    public GameObject newGameImage;

    public void UpdateLives(int currentLives)
    {
        livesImageDisplay.sprite = lives[currentLives];
    }

    public void UpdateScore()
    {
        score += 10;
        scoreText.text = "Score: " + score;
    }

    public void hideNewGameImage()
    {
        //hacemos desaparecer la imagen principal
        newGameImage.SetActive(false);
        scoreText.text = "Score: 0";
    }
    public void showNewGameImage()
    {
        //hacemos desaparecer la imagen principal
        newGameImage.SetActive(true);
    }

}
