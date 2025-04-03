using UnityEngine;
using System.Collections;

public class UnitShooter : UnitDamager
{
    [SerializeField] private Bullet _bullet;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        return;
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        return;
    }

    private void FixedUpdate()
    {
        if (_controller.Target == null)
            return;

        Hit(_controller.Target.Unit);
    }

    protected override void Hit(Unit unit)
    {
        if (!Ready)
            return;
        if (_unit.IsDead)
            return;

        Bullet bullet = Instantiate(_bullet, _controller.transform.position, _controller.transform.rotation);
        bullet.Damage = _unit.Damage;
        bullet.Target = _controller.Target;

        Ready = false;
        StartCoroutine(ReloadCoroutine());
    }
}
