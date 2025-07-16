using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Town
{
    [SerializeField] private int _id;
    [SerializeField] private string _name;
    [SerializeField] private List<Building> _buildings;

    public int Id => _id;
    public string Name => _name;
    public List<Building> Buildings => _buildings;

    public void Grow()
    {
        foreach (var building in _buildings)
        {
            building.Grow();
        }
    }
}
