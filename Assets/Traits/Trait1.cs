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
        if (SharedTrait.team1 == 0)
        {
            showText.text = "Game Over\nPlayer Team Wins\nPress Esc. to Exit\nPress Enter to Restart";
        }

        if (Trait0.defeated == false)
        {
            if (Input.GetKey(KeyCode.Keypad2))
            {
                //If in combat range and the other units turn
                if (SharedTrait.distance == 1 && SharedMovement.turn == (Trait0.unitID + 0.5))
                {
                    //Send info for combat into function
                    recieveDamage(Trait0.strength);
                    //Sets up text to be shown in gui
                    showText.text = "Unit " + unitID + " Traits\nHP: " + updatedHP + "\nStrength: " + strength;

                    SharedMovement.turn+=0.5f;
                }
            }

            if (Input.GetKey(KeyCode.Alpha2))
            {
                showText.text = "Unit " + unitID + " Traits\nHP: " + updatedHP + "\nStrength: " + strength;
            }

            if (Input.GetKey(KeyCode.Space))
            {
                showText.text = "Press 1 for Player(Blue) Traits\n\nPress 2 for Rival(Red) Traits\n\nPress a keypad number to deal damage to the corresponding token\n\nPress 0 on the keypad to end turn after moving\n\nClick a highlighted space to move there\n\nPress Esc.to Exit\n\nPress Enter to Restart\n\nPress space to show this message again";
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
        SharedTrait.team1--;
    }
}
