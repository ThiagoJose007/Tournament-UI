using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "Item", menuName ="Skins/Item")]
public class Item : ScriptableObject
{
    public string displayName;
    public Sprite Item_body;
    public Sprite img_rarity;
    public string text_rarity;
    public Sprite car_effect;
    public Sprite car_img;

}
