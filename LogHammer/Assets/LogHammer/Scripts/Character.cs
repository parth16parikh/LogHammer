using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    //Enum which will justify that Character will be controlled by human or AI
    public enum CharacterType
    {
        Human,
        AI
    }
    //Enum declared for Character side
    public enum CharacterSide
    {
        Left,
        Right
    }
    //enum for character state
    public enum CharacterState
    {
        Idle,
        Moving,
        AtBorder,
    }

    //all the Commands for character
    private Command m_moveTo;
    private Command m_moveUp;
    private Command m_moveDown;
    private Command m_moveRight;
    private Command m_moveLeft;
    private Command m_stopMovement;

    //reference to Charactermovement on gameobject
    private CharacterMovement m_characterMovement;
    //This enum shows that character is human or not
    private CharacterType typeOfCharacter;
    //stores current state of the character
    private CharacterState currentCharacterState;
    //store the value of character side (whether character is on right side or left side)
    private CharacterSide sideOfCharacter;

    //property for character side
    public CharacterType TypeOfCharacter
    {
        get { return typeOfCharacter; }
        set { typeOfCharacter = value; }
    }
    //property for character side
    public CharacterSide SideOfCharacter
    {
        get { return sideOfCharacter; }
        set { sideOfCharacter = value; }
    }
    //property for storing current state of character
    public CharacterState CurrentCharacterState
    {
        get { return currentCharacterState; }
        set { currentCharacterState = value; }
    }

    // Use this for initialization
    void Start()
    {
        m_moveTo = new MoveToCommand();
        m_moveUp = new MoveUpCommand();
        m_moveDown = new MoveDownCommand();
        m_moveLeft = new MoveLeftCommand();
        m_moveRight = new MoveRightCommand();
        m_stopMovement = new StopMovementCommand();
        m_characterMovement = GetComponent<CharacterMovement>();
        currentCharacterState = CharacterState.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        //execute commands according to button or mouse press
        if (TypeOfCharacter == CharacterType.Human)
        {
            Command nextStepForCharacter = GetCurrentCommand();
            if (nextStepForCharacter != null) { nextStepForCharacter.Execute(this); }
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
        if (collision.collider.tag == Constant.Separator)
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
