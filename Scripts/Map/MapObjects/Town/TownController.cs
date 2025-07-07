using UnityEngine;
using Zenject;

public class TownController : MonoBehaviour, IMapCollisionObject<TownController>
{
    [SerializeField]
    private Town _town;

    [Inject]
    private TownPanel _panel;

    public Town Town => _town;

    public TownController Payload => this;

    private void Start()
    {
        EventBus.Instance.Subscribe(EventType.NewDay,
            new LocalEvent(() => { Town.Grow(); }));
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
