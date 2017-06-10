using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Temp : MonoBehaviour
{
    void OnEnable()
    {
        //Subscribe to the Tap event
        InputHandler.Instance.Tap += Instance_Tap;
    }

    void OnDisable()
    {
        //Unsubscribe to the Tap Event
        InputHandler.Instance.Tap -= Instance_Tap;
    }

    //Function that got registered
    private void Instance_Tap(TLTouch currentTouch)
    {
        Debug.Log("Instance_Tap is called");
    }

}
