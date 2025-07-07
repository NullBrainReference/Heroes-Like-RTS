using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Town
{
    [SerializeField]
    private List<Building> _buildings;

    public List<Building> Buildings => _buildings;

    public void Grow()
    {
        foreach (var building in _buildings)
        {
            building.Grow();
        }
    }
}
