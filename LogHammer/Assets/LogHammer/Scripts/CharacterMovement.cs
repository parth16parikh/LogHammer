using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    //mouse click start position
    private Vector2 m_tapStartPos = Vector2.zero;
    //mouse's current position when pressed
    private Vector2 m_tapCurrentPos = Vector2.zero;
    //start time when mouse is pressed
    private float m_tapStartTime = Constant.Zero;
    //current time while mouse is still pressed
    private float m_tapCurrentTime = Constant.Zero;
    //holds the reference to the rigidbody which is on the character gameobject
    private Rigidbody m_rigidBody;
    //holds the reference to the Character script which is on the character gameobject
    private Character m_character;

    // Use this for initialization
    void Awake()
    {
        m_rigidBody = GetComponent<Rigidbody>();
        m_character = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        //record mouse start position and start time when mouse button is pressed
        if (Input.GetMouseButtonDown(Constant.IntZero))
        {
            m_tapStartPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            m_tapStartTime = Time.realtimeSinceStartup;
        }

        //If character is not moving then set its state idle
        if (m_character.CurrentCharacterState == Character.CharacterState.Moving && m_rigidBody.velocity == Vector3.zero)
        {
            m_character.CurrentCharacterState = Character.CharacterState.Idle;
        }
    }

    //move character to the current mouse position
    public void MoveToPosition()
    {
        //set character's state to moving state
        m_character.CurrentCharacterState = Character.CharacterState.Moving;

        //caculate direction and distance of the mouse
        m_tapCurrentPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 dragDirection = (m_tapStartPos - m_tapCurrentPos);
        //calculate time for mouse to travel that much distance
        m_tapCurrentTime = Time.realtimeSinceStartup;
        float timeDifference = m_tapCurrentTime - m_tapStartTime;

        //record current position as start position
        m_tapStartPos = m_tapCurrentPos;
        m_tapStartTime = m_tapCurrentTime;

        //calculate the resultant velocity of the character
        Vector3 resultantvelocity = new Vector3(dragDirection.x, Constant.Zero, dragDirection.y) / timeDifference;
        if (dragDirection != Vector2.zero && timeDifference != Constant.Zero)
        {
            m_rigidBody.velocity = Vector3.ClampMagnitude(m_rigidBody.velocity + resultantvelocity, Constant.ClampVelocity);
        }
    }

    // stop character movement when it collides with seperator
    public void StopMovement()
    {
        m_character.CurrentCharacterState = Character.CharacterState.AtBorder;
        m_rigidBody.velocity = Vector3.zero;
    }

    //character should go up when user press W
    public void MoveUp()
    {
        if (m_character.CurrentCharacterState == Character.CharacterState.Idle)
        {
            m_character.CurrentCharacterState = Character.CharacterState.Moving;
        }
        m_rigidBody.velocity = Vector3.ClampMagnitude((m_rigidBody.velocity + Vector3.back * Constant.ForceIntensity), Constant.ClampVelocity);
        Debug.Log("Move Character up");
    }

    //character should go down when user press S
    public void MoveDown()
    {
        if (m_character.CurrentCharacterState == Character.CharacterState.Idle)
        {
            m_character.CurrentCharacterState = Character.CharacterState.Moving;
        }
        m_rigidBody.velocity = Vector3.ClampMagnitude((m_rigidBody.velocity + Vector3.forward * Constant.ForceIntensity), Constant.ClampVelocity);
        Debug.Log("Move Character Down");
    }

    //character should go left when user press A
    public void MoveLeft()
    {
        if (m_character.SideOfCharacter == Character.CharacterSide.Right)
        {
            if (m_character.CurrentCharacterState == Character.CharacterState.AtBorder)
            {
                return;
            }
            else
            {
                m_character.CurrentCharacterState = Character.CharacterState.Moving;
            }
        }
        if (m_character.SideOfCharacter == Character.CharacterSide.Left)
        {
            if (m_character.CurrentCharacterState != Character.CharacterState.Moving)
            {
                m_character.CurrentCharacterState = Character.CharacterState.Moving;
            }
        }
        m_rigidBody.velocity = Vector3.ClampMagnitude((m_rigidBody.velocity + Vector3.right * Constant.ForceIntensity), Constant.ClampVelocity);
        Debug.Log("Move Character Left");
    }

    //character should go right when user press D
    public void MoveRight()
    {
        if (m_character.SideOfCharacter == Character.CharacterSide.Left)
        {
            if (m_character.CurrentCharacterState == Character.CharacterState.AtBorder)
            {
                return;
            }
            else
            {
                m_character.CurrentCharacterState = Character.CharacterState.Moving;
            }
        }
        if (m_character.SideOfCharacter == Character.CharacterSide.Right)
        {
            if (m_character.CurrentCharacterState != Character.CharacterState.Moving)
            {
                m_character.CurrentCharacterState = Character.CharacterState.Moving;
            }
        }
        m_rigidBody.velocity = Vector3.ClampMagnitude((m_rigidBody.velocity + Vector3.left * Constant.ForceIntensity), Constant.ClampVelocity);
        Debug.Log("Move Character Right");
    }
}
