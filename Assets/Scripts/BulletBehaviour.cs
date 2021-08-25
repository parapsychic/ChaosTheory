using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] BulletSpawner spawner;

    public float damage = 10f;
    public Vector2 velocity;
    [SerializeField] float lifetime = 3f;

    float startTime;

    

    private void Update()
    {
        transform.Translate(velocity * speed * Time.deltaTime);

        if (Time.time > startTime + lifetime)
            ResetBullet();
    }

    public void SetSpawner(BulletSpawner spawner)
    {
        this.spawner = spawner;
    }

    public void Activate()
    {
        startTime = Time.time;
        gameObject.SetActive(true);
    }

    void ResetBullet()
    {
        spawner.ReturnToPool(gameObject);
        gameObject.SetActive(false);

    }

}
