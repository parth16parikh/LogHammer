using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp2 : MonoBehaviour
{
    void OnEnable()
    {
        InputHandler.Instance.Tap += Instance_Tap;
    }

    void OnDisable()
    {
        InputHandler.Instance.Tap -= Instance_Tap;
    }

    private void Instance_Tap()
    {
        Debug.Log("Instance_Tap from Temp2");
    }
}
