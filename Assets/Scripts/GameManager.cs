using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;

    public enum gameStates { Start, Playing, GameOver, Menu};
    public gameStates gameState = gameStates.Start;

    public GameObject spawner;
    public GameObject mainCamera;
    public AudioSource backgroundMusic;
    public GameObject startCanvas;
    public GameObject menuCanvas;
    public GameObject mainCanvas;
    public Text mainScoreText;
    public GameObject gameOverCanvas;
    public Text gameOverScoreText;
    public GameObject RecordsCanvas;
    public Text gameRecordsText;
    public InputField nameInputFieled;
    public GameObject CreditCanvas;

    public AudioClip tapOnButtomAS;

    public int countOfMonstersToDefeat = 10;
    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        gm = gameObject.GetComponent<GameManager>();
        CountScore(0);
        SetCanvasVisibility(true, false, false, false, false, false);
        backgroundMusic.volume = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameState)
        {

            case gameStates.Playing:
                backgroundMusic.volume = 0.5f;
                if (spawner.transform.childCount >= countOfMonstersToDefeat)
                {
                    gameState = gameStates.GameOver;

                    gameOverScoreText.text = mainScoreText.text;

                    SetCanvasVisibility(false, false, false, true, false, false);

                    spawner.GetComponent<MonsterSpawner>().canSpawn = false;
                    mainCamera.GetComponent<CameraMotion>().canMove = false;
                }
                break;

            case gameStates.GameOver:
                for (int i = 0; i < spawner.transform.childCount; i++)
                {
                    spawner.transform.GetChild(i).GetComponent<MonsterDestructor>().Destroy(); ;
                }
                backgroundMusic.volume = 0.1f;
                break;

            case gameStates.Menu:
                break;
        }
    }

    public void CountScore(int amount)
    {
        score += amount;
        mainScoreText.text = score.ToString();

    }

    public void StartGame()
    {
        backgroundMusic.PlayOneShot(tapOnButtomAS);

        score = 0;
        mainScoreText.text = score.ToString();

        SetCanvasVisibility(false, true, false, false, false, false);
        gameState = gameStates.Playing;
        spawner.GetComponent<MonsterSpawner>().canSpawn = true;
        mainCamera.GetComponent<CameraMotion>().canMove = true;
    }

    public void OpenMenu()
    {
        backgroundMusic.PlayOneShot(tapOnButtomAS);

        SetCanvasVisibility(false, false, true, false, false, false);
        gameState = gameStates.Menu;
    }

    public void OpenRecords()
    {
        backgroundMusic.PlayOneShot(tapOnButtomAS);

        SetCanvasVisibility(false, false, false, false, true, false);
        gameState = gameStates.Menu;

        string textForRecords = "";
        for(int i = 0; i < 10; i++)
        {
            textForRecords += PlayerPrefs.GetString(i.ToString(), " ") + "\n";
        }
        gameRecordsText.text = textForRecords;
    }

    public void SaveRecords()
    {
        string name = nameInputFieled.textComponent.text;
        WriteRecord(new KeyValuePair<string, int>(name, score));
        OpenMenu();
    }

    private void WriteRecord(KeyValuePair<string, int> newResult)
    {
        if (PlayerPrefs.GetString("0", " ") == " ")
        {
            PlayerPrefs.SetString("0", newResult.Key + " : " + newResult.Value.ToString());
            for (int i = 1; i < 10; i++)
                PlayerPrefs.SetString(i.ToString(), "??? : 0");
        }
        else
        {
            KeyValuePair<string, int>[] rating = new KeyValuePair<string, int>[10];
            int placeNewResult = 10;
            for (int i = 0; i < 10; i++)
            {
                string result = PlayerPrefs.GetString(i.ToString(), " ");

                var resultSplit = result.Split();
                rating[i] = new KeyValuePair<string, int>(resultSplit[0], Convert.ToInt32(resultSplit[resultSplit.Length - 1]));
            }

            for (int i = 9; i >= 0; i--)
            {
                if (rating[i].Key == newResult.Key)
                {
                    if (rating[i].Value <= newResult.Value)
                        placeNewResult = i;
                    else
                        return;
                }
                else
                {
                    if (rating[i].Value <= newResult.Value)
                        placeNewResult = i;
                }
            }

            if (placeNewResult >= 10)
                return;

            PlayerPrefs.DeleteKey(newResult.Key);

            PlayerPrefs.SetString(placeNewResult.ToString(), newResult.Key + " : " + newResult.Value.ToString());
            for (int i = placeNewResult + 1; i < 10; i++)
            {
                PlayerPrefs.SetString(i.ToString(), rating[i - 1].Key + " : " + rating[i - 1].Value.ToString());
            }
        }
        PlayerPrefs.Save();
    }

    public void OpenCredits()
    {
        backgroundMusic.PlayOneShot(tapOnButtomAS);

        SetCanvasVisibility(false, false, false, false, false, true);
        gameState = gameStates.Menu;
    }

    public void QuitGame()
    {
        backgroundMusic.PlayOneShot(tapOnButtomAS);
        Application.Quit();
    }

    public void SetCanvasVisibility(bool startCanvasMode, bool mainCanvasMode, bool menuCanvasMode,
                                    bool gameOverCanvasMode, bool RecordsCanvasMode, bool CreditCanvasMode)
    {
        startCanvas.SetActive(startCanvasMode);
        mainCanvas.SetActive(mainCanvasMode);
        menuCanvas.SetActive(menuCanvasMode);
        gameOverCanvas.SetActive(gameOverCanvasMode);
        RecordsCanvas.SetActive(RecordsCanvasMode);
        CreditCanvas.SetActive(CreditCanvasMode);
    }
}
