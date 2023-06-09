using UnityEngine;

public class ShopKeeperController : MonoBehaviour
{
    public float speed = 2f;
    public float updateInterval = 1f;

    private Rigidbody2D playerRigidbody;
    private Animator animator;
    private Vector2 movementInput;
    private float timeElapsed = 0;
    private bool isContactPlayer = false;

    void Start()
    {
        playerRigidbody = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
    }

    void Update()
    {
        if (!isContactPlayer)
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed > updateInterval)
            {
                switch (Random.Range(0, 3))
                {
                    case 0:
                        movementInput.x = Random.Range(-1, 2);
                        break;
                    case 1:
                        movementInput.y = Random.Range(-1, 2);
                        break;
                    default:
                        movementInput.x = 0f;
                        movementInput.y = 0f;
                        break;
                }
                timeElapsed = 0f;
            }
        }
        else
        {
            movementInput.x = 0f;
            movementInput.y = 0f;
        }
        animator.SetFloat("Horizontal", movementInput.x);
        animator.SetFloat("Vertical", movementInput.y);
        animator.SetFloat("Speed", movementInput.sqrMagnitude);
    }

    void FixedUpdate()
    {
        playerRigidbody.MovePosition(playerRigidbody.position + (movementInput.normalized * speed * Time.fixedDeltaTime));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            isContactPlayer = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            isContactPlayer = false;
        }
    }
}
