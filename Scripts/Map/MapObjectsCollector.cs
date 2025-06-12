using UnityEngine;
using System.Collections.Generic;

public class MapObjectsCollector : MonoBehaviour
{
    [SerializeField]
    private List<MapGroupController> _groupControllers;
    [SerializeField]
    private List<TownController> _townControllers;

    public void Subscribe(MapGroupController controller)
    {
        _groupControllers.Add(controller);
    }

    public List<MapGroup> GetGroups()
    {
        var groups = new List<MapGroup>();
        foreach (var controller in _groupControllers)
        {
            if (controller == null)
                continue;

            var pos = controller.Position;
            controller.MapGroup.SetPos(pos.x, pos.y);
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

    public void DestroyAllGroups()
    {
        foreach (var controller in _groupControllers)
        {
            Destroy(controller.gameObject);
        }

        _groupControllers.Clear();
    }
}
