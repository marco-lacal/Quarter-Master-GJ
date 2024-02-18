using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class CoinStacksHandler : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab;

    [SerializeField] private Vector3 spawnOneLocation;
    [SerializeField] private Vector3 spawnTwoLocation;

    [SerializeField] private SpriteRenderer playerOneCircle;
    [SerializeField] private SpriteRenderer playerTwoCircle;

    [SerializeField] private int ScoreLimit;

    [SerializeField] private Color[] buttonColors;

    private KeyCode nextKeyPlayerOne;
    private KeyCode nextKeyPlayerTwo;

    private int playerOneSize;
    private int playerTwoSize;

    // Start is called before the first frame update
    void Start()
    {
        playerOneSize = 0;
        playerTwoSize = 0;

        Debug.Log(spawnOneLocation);
        Debug.Log(spawnTwoLocation);
    }

    void Update()
    {
        if(playerOneSize >= ScoreLimit)
        {
            Debug.Log("PLAYER 1 WINS");
        }
        else if(playerTwoSize >= ScoreLimit)
        {
            Debug.Log("PLAYER 2 WINS");
        }
    }

    public void SetNextPlayersKey(bool player, KeyCode next, int index)
    {
        if(player)
        {
            nextKeyPlayerOne = next;
            playerOneCircle.color = buttonColors[index];
            Debug.Log("Player 1: " + next);
        }
        else
        {
            nextKeyPlayerTwo = next;
            playerTwoCircle.color = buttonColors[index];
            Debug.Log("Player 2: " + next);
        }
    }

    public bool CheckSentKey(bool player, KeyCode pressedKey)
    {
        // Check player one
        if(player && pressedKey == nextKeyPlayerOne)
        {
            playerOneSize++;
            spawnOneLocation = new Vector3(spawnOneLocation.x, spawnOneLocation.y + 0.3f, 0);
            Instantiate(coinPrefab, spawnOneLocation, Quaternion.identity);
            return true;
        }
        // Check player two
        else if(!player && pressedKey == nextKeyPlayerTwo)
        {
            playerTwoSize++;
            spawnTwoLocation = new Vector3(spawnTwoLocation.x, spawnTwoLocation.y + 0.3f, 0);
            Instantiate(coinPrefab, spawnTwoLocation, Quaternion.identity);
            return true;
        }

        return false;
    }
}
