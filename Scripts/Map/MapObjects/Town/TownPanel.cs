using UnityEngine;
using TMPro;

public class TownPanel : MonoBehaviour
{
    private Town _town;

    private MapGroup _group;

    [SerializeField]
    private TextMeshProUGUI _unitText;
    [SerializeField]
    private TextMeshProUGUI _countText;

    public void Display(Town town, MapGroup mapGroup)
    {
        _town = town;
        _group = mapGroup;

        gameObject.SetActive(true);

        _unitText.text = _town.Buildings[0].UnitType.ToString();
        _countText.text = _town.Buildings[0].UnitsCount.ToString();
    }

    public void Hire()
    {
        _town.Buildings[0].Hire(_group);

        _countText.text = _town.Buildings[0].UnitsCount.ToString();
    }

    public void Hide()
    {
        _town = null;
        _group = null;

        gameObject.SetActive(false);
    }
}
