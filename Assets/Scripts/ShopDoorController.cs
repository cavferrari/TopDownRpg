using UnityEngine;
using UnityEngine.Tilemaps;

public class ShopDoorController : MonoBehaviour
{
    public Transform doorTransform;
    public GameObject shopCeiling;

    private TilemapRenderer shopDoorRenderer;
    private TilemapRenderer shopCeilingRenderer;
    private Transform playerTransform;
    private bool isPlayerInShop = false;

    void Start()
    {
        shopDoorRenderer = this.GetComponent<TilemapRenderer>();
        shopCeilingRenderer = shopCeiling.GetComponent<TilemapRenderer>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (playerTransform.position.y > doorTransform.position.y)
        {
            isPlayerInShop = true;
        }
        else
        {
            isPlayerInShop = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            shopDoorRenderer.enabled = false;
            shopCeilingRenderer.enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            shopDoorRenderer.enabled = true;
            if (isPlayerInShop)
            {
                shopCeilingRenderer.enabled = false;
            }
            else
            {
                shopCeilingRenderer.enabled = true;
            }
        }
    }
}
