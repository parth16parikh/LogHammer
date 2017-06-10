using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Declare an enum to indicate the values of Directional Swipe
public enum SwipeDirection
{
    None,
    Up,
    Down,
    Left,
    Right
};

//This class will handle all the touch inputs via events. It is a singleton class. Register to events by subscribing to them. Note that
//when you register any functions to any of the public events, it has a parameter "TLTouch"
public class InputHandler : MonoBehaviour
{
    //declaring a variable instance
    private static InputHandler instance;

    public delegate void InputEvents(TLTouch currentTouch);

    //whether multi-touch is enabled
    public bool m_enableMultiTouch;

    //declare all the public Events
    public event InputEvents Tap;
    public event InputEvents DoubleTaps;
    public event InputEvents DirectionalSwipe;
    public event InputEvents GeneralSwipe;
    public event InputEvents Hold;

    //temperory text for testing
    public Text text;

    //all the public threshold variables
    private float m_doubleTapTimeThreshold = 0.5f;
    private float m_tapRadiusThreshold = 60f;
    private float m_tapHoldTimeThreshold = 0.3f;
    private float m_swipeWidth = 120f;
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
        set
        {
            instance = value;
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    //Called when the object gets created
    public void Start()
    {
        m_currentTouch = new TLTouch();
        m_previousTouch = new TLTouch();
        if (m_enableMultiTouch)
            Input.multiTouchEnabled = false;
    }

    //calling the tap event. All the methods susbscribed to Tap will be called.
    public void OnTap()
    {
        Debug.Log("Inside the OnTap()");
        if (Tap != null)
        {
            Debug.Log("Inside the IF condition");
            Tap(m_currentTouch);
        }
    }

    ////calling the double tap event. All the methods susbscribed to DoubleTap will be called.
    //public void OnDoubleTap(TLTouch currentTouch)
    //{
    //    if (DoubleTaps != null)
    //        DoubleTaps(currentTouch);
    //}

    ////calling the General swipe event. All the methods subscribed to GeneralSwipe will be called.
    //public void OnGeneralSwipe(TLTouch currentTouch)
    //{
    //    if (GeneralSwipe != null)
    //        GeneralSwipe(currentTouch);
    //}

    ////calling the Directional swipe event. All the methods subscribed to DirectionalSwipe will be called.
    //public void OnDirectionalSwipe(TLTouch currentTouch)
    //{
    //    if (DirectionalSwipe != null)
    //        DirectionalSwipe(currentTouch);
    //}

    ////calling the Hold event. All the methods subscribed to Hold will be called.
    //public void OnHold(TLTouch currentTouch)
    //{
    //    if (Hold != null)
    //        OnHold(currentTouch);
    //}

    //Called Every frame
    private void Update()
    {

        if (Input.touchCount == 0)
            return;

        //Detect single touch events
        Touch touch = Input.GetTouch(0);

        //touch begins...
        if (touch.phase == TouchPhase.Began)

        {
            m_currentTouch.SetTouchStartInfo(touch.position, Time.time);
        }

        //touch ends...
        else if (touch.phase == TouchPhase.Ended)
        {
            //setting the properties in m_currentTouch
            m_currentTouch.SetTouchEndInfo(touch.position, Time.time);

            //calculating differences needed for carrying out comparisions
            float timeDifferenceBetweenTaps = m_currentTouch.EndTime - m_previousTouch.EndTime;
            float distanceBetweenTaps = Vector2.Distance(m_currentTouch.EndPosition, m_previousTouch.EndPosition);

            //detect double tap, if any
            if (m_currentTouch.HoldTime < m_tapHoldTimeThreshold && m_currentTouch.HoldDistance <= m_tapRadiusThreshold)
            {
                text.text = "Tap event";

                //Single tap event
                OnTap();
            }
            else if (timeDifferenceBetweenTaps <= m_doubleTapTimeThreshold && distanceBetweenTaps <= m_tapRadiusThreshold && m_currentTouch.HoldDistance <= m_tapRadiusThreshold && m_previousTouch.HoldDistance <= m_tapRadiusThreshold)
            {
                text.text = "Double Tap";

                //Double tap event
                //OnDoubleTap(m_currentTouch);
            }
            //detect swipe, if any
            else if (m_currentTouch.HoldTime <= m_swipeHoldTimeThreshold && m_currentTouch.HoldDistance >= m_swipeDistanceThreshold)
            {
                Vector2 swipeData = m_currentTouch.HoldVector;

                //General Swipe event
                //OnGeneralSwipe(m_currentTouch);

                bool swipeIsVertical = Mathf.Abs(swipeData.x) < m_swipeWidth;
                bool swipeIsHorizontal = Mathf.Abs(swipeData.y) < m_swipeWidth;

                if (swipeIsVertical && swipeData.y > 0f)
                {
                    text.text = "UP";
                    m_currentTouch.PSwipeDirection = SwipeDirection.Up;
                }
                else if (swipeIsVertical && swipeData.y < 0f)
                {
                    text.text = "DOWN";
                    m_currentTouch.PSwipeDirection = SwipeDirection.Down;
                }
                else if (swipeIsHorizontal && swipeData.x > 0f)
                {
                    text.text = "RIGHT";
                    m_currentTouch.PSwipeDirection = SwipeDirection.Right;
                }
                else if (swipeIsHorizontal && swipeData.x < 0f)
                {
                    text.text = "LEFT";
                    m_currentTouch.PSwipeDirection = SwipeDirection.Left;
                }
                else
                {
                    text.text = "NONE";
                    m_currentTouch.PSwipeDirection = SwipeDirection.None;
                }

                //Directional Swipe Event
               // OnDirectionalSwipe(m_currentTouch);
            }
            else if (m_currentTouch.HoldTime >= m_holdThresholdMinimum && m_currentTouch.HoldTime <= m_holdThresholdMaximum && m_currentTouch.HoldDistance <= m_tapRadiusThreshold)
            {
                text.text = "HOLD";

                //Hold event
                //OnHold(m_currentTouch);
            }
            else
            {
                m_currentTouch.PSwipeDirection = SwipeDirection.None;
            }

            //if (m_currentTouch.m_holdTime >= m_dragHoldThreshold && Vector2.Distance(m_c))
            m_previousTouch = new TLTouch(m_currentTouch);
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
