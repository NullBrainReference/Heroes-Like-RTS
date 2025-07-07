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
    private TimeManager _timeManager;

    [Inject]
    private DiContainer _diContainer;

    private void Start()
    {
        //For now it will duplicate on 2nd load so
        //TODO: Avoid duplications (mb unsub? so i'll need type)
        EventBus.Instance.Subscribe(
            EventType.Save,
            new LocalEvent(() => { SaveMap(); }));

        if (PlayerPrefs.GetString("loadmap", "no") == "load")
        {
            LoadMap();

            PlayerPrefs.SetString("loadmap", "no");
        }
    }

    public void LoadMap()
    {
        var save = JsonUtility.FromJson<GameSave>(PlayerPrefs.GetString("localsave"));

        var group1 = JsonUtility.FromJson<MapGroup>(PlayerPrefs.GetString("player1"));
        var group2 = JsonUtility.FromJson<MapGroup>(PlayerPrefs.GetString("player2"));

        List<MapGroup> toRemove = new List<MapGroup>();

        foreach (var group in save.Groups)
        {
            if (group.Key == group1.Key)
                group.UpdateWithGroup(group1);
            else if (group.Key == group2.Key)
                group.UpdateWithGroup(group2);
            
            if (group.IsDefeated())
                toRemove.Add(group);
        }

        foreach (var group in toRemove)
            save.Groups.Remove(group);

        SpawnMapGroups(save);
    }

    public void SaveMap()
    {
        var save = new GameSave(
            _mapObjectsCollector.GetGroups(),
            _mapObjectsCollector.GetTowns(),
            _timeManager.TimeModel);

        PlayerPrefs.SetString("localsave", JsonUtility.ToJson(save));
    }

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
            var prefab = _mapObjectsLib.GetMapGroupPrefab(g.TeamKey);
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
