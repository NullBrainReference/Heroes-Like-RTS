using UnityEngine;
using Zenject;

public class MapGroupController : MonoBehaviour, IMapSelectable, IMapCollisionObject<MapGroupController>
{
    [SerializeField]
    private MapGroup _mapGroup;
    [SerializeField]
    private UnitNavigator _unitNavigator;


    [Inject]
    private MapPlayerController _palyerController;
    [Inject]
    private MapObjectsCollector _objectsCollector;

    public MapGroup MapGroup => _mapGroup;

    public Vector3 Position { get => transform.position; }

    public MapGroupController Payload => this;

    private void Awake()
    {
        _objectsCollector.Subscribe(this);
    }

    private void OnMouseUp()
    {
        Debug.Log("MouseUp detected");
        Select();
    }

    public void SetMapGroup(MapGroup mapGroup)
    {
        _mapGroup = mapGroup;
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
        if (collisionObject is IMapCollisionObject<MapGroupController> groupObject)
        {
            SceneLoadUtil.LoadBattleSync(_mapGroup, groupObject.Payload.MapGroup);
        }
    }
}
