using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement0 : SharedMovement
{
    // Start is called before the first frame update
    void Start()
    {
        //Calls initalize from SharedMovement
        initalize();
    }

    // Update is called once per frame
    void Update()
    {
        //Only get info if movement is not happening, unit is not defeated, and it is the units turn
        if (!active && Trait0.defeated == false && turn == Trait0.unitID)
        {
            findMovableGround();
            checkClick();
        }
        
        //Show movement when turn is over
        else if (turn == 1)
        {
            ShowMovement();
        }
    }
    

    void checkClick()
    {
        //When input is a left click
        if (Input.GetMouseButtonUp(0))
        {
            //Get location of the click  
            Ray input = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit clickedLocation;

            //Confirm and store the click
            if (Physics.Raycast(input, out clickedLocation))
            {
                //Only works with ground
                if (clickedLocation.collider.tag == "Ground")
                {
                    //Save the ground that was clicked
                    Ground g = clickedLocation.collider.GetComponent<Ground>();             
                    //Ensure the ground can be moved to and move to that location and switch turn
                    if (g.moveable)
                    {
                        if (g.range)
                        {
                            MoveUnit(g);
                            turn++;
                        }
                    }
                }
            }
        }
    }
}
