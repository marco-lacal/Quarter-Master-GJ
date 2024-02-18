using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuNavigation : MonoBehaviour
{
    public GameObject start;
    public GameObject credits;
    public GameObject exit;
    public GameObject creditMenu;
    public GameObject creditExit; 
    public GameObject exitButton;   
    public GameObject startButton;
    public GameObject creditsButton;
    public GameObject creditExitButton;
    public GameObject exitcredit;
    public GameObject GameName;

    void Update()
    {
        if (Input.GetKeyDown("2"))
        {
            startButton.SetActive(false);
            creditsButton.SetActive(false);
            exitButton.SetActive(false);
            start.SetActive(false);
            credits.SetActive(false);
            exit.SetActive(false);
            GameName.SetActive(false);
        }

                if (Input.GetKeyDown(KeyCode.U))
        {
            startButton.SetActive(false);
            creditsButton.SetActive(false);
            exitButton.SetActive(false);
            start.SetActive(false);
            credits.SetActive(false);
            exit.SetActive(false);
            creditMenu.SetActive(true);
            creditExitButton.SetActive(true);
            exitcredit.SetActive(true);


        }

         if (Input.GetKeyDown(KeyCode.L))
        {
            startButton.SetActive(true);
            creditsButton.SetActive(true);
            exitButton.SetActive(true);
            start.SetActive(true);
            credits.SetActive(true);
            exit.SetActive(true);
            creditMenu.SetActive(false);
            creditExitButton.SetActive(false);
            exitcredit.SetActive(false);
        }


    }
}
