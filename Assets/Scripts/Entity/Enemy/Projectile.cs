using UnityEngine;

public class Projectile : KillingObject {
    public float speed;
    public float lifeTime;
    public float deviation;

    void Awake()
    {
        Destroy(gameObject, lifeTime);
    }

    void Start()
    {
        transform.Rotate(0, 0, Random.Range(-deviation, deviation));
    }

    void FixedUpdate()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }
}
