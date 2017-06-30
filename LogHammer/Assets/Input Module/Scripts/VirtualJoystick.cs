using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// This class will handle the inputs and the movement display of the joystick
/// </summary>
public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    /// <summary>
    /// Single instance of VirtualJoystick
    /// </summary>
    private static VirtualJoystick instance;

    // The images of the background and the joystick
    private Image m_backGround;
    private Image m_joyStick;

    /// <summary>
    /// The input of the joystick
    /// </summary>
    private Vector3 m_inputData;

    // Declare a delegate for the event                             
    public delegate void JoystickInput(Vector3 input);

    // Event associated with the delegate            
    private event JoystickInput joystickInputEvent;                     

    /// <summary>
    /// The Instance property of VirtualJoystick
    /// </summary>
    public static VirtualJoystick Instance
    {
        get
        {
            Debug.Assert(instance != null, "Instance of Virtual joystick is null");
            return instance;
        }
        private set
        {
            instance = value;
        }
    }

    /// <summary>
    /// Subscribe to this event if you have something to do on the Joystick input event
    /// </summary>
    public JoystickInput JoystickInputEvent
    {
        get { return joystickInputEvent; }
        set { joystickInputEvent = value; }
    }

    // Called right after the instance of this script is made, and before any other methods of the script
    private void Awake()
    {
        Instance = this;
        m_backGround = GetComponent<Image>();
        m_joyStick = transform.GetChild(0).GetComponent<Image>();
    }

    // Use this for initialization
    private void Start()
    { } 

    // Is called when a drag event occurs
    public void OnDrag(PointerEventData eventData)
    {
        // This is the position of the current position of the drag touch with respect to the rect transform in the first argument
        // of the ScreenPointToLocalPointInRectangle below i.e. the background image here
        Vector2 positionWithRespectToBackgroundImage;

        // Checking whether there is any touch inside the rect transform's area
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(m_backGround.rectTransform,
            eventData.position, eventData.pressEventCamera, out positionWithRespectToBackgroundImage))
        {
            // Convert position within 0 to 1 (w.r.t. the background image)
            positionWithRespectToBackgroundImage.x = 
                (positionWithRespectToBackgroundImage.x / m_backGround.rectTransform.sizeDelta.x);
            positionWithRespectToBackgroundImage.y =
                (positionWithRespectToBackgroundImage.y / m_backGround.rectTransform.sizeDelta.y);

            // Convert the input into proper form: 0 in middle of the backgound, and upto magnitude 1 on each sides
            m_inputData = 
                new Vector3(positionWithRespectToBackgroundImage.x * 2 + 1, 0f, positionWithRespectToBackgroundImage.y * 2 - 1);

            // If while dragging, magnitude crosses 1, it remains 1
            m_inputData = (m_inputData.magnitude > 1.0f) ? m_inputData.normalized : m_inputData;

            // Move the joystick
            m_joyStick.rectTransform.anchoredPosition = new Vector3(m_inputData.x * m_backGround.rectTransform.sizeDelta.x / 3, 
                    m_inputData.z * m_backGround.rectTransform.sizeDelta.y / 3);

            // Call the event
            if (JoystickInputEvent != null)
            {
                JoystickInputEvent(m_inputData);
            }
        }
    }

    // This event is called when pointer goes down
    public void OnPointerDown(PointerEventData eventData)
    {
        // Calls drag event
        OnDrag(eventData);
    }

    // This event is called when the pointer goes up
    public void OnPointerUp(PointerEventData eventData)
    {
        // Resets input to zero
        m_inputData = Vector3.zero;

        // Resets the joystick position to zero i.e. the center
        m_joyStick.rectTransform.anchoredPosition = m_inputData;
    }
}