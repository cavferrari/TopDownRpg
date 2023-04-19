using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;

    private Rigidbody2D playerRigidbody;
    private Animator animator;
    private Vector2 movementInput;
    private bool isContactShopKeeper = false;

    void Start()
    {
        playerRigidbody = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Horizontal", movementInput.x);
        animator.SetFloat("Vertical", movementInput.y);
        animator.SetFloat("Speed", movementInput.sqrMagnitude);
    }

    void FixedUpdate()
    {
        playerRigidbody.MovePosition(playerRigidbody.position + (movementInput.normalized * speed * Time.fixedDeltaTime));
    }

    public bool IsPlayerContactShopKeeper()
    {
        return isContactShopKeeper;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("ShopKeeper"))
        {
            isContactShopKeeper = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.Equals("ShopKeeper"))
        {
            isContactShopKeeper = false;
        }
    }
}
