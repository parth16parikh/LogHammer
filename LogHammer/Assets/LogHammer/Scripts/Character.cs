using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    //all the Commands for character
    private Command m_moveTo;
    private Command m_moveUp;
    private Command m_moveDown;
    private Command m_moveRight;
    private Command m_moveLeft;
    private Command m_stopMovement;

    //reference to Charactermovement on gameobject
    private CharacterMovement m_characterMovement;
    //stores current state of the character
    private CharacterState m_currentCharacterState;
    //store the value of character side (whether character is on right side or left side)
    private CharacterSide m_sideOfCharacter;
    private CharacterType m_typeOfCharacter;

    //Enum which will justify that Character will be controlled by human or AI
    public enum CharacterType
    {
        Human,
        AI
    }
    //property for character side
    public CharacterType TypeOfCharacter
    {
        get { return m_typeOfCharacter; }
        set { m_typeOfCharacter = value; }
    }
    //Enum declared for Character side
    public enum CharacterSide
    {
        Left,
        Right
    }
    //property for character side
    public CharacterSide SideOfCharacter
    {
        get { return m_sideOfCharacter; }
        set { m_sideOfCharacter = value; }
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
        get { return m_currentCharacterState; }
        set { m_currentCharacterState = value; }
    }

    // Use this for initialization
    void Start () {
        m_moveTo = new MoveToCommand();
        m_moveUp = new MoveUpCommand();
        m_moveDown = new MoveDownCommand();
        m_moveLeft = new MoveLeftCommand();
        m_moveRight = new MoveRightCommand();
        m_stopMovement = new StopMovementCommand();
        m_characterMovement = GetComponent<CharacterMovement>();
        m_currentCharacterState = CharacterState.Idle;
    }
	
	// Update is called once per frame
	void Update () {
        //execute commands according to button or mouse press
        if(TypeOfCharacter == CharacterType.Human)
        {
            Command nextStepForCharacter = GetCurrentCommand();
            if(nextStepForCharacter != null) { nextStepForCharacter.Execute(this); }
        }
    }

    private Command GetCurrentCommand()
    {
        if (Input.GetKey(KeyCode.W))
        {
            return m_moveUp;
        }
        if (Input.GetKey(KeyCode.A))
        {
            return m_moveLeft;
        }
        if (Input.GetKey(KeyCode.S))
        {
            return m_moveDown;
        }
        if (Input.GetKey(KeyCode.D))
        {
            return m_moveRight;
        }
        if (Input.GetMouseButton(Constant.IntZero))
        {
            return m_moveTo;
        }
        return null;
    }

    //detect player collision with seperater and execute stop character command
    public void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == Constant.Seprater)
        {
            m_stopMovement.Execute(this);
        }
    }

    //execute move to command
    public void MoveToPosition()
    {
       m_characterMovement.MoveToPosition();
    }

    //execute move up command
    public void MoveUp()
    {
        m_characterMovement.MoveUp();
    }

    //execute move down command
    public void MoveDown()
    {
        m_characterMovement.MoveDown();
    }

    //execute move left command
    public void MoveLeft()
    {
        m_characterMovement.MoveLeft();
    }

    //execute move right command
    public void MoveRight()
    {
        m_characterMovement.MoveRight();
    }

    //execute stop movement command
    public void StopMovement()
    {
        m_characterMovement.StopMovement();
    }
}
