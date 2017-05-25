using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

    public float clampForce = 8.0f;

    Command moveToCommand;
    Command moveUpCommand;
    Command moveDownCommand;
    Command moveRightCommand;
    Command moveLeftCommand;

    Vector2 tapStartPos = Vector2.zero;
    Vector2 tapCurrentPos = Vector2.zero;
    float tapStartTime = 0.0f;
    float tapEndTime = 0.0f;
    Rigidbody rgdBody;

	// Use this for initialization
	void Start () {
        moveToCommand    = new MoveToCommand();
        moveUpCommand    = new MoveUpCommand();
        moveDownCommand  = new MoveDownCommand();
        moveLeftCommand  = new MoveLeftCommand();
        moveRightCommand = new MoveRightCommand();

        rgdBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        
        if(Input.GetKey(KeyCode.W))
        {
            moveUpCommand.Execute(this);
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveLeftCommand.Execute(this);
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDownCommand.Execute(this);
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveRightCommand.Execute(this);
        }

        if (Input.GetMouseButtonDown(0))
        {
            tapStartPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            tapStartTime = Time.realtimeSinceStartup;
        }
        else if (Input.GetMouseButton(0))
        {
            moveToCommand.Execute(this);
        }

    }

    public void moveToPosition()
    {
        //caculate direction
        tapCurrentPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 dragDirection = (tapStartPos - tapCurrentPos);
        //calculate time
        tapEndTime = Time.realtimeSinceStartup;
        float timedifference = tapEndTime - tapStartTime;

        tapStartPos = tapCurrentPos;
        tapStartTime = tapEndTime;

        Vector3 resultantvelocity = new Vector3(dragDirection.x, 0.0f, dragDirection.y) / timedifference;
        
        if (dragDirection != Vector2.zero && timedifference != 0.0f )
        {
            rgdBody.velocity = Vector3.ClampMagnitude(rgdBody.velocity + resultantvelocity, clampForce);
        }
    }

    public void moveUp()
    {
        rgdBody.velocity = Vector3.ClampMagnitude((rgdBody.velocity + Vector3.back * 8.0f),clampForce);
        Debug.Log("Move up");
    }

    public void moveDown()
    {
        rgdBody.velocity = Vector3.ClampMagnitude((rgdBody.velocity + Vector3.forward * 8.0f), clampForce);
        Debug.Log("Move Down");
    }

    public void moveLeft()
    {
        rgdBody.velocity = Vector3.ClampMagnitude((rgdBody.velocity + Vector3.right * 8.0f), clampForce);
        Debug.Log("Move Left");
    }

    public void moveRight()
    {
        rgdBody.velocity = Vector3.ClampMagnitude((rgdBody.velocity + Vector3.left * 8.0f), clampForce);
        Debug.Log("Move Right");
    }

}
