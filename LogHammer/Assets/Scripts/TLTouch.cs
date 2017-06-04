using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TLTouch
{
    public TLTouch(TLTouch tlTouch)
    {
        m_touchStartPosition = tlTouch.m_touchStartPosition;
        m_touchEndPosition = tlTouch.m_touchEndPosition;
        m_startTime = tlTouch.m_startTime;
        m_endTime = tlTouch.m_endTime;
        m_holdTime = tlTouch.m_holdTime;
    }

    public TLTouch()
    { }

    //stores all the basic information about a particular touch
    public Vector2 m_touchStartPosition, m_touchEndPosition;
    public float m_startTime, m_endTime, m_holdTime;
}
