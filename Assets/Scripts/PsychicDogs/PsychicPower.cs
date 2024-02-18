using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PsychicPower : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI directionsText;

    [SerializeField] private GameObject playerOneCoin;
    [SerializeField] private GameObject playerTwoCoin;

    private Coroutine buttonMash;
    private Coroutine loserCoin;

    private int playerOnePressCounter;
    private int playerTwoPressCounter;

    // Start is called before the first frame update
    void Start()
    {
        playerOnePressCounter = 0;
        playerTwoPressCounter = 0;

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
    }

    IEnumerator ButtonMash()
    {
        directionsText.text = "Press Your Red Button!";
        float timer = 5.0f;

        while(timer > 0)
        {
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

        Debug.Log(playerOnePressCounter + "  " + playerTwoPressCounter);

        timer = 0;
        timerText.text = timer.ToString();

        directionsText.text = "Now Psychic Battle!!";

        loserCoin = StartCoroutine(LoserCoin());

        yield return null;
    }

    IEnumerator LoserCoin()
    {
        GameObject temp;

        if(playerOnePressCounter > playerTwoPressCounter)
        {
            temp = playerOneCoin;
        }
        else if(playerOnePressCounter < playerTwoPressCounter)
        {
            temp = playerTwoCoin;
        }
        else    // tied amoutn of presses. so generate random number from 1-100 and if its 1-50, player 1 loses, and vice versa
        {
            int flipCoin = Random.Range(1, 101);

            if(flipCoin >= 50)
            {
                temp = playerOneCoin;
            }
            else
            {
                temp = playerTwoCoin;
            }
        }

        yield return new WaitForSecondsRealtime(2);

        temp.AddComponent<Rigidbody2D>();

        while(temp.transform.position.y >= -8.5f)
        {
            yield return null;
        }

        Destroy(temp);
    }
}
