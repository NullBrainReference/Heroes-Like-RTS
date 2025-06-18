using UnityEngine;
using System.Collections;

public class UnitDamager : MonoBehaviour
{
    protected Unit _unit;

    [SerializeField]
    protected UnitController _controller;
    [SerializeField]
    private UnitController _target;

    [SerializeField]
    private bool _ready = true;

    protected bool Ready { get => _ready; set => _ready = value; }

    private void Awake()
    {
        _unit = _controller.Unit;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger)
            return;

        var unitController = collision.GetComponent<UnitController>();
        if (unitController == null)
            return;

        Debug.Log("UnitTrigger (Damager) Unit detected");

        if (_controller.Target == unitController)
        {
            _controller.InAttackRange = true;
            _target = unitController;
            Hit(unitController.Unit);
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.isTrigger)
            return;

        var unitController = collision.GetComponent<UnitController>();
        if (unitController == null)
            return;

        Debug.Log("UnitTrigger (Damager) Unit detected");

        if (_controller.Target == unitController)
        {
            _controller.InAttackRange = false;
            _target = null;
        }
    }

    protected virtual void Hit(Unit unit)
    {
        if (!_ready)
            return;
        if (_unit.IsDead)
            return;

        unit.TakeDamage(_unit);
        _target.View.Damage(); //TODO: Replace with UnitEventsController

        _ready = false;
        StartCoroutine(ReloadCoroutine());
    }
    
    protected IEnumerator ReloadCoroutine()
    {
        yield return new WaitForSeconds(1.5f);

        _ready = true;

        if (_target != null)
            Hit(_target.Unit);
    }
}
