using System.Collections;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    [SerializeField] private Unit _unit;
    [SerializeField] private UnitNavigator _navigator;

    [SerializeField] private UnitController _target;

    private bool _inAttackRange = false;

    public Unit Unit => _unit;
    public UnitController Target { get => _target; set => _target = value; }
    public bool InAttackRange { 
        get 
        {
            return _inAttackRange;
        } 
        set 
        {
            _inAttackRange = value;
            if (_inAttackRange)
                _navigator.Stop();
        }
    }

    private void Awake()
    {
        StartCoroutine(ChaseTarget());
    }

    private IEnumerator ChaseTarget()
    {
        yield return new WaitUntil(() => Target != null);

        _navigator.MoveToTarget(Target.transform.position);

        while (!_unit.IsDead)
        {
            yield return new WaitForSeconds(1);

            if (InAttackRange)
                continue;

            if (Target != null)
                _navigator.MoveToTarget(Target.transform.position);
        }
    }
}
