using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This class will handle all the touch inputs via events. It is a singleton class. Register to events by subscribing to them.
public class InputHandler : MonoBehaviour
{
    public enum SwipeDirection
    {
        NONE,
        UP,
        DOWN,
        LEFT,
        RIGHT
    };

    //declaring a variable instance
    private static InputHandler instance;
    public delegate void Inputs();

    //whether multi-touch is enabled
    public bool EnableMultiTouch;

    //declare all the public Events
    public event Inputs Tap;
    public event Inputs DoubleTaps;
    public event Inputs Drag;
    public event Inputs Swipe;
    public event Inputs Hold;

    //temperory text for testing
    public Text text;

    //all the public threshold variables
    private float m_doubleTapTimeThreshold = 0.5f;
    private float m_tapRadiusThreshold = 60f;
    private float m_tapHoldTimeThreshold = 0.3f;
    private float m_swipeWidth = 60f;
    private float m_swipeHoldTimeThreshold = 0.5f;
    private float m_swipeDistanceThreshold = 80f;
    private float m_dragHoldThreshold;
    private float m_holdThresholdMinimum = 0.7f;
    private float m_holdThresholdMaximum = 2f;

    //TLTouch variables to store information regarding current and previous touches
    private TLTouch m_currentTouch;
    private TLTouch m_previousTouch;

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

    //call the tap event. All the methods susbscribed to Tap will be called.
    public void OnTap()
    {
        if (Tap != null)
            Tap();
    }

    //call the double tap event. All the methods susbscribed to DoubleTap will be called.
    public void OnDoubleTap()
    {
        if (DoubleTaps != null)
            DoubleTaps();
    }

    public void Start()
    {
        m_currentTouch = new TLTouch();
        m_previousTouch = new TLTouch();
    }

    //Called Every frame
    private void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                m_currentTouch.SetTouchStartInfo(touch.position, Time.time);
            }

            if (touch.phase == TouchPhase.Ended)
            {
                m_currentTouch.SetTouchEndInfo(touch.position, Time.time);

                float timeDifferenceBetweenTaps = m_currentTouch.EndTime - m_previousTouch.EndTime;
                float distanceBetweenTaps = Vector2.Distance(m_currentTouch.EndPosition, m_previousTouch.EndPosition);
                
                if (timeDifferenceBetweenTaps <= m_doubleTapTimeThreshold && distanceBetweenTaps <= m_tapRadiusThreshold && m_currentTouch.HoldDistance <= m_tapRadiusThreshold && m_previousTouch.HoldDistance <= m_tapRadiusThreshold)
                {
                    text.text = "Double Tap";
                    OnDoubleTap();
                }
                else if (m_currentTouch.HoldTime < m_tapHoldTimeThreshold && m_currentTouch.HoldDistance <= m_tapRadiusThreshold)
                {
                    text.text = "Tap event";
                    OnTap();
                }
                if (m_currentTouch.HoldTime <= m_swipeHoldTimeThreshold && m_currentTouch.HoldDistance >= m_swipeDistanceThreshold)
                {
                    Vector2 swipeData = m_currentTouch.HoldVector;

                    bool swipeIsVertical = Mathf.Abs(swipeData.x) < m_swipeWidth;
                    bool swipeIsHorizontal = Mathf.Abs(swipeData.y) < m_swipeWidth;

                    if (swipeData.y > 0f && swipeIsVertical)
                        text.text = "UP";

                    if (swipeData.y < 0f && swipeIsVertical)
                        text.text = "DOWN";

                    if (swipeData.x > 0f && swipeIsHorizontal)
                        text.text = "RIGHT";

                    if (swipeData.x < 0f && swipeIsHorizontal)
                        text.text = "LEFT";
                }

                if (m_currentTouch.HoldTime >= m_holdThresholdMinimum && m_currentTouch.HoldTime <= m_holdThresholdMaximum && m_currentTouch.HoldDistance <= m_tapRadiusThreshold)
                {
                    text.text = "HOLD";
                }
                //if(m_currentTouch.m_holdTime >= m_dragHoldThreshold && Vector2.Distance(m_c))
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
