using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        //If enter key is clicked
        if (Input.GetKeyUp(KeyCode.Return))
        {
            //Reload scene and set proper turn and defeat status
            SceneManager.LoadScene("SampleScene");
            SharedMovement.turn = 0;
            Trait0.defeated = false;
            Trait1.defeated = false;
        }
    }
}
