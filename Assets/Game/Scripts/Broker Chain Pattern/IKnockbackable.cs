using UnityEngine;

public interface IKnockbackable
{
    void ApplyKnockback(Vector2 direction, float force);
    void SetKnockbackState(bool state);
    bool IsKnockedBack { get; }
    Rigidbody2D GetRigidbody2D();

}
