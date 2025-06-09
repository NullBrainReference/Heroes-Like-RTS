using UnityEngine;

public class TownOuterTrigger : MonoBehaviour
{
    [SerializeField]
    private TownController _controller;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var collisionObject = collision.gameObject.GetComponent<IMapCollisionObject<MapGroupController>>();

        if (collisionObject == null)
        {
            Debug.Log("no collision object found");
            return;
        }

        _controller.OnCollision(collisionObject);
    }
}
