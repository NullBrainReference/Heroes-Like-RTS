using UnityEngine;
using System;
using System.Collections.Generic;
using NUnit.Framework;

[System.Serializable]
public class GameSave
{
    [SerializeField]
    private List<MapGroup> _groups;
    [SerializeField]
    private List<Town> _towns;

    public GameSave(List<MapGroup> groups, List<Town> towns)
    {
        _groups = groups;
        _towns = towns;
    }
}
