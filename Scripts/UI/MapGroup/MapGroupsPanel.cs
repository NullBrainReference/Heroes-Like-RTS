using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MapGroupsPanel : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private GameObject _partyCellPrefab;

    [Inject]
    private DiContainer _diContainer;
    [Inject]
    private MapPlayerController _playerController;

    private List<MapGroup> _groups;
    private List<PartyCell> _cells;
    private float _space;
    private Vector2 _cellSize;

    private void Awake()
    {
        _cells = new List<PartyCell>();
        _groups = new List<MapGroup>();
        if (_partyCellPrefab.transform is RectTransform rect)
            _cellSize = rect.sizeDelta;

        var layout = _container.GetComponent<HorizontalLayoutGroup>();
        _space = layout.spacing;
    }

    private void Start()
    {
        EventBus.Instance.Subscribe(EventType.Load, new LocalEvent(() => {
            OnGameLoaded();
        }));
    }

    public void AddGroup(MapGroupController controller, Sprite icon)
    {
        if (controller.TeamKey != _playerController.TeamKey)
            return;

        if (_groups.Contains(controller.MapGroup))
            return;

        _groups.Add(controller.MapGroup);

        var cell = _diContainer.InstantiatePrefabForComponent<PartyCell>(_partyCellPrefab, _container);
        cell.SetGroup(controller);
        cell.SetIcon(icon);
        _cells.Add(cell);

        ResizeContainer();
    }

    private void ResizeContainer()
    {
        RectTransform rect = (RectTransform)_container;

        if (_groups.Count == 0)
        {
            rect.sizeDelta = new Vector2(_cellSize.x + _space, rect.sizeDelta.y);
            return;
        }

        rect.sizeDelta = new Vector2((_cellSize.x + _space) * _groups.Count + _space, rect.sizeDelta.y);
    }

    private void OnGameLoaded()
    {
        foreach (Transform child in _container)
        {
            Destroy(child.gameObject);
        }
        _groups.Clear();
        _cells.Clear();

        ResizeContainer();
    }
}
