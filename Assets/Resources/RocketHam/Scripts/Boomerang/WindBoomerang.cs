using UnityEngine;

public class WindBoomerang : Boomerang
{
    public WindBoomerang(BoomerangObj boomerang) : base(boomerang)
    {
        Type = BoomerangTypes.WIND;
        BoomerangColor = Color.blue;
    }
}
