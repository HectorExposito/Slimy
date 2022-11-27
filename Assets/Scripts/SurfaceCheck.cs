using UnityEngine;

public class SurfaceCheck : MonoBehaviour
{
    public bool isGrounded;
    public bool isInWall;

    public Rigidbody2D slimeRigidbody2D;
    public float defaultGravity;
    public float wallGravity;

    public BoxCollider2D surfaceCheckBoxCollider2D;
    public GameObject slime;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag(Tags.rightWall))
        {
            RotateSlime(WallCollision.TouchWall, true);
        }
        else if(collision.CompareTag(Tags.leftWall))
        {
            RotateSlime(WallCollision.TouchWall, false);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(Tags.ground))
        {
            isGrounded = true;
        }

        if (collision.CompareTag(Tags.rightWall) || collision.CompareTag(Tags.leftWall))
        {
            isInWall = true;
            slimeRigidbody2D.gravityScale = wallGravity;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.CompareTag(Tags.ground))
        {
            isGrounded = false;
        }

        if (collision.CompareTag(Tags.rightWall) || collision.CompareTag(Tags.leftWall))
        {
            slimeRigidbody2D.gravityScale = defaultGravity;
            isInWall = false;
            if (collision.CompareTag(Tags.rightWall))
            {
                RotateSlime(WallCollision.LeaveWall,true);
            }
            else
            {
                RotateSlime(WallCollision.LeaveWall, false);
            }
            
        }
    }
    
    private void RotateSlime(WallCollision wallCollision,bool isRightWall)
    {
        if (wallCollision == WallCollision.TouchWall)
        {
            
            if (isRightWall)
            {
                slime.transform.Rotate(0, 0, 90);
            }
            else
            {
                slime.transform.Rotate(0, 0, -90);
            }
        }
        else
        {
            
            if (isRightWall)
            {
                slime.transform.Rotate(0, 0, -90);
            }
            else
            {
                slime.transform.Rotate(0, 0, 90);
            }
        }
    }

    private enum WallCollision
    {
        TouchWall, LeaveWall
    }
    
   
}
