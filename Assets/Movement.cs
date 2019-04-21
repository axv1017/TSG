using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : SharedMovement
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
        //Only get info if movement is not happening
        if (!active)
        {
            findMovableGround();
            checkClick();
        }
        
        else
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
                    //Ensure the ground can be moved to and move to that location
                    if (g.moveable)
                    {
                        if (g.range)
                        {
                            MoveUnit(g);
                        }
                    }
                }
            }
        }
    }
}

