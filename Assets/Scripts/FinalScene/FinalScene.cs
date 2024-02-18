using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalScene : MonoBehaviour
{
    [SerializeField] private Transform meow;
    [SerializeField] private Transform yarg;

    [SerializeField] private Transform babyMouth;

    private Transform loser;

    // Start is called before the first frame update
    void Start()
    {
        // int p1 = GameManager.manager.getP1Score();
        // int p2 = GameManager.manager.getP2Score();

        // Debug.Log(GameManager.manager.getP1Score() + "  " + GameManager.manager.getP2Score());

        // // player 1 won
        // if(GameManager.manager.getP1Score() > GameManager.manager.getP2Score())
        // {
        //     meow.gameObject.SetActive(false);

        //     loser = yarg;
        // }
        // else
        // {
        //     yarg.gameObject.SetActive(false);

        //     loser = meow;
        // }

        Debug.Log(GameManager.manager.getWinner() + "  " + GameManager.manager.getLoser());

        loser = GameObject.Find(GameManager.manager.getLoser()).transform;
        GameObject.Find(GameManager.manager.getWinner()).SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(loser.position.x >= -6)
        {
            loser.position = Vector3.Lerp(loser.position, babyMouth.position, Time.deltaTime);
        }
    }
}
