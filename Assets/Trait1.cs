using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Trait1 : MonoBehaviour
{
    //Self explanitory
    public static int unitID = 1;
    //Hit Points a unit starts with
    public int startHP = 20;
    //Hit Points as they change during the game, get anywhere but only set here
    public int updatedHP { get; private set; }
    //How much damage a unit can do
    public static int strength = 8;
    //Is the unit alive
    public static bool defeated = false;
    //Text field to add to gui
    Text showText;

    //When game starts initalize text and set Hit Points to where they should start
    void Awake()
    {
        showText = GetComponent<Text>();
        updatedHP = startHP;
    }

    //Use different inputs of 1 to display traits and/or recieve damage, check and display gameover, and press space for general info
    void Update()
    {
        //Check if team is empty, if it is display game over
        if (Trait0.defeated == false)
        {
            if (Input.GetKeyUp(KeyCode.Keypad2))
            {
                //Send info for combat into function
                recieveDamage(Trait0.strength);
                //Sets up text to be shown in gui
                showText.text = "Rival Unit Traits\nID: " + unitID + "\nHP: " + updatedHP + "\nStrength: " + strength;
            }

            if (Input.GetKey(KeyCode.Alpha2))
            {
                showText.text = "Rival Unit Traits\nID: " + unitID + "\nHP: " + updatedHP + "\nStrength: " + strength;
            }
        }

        else
        {
            showText.text = "Game Over\nRival Team Wins\nPress Esc. to Exit\nPress Enter to Restart";
        }

    }

    //Subtracts defenders hit points from attackers strength then checks for defeat
    public void recieveDamage(int strength)
    {
        updatedHP -= strength;

        if (updatedHP <= 0)
        {
            Defeat();
        }
    }

    //Makes changes to showcase defeat
    public virtual void Defeat()
    {
        //Show unit as defeated
        GetComponent<Renderer>().material.color = Color.black;

        //Set Unit to defeated
        defeated = true;
    }
}
