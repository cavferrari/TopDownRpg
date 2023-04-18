using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct Skin
{
    public string name;
    public Sprite[] sprites;
}

public class PlayerCustomize : MonoBehaviour
{
    public Skin[] skins;

    private SpriteRenderer spriteRenderer;
    private string spriteName;
    private int skinNumber;
    private string equipedSkin = "Main";

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        UpdateSkin(equipedSkin);
    }

    public void UpdateEquipedSkin(string skinName)
    {
        equipedSkin = skinName;
    }

    private void UpdateSkin(string skinName)
    {
        spriteName = spriteRenderer.sprite.name;
        skinNumber = int.Parse(spriteName.Substring(spriteName.Length - 1));
        for (int i = 0; i < skins.Length; i++)
        {
            if (skins[i].name.Equals(equipedSkin))
            {
                spriteRenderer.sprite = skins[i].sprites[skinNumber];
            }
        }
    }
}
