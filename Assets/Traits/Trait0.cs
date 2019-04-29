using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Trait0 : MonoBehaviour
{
    //Self explanitory
    public static int unitID = 0;
    //Hit Points a unit starts with
    public int startHP = 15;
    //Hit Points as they change during the game, get anywhere but only set here
    public int updatedHP { get; private set; }
    //How much damage a unit can do
    public static int strength = 12;
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
        if (SharedTrait.team0 == 0)
        {
            showText.text = "Game Over\nRed Team Wins\nPress Esc. to Exit\nPress Enter to Restart";
        }

        if (Trait1.defeated == false)
        {
            if (Input.GetKey(KeyCode.Keypad1))
            {
                //If in combat range and the other units turn
                if (SharedTrait.distance == 1 && SharedMovement.turn == (Trait1.unitID + 0.5))
                {
                    //Send info for combat into function
                    recieveDamage(Trait1.strength);
                    //Sets up text to be shown in gui
                    showText.text = "Token " + (unitID + 1) + " Traits\nHP: " + updatedHP + "\nStrength: " + strength;

                    SharedMovement.turn=0;
                }
            }

            if (Input.GetKey(KeyCode.Alpha1))
            {
                showText.text = "Token " + (unitID + 1) + " Traits\nHP: " + updatedHP + "\nStrength: " + strength;
            }

            if (Input.GetKey(KeyCode.Space))
            {
                showText.text = "Press a non-keypad number to display that token's traits \n\nPress a keypad number to attack that token when able\n\nPress 0 on the keypad to end a turn without attacking";
            }
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

        //Remove 1 from the count of the team
        SharedTrait.team0--;
    }
}
