using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Crystal
{
    public bool IsActive { get; set; }
    public GameObject TargetObject { get; set; }

    public Crystal()
    {
        IsActive = false;
    }

    public void SetActive(bool condition)
    {
        if(IsActive != condition)
        {
            IsActive = condition;
        }
    }
}
