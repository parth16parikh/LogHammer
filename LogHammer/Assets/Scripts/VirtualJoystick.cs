using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//This class will handle the inputs and the mvement display of the joystick
public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    //The images of the background and the joystick
    private Image backGround;
    private Image joyStick;

    //The input of the joystick
    private Vector3 inputData;

    // Use this for initialization
    private void Start()
    {
        backGround = GetComponent<Image>();
        joyStick = transform.GetChild(0).GetComponent<Image>();
    }

    //Is called when a drag event occurs
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(backGround.rectTransform, eventData.position, eventData.pressEventCamera, out position))
        {
            //convert position within 0 to 1 (w.r.t. the backgorund image
            position.x = (position.x / backGround.rectTransform.sizeDelta.x);
            position.y = (position.y / backGround.rectTransform.sizeDelta.y);

            //convert the input into proper form: 0 in middle of the backgound, and upto magnitude 1 on each sides
            inputData = new Vector3(position.x*2 + 1, 0f, position.y*2 - 1);

            //if while dragging, magnitude crosses 1, it remains 1
            inputData = (inputData.magnitude > 1.0f) ? inputData.normalized : inputData;

            //move the joystick
            joyStick.rectTransform.anchoredPosition =
                new Vector3(inputData.x * backGround.rectTransform.sizeDelta.x / 3, inputData.z * backGround.rectTransform.sizeDelta.y /3 );
        } 
    }

    //this event is called when pointer goes down
    public void OnPointerDown(PointerEventData eventData)
    {
        //calls drag event
        OnDrag(eventData);
    }

    //this event is called when the pointer goes up
    public void OnPointerUp(PointerEventData eventData)
    {
        //resets input to zero
        inputData = Vector3.zero;

        //resets the joystick position to zero i.e. the center
        joyStick.rectTransform.anchoredPosition = inputData;
    }

    //called every frame
    public void Update()
    {
        //move the ball when we have some magnitude of input
        if(inputData.magnitude > 0f)
        {
            BallMovement.Instance.MoveBall(inputData);
        }
    }
}
