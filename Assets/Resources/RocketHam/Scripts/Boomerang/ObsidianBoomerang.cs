using UnityEngine;

public class ObsidianBoomerang : Boomerang
{
    public ObsidianBoomerang(BoomerangObj boomerang) : base(boomerang)
    {
        Type = BoomerangTypes.OBSIDIAN;
        BoomerangColor = Color.black;
    }
}
