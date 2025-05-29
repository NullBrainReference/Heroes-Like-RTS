using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class MapGroup
{
    [SerializeField]
    private List<Unit> _units = new List<Unit>();

    public List<Unit> Units => _units;


}
