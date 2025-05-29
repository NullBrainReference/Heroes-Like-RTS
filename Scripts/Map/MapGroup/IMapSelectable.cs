using UnityEngine;

public interface IMapSelectable
{
    public Vector3 Position { get; }

    public void Select();
    public void OnDeselect();

    public void Order(Vector3 pos);
}
