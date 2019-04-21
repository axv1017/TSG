using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //If the escape key is pressed
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            //Game will exit
            Application.Quit();
        }
    }
}
