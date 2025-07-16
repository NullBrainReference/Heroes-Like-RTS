using UnityEngine;
using UnityEngine.UI;

public class TownCell : MonoBehaviour
{
    [SerializeField] private Image icon;

    public void SetIcon(Sprite sprite)
    {
        icon.sprite = sprite;
    }
}
