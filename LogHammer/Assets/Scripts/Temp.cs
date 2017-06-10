using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Temp : MonoBehaviour
{
    void OnEnable()
    {
        //Subscribe to the Tap event
        Debug.Log("Inside the OnEnable!");
        InputHandler.Instance.Tap += Instance_Tap;
        Debug.Log("Logging after subscribing");
    }

    void OnDisable()
    {
        //Unsubscribe to the Tap Event
        InputHandler.Instance.Tap -= Instance_Tap;
    }

    //Function that got registered
    public void Instance_Tap(TLTouch currentTouch)
    {
        Debug.Log("Instance_Tap is called");
    }

}
