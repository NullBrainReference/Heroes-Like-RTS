using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class UnitManager : MonoBehaviour
{
    private Dictionary<string, List<UnitController>> units;

    public bool Ready { get; private set; }

    private void Awake()
    {
        units = new Dictionary<string, List<UnitController>>();
        Ready = true;
    }

    public void AddUnit(UnitController controller)
    {
        if (!units.ContainsKey(controller.Unit.TeamKey))
        {
            units.Add(controller.Unit.TeamKey, new List<UnitController>());
        }

        units[controller.Unit.TeamKey].Add(controller);
    }

    public UnitController GetClosestTarget(UnitController controller)
    {
        UnitController result = null;
        float closestDistance = float.MaxValue;

        foreach (var item in units)
        {
            if (item.Key == controller.Unit.TeamKey)
                continue;

            foreach(var target in item.Value)
            {
                if (target.Unit.IsDead)
                    continue;

                float distance = Vector3.Distance(controller.transform.position, target.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    result = target;
                }
            }
        }

        return result;
    }
}
