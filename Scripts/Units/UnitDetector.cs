using UnityEngine;

public class UnitDetector : MonoBehaviour
{
    [SerializeField]
    private UnitController _controller;


    //TODO: Redo - detecting with UnitManager
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("UnitTrigger Entered");

        //if (_controller.Target != null)
        //    return;
        if (collision.isTrigger)
            return;

        var unitController = collision.GetComponent<UnitController>();
        if (unitController == null)
            return;

        Debug.Log("UnitTrigger Unit detected");

        if (unitController.Unit.IsEnemy(_controller.Unit))
        {
            if (unitController.Unit.IsDead)
                return;

            if (_controller.Target == null)
                _controller.Target = unitController;

            _controller.Targets.Add(unitController);
        }
    }
}
