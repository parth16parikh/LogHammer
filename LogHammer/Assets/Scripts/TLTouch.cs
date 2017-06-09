using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TLTouch
{
    //copy constructor
    public TLTouch(TLTouch tlTouch)
    {
        startPosition = tlTouch.startPosition;
        endPosition = tlTouch.endPosition;
        startTime = tlTouch.startTime;
        endTime = tlTouch.endTime;
        holdTime = tlTouch.holdTime;
    }

    public TLTouch()
    {
    }

    //stores all the basic information about a particular touch
    private Vector2 startPosition, endPosition;
    private float startTime, endTime, holdTime, holdDistance;
    private Vector2 holdVector;

    //define the properties of above private variables
    public Vector2 StartPosition
    {
        get
        {
            return startPosition;
        }
        set
        {
            startPosition = value;
        }
    }

    public Vector2 EndPosition
    {
        get
        {
            return endPosition;
        }
        set
        {
            endPosition = value;
        }
    }

    public float StartTime
    {
        get
        {
            return startTime;
        }
        set
        {
            startTime = value;
        }
    }

    public float EndTime
    {
        get
        {
            return endTime;
        }
        set
        {
            endTime = value;
        }
    }
    
    public Vector2 HoldVector
    {
        set
        {
            holdVector = value;
        }
        get
        {
            return holdVector;
        }
    }

    public float HoldTime
    {
        set
        {
            holdTime = value;
        }
        get
        {
            return holdTime;
        }
    }

    public float HoldDistance
    {
        get
        {
            return holdDistance;
        }
        set
        {
            holdDistance = value;
        }
    }

    public void SetTouchStartInfo(Vector2 startPos, float startTime)
    {
        StartPosition = startPos;
        StartTime = startTime;
    }

    public void SetTouchEndInfo(Vector2 endPos, float endTime)
    {
        EndPosition = endPos;
        EndTime = endTime;
        HoldVector = new Vector2(EndPosition.x - StartPosition.x, EndPosition.y - StartPosition.y);
        HoldDistance = HoldVector.magnitude;
        HoldTime = EndTime - StartTime;
    }
}
