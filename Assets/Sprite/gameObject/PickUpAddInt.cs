using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpAddInt : MonoBehaviour {

    [HideInInspector]
    public int pick;
    public void PickUp()
    {
        ActivePickUp.PickUpInt += 1;      
        pick = ActivePickUp.PickUpInt;
    }
}
