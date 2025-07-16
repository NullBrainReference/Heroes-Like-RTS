using UnityEngine;
using Zenject;

public class TownController : MonoBehaviour, IMapCollisionObject<TownController>
{
    [SerializeField]
    private Town _town;
    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    [Inject]
    private TownPanel _panel;
    [Inject]
    private PlayerPanel _playerPanel;
    [Inject]
    private MapObjectsCollector _objectsCollector;

    public Town Town => _town;

    public Sprite Sprite => _spriteRenderer.sprite; 

    public TownController Payload => this;

    private void Start()
    {
        _objectsCollector.Subscribe(this);
        EventBus.Instance.Subscribe(EventType.NewDay,
            new LocalEvent(() => { Town.Grow(); }));

        _playerPanel.TownsPanel.AddTown(this);
    }

    public void SetTown(Town town)
    {
        _town = town;
    }

    public void OnCollision(IMapCollisionObject collisionObject)
    {
        if (collisionObject is IMapCollisionObject<MapGroupController> groupObject)
        {
            _panel.Display(_town, groupObject.Payload.MapGroup);

            //SceneLoadUtil.LoadBattleSync(_mapGroup, groupObject.Payload.MapGroup);
        }
    }
}
