using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameLimit : MonoBehaviour
{
    public enum limits
    {
        noLimit = 0,
        limit30 = 30,
        limit60 = 60,
        limit120 = 120,
        
    }

    public limits limit;

    void Awake()
    {
        Application.targetFrameRate = (int)limit;
        
    }
}