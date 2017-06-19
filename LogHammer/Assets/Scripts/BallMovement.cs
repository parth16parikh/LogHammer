using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    private Rigidbody rigidBody;

    static private BallMovement instance;

    public float speed;
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

    private void Awake()
    {
        Instance = this;
        rigidBody = GetComponent<Rigidbody>();
    }

    public void MoveBall(Vector3 force)
    {
        print(force);
        force = force * -1f;
        GetComponent<Rigidbody>().AddForce(force * speed);
    }
}
