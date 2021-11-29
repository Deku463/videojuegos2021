using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoretxt;
    public int scorePoint;
    public int scoreToWin = 5;
    public int DeathTime = 60;
    private int DeathTimeCounter = 0;
    public Transform timeBar;
    public GameObject winScreen;
    public GameObject looseScreen;
    public GameObject PauseButton;
    public GameObject PauseMenu;

    public Coroutine TimeToLoose;


    public void PlayerScorePoint(int _coreScore)
    {

        scorePoint += _coreScore;
        scoretxt.text = "X" + scorePoint.ToString();

        if (scorePoint >= scoreToWin)
        {
            win();
        }
    }

    private void Start()
    {
        winScreen.SetActive(false);
        looseScreen.SetActive(false);

        timeBar.localScale = Vector3.one;

        DeathTimeCounter = DeathTime;

        scorePoint = 0;

        scoretxt.text = "X" + scorePoint.ToString();

        TimeToLoose = StartCoroutine(TimeToLooseRoutine());

        PauseButton.SetActive(true);
        PauseMenu.SetActive(false);

        Time.timeScale = 1;

    }

    IEnumerator TimeToLooseRoutine()
    {
        while (DeathTimeCounter > 0) {
            yield return new WaitForSeconds(1);
            DeathTimeCounter--;
            float scale_x = Remap(DeathTimeCounter, 0, DeathTime, 0, 1);
            timeBar.localScale = new Vector3(scale_x,1,1);
        }

        GameOver();

    }

    public void win()
    {

        StopCoroutine(TimeToLoose);

        winScreen.SetActive(true);
        looseScreen.SetActive(false);

        PauseButton.SetActive(false);
        PauseMenu.SetActive(false);

        Time.timeScale = 0;


    }

    public float Remap( float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    public void GameOver() {
        StopCoroutine(TimeToLoose);

        looseScreen.SetActive(true);
        winScreen.SetActive(false);

        PauseButton.SetActive(false);
        PauseMenu.SetActive(false);

        Time.timeScale = 0;

    }



}