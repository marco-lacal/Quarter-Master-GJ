using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;
    [SerializeField] private int numGames;

    // [SerializeField] private Transform meow;
    // [SerializeField] private Transform yarg;

    private Vector2 meowPosition;
    private Vector2 yargPosition;

    private static int sceneNum;
    private static int previousScene;
    [SerializeField] private int scoreToWin = 4;

    private static int playerOneScore;
    private static int playerTwoScore;

    private string winner;
    private string loser;


    void Awake()
    {
        if(manager == null)
        {
            manager = this;
            DontDestroyOnLoad(this);

            sceneNum = playerOneScore = playerTwoScore = 0;
        }
        else if(manager != this)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // Start Game
        if(Input.GetKeyDown("2") && sceneNum == 0)
        {
            NextScene();
            return;
        }

        if(Input.GetKeyDown("3"))
        {
            UnityEditor.EditorApplication.isPlaying = false;

            // use this for build
            // Application.Quit()
        }
    }

    public int getP1Score()
    {
        return playerOneScore;
    }

    public int getP2Score()
    {
        return playerTwoScore;
    }

    public string getWinner()
    {
        return winner;
    }

    public string getLoser()
    {
        return loser;
    }

    void NextScene()
    {
        sceneNum = Random.Range(1, numGames+1);

        StartCoroutine(GenerateNewNumber());
    }

    public void MinigameWin(bool player)
    {
        if(player)
        {
            playerOneScore++;
        }
        else
        {
            playerTwoScore++;
        }

        if(playerOneScore >= scoreToWin)
        {
            sceneNum = 7;
            
            winner = "Meow";
            loser = "yarg";

            StartCoroutine(ShortTimer(true));
        }
        else if(playerTwoScore >= scoreToWin)
        {
            sceneNum = 7;

            winner = "yarg";
            loser = "Meow";

            StartCoroutine(ShortTimer(true));
        }
        else
        {
            StartCoroutine(ShortTimer(false));
        }
    }

    IEnumerator GenerateNewNumber()
    {
        while(sceneNum == previousScene)
        {
            sceneNum = Random.Range(1, numGames+1);
            yield return null;
        }

        previousScene = sceneNum;

        SceneManager.LoadScene(sceneNum);
    }

    IEnumerator ShortTimer(bool isEnd)
    {
        yield return new WaitForSeconds(2);

        if(isEnd)
        {
            playerOneScore = 0;
            playerTwoScore = 0;

            SceneManager.LoadScene(sceneNum);
        }
        else
        {
            NextScene();
        }
    }
}
