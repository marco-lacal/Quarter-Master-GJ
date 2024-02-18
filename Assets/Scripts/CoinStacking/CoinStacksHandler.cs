using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class CoinStacksHandler : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab;

    [SerializeField] private Vector3 stackOneLocation;
    [SerializeField] private Vector3 stackTwoLocation;

    [SerializeField] private int ScoreLimit;

    private KeyCode nextKeyPlayerOne;
    private KeyCode nextKeyPlayerTwo;

    private int playerOneSize;
    private int playerTwoSize;

    // Start is called before the first frame update
    void Start()
    {
        playerOneSize = 0;
        playerTwoSize = 0;

        Debug.Log(stackOneLocation);
        Debug.Log(stackTwoLocation);
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

    public void SetNextPlayersKey(bool player, KeyCode next)
    {
        if(player)
        {
            nextKeyPlayerOne = next;
            Debug.Log("Player 1: " + next);
        }
        else
        {
            nextKeyPlayerTwo = next;
            Debug.Log("Player 2: " + next);
        }
    }

    public bool CheckSentKey(bool player, KeyCode pressedKey)
    {
        // Check player one
        if(player && pressedKey == nextKeyPlayerOne)
        {
            playerOneSize++;
            stackOneLocation = new Vector3(stackOneLocation.x, stackOneLocation.y + 0.3f, 0);
            Instantiate(coinPrefab, stackOneLocation, Quaternion.identity);
            return true;
        }
        // Check player two
        else if(!player && pressedKey == nextKeyPlayerTwo)
        {
            playerTwoSize++;
            stackTwoLocation = new Vector3(stackTwoLocation.x, stackTwoLocation.y + 0.3f, 0);
            Instantiate(coinPrefab, stackTwoLocation, Quaternion.identity);
            return true;
        }

        return false;
    }
}
