using UnityEngine;
using Zenject;
using System.Collections.Generic;
using System.Collections;
using System.Drawing;

public class MapController : MonoBehaviour
{
    [Inject]
    private MapObjectsCollector _mapObjectsCollector;
    [Inject]
    private MapObjectsLib _mapObjectsLib;

    [Inject]
    private DiContainer _diContainer;

    public void SpawnMapGroups(GameSave gameSave)
    {
        StartCoroutine(SpawnMapGroupsCoroutine(gameSave));

        //_mapObjectsCollector.DestroyAllGroups();
        //
        //foreach (var g in gameSave.Groups)
        //{
        //    var prefab = _mapObjectsLib.GetMapGroupPrefab(g.Key == "A" ? TeamTag.White : TeamTag.Red);
        //    SpawnMapGroup(g, prefab);
        //}
    }

    private IEnumerator SpawnMapGroupsCoroutine(GameSave gameSave)
    {
        _mapObjectsCollector.DestroyAllGroups();

        yield return new WaitForSeconds(3);

        foreach (var g in gameSave.Groups)
        {
            var prefab = _mapObjectsLib.GetMapGroupPrefab(g.Key == "A" ? TeamTag.White : TeamTag.Red);
            SpawnMapGroup(g, prefab);
        }
    }

    private void SpawnMapGroup(MapGroup mapGroup, MapGroupController prefab)
    {
        var unitController = _diContainer.InstantiatePrefabForComponent<MapGroupController>(
            prefab,
            new Vector3(mapGroup.Pos.x, mapGroup.Pos.y, 0),
            transform.rotation,
            null);

        unitController.SetMapGroup(mapGroup);
    }
}
