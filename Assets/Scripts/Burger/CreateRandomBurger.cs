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

    // Previous implementation with static burger sizes set by inspector

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

    // to created a clone of burgerParts. this block of code was used alot so just made function for reusablity
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

        AddBurgerPart(0, spawnLocation);

        spawnLocation.y += 0.4f;

        while(createdBurger.Count <= 9)
        {
            // max size. put the cap on it
            if(createdBurger.Count == 9)
            {
                // put the top bun on it now

                AddBurgerPart(burgerParts.Length - 1, spawnLocation);

                break;
            }

            // as soon as we hit at least 5 pieces (bottomBun and 4 lettuce/patty/cheese/tomato), random chance to make bigger burger
            if(createdBurger.Count >= 5)
            {
                int endEarly = Random.Range(1, 101);

                // 33% chance to end it early
                if(endEarly <= 33)
                {
                    // put top bun early

                    AddBurgerPart(burgerParts.Length - 1, spawnLocation);

                    break;
                }
            }

            // else, continue adding to make a big burger

            int randNumber = Random.Range(1, burgerParts.Length - 1);

            AddBurgerPart(randNumber, spawnLocation);

            spawnLocation.y += .4f;
        }

        // two pointer search. each index variable keeps track of that players current progress through completing the burger
        // example: player 1 has only 1 piece down and is working on the second, where player 2 has 4 pieces down and is working on the fifth
        /*
            player1Index
                V
            [0, 1, 1, 4, 3, 2, 3, 2, 5]
                         ^
                    player2Index
        */
        player1Index = 0;
        player2Index = 0;
    }

    // determine if the key sent in is the next piece to add for the specified player
    public GameObject CheckSentKey(bool player, int burgerIndex)
    {
        // if one player has built the whole burger, stop more inputs
        if(player1Index >= createdBurger.Count || player2Index >= createdBurger.Count)
        {
            Debug.Log("GAMES OVER");
            return null;
        }

        // if player 1 entered the correct part
        if(player && burgerParts[burgerIndex].tag == createdBurger[player1Index].tag)
        {
            // advance their pointer forward
            player1Index++;

            if(player1Index >= createdBurger.Count)
            {
                // END OF THE MINIGAME: PLAYER 1 WON

                Debug.Log("PLAYER 1 WON");

                // AT THIS POINT, UPDATE GAMEMANAGER OR WHATEVER AND CLOSE THIS MINIGAME
            }
            
            // send the piece to PlayerPlate to instantiate it
            return burgerParts[burgerIndex];
        }
        // if player 2 entered the correct part
        else if(!player && GameObject.ReferenceEquals(burgerParts[burgerIndex], createdBurger[player2Index]))
        {
            // advance their pointer forward
            player2Index++;

            if(player2Index >= createdBurger.Count)
            {
                // END OF THE MINIGAME: PLAYER 2 WON

                Debug.Log("PLAYER 2 WON");

                // AT THIS POINT, UPDATE GAMEMANAGER OR WHATEVER AND CLOSE THIS MINIGAME
            }

            // send the piece to PlayerPlate to instantiate it
            return burgerParts[burgerIndex];
        }
        
        // wrong choice
        return null;
    }
}
