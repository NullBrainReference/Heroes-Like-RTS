using UnityEngine;

public class MapGroupOuterTrigger : MonoBehaviour
{
    [SerializeField]
    private MapGroupController _controller;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var collisionObject = collision.gameObject.GetComponent<IMapCollisionObject>();

        if (collisionObject == null)
        {
            Debug.Log("no collision object found");
            return;
        }

        SceneLoadUtil.LoadBattleSync(_controller.MapGroup, new MapGroup()); //TODO: make real one
        Debug.Log("passed scene load");
    }
}
