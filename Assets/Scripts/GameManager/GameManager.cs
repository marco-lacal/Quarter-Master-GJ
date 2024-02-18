using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;
    [SerializeField] private int numGames;

    private int sceneNum;
    private int previousScene;

    private int playerOneScore;
    private int playerTwoScore;

    void Awake()
    {
        if(manager == null)
        {
            Debug.Log("WAS NULL");
            manager = this;
            DontDestroyOnLoad(this);
        }
        else if(manager != this)
        {
            Debug.Log("Wasnt");
            Destroy(gameObject);
        }
    }

    void Start()
    {
        sceneNum = 0;
    }

    void Update()
    {
        Debug.Log(sceneNum);
        // Start Game
        if(Input.GetKeyDown("2") && sceneNum == 0)
        {
            Debug.Log("HELLO");
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

        StartCoroutine(ShortTimer());
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

    IEnumerator ShortTimer()
    {
        yield return new WaitForSeconds(2);

        NextScene();
    }
}
