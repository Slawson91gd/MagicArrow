using UnityEngine;

public class NormalBoomerang : Boomerang
{
    public NormalBoomerang(BoomerangObj boomerang) : base(boomerang)
    {
        Type = BoomerangTypes.NORMAL;
        BoomerangColor = Color.white;
    }
}
