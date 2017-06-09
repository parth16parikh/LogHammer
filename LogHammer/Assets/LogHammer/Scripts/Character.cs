using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    private Command moveToCommand;
    private Command moveUpCommand;
    private Command moveDownCommand;
    private Command moveRightCommand;
    private Command moveLeftCommand;
    private Command stopMovementCommand;
    private CharacterMovement characterMovement;
    private CharacterState currentCharacterState;
    private CharacterSide currentSideOfCharacter;
    
    public enum CharacterSide
    {
        Left,
        Right
    }

    public CharacterSide CurrentSideOfCharacter
    {
        get { return currentSideOfCharacter; }
    }

    public enum CharacterState
    {
        Idle,
        Moving,
        AtBorder,
    }
    public CharacterState CurrentCharacterState
    {
        get { return currentCharacterState; }
        set
        {
            currentCharacterState = value;
        }
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
        currentSideOfCharacter = CharacterSide.Left;
        currentCharacterState = CharacterState.Idle;
    }
	
	// Update is called once per frame
	void Update () {
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

    public void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == Constant.SEPRATER)
        {
            stopMovementCommand.Execute(this);
        }
    }

    public void moveToPosition()
    {
       characterMovement.moveToPosition();
    }

    public void moveUp()
    {
        characterMovement.moveUp();
    }

    public void moveDown()
    {
        characterMovement.moveDown();
    }

    public void moveLeft()
    {
        characterMovement.moveLeft();
    }

    public void moveRight()
    {
        characterMovement.moveRight();
    }

    public void stopMovement()
    {
        characterMovement.stopMovement();
    }
}
