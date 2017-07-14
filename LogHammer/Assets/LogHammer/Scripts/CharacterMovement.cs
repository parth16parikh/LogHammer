using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    /// <summary>
    /// mouse click start position
    /// </summary>
    private Vector2 m_tapStartPos = Vector2.zero;
    /// <summary>
    /// mouse's current position when pressed
    /// </summary>
    private Vector2 m_tapCurrentPos = Vector2.zero;
    /// <summary>
    /// start time when mouse is pressed
    /// </summary>
    private float m_tapStartTime = Constant.Zero;
    /// <summary>
    /// current time while mouse is still pressed
    /// </summary>
    private float m_tapCurrentTime = Constant.Zero;
    /// <summary>
    /// holds the reference to the rigidbody which is on the character gameobject
    /// </summary>
    private Rigidbody m_rigidBody;
    /// <summary>
    /// holds the reference to the Character script which is on the character gameobject
    /// </summary>
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

    /// <summary>
    /// move character to the current mouse position
    /// </summary>
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

    /// <summary>
    /// stop character movement when it collides with seperator
    /// </summary>
    public void StopMovement()
    {
        m_character.CurrentCharacterState = Character.CharacterState.AtBorder;
        m_rigidBody.velocity = Vector3.zero;
    }

    /// <summary>
    /// character should go up when user press W
    /// </summary>
    public void MoveUp()
    {
        if (m_character.CurrentCharacterState == Character.CharacterState.Idle)
        {
            m_character.CurrentCharacterState = Character.CharacterState.Moving;
        }
        m_rigidBody.velocity = Vector3.ClampMagnitude((m_rigidBody.velocity + Vector3.back * Constant.ForceIntensity), Constant.ClampVelocity);
        Debug.Log("Move Character up");
    }

    /// <summary>
    /// character should go down when user press S
    /// </summary>
    public void MoveDown()
    {
        if (m_character.CurrentCharacterState == Character.CharacterState.Idle)
        {
            m_character.CurrentCharacterState = Character.CharacterState.Moving;
        }
        m_rigidBody.velocity = Vector3.ClampMagnitude((m_rigidBody.velocity + Vector3.forward * Constant.ForceIntensity), Constant.ClampVelocity);
        Debug.Log("Move Character Down");
    }

    /// <summary>
    /// character should go left when user press A
    /// </summary>
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

    /// <summary>
    /// character should go right when user press D
    /// </summary>
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
