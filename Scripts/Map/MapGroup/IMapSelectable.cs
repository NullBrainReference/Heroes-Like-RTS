using UnityEngine;

public interface IMapSelectable
{
    public Vector3 Position { get; }
    public TeamTag TeamKey { get; }

    public void Select();
    public void OnDeselect();

    public void Order(Vector3 pos);
}
