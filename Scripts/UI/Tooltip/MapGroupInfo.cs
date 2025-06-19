using UnityEngine;
using Zenject;

public class MapGroupInfo : MonoBehaviour
{
    private MapGroup _mapGroup;

    [Inject]
    private TooltipsManager _tooltips;

    private void Start()
    {
        _mapGroup = GetComponent<MapGroupController>().MapGroup;
    }

    public void OnPoint()
    {
        _tooltips.MapGroupTooltip.Show(_mapGroup);
    }
    public void OnLeave()
    {
        _tooltips.MapGroupTooltip.Hide();
    }

    //TODO: replace with new input sys
    private void OnMouseEnter()
    {
        OnPoint();
    }

    private void OnMouseExit()
    {
        OnLeave();
    }
}
