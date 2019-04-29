using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedTrait : MonoBehaviour
{
    //Traits that will be shared by multiple units
    public static int team0 = 1;
    public static int team1 = 1;

    //Stores units and distance to use to determine
    public static float distance;
    public GameObject Unit;
    public GameObject RivalUnit;

    private void Update()
    {
        // Find distance between 2 units to determine if combat is possible
         distance = Vector3.Distance(Unit.transform.position, RivalUnit.transform.position);
    }
}
