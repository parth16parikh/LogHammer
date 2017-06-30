using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stores all the information about a particular touch
/// </summary>
public class TLTouch
{
    // Stores all the basic information about a particular touch
    private Vector2 startPosition, endPosition;
    private float startTime, endTime, holdTime, holdDistance;
    private Vector2 vectorDifferenceBetweenStartAndEnd;
    private SwipeDirection swipeDirection;

    // Define the properties of above private variables
    /// <summary>
    /// The position where the touch started
    /// </summary>
    public Vector2 StartPosition
    {
        get { return startPosition; }
        set { startPosition = value; }
    }

    /// <summary>
    /// The position where the touch ended
    /// </summary>
    public Vector2 EndPosition
    {
        get { return endPosition; }
        set { endPosition = value; }
    }

    /// <summary>
    /// The time when the touch started
    /// </summary>
    public float StartTime
    {
        get { return startTime; }
        set { startTime = value; }
    }

    /// <summary>
    /// The time when the touch ended
    /// </summary>
    public float EndTime
    {
        get { return endTime; }
        set { endTime = value; }
    }

    /// <summary>
    /// The vector representation between the start and the end points
    /// </summary>
    public Vector2 VectorDifferenceBetweenStartAndEnd
    {
        set { vectorDifferenceBetweenStartAndEnd = value; }
        get { return vectorDifferenceBetweenStartAndEnd; }
    }

    /// <summary>
    /// The time for which the touch was held
    /// </summary>
    public float HoldTime
    {
        set { holdTime = value; }
        get { return holdTime; }
    }

    /// <summary>
    /// The distance between the touch start and end
    /// </summary>
    public float HoldDistance
    {
        get { return holdDistance; }
        set { holdDistance = value; }
    }

    /// <summary>
    /// The direction of swipe if the touch has been any association with a swipe
    /// </summary>
    public SwipeDirection SwipeDirection
    {
        set { swipeDirection = value; }
        get { return swipeDirection; }
    }

    // Default constructor
    public TLTouch()
    {
    }

    /// <summary>
    /// Set the values of touch start variables
    /// </summary>
    /// <param name="startPos">
    /// The start position of the touch
    /// </param>
    /// <param name="startTime">
    /// The start time of the touch
    /// </param>
    public void SetTouchStartInfo(Vector2 startPos, float startTime)
    {
        StartPosition = startPos;
        StartTime = startTime;
    }

    /// <summary>
    /// Set the values of touch end variables
    /// </summary>
    /// <param name="endPos">
    /// The end position of the touch
    /// </param>
    /// <param name="endTime">
    /// The end time of the touch
    /// </param>
    public void SetTouchEndInfo(Vector2 endPos, float endTime)
    {
        EndPosition = endPos;
        EndTime = endTime;
        VectorDifferenceBetweenStartAndEnd = new Vector2(EndPosition.x - StartPosition.x, EndPosition.y - StartPosition.y);
        HoldDistance = VectorDifferenceBetweenStartAndEnd.magnitude;
        HoldTime = EndTime - StartTime;
    }
}
