using UnityEngine;
using UnityEngine.UIElements;

public class GarageScreen : MonoBehaviour
{
    public UIDocument uiDocument; // O UIDocument da cena principal
    public VisualTreeAsset navbarUXML; // O UXML da Navbar
    public VisualTreeAsset buttonCategoryUXML; // O UXML do botão
    public VisualTreeAsset Select_buttonUXML;
    public VisualTreeAsset ItemContainerUXML;

    // Informações para personalizar os botões
    private readonly string[] buttonTexts = { "Skins", "Color", "Wheels", "Accessories", "Bumper", "Spoiler" };
    public Sprite[] buttonImages; // Imagens para os botões (atribuídas no Inspector)

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

        // Crie e personalize os botões
        for (int i = 0; i < buttonTexts.Length; i++)
        {
            // Clone o UXML do botão
            var buttonElement = buttonCategoryUXML.Instantiate();

            // Atribua o nome do botão com base no texto da categoria
            string buttonName = buttonTexts[i]; // Usando o texto da categoria como nome
            buttonElement.name = buttonName.ToLower().Replace(" ", "-"); // Convertendo para lowercase e substituindo espaços por hífens

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

            // Adicione o botão ao contêiner
            uiDocument.rootVisualElement.Q("Select_Category").Add(buttonElement);

        }

    }


    }