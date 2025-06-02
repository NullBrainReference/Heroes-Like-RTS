using UnityEngine;

public class MapGroupOuterTrigger : MonoBehaviour
{
    [SerializeField]
    private MapGroupController _controller;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var collisionObject = collision.gameObject.GetComponent<IMapCollisionObject<MapGroupController>>();

        if (collisionObject == null)
        {
            Debug.Log("no collision object found");
            return;
        }

        _controller.OnCollision(collisionObject);

        //SceneLoadUtil.LoadBattleSync(_controller.MapGroup, collisionObject.Payload.MapGroup); //TODO: make real one
        //Debug.Log("passed scene load");
    }
}
