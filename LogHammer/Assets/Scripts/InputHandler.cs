using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class will handle all the touch inputs via events. It is a singleton class. Register to events by subscribing to them.
public class InputHandler : MonoBehaviour
{
    //declaring a variable instance
    private static InputHandler instance;
    public delegate void Inputs();

    public event Inputs Tap;
    public event Inputs DoubleTaps;
    public event Inputs ZoomIn;
    public event Inputs ZoomOut;
    public event Inputs Drag;
    public event Inputs Swipe;
    public event Inputs Hold;

    private float m_doubleTapThreshold = 10f;

    private Tap m_tapInput;                                   //get the coordinated where the tap took place

    //constructor
    private InputHandler()
    { }

    //defining the Instance property of the instance variable
    public static InputHandler Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new InputHandler();
            }
            return instance;
        }
    }

    public void OnTap()
    {
        //call the tap event. All the methods susbscribed to Tap will be called.
        if (Tap != null)
            Tap();
    }


    public void OnDoubleTap()
    {
        if (DoubleTaps != null)
            DoubleTaps();
    }

    public void OnZoomIn()
    {
        if (ZoomIn != null)
            ZoomIn();
    }

    public void OnZoomOut()
    {
        if (ZoomOut != null)
            ZoomOut();
    }

    private void Update()
    {
        int listSize = Input.touchCount;
        List<Touch> touches;

        touches = new List<Touch>(listSize);

        for(int i = 0; i < listSize; i++)
        {
            touches.Add(Input.GetTouch(i));
        }

        //detect tap
        if (Input.touchCount == 1)
        {
            Debug.Log("Tap event is detected");
            OnTap();
        }

        //detect doubletap, zoom in and zoom out events since all these 
        if (Input.touchCount == 2)
        {
            Touch touchzero = Input.GetTouch(0);
            Touch touchone = Input.GetTouch(1);

            //detect doubletap
            if (Mathf.Abs(Vector3.Distance(touchzero.position, touchone.position)) < m_doubleTapThreshold)
            {
                Debug.Log("Double tap is detected");
                OnDoubleTap();
            }

            //detect zoom in and zoom out
            Vector2 touchzeroInPreviousFrame = touchzero.position - touchzero.deltaPosition;
            Vector2 touchoneInPreviousFrame = touchone.position - touchone.deltaPosition;

            float differenceInPreviousFrame = (touchoneInPreviousFrame - touchzeroInPreviousFrame).magnitude;
            float differenceInThisFrame = (touchone.position - touchzero.position).magnitude;

            if(differenceInThisFrame > differenceInPreviousFrame)
            {
                OnZoomIn();
            }
            else if(differenceInThisFrame < differenceInPreviousFrame)
            {
                OnZoomOut();
            }
        }

        
    }
}
