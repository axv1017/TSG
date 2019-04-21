using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement1 : SharedMovement
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
        if (!active && Trait1.defeated == false && turn == Trait1.unitID)
        {
            findMovableGround();
            checkClick();
        }

        //Show movement when turn is over
        else if (turn == 0)
        {
            ShowMovement();
        }
    }

    void checkClick()
    {
        //When input is a right click
        if (Input.GetMouseButtonUp(1))
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
                            turn=0;
                        }
                    }
                }
            }
        }
    }
}
