using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlates : MonoBehaviour
{
    [SerializeField] private int playerNum;

    [SerializeField] private CreateRandomBurger burgerRef;

    [SerializeField] private KeyCode[] mappings;

    private Vector3 spawnPoint;

    private Stack<GameObject> builtBurger;

    void Start()
    {
        // foreach(KeyCode entry in mappings)
        // {
        //     Debug.Log(entry.ToString());
        // }

        if(!burgerRef)
        {
            Debug.Log("YOU DIDNT SET THE REFERENCE");
            return;
        }

        builtBurger = new Stack<GameObject>();
        spawnPoint = transform.position + new Vector3(0, 0.4f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
        {
            // foreach(KeyCode mapping in mappings)
            // {
            //     if(Input.GetKeyDown(mapping))
            //     {
            //         // if(handlerRef.CheckSentKey(playerNum == 1 ? true : false, mapping))
            //         // {
            //         //     int randNumber = Random.Range(0, mappings.Length);

            //         //     handlerRef.SetNextPlayersKey(playerNum == 1 ? true : false, mappings[randNumber], randNumber);
            //         // }
            //         break;
            //     }
            // }

            for(int i = 0; i < mappings.Length; i++)
            {
                if(Input.GetKeyDown(mappings[i]))
                {
                    GameObject result = burgerRef.CheckSentKey(playerNum == 1 ? true : false, i);

                    if(result != null)
                    {
                        GameObject temp = Instantiate(result, spawnPoint, Quaternion.identity);
                        spawnPoint.y += 0.4f;
                        temp.transform.parent = transform;

                        builtBurger.Push(temp);
                    }
                }
            }
        }
    }
}
