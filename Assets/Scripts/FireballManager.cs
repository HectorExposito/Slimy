using UnityEngine;

public class FireballManager : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;
    public GameObject fireball;

    public float speed;
    private Vector2 force;

    
    public void Move()
    {
        
        force = new Vector2(0,speed);
        rigidbody2D.AddForce(force);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Tags.deleteFireballZone))
        {
            Destroy(fireball);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(Tags.deleteFireballZone))
        {
            Destroy(fireball);
        }
    }
    private void Update()
    {
        Move();
    }
}
