using UnityEngine;
using UnityEngine.InputSystem;

public class MenuPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject _panel;

    private void Start()
    {
        MapInputsManager.Instance.SubscribeOnMenu(Switch);
    }

    private void Switch(InputAction.CallbackContext context)
    {
        _panel.SetActive(!_panel.activeInHierarchy);
    }

    private void OnDestroy()
    {
        //MapInputsManager.Instance.UnsubscribeOnMenu(Switch);
    }
}
