using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
public class Items_category 
{
    public  VisualElement button;
    public Item item;
    public Items_category(Item item, VisualTreeAsset template)
    {
        TemplateContainer itemButtonContainer = template.Instantiate();
        button = itemButtonContainer.Q<VisualElement>("Item_Body");
        this.item = item;
        button.style.backgroundImage = new StyleBackground(item.Item_body);

        button.RegisterCallback<ClickEvent>(OnClick);
    }

    public void OnClick(ClickEvent evt)
    {
        Debug.Log("The inventory slot with the name" + item.displayName + "has been clicked");  
    }
}


