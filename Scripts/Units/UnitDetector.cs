using UnityEngine;

public class UnitDetector : MonoBehaviour
{
    [SerializeField]
    private UnitController _controller;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("UnitTrigger Entered");

        if (_controller.Target != null)
            return;
        if (collision.isTrigger)
            return;

        var unitController = collision.GetComponent<UnitController>();
        if (unitController == null)
            return;

        Debug.Log("UnitTrigger Unit detected");

        if (unitController.Unit.IsEnemy(_controller.Unit))
        {
            _controller.Target = unitController;
        }
    }
}
