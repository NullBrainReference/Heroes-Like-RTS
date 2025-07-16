using UnityEngine;
using TMPro;

public class MapGroupTooltip : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _groupText;
    [SerializeField]
    private Canvas _canvas;

    private RectTransform _rectTransform;

    private void Awake()
    {
        if (_rectTransform == null)
            _rectTransform = gameObject.GetComponent<RectTransform>();    
    }

    public void Show(MapGroup mapGroup)
    {
        var pos = Input.mousePosition;

        SetScreenPos(pos);

        gameObject.SetActive(true);

        _groupText.text = "";

        foreach (var pair in MapGroupProcessor.GetUnitsWithCount(mapGroup))
        {
            _groupText.text += $"{pair.unitType.ToString()} : {pair.count}\n";
        }
    }

    private void FixedUpdate()
    {
        if (!gameObject.activeInHierarchy)
            return;

        var pos = Input.mousePosition;

        SetScreenPos(pos);
    }

    private void SetScreenPos(Vector3 pos)
    {
        if (_rectTransform == null)
            _rectTransform = gameObject.GetComponent<RectTransform>();

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _canvas.transform as RectTransform,
            pos,
            _canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : _canvas.worldCamera,
            out Vector2 localPoint
        );

        _rectTransform.anchoredPosition = localPoint + new Vector2(0, _rectTransform.rect.height) / 2;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
