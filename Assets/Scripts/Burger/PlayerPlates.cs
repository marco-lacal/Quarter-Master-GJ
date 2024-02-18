using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlates : MonoBehaviour
{
    [SerializeField] private int playerNum;

    [SerializeField] private CreateRandomBurger burgerRef;

    [SerializeField] private KeyCode[] mappings;

    private Vector3 spawnPoint;

    // think stack of plates. You add and remove from the top. not super needed, but having references to all objects is good to not have lost memory references/objects
    private Stack<GameObject> builtBurger;

    void Start()
    {
        // foreach(KeyCode entry in mappings)
        // {
        //     Debug.Log(entry.ToString());
        // }

        // dont be dumbass
        if(!burgerRef)
        {
            Debug.Log("YOU DIDNT SET THE REFERENCE");
            return;
        }

        // initialize the stack for use
        builtBurger = new Stack<GameObject>();
        spawnPoint = transform.position + new Vector3(0, 0.4f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // if any key at all was pressed down
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

            // loop through the array of possible keys to press, using int index value. Used for the burger script function we call
            for(int i = 0; i < mappings.Length; i++)
            {
                // if the ith-indexed element of the keycode array is pressed
                if(Input.GetKeyDown(mappings[i]))
                {
                    // send which player and send the index of the pressed key
                    GameObject result = burgerRef.CheckSentKey(playerNum == 1 ? true : false, i);

                    // if CheckSentKey returned a sufficient GameObject (meaning it was successful)
                    if(result != null)
                    {
                        // instantiate the part we correctly picked

                        GameObject temp = Instantiate(result, spawnPoint, Quaternion.identity);
                        spawnPoint.y += 0.4f;
                        temp.transform.parent = transform;

                        builtBurger.Push(temp);     // place gameobject on top of stack
                    }
                }
            }
        }
    }
}
