using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    /// <summary>
    /// Enum which will justify that Character will be controlled by human or AI
    /// </summary>
    public enum CharacterType
    {
        Human,
        AI
    }
    /// <summary>
    /// Enum declared for Character side
    /// </summary>
    public enum CharacterSide
    {
        Left,
        Right
    }
    /// <summary>
    /// enum for character state
    /// </summary>
    public enum CharacterState
    {
        Idle,
        Moving,
        AtBorder,
    }

    /// <summary>
    /// all the Commands for character
    /// </summary>
    private Command m_moveTo;
    private Command m_moveUp;
    private Command m_moveDown;
    private Command m_moveRight;
    private Command m_moveLeft;
    private Command m_stopMovement;

    /// <summary>
    /// reference to Charactermovement on gameobject
    /// </summary>
    private CharacterMovement m_characterMovement;
    /// <summary>
    /// This enum shows that character is human or not
    /// </summary>
    private CharacterType typeOfCharacter;
    /// <summary>
    /// stores current state of the character
    /// </summary>
    private CharacterState currentCharacterState;
    /// <summary>
    /// store the value of character side (whether character is on right side or left side)
    /// </summary>
    private CharacterSide sideOfCharacter;

    /// <summary>
    /// property for character side
    /// </summary>
    public CharacterType TypeOfCharacter
    {
        get { return typeOfCharacter; }
        set { typeOfCharacter = value; }
    }

    /// <summary>
    /// property for character side
    /// </summary>
    public CharacterSide SideOfCharacter
    {
        get { return sideOfCharacter; }
        set { sideOfCharacter = value; }
    }
    /// <summary>
    /// property for storing current state of character
    /// </summary>
    public CharacterState CurrentCharacterState
    {
        get { return currentCharacterState; }
        set { currentCharacterState = value; }
    }

    // Use this for initialization
    void Awake()
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

    /// <summary>
    /// get current command that will be applicable on character
    /// </summary>
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

    /// <summary>
    /// detect player collision with seperater and execute stop character command
    /// </summary>
    public void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == Constant.Separator)
        {
            m_stopMovement.Execute(this);
        }
    }

    /// <summary>
    /// execute move to command
    /// </summary>
    public void MoveToPosition()
    {
        m_characterMovement.MoveToPosition();
    }

    /// <summary>
    /// execute move up command
    /// </summary>
    public void MoveUp()
    {
        m_characterMovement.MoveUp();
    }

    /// <summary>
    /// execute move down command
    /// </summary>
    public void MoveDown()
    {
        m_characterMovement.MoveDown();
    }

    /// <summary>
    /// execute move left command
    /// </summary>
    public void MoveLeft()
    {
        m_characterMovement.MoveLeft();
    }

    /// <summary>
    /// execute move right command
    /// </summary>
    public void MoveRight()
    {
        m_characterMovement.MoveRight();
    }

    /// <summary>
    /// execute stop movement command
    /// </summary>
    public void StopMovement()
    {
        m_characterMovement.StopMovement();
    }
}
