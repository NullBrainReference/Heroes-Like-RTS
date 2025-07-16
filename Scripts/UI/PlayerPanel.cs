using UnityEngine;

public class PlayerPanel : MonoBehaviour
{
    [SerializeField] private TownsPanel _townsPanel;
    [SerializeField] private MapGroupsPanel _partiesPanel;

    public TownsPanel TownsPanel => _townsPanel;
    public MapGroupsPanel PartiesPanel => _partiesPanel;
}
