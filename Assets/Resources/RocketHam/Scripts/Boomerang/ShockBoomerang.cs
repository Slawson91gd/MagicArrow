using UnityEngine;

public class ShockBoomerang : Boomerang
{
    public ShockBoomerang(BoomerangObj boomerang) : base(boomerang)
    {
        Type = BoomerangTypes.SHOCK;
        BoomerangColor = Color.yellow;
    }
}
