using UnityEngine;

public interface IMapCollisionObject<T>: IMapCollisionObject
{
    public T Payload { get; }
}

public interface IMapCollisionObject
{
    public void OnCollision(IMapCollisionObject collisionObject);
}
