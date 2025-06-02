using UnityEngine;
using Zenject;

public class Spawner : MonoBehaviour
{
    [Inject]
    private DiContainer _diContainer;

    private bool _ready = true;

    public bool Ready => _ready;

    private void OnCollisionExit2D(Collision2D collision)
    {
        var controller = collision.gameObject.GetComponent<UnitController>();

        if (controller == null)
            return;

        _ready = true;
    }

    public void Spawn(UnitController unitPrefab)
    {
        _diContainer.InstantiatePrefab(unitPrefab, this.transform);

        _ready = false;
    }
}
