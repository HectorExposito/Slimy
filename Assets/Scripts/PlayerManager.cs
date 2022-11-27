using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    public float shootPower = 10f;
    public Rigidbody2D rigidbody2d;

    public Vector2 minShootPower;
    public Vector2 maxShootPower;

    Camera camera;
    public Vector2 force;
    Vector3 startPoint;
    Vector3 endPoint;

    public SurfaceCheck surfaceCheck;

    public GameObject spawn;
    
    public static Vector2 lastCheckpointPosition;

    bool firstThrow;

    Animator slimeAnimator;

    bool disableMovement;


    private void Awake()
    {
        camera = Camera.main;
    }
    private void Start()
    {
        lastCheckpointPosition = spawn.transform.position;
        GameObject.FindGameObjectWithTag(Tags.player).transform.position = lastCheckpointPosition;
    }

    private void Update()
    {
        
        if (surfaceCheck.isGrounded || surfaceCheck.isInWall)
        {
            DragAndShoot();
        }

        
    }

    private void DragAndShoot()
    {
        if (!disableMovement)
        {
            if (Input.GetMouseButtonDown(0))
            {
                startPoint = camera.ScreenToWorldPoint(Input.mousePosition);
                startPoint.z = 15;
            }

            if (Input.GetMouseButtonUp(0))
            {
                endPoint = camera.ScreenToWorldPoint(Input.mousePosition);
                endPoint.z = 15;


                if (!firstThrow)
                {
                    firstThrow = true;
                    GameManager.Instance.DisableTutorial();
                }


                force = new Vector2(Mathf.Clamp(startPoint.x - endPoint.x, minShootPower.x, maxShootPower.x),
                                    Mathf.Clamp(startPoint.y - endPoint.y, minShootPower.y, maxShootPower.y));


                if (!(surfaceCheck.isInWall && CheckIfSlimeGoesVertical()))
                {
                    rigidbody2d.AddForce(force * shootPower, ForceMode2D.Impulse);
                }

            }
        }
        
    }

    private bool CheckIfSlimeGoesVertical()
    {
        if(Mathf.Abs(startPoint.x- endPoint.x)<2.5)
        {
            return true;
        }
        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Tags.gameOverZone))
        {
            DecreaseLife();
        } else if (collision.CompareTag(Tags.greenBucket)|| collision.CompareTag(Tags.blueBucket)|| collision.CompareTag(Tags.redBucket))
        {
            DeactivateBucket(collision);
        } else if(collision.CompareTag(Tags.leftWall) || collision.CompareTag(Tags.rightWall)) 
        {
            {
                StartCoroutine(DisableImpulseCoroutine());
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(Tags.fireball))
        {
            DecreaseLife();
            Destroy(collision.gameObject);
        }
    }


    public void ResetSpawn()
    {
        lastCheckpointPosition = spawn.transform.position;
    }

    
    private void DeactivateBucket(Collider2D collision)
    {
        disableMovement = true;
        ChangeAnimation(SlimeStates.LEVEL_COMPLETED);
        
        switch (collision.tag)
        {
            case Tags.greenBucket:
                GameObject.FindGameObjectWithTag(Tags.greenBucket).SetActive(false);
                GameManager.Instance.LevelCompleted(0);
                break;
            case Tags.blueBucket:
                GameObject.FindGameObjectWithTag(Tags.blueBucket).SetActive(false);
                GameManager.Instance.LevelCompleted(1);
                break;
            case Tags.redBucket:
                GameObject.FindGameObjectWithTag(Tags.redBucket).SetActive(false);
                GameManager.Instance.LevelCompleted(2);
                break;
            default:
                break;
        }
    }

   
   public void DecreaseLife()
    {
        HeartManager.Instance.DecreaseLife();
        rigidbody2d.velocity = new Vector2(0, 0);
        if (HeartManager.Instance.GetLifes() != 0)
        {
            GameObject.FindGameObjectWithTag(Tags.player).transform.position = lastCheckpointPosition;
            ChangeAnimation(SlimeStates.RESPAWN);
        }
        
    }

    private void ChangeAnimation(SlimeStates state)
    {
        slimeAnimator = GetComponent<Animator>();
        switch (state)
        {
            case SlimeStates.LEVEL_COMPLETED:
                slimeAnimator.SetBool("LevelCompleted", true);
                break;
            case SlimeStates.RESPAWN:
                slimeAnimator.SetBool("Respawn",true);
                StartCoroutine(SlimeIdleAnimationCoroutine());
                break;
        }
        
    }

    private IEnumerator DisableImpulseCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        rigidbody2d.velocity = new Vector2(0, 0);
    }

    private IEnumerator SlimeIdleAnimationCoroutine()
    {
        yield return new WaitForSeconds(0.7f);
        slimeAnimator.SetBool("Respawn", false);
    }

    private enum SlimeStates
    {
        LEVEL_COMPLETED, RESPAWN
    }

}
