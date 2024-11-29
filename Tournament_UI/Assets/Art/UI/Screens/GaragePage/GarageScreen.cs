using UnityEngine;
using UnityEngine.UIElements;

public class GarageScreen : MonoBehaviour
{
    public UIDocument uiDocument; // O UIDocument da cena principal
    public VisualTreeAsset navbarUXML; // O UXML da Navbar
    public VisualTreeAsset buttonCategoryUXML; // O UXML do botão

    // Informações para personalizar os botões
    private readonly string[] buttonTexts = { "Skins", "Color", "Wheels", "Acessories", "Bumper", "Spoiler" };
    public Sprite[] buttonImages; // Imagens para os botões (atribuídas no Inspector)

    void Start()
    {
        // Obtenha o rootVisualElement da cena
        var root = uiDocument.rootVisualElement;

        // Carregue o UXML da Navbar
        VisualElement navbar = navbarUXML.CloneTree();
        root.Add(navbar);

        // Crie um contêiner para os botões
        var buttonContainer = new VisualElement();
        buttonContainer.style.flexDirection = FlexDirection.Column;
        buttonContainer.style.alignItems = Align.FlexStart;
        buttonContainer.style.paddingLeft = 30;
        buttonContainer.style.marginTop = 150;
        root.Add(buttonContainer);

        // Crie e personalize os botões
        for (int i = 0; i < buttonTexts.Length; i++)
        {
            var buttonElement = buttonCategoryUXML.CloneTree();

            // Atualize o texto
            var buttonText = buttonElement.Q<Label>(className: "text_category_button"); // Use a classe para buscar o texto
            if (buttonText != null)
                buttonText.text = buttonTexts[i];

            // Atualize a imagem
            var buttonImage = buttonElement.Q<VisualElement>(className: "category_img"); // Use a classe para buscar a imagem
            if (buttonImage != null && buttonImages != null && i < buttonImages.Length)
            {
                buttonImage.style.backgroundImage = new StyleBackground(buttonImages[i].texture);
            }

            // Adicione o botão ao contêiner
            buttonContainer.Add(buttonElement);
        }
    }
}
