using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    //Ground that is normal, used to to show difference from future obsticles like mtns, rivers, forests, etc.
    public bool moveable = true;
    //Ground that any unit is located on
    public bool currentLocation = false;
    //Ground that a unit is about to move to
    public bool newLocation = false;
    //Ground that can be moved to by a unit
    public bool range = false;
    //Ground that a unit can reach to attack
    public bool combat = false;

    //List of all neighbors for ground
    public List<Ground> neighborList = new List<Ground>();
    //List of all neighbors for ground that will not exclude occupied ground
    public List<Ground> combatList = new List<Ground>();
    //Flag that a neighbor has been processed for the above list
    public bool tested = false;
    //Ground that is being processed
    public Ground selected = null;
    //How far away the next bit of ground is
    public int length = 0;

    // Update is called once per frame
    void Update()
    {
        //Makes a units current position blue
        if (currentLocation)
        {
            GetComponent<Renderer>().material.color = Color.blue;
        }

        //Makes the ground a unit is about to move to blue
        else if (newLocation)
        {
            GetComponent<Renderer>().material.color = Color.blue;
        }

        //Makes ground a unit can move to cyan
        else if (range)
        {
            GetComponent<Renderer>().material.color = Color.cyan;
        }

        //Makes the ground a unit can reach to attack another unit red
        else if (combat)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }

        //Makes all ground not involved in the above scenarios clear which would show the default color
        else
        {
            GetComponent<Renderer>().material.color = Color.white;
        }
    }

    //Returns all values to their default for a start of a new turn
    public void Reset()
    {
        //moveable = true;
        currentLocation = false;
        newLocation = false;
        range = false;
        combat = false;

        neighborList.Clear();
        tested = false;
        selected = null;
        length = 0;
    }

    public void findNewRange(float upwardRange)
    {
        //Calls reset for all values for the start of new turn
        Reset();

        //Searches neighbors for selectable ground in all directions as well as how far up/down to look
        testGround(Vector3.forward, upwardRange);
        testGround(Vector3.back, upwardRange);
        testGround(Vector3.right, upwardRange);
        testGround(Vector3.left, upwardRange);
    }

    //Function to find all ground
    public void testGround(Vector3 axis, float upwardRange)
    {
        //Used to look for the center to identify a piece of ground
        Vector3 center = new Vector3(0.25f, (1 + upwardRange)/2, 0.25f);
        //Used to find out if there is ground touching other ground
        Collider[] neighbors = Physics.OverlapBox(transform.position + axis, center);

        //Checks all ground that is touching
        foreach (Collider neighborGround in neighbors)
        {
            //Retrieves the ground that is found
            Ground Ground = neighborGround.GetComponent<Ground>();
            //If ground is found and is movable
            if (Ground != null && Ground.moveable)
            {
                //Makes list of ground for combat without the below restrictions on occupied spaces
                combatList.Add(Ground);

                //Used to find if unit is on ground
                RaycastHit occupied;

                //Checks if a unit is on ground if this is not the case and the above 
                //condition is also met the ground is movable and added to our list
                if (!Physics.Raycast(Ground.transform.position, Vector3.up, out occupied, 1))
                {
                    neighborList.Add(Ground);
                }
            }
        }
    }
}
