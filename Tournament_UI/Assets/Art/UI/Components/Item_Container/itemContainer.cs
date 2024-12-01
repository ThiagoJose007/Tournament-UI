using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System;

public class ItemContainer : MonoBehaviour
{
    UIDocument uiInventory;
    public VisualTreeAsset itemTemplate;
    public List<Item> items;
    private VisualElement currentlyActiveButton; // Refer�ncia ao bot�o ativo

    public void OnEnable()
    {
        uiInventory = GetComponent<UIDocument>();

        // Localiza o cont�iner onde os itens ser�o adicionados
        VisualElement itemContainer = uiInventory.rootVisualElement.Q("ItemContainer");
        foreach (Item item in items)
        {
            // Instancia um novo elemento a partir do template
            var itemElement = CreateItemElement(item);

            // Adiciona o elemento ao cont�iner
            itemContainer.Add(itemElement);
            itemElement.AddToClassList("item");  // Adiciona a classe "item" para cada item

        }
        if (itemContainer.childCount > 0)
        {
            var firstButton = itemContainer[0].Q<Button>("Equip_button"); // Substitua "InnerButtonName" pelo nome do seu bot�o real

            if (firstButton != null)
            {
                SetActiveButton(firstButton);  // Define o primeiro bot�o como ativo
            }
        }

    }

    // M�todo para criar e configurar um elemento de item
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

        // Configura o bot�o interno para detectar cliques
        ConfigureButton(itemElement, item);

        return itemElement;
        // Define o primeiro bot�o como ativo
    }

    internal static object Q<T>(string className)
    {
        throw new NotImplementedException();
    }

    // M�todo auxiliar para configurar um elemento visual com uma imagem
    private void ConfigureElement(VisualElement parent, string elementName, Texture2D texture)
    {
        var element = parent.Q<VisualElement>(elementName);
        if (element != null && texture != null)
        {
            element.style.backgroundImage = new StyleBackground(texture);
        }
    }

    // M�todo para configurar o clique no bot�o interno
    private void ConfigureButton(VisualElement itemElement, Item item)
    {
        var button = itemElement.Q<Button>("Equip_button"); // Substitua "InnerButtonName" pelo nome real do bot�o no UXML
        if (button != null)
        {
            button.RegisterCallback<ClickEvent>((evt) =>
            {
                SetActiveButton(button);  // Define o bot�o como ativo
                button.text = "EQUIPPED";  // Altera o texto do bot�o para "EQUIPPED"
                Debug.Log($"O bot�o do item '{item.displayName}' foi clicado e agora est� marcado como 'EQUIPPED'!");
            });
        }
    }

    // Define o bot�o clicado como ativo
    private void SetActiveButton(VisualElement button)
    {
        // Remove o estado ativo do bot�o anterior
        if (currentlyActiveButton != null)
        {
            currentlyActiveButton.RemoveFromClassList("active");
        }
        else
        {
            currentlyActiveButton = button;
            currentlyActiveButton.AddToClassList("active");
        }

        // Define o bot�o atual como ativo
        currentlyActiveButton = button;
        currentlyActiveButton.AddToClassList("active");
    }
}
