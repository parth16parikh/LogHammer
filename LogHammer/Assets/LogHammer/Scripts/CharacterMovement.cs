using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {
    //mouse click start position
    private Vector2 tapStartPos = Vector2.zero;
    //mouse's current position when pressed
    private Vector2 tapCurrentPos = Vector2.zero;
    //start time when mouse is pressed
    private float tapStartTime = Constant.ZERO;
    //current time while mosue is still pressed
    private float tapCurrentTime = Constant.ZERO;
    //holds the reference to the rigidbody which is on the character gameobject
    private Rigidbody rgdBody;
    //holds the reference to the Character script which is on the character gameobject
    private Character character;

	// Use this for initialization
	void Start () {
        rgdBody = GetComponent<Rigidbody>();
        character = GetComponent<Character>();
    }
	
	// Update is called once per frame
	void Update () {
        //record mouse start position and start time when mouse button is pressed
        if (Input.GetMouseButtonDown(Constant.INTZERO))
        {
            tapStartPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            tapStartTime = Time.realtimeSinceStartup;
        }

        //If character is not moving then set its state idle
        if (character.CurrentCharacterState == Character.CharacterState.Moving && rgdBody.velocity == Vector3.zero)
        {
            character.CurrentCharacterState = Character.CharacterState.Idle;
        }
    }
 
    //move character to the current mouse position
    public void moveToPosition()
    {
        //set character's state to moving state
        character.CurrentCharacterState = Character.CharacterState.Moving;

        //caculate direction and distance of the mouse
        tapCurrentPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 dragDirection = (tapStartPos - tapCurrentPos);
        //calculate time for mouse to travel that much ditance
        tapCurrentTime = Time.realtimeSinceStartup;
        float timedifference = tapCurrentTime - tapStartTime;

        //record current position as start position
        tapStartPos = tapCurrentPos;
        tapStartTime = tapCurrentTime;

        //calculate the resultant velocity of the character
        Vector3 resultantvelocity = new Vector3(dragDirection.x, Constant.ZERO, dragDirection.y) / timedifference;
        if (dragDirection != Vector2.zero && timedifference != Constant.ZERO)
        {
            rgdBody.velocity = Vector3.ClampMagnitude(rgdBody.velocity + resultantvelocity, Constant.CLAMP_VELOCITY);
        }
    }

    // stop character movement when it collides with seperater
    public void stopMovement()
    {
        character.CurrentCharacterState = Character.CharacterState.AtBorder;
        rgdBody.velocity = Vector3.zero;
    }
    //character should go up when user press W
    public void moveUp()
    {
        if (character.CurrentCharacterState != Character.CharacterState.AtBorder && character.CurrentCharacterState != Character.CharacterState.Moving)
        {
            character.CurrentCharacterState = Character.CharacterState.Moving;
        }
        rgdBody.velocity = Vector3.ClampMagnitude((rgdBody.velocity + Vector3.back * Constant.FORCE_INTENSITY), Constant.CLAMP_VELOCITY);
        Debug.Log("Move Character up");
    }
    //character should go down when user press S
    public void moveDown()
    {
        if (character.CurrentCharacterState != Character.CharacterState.AtBorder && character.CurrentCharacterState != Character.CharacterState.Moving)
        {
            character.CurrentCharacterState = Character.CharacterState.Moving;
        }
        rgdBody.velocity = Vector3.ClampMagnitude((rgdBody.velocity + Vector3.forward * Constant.FORCE_INTENSITY), Constant.CLAMP_VELOCITY);
        Debug.Log("Move Character Down");
    }
    //character should go left when user press A
    public void moveLeft()
    {
        if (character.SideOfCharacter == Character.CharacterSide.Left)
        {
            if (character.CurrentCharacterState != Character.CharacterState.Moving)
            {
                character.CurrentCharacterState = Character.CharacterState.Moving;
            }
        }
        if(character.SideOfCharacter == Character.CharacterSide.Right)
        {
            if (character.CurrentCharacterState == Character.CharacterState.AtBorder)
            {
                return;
            }
            else
            {
                character.CurrentCharacterState = Character.CharacterState.Moving;
            }
        }
        rgdBody.velocity = Vector3.ClampMagnitude((rgdBody.velocity + Vector3.right * Constant.FORCE_INTENSITY), Constant.CLAMP_VELOCITY);
        Debug.Log("Move Character Left");
    }
    //character should go right when user press D
    public void moveRight()
    {
        if (character.SideOfCharacter == Character.CharacterSide.Left)
        {
            if (character.CurrentCharacterState == Character.CharacterState.AtBorder)
            {
                return;
            }
            else
            {
                character.CurrentCharacterState = Character.CharacterState.Moving;
            }
        }
        if (character.SideOfCharacter == Character.CharacterSide.Right)
        {
            if (character.CurrentCharacterState != Character.CharacterState.Moving)
            {
                character.CurrentCharacterState = Character.CharacterState.Moving;
            }
        }
        rgdBody.velocity = Vector3.ClampMagnitude((rgdBody.velocity + Vector3.left * Constant.FORCE_INTENSITY), Constant.CLAMP_VELOCITY);
        Debug.Log("Move Character Right");
    }   
}
