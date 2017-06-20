using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Handles the movement of the Player
public class BallMovement : MonoBehaviour
{
    //to store the reference of rigidbody component of the player, to which we will apply some force, for its movement
    private Rigidbody rigidBody;

    //instance variable
    static private BallMovement instance;

    //assign speed publically
    public float speed;

    //instance property
    static public BallMovement Instance
    {
        get
        {
            if (instance == null)
                instance = new BallMovement();
            return instance;
        }
        set
        {
            instance = value;
        }
    }

    //called right after the instance of this script is made, and before any other methods of the script
    private void Awake()
    {
        //makes this class singleton
        Instance = this;

        //assign rigidbody instance
        rigidBody = GetComponent<Rigidbody>();
    }

    //moves the ball according to the Force
    public void MoveBall(Vector3 force)
    {
        //the input that we got has opposite axes, hence resetting the axes here
        force = force * -1f;

        //applying the force
        rigidBody.AddForce(force * speed);
    }
}
