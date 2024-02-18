using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerStacks : MonoBehaviour
{
    [SerializeField] private int playerNum;

    [SerializeField] private CoinStacksHandler handlerRef;

    [SerializeField] private KeyCode[] mappings;

    void Start()
    {
        // foreach(KeyCode entry in mappings)
        // {
        //     Debug.Log(entry.ToString());
        // }

        if(!handlerRef)
        {
            Debug.Log("YOU DIDNT SET THE REFERENCE");
            return;
        }

        handlerRef.SetNextPlayersKey(playerNum == 1 ? true : false, mappings[Random.Range(0, mappings.Length)]);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
        {
            foreach(KeyCode mapping in mappings)
            {
                if(Input.GetKeyDown(mapping))
                {
                    if(handlerRef.CheckSentKey(playerNum == 1 ? true : false, mapping))
                    {
                        handlerRef.SetNextPlayersKey(playerNum == 1 ? true : false, mappings[Random.Range(0, mappings.Length)]);
                    }
                    break;
                }
            }
        }
    }
}
