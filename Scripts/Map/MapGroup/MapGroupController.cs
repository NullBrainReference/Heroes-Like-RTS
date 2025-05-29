using UnityEngine;
using Zenject;

public class MapGroupController : MonoBehaviour, IMapSelectable, IMapCollisionObject
{
    [SerializeField]
    private MapGroup _mapGroup;
    [SerializeField]
    private UnitNavigator _unitNavigator;


    [Inject]
    MapPlayerController _palyerController;

    public MapGroup MapGroup => _mapGroup;

    public Vector3 Position { get => transform.position; }

    private void OnMouseUp()
    {
        Debug.Log("MouseUp detected");
        Select();
    }

    public void Select()
    {
        _palyerController.Select(this);
    }

    public void OnDeselect()
    {

    }

    public void Order(Vector3 pos)
    {
        _unitNavigator.MoveToTarget(pos);
        Debug.Log("Order was given");
    }

    public void OnCollision(IMapCollisionObject collisionObject)
    {
        throw new System.NotImplementedException();
    }
}
