﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI : MonoBehaviour {
    public Text[] textfields;
    public GameObject[] boxes;
    public GameObject[] scoreMeterStars;
    public new Camera camera;
    public GameObject GameOver;
    public GameObject MainMenuButton;
    public GameObject startMenu;

    GameMaster GM;
    Abilitys AM;
    public GameObject ContinueGameButton;
    public GameObject Settings;
    public GameObject TutorialButton;
    public GameObject Tutorial1;
    public GameObject NextTutButton;
    private int Tut;
    private int totalScore;
    public GameObject Tutorial2;
    public GameObject SettingsButton;
    public GameObject scoreMeterBlue;
    public GameObject scoreMeterRed;
    public GameObject[] GameOverStars;

    private float scalefactor = (1.17854f - 0.01006653f) / 300;
    private float posfactor = (0.0819f - 0.08251023f) / 300;

    // Use this for initialization
    void Start () {
        //textfields = transform.GetComponentsInChildren<Text>();
        GM = GameObject.Find("Game Master").GetComponent<GameMaster>();
        AM = GameObject.Find("Game Master").GetComponent<Abilitys>();
        boxes = GM.boxes.ToArray();
        GameOver.SetActive(false);
        MainMenuButton.SetActive(false);
        Settings.SetActive(false);
        Tutorial1.SetActive(false);
        Tutorial2.SetActive(false);
        TutorialButton.SetActive(false);
        NextTutButton.SetActive(false);
        ContinueGameButton.SetActive(false);
        for (int i = 0; i < GameOverStars.Length; i++)
        {
            GameOverStars[i].SetActive(false);
            GameOverStars[i].transform.parent.gameObject.SetActive(false);
        }
        //if (textfields.Length == 0)
            //textfields = transform.GetComponentsInChildren<Text>();
    }
    public void Reset()
    {
        GameOver.SetActive(true);
        GM = GameObject.Find("Game Master").GetComponent<GameMaster>();
        if(textfields.Length == 0)
            textfields = transform.GetComponentsInChildren<Text>();
        boxes = GM.boxes.ToArray();
        for (int i = 0; i < 10; i++)
        {
            textfields[i].gameObject.SetActive(true);
        }
        GameOver.SetActive(false);
        MainMenuButton.SetActive(false);
        for (int i = 0; i < GameOverStars.Length; i++)
        {
            GameOverStars[i].SetActive(false);
            GameOverStars[i].transform.parent.gameObject.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if(boxes.Length == 0)
        {
            
            boxes = GM.boxes.ToArray();
            return;
        }
        textfields[0].text = checkZero(boxes[0].transform.GetComponent<Box>().hp);
        textfields[0].rectTransform.position = camera.WorldToScreenPoint(boxes[0].transform.localPosition) + new Vector3(0, 7, 0);
        textfields[1].text = checkZero(boxes[1].transform.GetComponent<Box>().hp);
        textfields[1].rectTransform.position = camera.WorldToScreenPoint(boxes[1].transform.localPosition) + new Vector3(0, 7, 0);
        textfields[2].text = checkZero(boxes[2].transform.GetComponent<Box>().hp);
        textfields[2].rectTransform.position = camera.WorldToScreenPoint(boxes[2].transform.localPosition) + new Vector3(0, 7, 0);
        textfields[5].text = checkZero(boxes[3].transform.GetComponent<Box>().hp);
        textfields[5].rectTransform.position = camera.WorldToScreenPoint(boxes[3].transform.localPosition) + new Vector3(0, 7, 0);
        textfields[4].text = checkZero(boxes[4].transform.GetComponent<Box>().hp);
        textfields[4].rectTransform.position = camera.WorldToScreenPoint(boxes[4].transform.localPosition) + new Vector3(0, 7, 0);
        textfields[3].text = checkZero(boxes[5].transform.GetComponent<Box>().hp);
        textfields[3].rectTransform.position = camera.WorldToScreenPoint(boxes[5].transform.localPosition) + new Vector3(0, 7, 0);

        totalScore = (GM.redScore + GM.blueScore);
        textfields[6].text = totalScore.ToString();

        textfields[7].text = GM.blueScore.ToString();
        textfields[8].text = GM.redScore.ToString();
        float temp = Mathf.Clamp((AM.slowRemaning - Mathf.FloorToInt(Time.time)), 0, Mathf.Infinity);
        if (temp == 0)
            textfields[9].text = "";
        else
            textfields[9].text = temp.ToString();

        if(GM.GameOver)
        {
            for (int i = 0; i < 10; i++)
            {
                textfields[i].gameObject.SetActive(false);
            }
            GameOver.SetActive(true);
            MainMenuButton.SetActive(true);
            for (int i = 0; i < GameOverStars.Length; i++)
            {
                GameOverStars[i].transform.parent.gameObject.SetActive(true);
            }
            
            textfields[10].text = GM.blueScore.ToString();
            textfields[11].text = GM.redScore.ToString();
            
            textfields[12].text = totalScore.ToString();
            if (GM.blueScore > 299 && GM.redScore > 299)
            {
                GameOverStars[2].SetActive(true);
            }
            if (GM.blueScore > 199 && GM.redScore > 199)
            {
                GameOverStars[1].SetActive(true);
            }
            if (GM.blueScore > 99 && GM.redScore > 99)
            {
                GameOverStars[0].SetActive(true);
            }
        }
        scoreMeterBlue.transform.localPosition = new Vector3(scoreMeterBlue.transform.localPosition.x, 0.08251023f + (posfactor * GM.blueScore), scoreMeterBlue.transform.localPosition.z);
        scoreMeterBlue.transform.localScale = new Vector3(scoreMeterBlue.transform.localScale.x, 0.01006653f + (scalefactor * GM.blueScore), scoreMeterBlue.transform.localScale.z);
        scoreMeterRed.transform.localPosition = new Vector3(scoreMeterRed.transform.localPosition.x, 0.08251023f + (posfactor * GM.redScore), scoreMeterRed.transform.localPosition.z);
        scoreMeterRed.transform.localScale = new Vector3(scoreMeterRed.transform.localScale.x, 0.01006653f + (scalefactor * GM.redScore), scoreMeterBlue.transform.localScale.z);
        if (GM.blueScore > 99 && GM.redScore > 99)
            scoreMeterStars[0].SetActive(true);
        if (GM.blueScore > 199 && GM.redScore > 199)
            scoreMeterStars[1].SetActive(true);
        if (GM.blueScore > 299 && GM.redScore > 299)
            scoreMeterStars[2].SetActive(true);

    }
    public void mainMenu()
    {
        startMenu.SetActive(true);
        startMenu.GetComponent<MainMenu>().reset();
        gameObject.SetActive(false);
        GM.gameObject.SetActive(false);
        for(int i = 0; i < 6;i++)
        {
            GM.boxes[i].SetActive(false);
        }

        for (int i = 0; i < GameOverStars.Length; i++)
        {
            GameOverStars[i].transform.parent.gameObject.SetActive(false);
        }
    }
    public void SettingsFunc()
    {
        Time.timeScale = 0;
        Settings.SetActive(true);
        SettingsButton.SetActive(true);
        ContinueGameButton.SetActive(true);
        TutorialButton.SetActive(true);
        for (int i = 0; i < textfields.Length; i++)
        {
            textfields[i].gameObject.SetActive(false);
        }

    }
    public void TutorialFunc()
    {
        Settings.SetActive(false);
        TutorialButton.SetActive(false);
        Tutorial1.SetActive(true);
        NextTutButton.SetActive(true);
        Tut = 1;
    }
    public void TutorialNextFunc()
    {
        if (Tut == 1)
        {
            Tutorial1.SetActive(false);
            Tutorial2.SetActive(true);
            Tut++;
        }
        else
        {
            Tutorial2.SetActive(false);
            NextTutButton.SetActive(false);
            Settings.SetActive(true);
            ContinueGameButton.SetActive(true);
        }
    }
    public void Continue()
    {
        Settings.SetActive(false);
        Tutorial1.SetActive(false);
        Tutorial2.SetActive(false);
        TutorialButton.SetActive(false);
        NextTutButton.SetActive(false);
        ContinueGameButton.SetActive(false);
        for (int i = 0; i < textfields.Length; i++)
        {
            textfields[i].gameObject.SetActive(true);
        }
        Time.timeScale = 1;
    }
    string checkZero(int value)
    {
        if (value <= 0)
            return "";
        else
            return value.ToString();
    }

}
