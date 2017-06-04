using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Text text;

    private float m_doubleTapTimeThreshold = 0.5f;
    private float m_doubleTapDistanceThreshold = 60f;

    private TLTouch m_currentTouch;
    private TLTouch m_previousTouch;

    private float m_tapThreshold = 0.5f;

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

    private void Start()
    {
        m_currentTouch = new TLTouch();
        m_previousTouch = new TLTouch();
    }

    private void Update()
    {
        //detect tap
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                m_currentTouch.m_touchStartPosition = touch.position;
                m_currentTouch.m_startTime = Time.time;
                Debug.Log("Start Time " + m_currentTouch.m_startTime);
            }
            if (touch.phase == TouchPhase.Ended)
            {
                m_currentTouch.m_touchEndPosition = touch.position;
                m_currentTouch.m_endTime = Time.time;
                Debug.Log("End Time " + m_currentTouch.m_endTime);
                m_currentTouch.m_holdTime = m_currentTouch.m_endTime - m_currentTouch.m_startTime;
                Debug.Log("Hold Time " + m_currentTouch.m_holdTime);
                if (m_currentTouch.m_holdTime < m_tapThreshold)
                {
                    text.text = "Tap event";
                    OnTap();
                }

                float diffBtwnTaps = m_currentTouch.m_startTime - m_previousTouch.m_endTime;
                float diffInDistanceBtwnTaps = Vector2.Distance(m_currentTouch.m_touchStartPosition, m_previousTouch.m_touchStartPosition);

                Debug.Log("Difference 1 " + diffBtwnTaps + " Difference 2 : " + diffInDistanceBtwnTaps);

                if (diffBtwnTaps <= m_doubleTapTimeThreshold && diffInDistanceBtwnTaps <= m_doubleTapDistanceThreshold)
                {
                    text.text = "Double Tap";
                    OnDoubleTap();
                }
                m_previousTouch = new TLTouch(m_currentTouch);
            }


        }

        ////detect doubletap, zoom in and zoom out events since all these 
        //if (Input.touchCount >= 2)
        //{
        //    Touch touchzero = Input.GetTouch(0);
        //    Touch touchone = Input.GetTouch(1);

        //    //detect zoom in and zoom out
        //    Vector2 touchzeroInPreviousFrame = touchzero.position - touchzero.deltaPosition;
        //    Vector2 touchoneInPreviousFrame = touchone.position - touchone.deltaPosition;

        //    float differenceInPreviousFrame = (touchoneInPreviousFrame - touchzeroInPreviousFrame).magnitude;
        //    float differenceInThisFrame = (touchone.position - touchzero.position).magnitude;

        //    if(differenceInThisFrame > differenceInPreviousFrame)
        //    {
        //        OnZoomIn();
        //    }
        //    else if(differenceInThisFrame < differenceInPreviousFrame)
        //    {
        //        OnZoomOut();
        //    }
        //}


    }
}
