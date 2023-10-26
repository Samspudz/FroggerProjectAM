using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject frogPlayer;
    FroggerScript frogScript;
    public Transform startPoint;

    public int gameScore = 0;
    public int playerLives = 3;
    public TMP_Text scoreCount;
    public TMP_Text lifeCount;
    public TMP_Text finalScoreCount;
    public ScoringScript[] sZone;

    public Image timebar;
    public float timeCount;
    public int padCount = 5;
    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject finalScorePanel;

    private void Awake()
    {
        frogScript = GameObject.FindGameObjectWithTag("Player").GetComponent<FroggerScript>();

        winPanel.SetActive(false);
        losePanel.SetActive(false);
        finalScorePanel.SetActive(false);
        gameScore = 0;
        playerLives = 3;
        padCount = 5;
        timeCount = 30;
    }

    void Update()
    {
        scoreCount.text = gameScore.ToString();
        lifeCount.text = playerLives.ToString();
        finalScoreCount.text = gameScore.ToString();

        timeCount -= Time.deltaTime;
        timebar.fillAmount = timeCount / 30;

        if (frogScript == null)
            return;

        if (timeCount <= 0 && !frogScript.isDead)
        {

            frogScript.TimeOut();
        }
    }

    public void NewFrog()
    {
        if (playerLives > 0 && padCount > 0)
        {
            foreach (ScoringScript z in sZone)
            {
                z.zoneTrigger = false;
            }

            GameObject g = Instantiate(frogPlayer, startPoint.position, startPoint.rotation);
            timeCount = 30;
            frogScript = g.GetComponent<FroggerScript>();
            
        }
        else
        {
            if (playerLives <= 0)
            {
                LoseScreen();
            }
            else
            {
                WinScreen();
            }
        }
    }

    public void WinScreen()
    {
        winPanel.SetActive(true);
        finalScorePanel.SetActive(true);
        StartCoroutine(MainMenu());
    }   
    
    public void LoseScreen()
    {
        losePanel.SetActive(true);
        finalScorePanel.SetActive(true);
        StartCoroutine(MainMenu());
    }

    IEnumerator MainMenu()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(0);
    }
}
