using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private Image backGround;
    private Image joyStick;
    private Vector3 inputData;

    private void Start()
    {
        backGround = GetComponent<Image>();
        joyStick = transform.GetChild(0).GetComponent<Image>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(backGround.rectTransform, eventData.position, eventData.pressEventCamera, out position))
        {
            position.x = (position.x / backGround.rectTransform.sizeDelta.x);
            position.y = (position.y / backGround.rectTransform.sizeDelta.y);

            inputData = new Vector3(position.x*2 + 1, 0f, position.y*2 - 1);

            inputData = (inputData.magnitude > 1.0f) ? inputData.normalized : inputData;
            //move the joystick
            joyStick.rectTransform.anchoredPosition =
                new Vector3(inputData.x * backGround.rectTransform.sizeDelta.x / 3, inputData.z * backGround.rectTransform.sizeDelta.y /3 );
        } 
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputData = Vector3.zero;
        joyStick.rectTransform.anchoredPosition = inputData;
    }

    public void Update()
    {
        if(inputData.magnitude > 0f)
        {
            BallMovement.Instance.MoveBall(inputData);
        }
    }
}
