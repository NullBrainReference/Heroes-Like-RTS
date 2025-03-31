using UnityEngine;
using System.Collections;

public class UnitDamager : MonoBehaviour
{
    private Unit _unit;

    [SerializeField]
    private UnitController _controller;
    [SerializeField]
    private UnitController _target;

    [SerializeField]
    private bool _ready = true;

    private void Awake()
    {
        _unit = _controller.Unit;
    }

    private void OnTriggerEnter2D(Collider2D collision)
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

    private void OnTriggerExit2D(Collider2D collision)
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

    private void Hit(Unit unit)
    {
        if (!_ready)
            return;
        if (_unit.IsDead)
            return;

        unit.TakeDamage(_unit);

        _ready = false;
        StartCoroutine(ReloadCoroutine());
    }
    
    private IEnumerator ReloadCoroutine()
    {
        yield return new WaitForSeconds(1.5f);

        _ready = true;

        if (_target != null)
            Hit(_target.Unit);
    }
}
