using UnityEngine;
using System.Collections.Generic;

public class MapObjectsCollector : MonoBehaviour
{
    [SerializeField]
    private List<MapGroupController> _groupControllers;
    [SerializeField]
    private List<TownController> _townControllers;



    public List<MapGroup> GetGroups()
    {
        var groups = new List<MapGroup>();
        foreach (var controller in _groupControllers)
        {
            if (controller == null)
                continue;

            groups.Add(controller.MapGroup);
        }

        return groups;
    }

    public List<Town> GetTowns()
    {
        var groups = new List<Town>();
        foreach (var controller in _townControllers)
        {
            groups.Add(controller.Town);
        }

        return groups;
    }
}
