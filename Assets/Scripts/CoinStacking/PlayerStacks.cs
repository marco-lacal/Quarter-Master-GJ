using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerStacks : MonoBehaviour
{
    [SerializeField] private int playerNum;

    [SerializeField] private CoinStacksHandler handlerRef;

    // array of all keycodes this player stack will use (both players will have unique keycode arrays)
    [SerializeField] private KeyCode[] mappings;

    void Start()
    {
        // foreach(KeyCode entry in mappings)
        // {
        //     Debug.Log(entry.ToString());
        // }

        // dont be dumbass
        if(!handlerRef)
        {
            Debug.Log("YOU DIDNT SET THE REFERENCE");
            return;
        }

        // randomly select the first key to stack a coin
        int randNumber = Random.Range(0, mappings.Length);
        handlerRef.SetNextPlayersKey(playerNum == 1 ? true : false, mappings[randNumber], randNumber);
    }

    // Update is called once per frame
    void Update()
    {
        // if any key at all pressed
        if(Input.anyKeyDown)
        {
            // loop through all keycode objects in the array. want the specific keycodes so looping by integer (like in playerplates) not desired
            foreach(KeyCode mapping in mappings)
            {
                if(Input.GetKeyDown(mapping))
                {
                    // if checksentkey returns successful
                    if(handlerRef.CheckSentKey(playerNum == 1 ? true : false, mapping))
                    {
                        // set the next button to press
                        int randNumber = Random.Range(0, mappings.Length);
                        handlerRef.SetNextPlayersKey(playerNum == 1 ? true : false, mappings[randNumber], randNumber);
                    }
                    break;
                }
            }
        }
    }
}
