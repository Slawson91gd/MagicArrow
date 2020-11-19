using UnityEngine;

public class IceBoomerang : Boomerang
{
    public IceBoomerang(BoomerangObj boomerang) : base(boomerang)
    {
        Type = BoomerangTypes.ICE;
        BoomerangColor = Color.cyan;
    }
}
