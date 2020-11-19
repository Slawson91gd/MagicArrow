using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBoomerang : Boomerang
{
    public FireBoomerang(BoomerangObj boomerang) : base(boomerang)
    {
        Type = BoomerangTypes.FIRE;
        BoomerangColor = Color.red;
    }
}
