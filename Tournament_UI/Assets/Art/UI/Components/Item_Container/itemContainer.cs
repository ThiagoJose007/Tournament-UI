using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System;

public class ItemContainer : MonoBehaviour
{
    UIDocument uiInventory;
    public VisualTreeAsset itemTemplate;
    public List<Item> items;
    private VisualElement currentlyActiveButton; // Referência ao botão ativo

    public void OnEnable()
    {
        uiInventory = GetComponent<UIDocument>();

        // Localiza o contêiner onde os itens serão adicionados
        VisualElement itemContainer = uiInventory.rootVisualElement.Q("ItemContainer");
        foreach (Item item in items)
        {
            // Instancia um novo elemento a partir do template
            var itemElement = CreateItemElement(item);

            // Adiciona o elemento ao contêiner
            itemContainer.Add(itemElement);
            itemElement.AddToClassList("item");  // Adiciona a classe "item" para cada item

        }
        if (itemContainer.childCount > 0)
        {
            var firstButton = itemContainer[0].Q<Button>("Equip_button"); // Substitua "InnerButtonName" pelo nome do seu botão real

            if (firstButton != null)
            {
                SetActiveButton(firstButton);  // Define o primeiro botão como ativo
            }
        }

    }

    // Método para criar e configurar um elemento de item
    private VisualElement CreateItemElement(Item item)
    {
        // Instancia o template
        var itemElement = itemTemplate.Instantiate();

        // Configura os elementos internos
        ConfigureElement(itemElement, "Item_Body", item.Item_body?.texture);
        ConfigureElement(itemElement, "Img_rarity", item.img_rarity?.texture);
        ConfigureElement(itemElement, "Car_Img", item.car_img?.texture);
        ConfigureElement(itemElement, "Car_effect", item.car_effect?.texture);

        var rarityTextElement = itemElement.Q<Label>("Text_rarity");
        if (rarityTextElement != null)
            rarityTextElement.text = item.text_rarity;

        // Configura o botão interno para detectar cliques
        ConfigureButton(itemElement, item);

        return itemElement;
        // Define o primeiro botão como ativo
    }

    internal static object Q<T>(string className)
    {
        throw new NotImplementedException();
    }

    // Método auxiliar para configurar um elemento visual com uma imagem
    private void ConfigureElement(VisualElement parent, string elementName, Texture2D texture)
    {
        var element = parent.Q<VisualElement>(elementName);
        if (element != null && texture != null)
        {
            element.style.backgroundImage = new StyleBackground(texture);
        }
    }

    // Método para configurar o clique no botão interno
    private void ConfigureButton(VisualElement itemElement, Item item)
    {
        var button = itemElement.Q<Button>("Equip_button"); // Substitua "InnerButtonName" pelo nome real do botão no UXML
        if (button != null)
        {
            button.RegisterCallback<ClickEvent>((evt) =>
            {
                SetActiveButton(button);  // Define o botão como ativo
                button.text = "EQUIPPED";  // Altera o texto do botão para "EQUIPPED"
                Debug.Log($"O botão do item '{item.displayName}' foi clicado e agora está marcado como 'EQUIPPED'!");
            });
        }
    }

    // Define o botão clicado como ativo
    private void SetActiveButton(VisualElement button)
    {
        // Remove o estado ativo do botão anterior
        if (currentlyActiveButton != null)
        {
            currentlyActiveButton.RemoveFromClassList("active");
        }
        else
        {
            currentlyActiveButton = button;
            currentlyActiveButton.AddToClassList("active");
        }

        // Define o botão atual como ativo
        currentlyActiveButton = button;
        currentlyActiveButton.AddToClassList("active");
    }
}
