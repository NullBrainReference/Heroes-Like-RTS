using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    public float Damage { get; set; }
    public UnitController Target { get; set; }

    private void FixedUpdate()
    {
        if (Target == null)
            return;

        Move();        
    }

    public void Move()
    {
        if (Target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = (Target.transform.position - transform.position).normalized;

        transform.position += direction * _speed * Time.deltaTime;

        float distanceToTarget = Vector3.Distance(transform.position, Target.transform.position);
        if (distanceToTarget <= 0.1f)
        {
            HitTarget();
        }
    }

    private void HitTarget()
    {
        Target.Unit.TakeDamage(Damage);
        Target.View.Damage();
        Destroy(gameObject);
    }
}
