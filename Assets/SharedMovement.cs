using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedMovement : MonoBehaviour
{
    //List of ground that can be moved to, used to make changes every turn
    List<Ground> moveableGround = new List<Ground>();
    //Order that the ground is moved through
    Stack<Ground> order= new Stack<Ground>();

    //Array of all ground
    GameObject[] Ground;
    //Ground that unit starts on
    Ground selectedGround;

    //Is movement currently happening
    public bool active = false;
    //How many spaces a unit can move per turn
    public int unitRange = 2;
    //How far up (and down) to search for ground
    public float upwardRange = 2;
    //Units position on top of ground
    float stance = 0;
    //Whose turn it is
    public static int turn = 0;

    /* 
        Used in error in ShowMovement

    public float pace = 2;
    Vector3 speed = new Vector3();
    Vector3 direction = new Vector3();
    */


    protected void initalize()
    {
        //Finds all ground by tag and adds it to the array for ground
        Ground = GameObject.FindGameObjectsWithTag("Ground");

        //Where the unit should be positioned on top of ground
        stance = GetComponent<Collider>().bounds.extents.y;
    }

    //Finds then sets the ground our unit is on top of
    public void getSelectedGround()
    {
        selectedGround = getOccupiedGround(gameObject);
        selectedGround.currentLocation = true;
    }

    //Find all ground that has a unit on top
    public Ground getOccupiedGround(GameObject occupied)
    {
        //Identifies that something is on top of the ground
        RaycastHit touch;
        //Ground that has something on top of it
        Ground Ground = null;

        //Identifies and sets all ground that has a unit on top
        if (Physics.Raycast(occupied.transform.position, Vector3.down, out touch, 1))
        {
            Ground = touch.collider.GetComponent<Ground>();
        }
        return Ground;
    }


    public void makeGroundList()
    {
        //Goes through all ground
        foreach (GameObject Ground in Ground)
        {
            //Finds the neighbors for each ground by using the tag and function in the ground script
            Ground g = Ground.GetComponent<Ground>();
            g.findNewRange(upwardRange);
        }
    }

    public void findMovableGround()
    {
        //Gets the selected ground and the list of neighbors
        makeGroundList();
        getSelectedGround();

        //Going through the ground
        Queue <Ground> action = new Queue<Ground>();

        //Sends selected ground through queue and specifies not to go through it again
        action.Enqueue(selectedGround);
        selectedGround.tested = true;
        //selectedGround.selected = null;

        //Goes through ground while some can go through the queue
        while (action.Count > 0)
        {
            //Gets ground at top of queue
            Ground g = action.Dequeue();

            //Adds current ground to the needed list and sets trait to change color
            moveableGround.Add(g);
            g.range = true;

            //Goes through only when within range
            if (g.length < unitRange)
            {
                //Adds neighbors to queue
                foreach (Ground Ground in g.neighborList)
                {
                    //Ensures ground does not go through more than once
                    if (!Ground.tested)
                    {
                        //Tracks ground
                        Ground.selected = g;
                        //Specify that we have looked at this ground once
                        Ground.tested = true;
                        //Adds 1 to distance from previous ground
                        Ground.length = 1 + g.length;
                        //Adds next neihbor to the queue
                        action.Enqueue(Ground);
                    }
                }
            }
        }
    }

    public void MoveUnit(Ground Ground)
    {
        //Clears data from past movements
        order.Clear();
        //Sets ground as new location, changes color to showcase
        Ground.newLocation = true;
        //Sets unit as actively moving
        active = true;
        //Ground next to new location
        Ground neighbor = Ground;

        //Go through neighbors until we end at the unit 
        while (neighbor != null)
        {
            order.Push(neighbor);
            neighbor = neighbor.selected;
        }
    }

    public void ShowMovement()
    {
        //Move when ground is in the stack
        if (order.Count > 0)
        {
            //Top of the stack
            Ground g = order.Peek();
            //Tracks the next location for the unit
            Vector3 newLocation = g.transform.position;

            //Ensures unit stands on top of ground when moved
            newLocation.y += stance + g.GetComponent<Collider>().bounds.extents.y;

            /*
             NOT WORKING: Unit will jump to selected space rather 
             than being shown moving in the spaces inbetween
             
            if (Vector3.Distance(transform.position, newLocation) >= 0.05f)
            {
                CalculateDirection(newLocation);
                SetSpeed();

                transform.forward = direction;
                transform.position = speed * Time.deltaTime;
            }

            else
            {
                transform.position = newLocation;
                order.Pop();
            }
            */

            //Unit physically moving to new location and it being processed in the stack
            transform.position = newLocation;
            order.Pop();
        }

        //For when movement ends, clears list and changes movement activity
        else
        {
            DeleteMoveableGround();
            active = false;
        }
    }

    //Clears list and restore default values
    protected void DeleteMoveableGround()
    {
        if (selectedGround != null)
        {
            selectedGround.currentLocation = false;
            selectedGround = null;
        }

        foreach (Ground Ground in moveableGround)
        {
            Ground.Reset();
        }

        moveableGround.Clear();
    }

    /* 
        Used in error in ShowMovement

    void CalculateDirection(Vector3 newLocation)
    {
        direction = newLocation - transform.position;
        direction.Normalize();
    }

    void SetSpeed()
    {
        speed = direction * pace;
    }
    */
}
