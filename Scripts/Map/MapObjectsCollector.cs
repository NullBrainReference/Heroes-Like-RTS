using UnityEngine;
using System.Collections.Generic;

public class MapObjectsCollector : MonoBehaviour
{
    [SerializeField]
    private List<MapGroupController> _groupControllers;
    [SerializeField]
    private List<TownController> _townControllers;

    public List<TownController> TownControllers => _townControllers;
    public List<MapGroupController> GroupControllers => _groupControllers;

    private void Awake()
    {
        _groupControllers = new List<MapGroupController>();
        _townControllers = new List<TownController>();
    }

    public void Subscribe(MapGroupController controller)
    {
        _groupControllers.Add(controller);
    }

    public void Subscribe(TownController controller)
    {
        _townControllers.Add(controller);
    }

    public void RestoreModel(Town town)
    {
        var controller = _townControllers.Find(x => x.Town.Id == town.Id);

        if (controller != null)
        {
            controller.SetTown(town);
        }
        else
        {
            Debug.LogError($"TownController for town {town.Name} not found.");
        }
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
