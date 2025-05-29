using UnityEngine;
using UnityEngine.AI;

public class UnitNavigator : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;

        _agent.updatePosition = true;
    }

    public void MoveToTarget(Vector3 pos)
    {
        _agent.SetDestination(pos);
        _agent.isStopped = false;
    }

    public void Stop()
    {
        _agent.isStopped = true;
    }
}
