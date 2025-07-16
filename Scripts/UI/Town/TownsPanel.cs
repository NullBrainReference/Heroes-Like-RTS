using UnityEngine;
using System.Collections.Generic;
using Zenject;
using UnityEngine.UI;

public class TownsPanel : MonoBehaviour
{
    [SerializeField] private Transform _container;

    [SerializeField] private GameObject _townCellPrefab;

    [Inject]
    private DiContainer _diContainer;

    private List<Town> towns;

    private float _space;
    private Vector2 cellSize;

    private void Awake()
    {
        towns = new List<Town>();
        if (_townCellPrefab.transform is RectTransform rect)
            cellSize = rect.sizeDelta;

        var layout = _container.GetComponent<HorizontalLayoutGroup>();
        _space = layout.spacing;
    }

    public void AddTown(TownController controller)
    {
        if (towns.Contains(controller.Town))
            return;

        towns.Add(controller.Town);

        var cell = _diContainer.InstantiatePrefabForComponent<TownCell>(_townCellPrefab, _container);

        cell.SetIcon(controller.Sprite);

        ResizeContainer();
    }

    private void ResizeContainer()
    {
        RectTransform rect = (RectTransform)_container;

        rect.sizeDelta = new Vector2((cellSize.x + _space) * towns.Count + _space, rect.sizeDelta.y);
    }
}
