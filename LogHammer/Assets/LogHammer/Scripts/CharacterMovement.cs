using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {
    
    Vector2 tapStartPos = Vector2.zero;
    Vector2 tapCurrentPos = Vector2.zero;
    float tapStartTime = Constant.ZERO;
    float tapEndTime = Constant.ZERO;
    Rigidbody rgdBody;
    Character character;

	// Use this for initialization
	void Start () {
        rgdBody = GetComponent<Rigidbody>();
        character = GetComponent<Character>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(Constant.INTZERO))
        {
            tapStartPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            tapStartTime = Time.realtimeSinceStartup;
        }

        if (character.CurrentCharacterState != Character.CharacterState.Idle && character.CurrentCharacterState != Character.CharacterState.AtBorder && rgdBody.velocity == Vector3.zero)
        {
            character.CurrentCharacterState = Character.CharacterState.Idle;
        }
    }
 
    public void moveToPosition()
    {
        character.CurrentCharacterState = Character.CharacterState.Moving;

        //caculate direction
        tapCurrentPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 dragDirection = (tapStartPos - tapCurrentPos);
        //calculate time
        tapEndTime = Time.realtimeSinceStartup;
        float timedifference = tapEndTime - tapStartTime;

        tapStartPos = tapCurrentPos;
        tapStartTime = tapEndTime;

        Vector3 resultantvelocity = new Vector3(dragDirection.x, Constant.ZERO, dragDirection.y) / timedifference;
        if (dragDirection != Vector2.zero && timedifference != Constant.ZERO)
        {
            rgdBody.velocity = Vector3.ClampMagnitude(rgdBody.velocity + resultantvelocity, Constant.CLAMP_VELOCITY);
        }
    }

    public void stopMovement()
    {
        character.CurrentCharacterState = Character.CharacterState.AtBorder;
        rgdBody.velocity = Vector3.zero;
    }

    public void moveUp()
    {
        if (character.CurrentCharacterState != Character.CharacterState.AtBorder && character.CurrentCharacterState != Character.CharacterState.Moving)
        {
            character.CurrentCharacterState = Character.CharacterState.Moving;
        }
        rgdBody.velocity = Vector3.ClampMagnitude((rgdBody.velocity + Vector3.back * Constant.FORCE_INTENSITY), Constant.CLAMP_VELOCITY);
        Debug.Log("Move Character up");
    }

    public void moveDown()
    {
        if (character.CurrentCharacterState != Character.CharacterState.AtBorder && character.CurrentCharacterState != Character.CharacterState.Moving)
        {
            character.CurrentCharacterState = Character.CharacterState.Moving;
        }
        rgdBody.velocity = Vector3.ClampMagnitude((rgdBody.velocity + Vector3.forward * Constant.FORCE_INTENSITY), Constant.CLAMP_VELOCITY);
        Debug.Log("Move Character Down");
    }

    public void moveLeft()
    {
        if (character.CurrentCharacterState != Character.CharacterState.Moving)
        {
            character.CurrentCharacterState = Character.CharacterState.Moving;
        }
        rgdBody.velocity = Vector3.ClampMagnitude((rgdBody.velocity + Vector3.right * Constant.FORCE_INTENSITY), Constant.CLAMP_VELOCITY);
        Debug.Log("Move Character Left");
    }

    public void moveRight()
    {
        if (character.CurrentCharacterState == Character.CharacterState.AtBorder)
        {
            return; 
        }
        else
        {
            character.CurrentCharacterState = Character.CharacterState.Moving;
        }
        rgdBody.velocity = Vector3.ClampMagnitude((rgdBody.velocity + Vector3.left * Constant.FORCE_INTENSITY), Constant.CLAMP_VELOCITY);
        Debug.Log("Move Character Right");
    }   
}
