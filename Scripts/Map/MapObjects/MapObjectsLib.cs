using UnityEngine;

[CreateAssetMenu(fileName = "MapObjectsLib", menuName = "Scriptable Objects/MapObjectsLib")]
public class MapObjectsLib : ScriptableObject
{
    [SerializeField]
    private MapGroupController mapGroupWhite;
    [SerializeField]
    private MapGroupController mapGroupRed;

    public MapGroupController GetMapGroupPrefab(TeamTag team)
    {
        switch (team)
        {
            case TeamTag.White:
                return mapGroupWhite;
            case TeamTag.Red:
                return mapGroupRed;
        }

        return mapGroupWhite;
    }
}
