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
    private string skinName;
    private string equipedSkinHeader = "Main";
    private string spriteHeader;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        UpdateSkin();
    }

    public void UpdateEquipedSkin(string skinHeader)
    {
        equipedSkinHeader = skinHeader;
    }

    private void UpdateSkin()
    {
        spriteName = spriteRenderer.sprite.name;
        spriteHeader = spriteName.Substring(0, spriteName.IndexOf("_") + 1);
        skinNumber = int.Parse(spriteName.Substring(spriteName.Length - 1));
        skinName = spriteName.Replace(spriteHeader, "");
        skinName = skinName.Remove(skinName.Length - 2);
        skinName = equipedSkinHeader + "_" + skinName + "-" + skinNumber;
        for (int i = 0; i < skins.Length; i++)
        {
            if (skins[i].name.Equals(equipedSkinHeader))
            {
                for (int j = 0; j < skins[i].sprites.Length; j++)
                {
                    if (skins[i].sprites[j].name.Equals(skinName))
                    {
                        spriteRenderer.sprite = skins[i].sprites[j];
                    }
                }
            }
        }
    }
}
