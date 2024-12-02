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
    public VisualTreeAsset FilterUXML;
    public VisualTreeAsset itemTemplate;
    // Dicion�rio para mapear categorias para listas de itens
    public List<Item> items;
    public List<Item> wheelItems = new List<Item>();
    public List<Item> accessoryItems = new List<Item>();
    public List<Item> bumperItems = new List<Item>();

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
        root.Q("Navbar").Add(navbar);

        VisualElement select_button = Select_buttonUXML.Instantiate();
        root.Q("Aba_Choose").Add(select_button);

        var Button_active = select_button.Q<VisualElement>("Button");
        Button_active.AddToClassList("select_button-active");

        VisualElement itemContainer = ItemContainerUXML.Instantiate();
        root.Q("Inventory").Add(itemContainer);
        var ContainerText = itemContainer.Q<Label>("Name_inventory");

        // Crie e personalize os bot�es
        for (int i = 0; i < buttonTexts.Length; i++)
        {
            // Clone o UXML do bot�o
            var buttonElement = buttonCategoryUXML.Instantiate();

            // Atribua o nome e o texto do bot�o
            string buttonName = buttonTexts[i];
            buttonElement.name = buttonName.ToLower().Replace(" ", "-");
            var buttonText = buttonElement.Q<Label>(className: "text_category_button");
            if (buttonText != null)
                buttonText.text = buttonName;

            // Atribua a imagem ao bot�o
            var buttonImage = buttonElement.Q<VisualElement>(className: "category_img");
            var buttonBackground = buttonElement.Q<VisualElement>("Category_Button");
            if (buttonImage != null && buttonImages != null && i < buttonImages.Length)
            {
                buttonImage.style.backgroundImage = new StyleBackground(buttonImages[i].texture);
            }

            // Adicione a classe "active" ao primeiro bot�o
            if (i == 0)
            {
                buttonBackground.AddToClassList("button_category-active");
                UpdateNameContainer(buttonName);
                UpdateItemContainer(items); // Mostra os itens da primeira categoria por padr�o
            }

            // Registrar evento de clique no bot�o
            buttonElement.RegisterCallback<ClickEvent>(evt =>
            {
                // Remove a classe "active" de todos os bot�es
                var allButtons = root.Q("Select_Category").Children();
                foreach (var button in allButtons)
                {
                    var buttonBackground = button.Q<VisualElement>("Category_Button");
                    buttonBackground.RemoveFromClassList("button_category-active");
                }

                // Adiciona a classe "active" ao bot�o clicado
                buttonBackground.AddToClassList("button_category-active");

                // Atualiza o texto do NameContainer e o invent�rio
                UpdateNameContainer(buttonName);
                var inventory = uiDocument.rootVisualElement.Q("ItemContainer");
                switch (buttonName)
                {
                    case "Skins":
                        UpdateItemContainer(items);
                        break;
                    case "Color":
                        inventory.Clear();
                        break;
                    case "Wheels":
                        UpdateItemContainer(wheelItems);
                        break;
                    case "Accessories":
                        UpdateItemContainer(accessoryItems);
                        break;
                    case "Bumper":
                        UpdateItemContainer(bumperItems);
                        break;
                    case "Spoiler":
                        inventory.Clear();
                        break;
                }
            });

            // Adicione o bot�o ao cont�iner
            root.Q("Select_Category").Add(buttonElement);
        }

        // Configura o filtro
        VisualElement filter = FilterUXML.Instantiate();
        root.Q("Filter").Add(filter);

        // Configura os itens iniciais
        UpdateItemContainer(items);
    }

    private void UpdateItemContainer(List<Item> itemList)
    {
        // Localiza o cont�iner de itens
        var inventory = uiDocument.rootVisualElement.Q("ItemContainer");
        if (inventory == null)
        {
            Debug.LogError("Elemento 'ItemContainer' n�o encontrado!");
            return;
        }

        // Limpa os itens existentes
        inventory.Clear();

        // Adiciona os itens da lista ao cont�iner
        foreach (var item in itemList)
        {
            var itemElement = CreateItemElement(item);
            inventory.Add(itemElement);
            itemElement.AddToClassList("item");  // Adiciona a classe "item" para cada item
        }

        // Configura o primeiro bot�o como ativo, se houver itens
        if (inventory.childCount > 0)
        {
            var firstButton = inventory[0].Q<Button>("Equip_button");
            if (firstButton != null)
            {
                SetActiveButton(firstButton);
                firstButton.text = "EQUIPPED";
            }
        }
    }

    private void UpdateNameContainer(string categoryName)
    {
        var nameContainer = uiDocument.rootVisualElement.Q<Label>("Name_inventory");
        if (nameContainer != null)
        {
            nameContainer.text = categoryName;
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
    private void SetActiveButton(Button button)
    {
        // Remove o estado ativo de todos os bot�es
        var allButtons = uiDocument.rootVisualElement.Query<Button>("Equip_button").ToList();
        foreach (var btn in allButtons)
        {
            btn.RemoveFromClassList("active");
            btn.text = "EQUIP"; // Reseta o texto dos outros bot�es
        }

        // Define o bot�o atual como ativo
        button.AddToClassList("active");
        button.text = "EQUIPPED";
        currentlyActiveButton = button;
    }

}
