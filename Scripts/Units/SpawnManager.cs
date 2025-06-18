using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private List<Spawner> _spawnsLeft;
    [SerializeField]
    private List<Spawner> _spawnsRight;
    [SerializeField]
    private List<Spawner> _spawnsBottom;
    [SerializeField]
    private List<Spawner> _spawnsTop;

    [SerializeField]
    private Color _colorLeft;
    [SerializeField]
    private Color _colorRight;

    private Dictionary<string, MapGroup> _groups;

    [SerializeField]
    private UnitsLib _unitsLib;

    public UnitsLib UnitsLib => _unitsLib;

    private void Start()
    {
        _groups = new Dictionary<string, MapGroup>();

        StartSpawning();
    }

    public void StartSpawning()
    {
        var group1 = JsonUtility.FromJson<MapGroup>(PlayerPrefs.GetString("player1", ""));
        var group2 = JsonUtility.FromJson<MapGroup>(PlayerPrefs.GetString("player2", ""));

        _groups.Add(group1.TeamKey, group1);
        _groups.Add(group2.TeamKey, group2);

        StartCoroutine(SpawnCoroutine(group1.Units, _spawnsLeft, _colorLeft));
        StartCoroutine(SpawnCoroutine(group2.Units, _spawnsRight, _colorRight));

        //TODO replace with bus
        StartCoroutine(ExitOnConditionCoroutine());
    }

    private IEnumerator ExitOnConditionCoroutine()
    {
        bool repeat = true;

        while (repeat)
        {
            foreach (var group in _groups)
            {
                if (!group.Value.IsDefeated())
                    continue;

                //PlayerPrefs.SetString("player1", JsonUtility.ToJson(_groups["A"]));
                //PlayerPrefs.SetString("player2", JsonUtility.ToJson(_groups["B"]));

                SceneLoadUtil.LoadMapSync(_groups["A"], _groups["B"]);

                repeat = false;
                break;
            }

            if (repeat)
                yield return new WaitForSeconds(5);
        }
    }

    private IEnumerator SpawnCoroutine(List<Unit> units, List<Spawner> spawnersGroup, Color color)
    {
        List<Unit> unitsToSpawn = new List<Unit>();
        foreach (var unit in units)
            unitsToSpawn.Add(unit);

        while (unitsToSpawn.Count > 0)
        {
            var unitPrefab = _unitsLib.GetUnitPrefab(unitsToSpawn[0].UnitType);

            foreach (var spawner in spawnersGroup)
            {
                if (spawner.Ready)
                {
                    spawner.Spawn(unitPrefab, unitsToSpawn[0], color);
                    unitsToSpawn.Remove(unitsToSpawn[0]);
                    break;
                }
            }

            yield return new WaitForEndOfFrame();
        }
    }
}
