using UnityEngine;
using Zenject;

public class MapGroupController : MonoBehaviour, IMapSelectable, IMapCollisionObject<MapGroupController>
{
    [SerializeField]
    private MapGroup _mapGroup;
    [SerializeField]
    private UnitNavigator _unitNavigator;
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    [SerializeField]
    private GameObject _selectionCircle;


    [Inject]
    private MapPlayerController _palyerController;
    [Inject]
    private MapObjectsCollector _objectsCollector;
    [Inject]
    private PlayerPanel _playerPanel;

    public MapGroup MapGroup => _mapGroup;
    public Sprite Icon => _spriteRenderer.sprite;

    public Vector3 Position { get => transform.position; }
    public TeamTag TeamKey => _mapGroup.TeamKey;

    public MapGroupController Payload => this;

    private void Start()
    {
        _objectsCollector.Subscribe(this);
        _playerPanel.PartiesPanel.AddGroup(this, Icon);
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
        _selectionCircle.SetActive(true);
    }

    public void OnDeselect()
    {
        _selectionCircle.SetActive(false);
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
