using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PsychicPower : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;

    [SerializeField] private GameObject playerOneCoin;
    [SerializeField] private GameObject playerTwoCoin;

    [SerializeField] private float timer = 10;

    private Coroutine buttonMash;
    private Coroutine loserCoin;

    private int playerOnePressCounter;
    private int playerTwoPressCounter;

    // set to be passed into a potential future game manager
    private int winner;

    void Awake()
    {
        Transform meow = GameObject.Find("Meow").transform;
        Transform yarg = GameObject.Find("yarg").transform;

        Debug.Log(GameManager.manager.getP1Score() + " " + GameManager.manager.getP2Score());

        meow.position += new Vector3(0, GameManager.manager.getP1Score() * 2f, 0);
        yarg.position += new Vector3(0, GameManager.manager.getP2Score() * 2f, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        // initialize the presses counters to 0
        playerOnePressCounter = 0;
        playerTwoPressCounter = 0;

        // start timer and button mashing coroutine
        buttonMash = StartCoroutine(ButtonMash());
    }

    void Update()
    {
        if(buttonMash != null)
        {
            return;
        }

        if(loserCoin != null)
        {
            return;
        }

        // INSERT END GAME AND UPDATE COIN MOVEMENTS HERE
        Debug.Log("GAME HAS ENDED: The winner is player " + winner);
    }

    // Coroutine for the timer and button mashing inputs
    IEnumerator ButtonMash()
    {
        while(timer > 0)
        {
            // Increment each players counter by 1 for each time they pressed the key DOWN

            if(Input.GetKeyDown(KeyCode.J))
            {
                playerOnePressCounter++;
            }
            if(Input.GetKeyDown(KeyCode.F))
            {
                playerTwoPressCounter++;
            }

            timer -= Time.deltaTime;
            timerText.text = timer.ToString();
            yield return null;
        }

        // Debug.Log(playerOnePressCounter + "  " + playerTwoPressCounter);

        timer = 0;
        timerText.text = timer.ToString();

        loserCoin = StartCoroutine(LoserCoin());

        buttonMash = null;

        yield return null;
    }

    IEnumerator LoserCoin()
    {
        // create empty GameObject that will only be used to point to the correct GameObject based on the points
        GameObject loser;

        // if player 1 wins, set the empty GameObject to player 2
        if(playerOnePressCounter > playerTwoPressCounter)
        {
            winner = 1;
            loser = playerTwoCoin;
        }
        // vice versa
        else if(playerOnePressCounter < playerTwoPressCounter)
        {
            winner = 2;
            loser = playerOneCoin;
        }
        else    // tied amount of presses. so generate random number from 1-100 and if its 1-50, player 1 loses, and vice versa
        {
            int flipCoin = Random.Range(1, 101);

            if(flipCoin <= 50)      // 1-50
            {
                loser = playerOneCoin;
            }
            else                    //51-100
            {
                loser = playerTwoCoin;
            }
        }

        // 2 seconds delay before big reveal
        yield return new WaitForSeconds(2);

        // add phsyics to the coin to fall
        loser.AddComponent<Rigidbody2D>();

        // once the coin is off screen, destroy it
        while(loser.transform.position.y >= -8.5f)
        {
            yield return null;
        }

        Destroy(loser);

        loserCoin = null;

        if(winner == 1)
        {
            GameManager.manager.MinigameWin(true);
        }
        else
        {
            GameManager.manager.MinigameWin(false);
        }
    }
}
