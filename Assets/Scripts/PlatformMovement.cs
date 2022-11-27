using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public bool verticalPlatform;
    public GameObject platform;
    public bool movingRight;
    public bool movingUp;
    

    public GameObject limitPosition;
    private Vector2 pointA;
    private Vector2 pointB;
    private float speed;
    public float time;
    private void Start()
    {
        pointA=limitPosition.transform.position;
        pointB = platform.transform.position;
    }

    private void Move()
    {
        if (verticalPlatform)
        {
            VerticalMove();
        }
        else
        {
            HorizontalMove();
        }
    }
    private void HorizontalMove()
    {
        if (pointB.x > pointA.x)
        {
            Vector2 aux = pointA;
            pointA = pointB;
            pointB = aux;
        }

        CalculateSpeed(false);

        if (movingRight)
        {
            transform.position = Vector2.MoveTowards(transform.position, pointA, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, pointB, speed * Time.deltaTime);
        }

        if (transform.position.x >= pointA.x)
        {
            movingRight = false;
        }
        else if (transform.position.x <= pointB.x)
        {
            movingRight = true;
        }
    }

    private void VerticalMove()
    {
        if (pointB.y > pointA.y)
        {
            Vector2 aux = pointA;
            pointA = pointB;
            pointB = aux;
        }

        CalculateSpeed(true);
        
        if (movingUp)
        {
            transform.position = Vector2.MoveTowards(transform.position, pointA, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, pointB, speed * Time.deltaTime);
        }

        if (transform.position.y >= pointA.y)
        {
            movingUp = false;
        }
        else if (transform.position.y <= pointB.y)
        {
            movingUp = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Tags.surfaceCheck))
        {
            GameObject.FindGameObjectWithTag(Tags.player).transform.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject.FindGameObjectWithTag(Tags.player).transform.SetParent(null);
    }

    private void CalculateSpeed(bool isVertical)
    {
        if (isVertical)
        {
            speed = (pointA.y - pointB.y)*2 / time;
        }
        else
        {
            speed = (pointA.x - pointB.x)*2 / time;
        }
    }
    private void Update()
    {
        Move();
    }

    
}
