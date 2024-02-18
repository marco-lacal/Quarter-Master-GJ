using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRandomBurger : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject[] burgerParts = new GameObject[6];
    // [Header("Default number of parts for a burger")]
    // [SerializeField] private int sizeOfBurger = 6;

    // private GameObject[] createdBurger;

    private List<GameObject> createdBurger;

    private int player1Index;
    private int player2Index;

    // void Start()
    // {
    //     createdBurger = new GameObject[sizeOfBurger];

    //     Vector2 spawnLocation = spawnPoint.position;

    //     createdBurger[0] = Instantiate(burgerParts[0], spawnLocation, Quaternion.identity);     // bottom bun

    //     spawnLocation.y += .4f;

    //     for(int i = 1; i < sizeOfBurger - 1; i++)
    //     {
    //         int randNumber = Random.Range(1, burgerParts.Length - 1);
    //         createdBurger[randNumber] = Instantiate(burgerParts[randNumber], spawnLocation, Quaternion.identity);
    //         createdBurger[randNumber].transform.parent = transform;

    //         spawnLocation.y += .4f;
    //     }

    //     createdBurger[sizeOfBurger-1] = Instantiate(burgerParts[burgerParts.Length - 1], spawnLocation, Quaternion.identity);     // top bun

    //     player1Index = 0;
    //     player2Index = 0;
    // }

    private void AddBurgerPart(int burgerIndex, Vector3 spawn)
    {
        GameObject temp = Instantiate(burgerParts[burgerIndex], spawn, Quaternion.identity);
        temp.transform.parent = transform;

        createdBurger.Add(temp);
    }

    void Start()
    {
        createdBurger = new List<GameObject>();

        Vector2 spawnLocation = spawnPoint.position;

        // GameObject bottomBun = Instantiate(burgerParts[0], spawnLocation, Quaternion.identity);
        // bottomBun.transform.parent = transform;

        // createdBurger.Add(bottomBun);

        AddBurgerPart(0, spawnLocation);

        spawnLocation.y += 0.4f;

        while(createdBurger.Count <= 9)
        {
            if(createdBurger.Count == 9)
            {
                // put the top bun on it now
                // GameObject topbun = Instantiate(burgerParts[burgerParts.Length - 1], spawnLocation, Quaternion.identity);
                // topbun.transform.parent = transform;

                // createdBurger.Add(topbun);

                AddBurgerPart(burgerParts.Length - 1, spawnLocation);

                break;
            }

            if(createdBurger.Count >= 5)
            {
                Debug.Log("RANDOM CHANCE");

                int endEarly = Random.Range(1, 101);

                if(endEarly <= 33)
                {
                    Debug.Log("BING");

                    // put top bun early
                    // GameObject topbun = Instantiate(burgerParts[burgerParts.Length - 1], spawnLocation, Quaternion.identity);
                    // topbun.transform.parent = transform;

                    // createdBurger.Add(topbun);

                    AddBurgerPart(burgerParts.Length - 1, spawnLocation);

                    break;
                }
            }

            // else, continue adding to make a big burger

            int randNumber = Random.Range(1, burgerParts.Length - 1);

            // GameObject temp = Instantiate(burgerParts[randNumber], spawnLocation, Quaternion.identity);
            // temp.transform.parent = transform;

            // createdBurger.Add(temp);

            AddBurgerPart(randNumber, spawnLocation);

            spawnLocation.y += .4f;
        }

        player1Index = 0;
        player2Index = 0;

        for(int i = 0; i < createdBurger.Count; i++)
        {
            Debug.Log(createdBurger[i].name + " " + i);
        }
    }

    public GameObject CheckSentKey(bool player, int burgerIndex)
    {
        if(player1Index >= createdBurger.Count || player2Index >= createdBurger.Count)
        {
            Debug.Log("GAMES OVER");
            return null;
        }

        Debug.Log(burgerParts[burgerIndex] + " " + createdBurger[player1Index] + ", " + GameObject.ReferenceEquals(burgerParts[burgerIndex], createdBurger[player1Index]));

        if(player && burgerParts[burgerIndex].tag == createdBurger[player1Index].tag)
        {
            player1Index++;

            if(player1Index >= createdBurger.Count)
            {
                // END OF THE MINIGAME: PLAYER 1 WON

                Debug.Log("PLAYER 1 WON");
            }
            
            return burgerParts[burgerIndex];
        }
        else if(!player && GameObject.ReferenceEquals(burgerParts[burgerIndex], createdBurger[player2Index]))
        {
            player2Index++;

            if(player2Index >= createdBurger.Count)
            {
                // END OF THE MINIGAME: PLAYER 2 WON

                Debug.Log("PLAYER 2 WON");
            }

            return burgerParts[burgerIndex];
        }
        
        return null;
    }
}
