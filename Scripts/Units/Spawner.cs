using UnityEngine;
using System.Collections;
using Zenject;

public class Spawner : MonoBehaviour
{
    [Inject]
    private DiContainer _diContainer;

    private bool _ready = true;

    public bool Ready => _ready;

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    var controller = collision.gameObject.GetComponent<UnitController>();
    //
    //    if (controller == null)
    //        return;
    //
    //    _ready = true;
    //}

    public void Spawn(UnitController unitPrefab, Unit unit, Color color)
    {
        var unitController = _diContainer.InstantiatePrefabForComponent<UnitController>(unitPrefab, transform);

        unitController.SetUnit(unit);
        unitController.View.SetColor(color);

        _ready = false;

        StartCoroutine(RefreshCoroutine());
    }

    private IEnumerator RefreshCoroutine()
    {
        yield return new WaitForSeconds(1.5f);

        _ready = true;
    }
}
