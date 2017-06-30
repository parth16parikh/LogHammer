using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Enum to indicate the directions of Directional Swipe
/// </summary>
public enum SwipeDirection
{
    None,
    Up,
    Down,
    Left,
    Right
};

/// <summary>
/// This class will handle all the touch inputs via events. It is a singleton class. Register to events by subscribing to them. Note that
/// when you register any functions to any of the public events, it has to have a parameter "TLTouch"
/// </summary>
public class InputHandler : MonoBehaviour
{
    /// <summary>
    /// Single instance of InputHandler
    /// </summary>
    private static InputHandler instance;

    // Whether multi-touch is enabled
    [SerializeField]
    private bool m_enableMultiTouch;

    // Temperory text for testing
    [SerializeField]
    private Text m_text;

    // All the threshold variables
    [SerializeField]
    private float m_doubleTapTimeThreshold = 0.5f;
    [SerializeField]
    private float m_tapRadiusThreshold = 60f;
    [SerializeField]
    private float m_tapHoldTimeThreshold = 0.3f;
    [SerializeField]
    private float m_swipeWidthThreshold = 120f;
    [SerializeField]
    private float m_swipeHoldTimeThreshold = 0.5f;
    [SerializeField]
    private float m_swipeDistanceThreshold = 80f;
    [SerializeField]
    private float m_dragHoldThreshold;
    [SerializeField]
    private float m_holdThresholdMinimum = 0.7f;
    [SerializeField]
    private float m_holdThresholdMaximum = 2f;

    // TLTouch variables to store information regarding current and previous touches
    private TLTouch m_currentTouch;
    private TLTouch m_previousTouch;

    // Declaring a delegate 
    public delegate void InputEvents(TLTouch currentTouch);

    // Declare all the public Events
    private event InputEvents tap;
    private event InputEvents doubleTap;
    private event InputEvents directionalSwipe;
    private event InputEvents generalSwipe;
    private event InputEvents hold;

    // Defining all the properties
    /// <summary>
    /// Defining the Instance property of the instance variable
    /// </summary>
    public static InputHandler Instance
    {
        get
        {
            Debug.Assert(instance != null, "Instance of InputHandler is null");
            return instance;
        }
        private set
        {
            instance = value;
        }
    }

    /// <summary>
    /// Subscribe to get the Tap event. Make sure to subscribe methods with parameter "TLTouch"
    /// </summary>
    public InputEvents Tap
    {
        get { return tap; }
        set { tap = value; }
    }

    /// <summary>
    /// Subscribe to get the Double Tap event. Make sure to subscribe methods with parameter "TLTouch"
    /// </summary>
    public InputEvents DoubleTap
    {
        get { return doubleTap; }
        set { doubleTap = value; }
    }

    /// <summary>
    /// Subscribe to get the Directional Swipe event. Make sure to subscribe methods with parameter "TLTouch"
    /// </summary>
    public InputEvents DirectionalSwipe
    {
        get { return directionalSwipe; }
        set { directionalSwipe = value; }
    }

    /// <summary>
    /// Subscribe to get the General Swipe event. Make sure to subscribe methods with parameter "TLTouch"
    /// </summary>
    public InputEvents GeneralSwipe
    {
        get { return generalSwipe; }
        set { generalSwipe = value; }
    }

    /// <summary>
    /// Subscribe to get the Hold event. Make sure to subscribe methods with parameter "TLTouch"
    /// </summary>
    public InputEvents Hold
    {
        get { return hold; }
        set { hold = value; }
    }
    
    // Called right after the instance of this script is made, and before any other methods of the script
    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    public void Start()
    {
        m_currentTouch = new TLTouch();
        m_previousTouch = new TLTouch();
        if (m_enableMultiTouch)
        { Input.multiTouchEnabled = false; }
    }

    /// <summary>
    /// Calls the tap event. All the methods susbscribed to Tap will be called.
    /// </summary>
    public void OnTap()
    {
        Debug.Assert(Tap != null, "Tap events not subsrcribed");
        if (Tap != null)
        { Tap(m_currentTouch); }
    }

    /// <summary>
    /// Calls the double tap event. All the methods susbscribed to DoubleTap will be called.
    /// </summary>
    public void OnDoubleTap()
    {
        Debug.Assert(DoubleTap != null, "DoubleTap events not subsrcribed");
        if (DoubleTap != null)
        { DoubleTap(m_currentTouch); }
    }

    /// <summary>
    /// Calls the General swipe event. All the methods subscribed to GeneralSwipe will be called.
    /// </summary>
    public void OnGeneralSwipe()
    {
        Debug.Assert(GeneralSwipe != null, "GeneralSwipe events not subsrcribed");
        if (GeneralSwipe != null)
        { GeneralSwipe(m_currentTouch); }
    }

    /// <summary>
    /// Calls the Directional swipe event. All the methods subscribed to DirectionalSwipe will be called.
    /// </summary>
    public void OnDirectionalSwipe()
    {
        Debug.Assert(DirectionalSwipe != null, "DirectionalSwipe events not subsrcribed");
        if (DirectionalSwipe != null)
        { DirectionalSwipe(m_currentTouch); }
    }

    /// <summary>
    /// Calls the Hold event. All the methods subscribed to Hold will be called.
    /// </summary>
    public void OnHold()
    {
        Debug.Assert(Hold != null, "Hold events not subsrcribed");
        if (Hold != null)
        { Hold(m_currentTouch); }
    }

    // Called Every frame
    private void Update()
    {
        // If there is no input, then return
        if (Input.touchCount == 0)
        { return; }

        // If multi touch is on
        if (m_enableMultiTouch)
        { Debug.LogError("Multi touch coming soon!!"); }

        // Detect single touch events
        Touch touch = Input.GetTouch(0);

        // Touch begins
        if (touch.phase == TouchPhase.Began)
        {
            m_currentTouch.SetTouchStartInfo(touch.position, Time.time);
        }

        // Touch ends
        else if (touch.phase == TouchPhase.Ended)
        {
            // Setting the properties in m_currentTouch
            m_currentTouch.SetTouchEndInfo(touch.position, Time.time);

            // Calculating differences needed for carrying out comparisions
            float timeDifferenceBetweenTaps = m_currentTouch.EndTime - m_previousTouch.EndTime;
            float distanceBetweenTaps = Vector2.Distance(m_currentTouch.EndPosition, m_previousTouch.EndPosition);

            // Detect double tap, if any
            if (m_currentTouch.HoldTime < m_tapHoldTimeThreshold && m_currentTouch.HoldDistance <= m_tapRadiusThreshold)
            {
                m_text.text = "Tap event";

                // Single tap event
                OnTap();
            }
            else if (timeDifferenceBetweenTaps <= m_doubleTapTimeThreshold && distanceBetweenTaps <= m_tapRadiusThreshold
                        && m_currentTouch.HoldDistance <= m_tapRadiusThreshold && m_previousTouch.HoldDistance <= m_tapRadiusThreshold)
            {
                m_text.text = "Double Tap";

                // Double tap event
                OnDoubleTap();
            }
            // Detect swipe, if any
            else if (m_currentTouch.HoldTime <= m_swipeHoldTimeThreshold && m_currentTouch.HoldDistance >= m_swipeDistanceThreshold)
            {
                // Store the swipe vector in Swipe Data
                Vector2 swipeData = m_currentTouch.VectorDifferenceBetweenStartAndEnd;

                // General Swipe event
                OnGeneralSwipe();

                // Check whether swipe is horizontal or vertical
                bool swipeIsVertical = Mathf.Abs(swipeData.x) < m_swipeWidthThreshold;
                bool swipeIsHorizontal = Mathf.Abs(swipeData.y) < m_swipeWidthThreshold;

                // Condition for vertical swipe
                if (swipeIsVertical && swipeData.y > 0f)
                {
                    m_text.text = "UP";
                    m_currentTouch.SwipeDirection = SwipeDirection.Up;
                }
                else if (swipeIsVertical && swipeData.y < 0f)
                {
                    m_text.text = "DOWN";
                    m_currentTouch.SwipeDirection = SwipeDirection.Down;
                }
                // Condition for horizontal swipe
                else if (swipeIsHorizontal && swipeData.x > 0f)
                {
                    m_text.text = "RIGHT";
                    m_currentTouch.SwipeDirection = SwipeDirection.Right;
                }
                else if (swipeIsHorizontal && swipeData.x < 0f)
                {
                    m_text.text = "LEFT";
                    m_currentTouch.SwipeDirection = SwipeDirection.Left;
                }
                else
                {
                    m_text.text = "NONE";
                    m_currentTouch.SwipeDirection = SwipeDirection.None;
                }

                //Directional Swipe Event
                OnDirectionalSwipe();
            }
            else if (m_currentTouch.HoldTime >= m_holdThresholdMinimum && m_currentTouch.HoldTime <= m_holdThresholdMaximum 
                        && m_currentTouch.HoldDistance <= m_tapRadiusThreshold)
            {
                m_text.text = "HOLD";

                //Hold event
                OnHold();
            }
            else
            {
                m_currentTouch.SwipeDirection = SwipeDirection.None;
            }

            m_previousTouch = m_currentTouch;
        }
    }
}
