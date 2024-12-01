using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GarageScreen : MonoBehaviour
{
    public UIDocument uiDocument; // O UIDocument da cena principal
    public VisualTreeAsset navbarUXML; // O UXML da Navbar
    public VisualTreeAsset buttonCategoryUXML; // O UXML do bot�o
    public VisualTreeAsset Select_buttonUXML;
    public VisualTreeAsset ItemContainerUXML;
    public VisualTreeAsset itemTemplate;
    public List<Item> items;
    private VisualElement currentlyActiveButton;

    // Informa��es para personalizar os bot�es
    private readonly string[] buttonTexts = { "Skins", "Color", "Wheels", "Accessories", "Bumper", "Spoiler" };
    public Sprite[] buttonImages; // Imagens para os bot�es (atribu�das no Inspector)

    void Start()
    {
        // Obtenha o rootVisualElement da cena
        var root = uiDocument.rootVisualElement;
        uiDocument = GetComponent<UIDocument>();

        // Carregue o UXML da Navbar
        VisualElement navbar = navbarUXML.Instantiate();
        uiDocument.rootVisualElement.Q("Navbar").Add(navbar);

        VisualElement select_button = Select_buttonUXML.Instantiate();
        uiDocument.rootVisualElement.Q("Aba_Choose").Add(select_button);

        VisualElement itemContainer = ItemContainerUXML.Instantiate();
        uiDocument.rootVisualElement.Q("Inventory").Add(itemContainer);
        var ContainerText = itemContainer.Q<Label>("Name_inventory");
        ContainerText.text = "Skins";

        // Crie e personalize os bot�es
        for (int i = 0; i < buttonTexts.Length; i++)
        {
            // Clone o UXML do bot�o
            var buttonElement = buttonCategoryUXML.Instantiate();

            // Atribua o nome do bot�o com base no texto da categoria
            string buttonName = buttonTexts[i]; // Usando o texto da categoria como nome
            buttonElement.name = buttonName.ToLower().Replace(" ", "-"); // Convertendo para lowercase e substituindo espa�os por h�fens

            // Atualize o texto
            var buttonText = buttonElement.Q<Label>(className: "text_category_button");
            if (buttonText != null)
                buttonText.text = buttonTexts[i];

            // Atualize a imagem
            var buttonImage = buttonElement.Q<VisualElement>(className: "category_img");
            if (buttonImage != null && buttonImages != null && i < buttonImages.Length)
            {
                buttonImage.style.backgroundImage = new StyleBackground(buttonImages[i].texture);
            }

            if (i == 0)
            {
                buttonElement.AddToClassList("button_category-active");
            }

            // Registrar o evento de clique no bot�o
            buttonElement.RegisterCallback<ClickEvent>(evt =>
            {
                // Remover a classe "active" de todos os bot�es
                var allButtons = uiDocument.rootVisualElement.Q("Select_Category").Children();
                foreach (var button in allButtons)
                {
                    button.RemoveFromClassList("button_category-active");
                }

                // Adicionar a classe "active" ao bot�o clicado
                buttonElement.AddToClassList("button_category-active");
            });
            // Adicione o bot�o ao cont�iner
            uiDocument.rootVisualElement.Q("Select_Category").Add(buttonElement);

        }


        // Localiza o cont�iner onde os itens ser�o adicionados
        VisualElement Inventory = uiDocument.rootVisualElement.Q("ItemContainer");
        foreach (Item item in items)
        {
            // Instancia um novo elemento a partir do template
            var itemElement = CreateItemElement(item);

            // Adiciona o elemento ao cont�iner
            Inventory.Add(itemElement);
            itemElement.AddToClassList("item");  // Adiciona a classe "item" para cada item

        }
        if (itemContainer.childCount > 0)
        {
            var firstButton = Inventory[0].Q<Button>("Equip_button"); // Substitua "InnerButtonName" pelo nome do seu bot�o real

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
