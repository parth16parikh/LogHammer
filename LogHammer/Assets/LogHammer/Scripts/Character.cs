using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
    //all the Commands for character
    private Command moveToCommand;
    private Command moveUpCommand;
    private Command moveDownCommand;
    private Command moveRightCommand;
    private Command moveLeftCommand;
    private Command stopMovementCommand;
    //reference to Charactermovement on gameobject
    private CharacterMovement characterMovement;
    //stores current state of the character
    private CharacterState currentCharacterState;
    //store the value of character side (whether character is on right side or left side)
    private CharacterSide sideOfCharacter;
    
    //Enum declared for Character side
    public enum CharacterSide
    {
        Left,
        Right
    }
    //property for character side
    public CharacterSide SideOfCharacter
    {
        get { return sideOfCharacter; }
        set { sideOfCharacter = value; }
    }
    //enum for character state
    public enum CharacterState
    {
        Idle,
        Moving,
        AtBorder,
    }
    //property for storing current state of character
    public CharacterState CurrentCharacterState
    {
        get { return currentCharacterState; }
        set { currentCharacterState = value; }
    }

    // Use this for initialization
    void Start () {
        moveToCommand = new MoveToCommand();
        moveUpCommand = new MoveUpCommand();
        moveDownCommand = new MoveDownCommand();
        moveLeftCommand = new MoveLeftCommand();
        moveRightCommand = new MoveRightCommand();
        stopMovementCommand = new StopMovementCommand();
        characterMovement = GetComponent<CharacterMovement>();
        currentCharacterState = CharacterState.Idle;
    }
	
	// Update is called once per frame
	void Update () {

        //execute commands according to button or mouse press
        if (Input.GetKey(KeyCode.W))
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
        if (Input.GetMouseButton(Constant.INTZERO))
        {
            moveToCommand.Execute(this);
        }
    }

    //detect player collision with seperater and execute stop character command
    public void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == Constant.SEPRATER)
        {
            stopMovementCommand.Execute(this);
        }
    }
    //execute move to command
    public void moveToPosition()
    {
       characterMovement.moveToPosition();
    }
    //execute move up command
    public void moveUp()
    {
        characterMovement.moveUp();
    }
    //execute move down command
    public void moveDown()
    {
        characterMovement.moveDown();
    }
    //execute move left command
    public void moveLeft()
    {
        characterMovement.moveLeft();
    }
    //execute move right command
    public void moveRight()
    {
        characterMovement.moveRight();
    }
    //execute stop movement command
    public void stopMovement()
    {
        characterMovement.stopMovement();
    }
}
