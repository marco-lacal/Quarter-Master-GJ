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

    void Awake()
    {
        if(manager == null)
        {
            Debug.Log("RUN ONCE");
            manager = this;
            DontDestroyOnLoad(this);

            sceneNum = playerOneScore = playerTwoScore = 0;
        }
        else if(manager != this)
        {
            Debug.Log("RUN MANY");
            Destroy(gameObject);
        }

        Transform meow = GameObject.Find("Meow").transform;
        Transform yarg = GameObject.Find("yarg").transform;

        if(sceneNum == 7)
        {
            Debug.Log("HELLO from ");

            if(playerOneScore > playerTwoScore)
            {
                yarg.gameObject.SetActive(false);
            }
            else
            {
                meow.gameObject.SetActive(false);
            }
        }

        Debug.Log(meow.name + "  " + yarg.name);

        meow.position += new Vector3(0, playerOneScore * 2f, 0);
        yarg.position += new Vector3(0, playerTwoScore * 2f, 0);

        Debug.Log((playerOneScore * 20f) + "  " + (playerTwoScore * 20f));

        Debug.Log("Score: " + playerOneScore + "  " + playerTwoScore);
    }

    void Update()
    {
        // Start Game
        if(Input.GetKeyDown("2") && sceneNum == 0)
        {
            NextScene();
            return;
        }

        if(Input.GetKeyDown(KeyCode.Keypad3))
        {
            UnityEditor.EditorApplication.isPlaying = false;

            // use this for build
            // Application.Quit()
        }
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

        Debug.Log("WIN " + playerOneScore + " " + playerTwoScore);

        if(playerOneScore >= scoreToWin)
        {
            sceneNum = 7;

            StartCoroutine(ShortTimer(true));
        }
        else if(playerTwoScore >= scoreToWin)
        {
            sceneNum = 7;

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
