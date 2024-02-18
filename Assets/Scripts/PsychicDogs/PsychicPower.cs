using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PsychicPower : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    IEnumerator ThreeSecondSetup()
    {
        float timer = 3.0f;

        while(timer > 0)
        {
            timer -= Time.deltaTime;
            Debug.Log(timer);
            yield return null;
        }

        yield return null;
    }
}
