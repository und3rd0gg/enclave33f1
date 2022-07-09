using System.Collections;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Projectile : MonoBehaviour
{
    public enum Type
    {
        None,
        Arrow,
        Bullet,
        Buckshot
    }

    public Type projectileType;

    public Rigidbody2D _rigidbody2D;

    [SerializeField]
    private float lifetime;

    private void Start()
    {
        Destroy(lifetime);
    }

    private void Destroy()
    {
        GameManager.Instance.ReturnProjectileToPool(this);
    }

    private void Destroy(float timeToLive)
    {
        StartCoroutine(DestroyCoroutine());
        
        IEnumerator DestroyCoroutine()
        {
            yield return new WaitForSeconds(timeToLive);
            Destroy();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy();
    }
}