using UnityEngine;

public class MergeListener : MonoBehaviour, IActivatable
{
    [SerializeField] private Collider2D _collisionCollider;

    public void Deactivate() => _collisionCollider.enabled = false;
    public void Activate() => _collisionCollider.enabled = true;

    private void OnValidate()
    {
        if (_collisionCollider.isTrigger == true)
            _collisionCollider = null;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        transform.rotation = Quaternion.identity;
        collision.transform.rotation = Quaternion.identity;
    }
}
