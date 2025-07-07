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

    [SerializeField]
    private TimeModel _timeModel;

    public GameSave(List<MapGroup> groups, List<Town> towns, TimeModel time)
    {
        _groups = groups;
        _towns = towns;

        _timeModel = time;
    }

    public List<MapGroup> Groups => _groups;
    public List<Town> Towns => _towns;
    public TimeModel Time => _timeModel;
}
