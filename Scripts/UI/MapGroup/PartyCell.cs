using UnityEngine;
using UnityEngine.UI;

public class PartyCell : MonoBehaviour
{
    [SerializeField] private Image _icon;

    private MapGroupController _mapGroupController;

    public void SetGroup(MapGroupController groupController)
    {
        _mapGroupController = groupController;
    }

    public void SetIcon(Sprite sprite)
    {
        _icon.sprite = sprite;
    }

    public void OnClick()
    {
        _mapGroupController.Select();
    }
}
