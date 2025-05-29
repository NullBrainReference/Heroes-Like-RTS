using UnityEngine;
using System.Collections.Generic;
using Zenject;
using UnityEngine.InputSystem;

public class MapPlayerController : MonoBehaviour
{
    [SerializeField]
    private List<MapGroupController> _playerGroups;

    [SerializeField]
    private IMapSelectable _selected;

    //[SerializeField]
    //private Camera _camera;

    private void Start()
    {
        MapInputsManager.Instance.SubscribeOnMove(MoveOnClick);
    }

    public void Select(IMapSelectable selectable)
    {
        _selected = selectable;
        Debug.Log("select called");
    }

    private void MoveOnClick(InputAction.CallbackContext context)
    {
        Debug.Log("MovrOnClick entered");
        if (_selected == null)
            return;

        _selected.Order(GetPosOnClick());
    }

    private Vector3 GetPosOnClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit.collider != null)
        {
            Vector2 worldPosition = hit.point;
            Debug.Log($"Hit pos xy ({worldPosition.x}, {worldPosition.y})");

            return worldPosition;
        }
        else
        {
            Debug.Log($"hit failed, no pos");
            return _selected.Position;
        }
    }
}
