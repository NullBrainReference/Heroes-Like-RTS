using UnityEngine;

public class UnitView : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    public void SetColor(Color color)
    {
        _spriteRenderer.color = color;
    }

    public void FlipX(bool flip)
    {
        if (_spriteRenderer.flipX == flip) 
            return;

        _spriteRenderer.flipX = flip;
    }
}
