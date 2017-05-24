using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

    public float movementSpeed = 100.0f;
    Command moveTo;
    Vector2 tapStartPos = Vector2.zero;
    Vector2 tapCurrentPos = Vector2.zero;
    CharacterController characterController;
	// Use this for initialization
	void Start () {
        moveTo = new MoveToCommand();
	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetMouseButtonDown(0))
        {
            tapStartPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
		else if(Input.GetMouseButton(0))
        {
            moveTo.Execute(this);
        }
	}

    public void moveToPosition()
    {
        tapCurrentPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 drag = (tapCurrentPos - tapStartPos)/movementSpeed;
        tapStartPos = tapCurrentPos;
        if (drag != Vector2.zero)
        {
            transform.position = transform.position - new Vector3(drag.x, 0.0f, drag.y);
            //characterController.Move(new Vector3(drag.x, 0.0f, drag.y));
        }
    }

}
