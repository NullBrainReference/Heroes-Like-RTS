using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class MapGroup
{
    [SerializeField]
    private List<Unit> _units = new List<Unit>();
    [SerializeField]
    private string _key;

    public List<Unit> Units => _units;
    public string Key => _key;
}
