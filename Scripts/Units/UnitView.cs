using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public enum UnitAnimationType
{
    Damage,

}

public class UnitView : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    private Color _color;

    private Dictionary<UnitAnimationType, Tween> _activeAnimations;

    private void Awake()
    {
        _activeAnimations = new Dictionary<UnitAnimationType, Tween>();
    }
    public void SetColor(Color color)
    {
        _spriteRenderer.color = color;
        _color = color;
    }

    public void FlipX(bool flip)
    {
        if (_spriteRenderer.flipX == flip) 
            return;

        _spriteRenderer.flipX = flip;
    }

    public void Damage()
    {
        if (_activeAnimations.Remove(UnitAnimationType.Damage, out var existingTween))
        {
            existingTween?.Kill();
        }

        var seq = DOTween.Sequence();

        seq.Append(_spriteRenderer.DOColor(Color.red, 0.2f));
        seq.Append(_spriteRenderer.DOColor(_color, 0.2f));

        seq.OnComplete(() =>
        {
            seq.Kill();
        });

        _activeAnimations.Add(UnitAnimationType.Damage, seq);
    }
}
