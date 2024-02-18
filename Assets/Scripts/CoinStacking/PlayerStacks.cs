using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerStacks : MonoBehaviour
{

    [SerializeField] private KeyCode[] mappings;

    void Start()
    {
        foreach(KeyCode entry in mappings)
        {
            Debug.Log(entry.ToString());
        }
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
                    Debug.Log(mapping.ToString() + " was pressed down!");
                    break;
                }
            }
        }
    }
}
