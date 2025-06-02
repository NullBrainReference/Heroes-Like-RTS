using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Zenject;

public class UnitController : MonoBehaviour
{
    [SerializeField] private Unit _unit;
    [SerializeField] private UnitNavigator _navigator;

    [Inject]
    private UnitManager _manager;

    [SerializeField] 
    private UnitController _target;
    //private List<UnitController> _targets; //TODO: Redo - target picking should be done with _unitManager

    private bool _inAttackRange = false;

    public Unit Unit => _unit;
    public UnitController Target { get => _target; set => _target = value; }
    //public List<UnitController> Targets => _targets;

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
        //_targets = new List<UnitController>();
        StartCoroutine(WaitForManager());
        StartCoroutine(ChaseTarget());
    }

    public void SetUnit(Unit unit)
    {
        _unit = unit;
    }

    private IEnumerator WaitForManager()
    {
        yield return new WaitUntil(() => _manager.Ready);

        _manager.AddUnit(this);

        yield return new WaitForSeconds(1); //TODO remove after spawn behaviour added

        _target = _manager.GetClosestTarget(this);
    }

    private IEnumerator ChaseTarget()
    {
        yield return new WaitUntil(() => Target != null);

        _navigator.MoveToTarget(Target.transform.position);

        while (!_unit.IsDead)
        {
            yield return new WaitForSeconds(1);

            if (Target.Unit.IsDead)
                SwithTarget();
            if (Target == null)
                yield break;

            if (InAttackRange)
                continue;

            if (Target != null)
                _navigator.MoveToTarget(Target.transform.position);
        }

        _navigator.Stop();
        gameObject.SetActive(false);
    }

    private void SwithTarget()
    {
        InAttackRange = false;

        _target = _manager.GetClosestTarget(this);

        //Targets.RemoveAll(x => x.Unit.IsDead);

        //var nearestObject = Targets
        //    .OrderBy(x => Vector3.Distance(transform.position, x.transform.position))
        //    .FirstOrDefault();
        //
        //if (nearestObject != null)
        //    _target = nearestObject;
    }
}
