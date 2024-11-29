using UnityEngine;
using UnityEngine.UIElements;

public class GarageScreen : MonoBehaviour
{
    public UIDocument uiDocument; // O UIDocument da cena principal
    public VisualTreeAsset navbarUXML; // O UXML da Navbar
    public VisualTreeAsset buttonCategoryUXML; // O UXML do bot�o

    // Informa��es para personalizar os bot�es
    private readonly string[] buttonTexts = { "Skins", "Color", "Wheels", "Acessories", "Bumper", "Spoiler" };
    public Sprite[] buttonImages; // Imagens para os bot�es (atribu�das no Inspector)

    void Start()
    {
        // Obtenha o rootVisualElement da cena
        var root = uiDocument.rootVisualElement;

        // Carregue o UXML da Navbar
        VisualElement navbar = navbarUXML.CloneTree();
        root.Add(navbar);

        // Crie um cont�iner para os bot�es
        var buttonContainer = new VisualElement();
        buttonContainer.style.flexDirection = FlexDirection.Column;
        buttonContainer.style.alignItems = Align.FlexStart;
        buttonContainer.style.paddingLeft = 30;
        buttonContainer.style.marginTop = 150;
        root.Add(buttonContainer);

        // Crie e personalize os bot�es
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

            // Adicione o bot�o ao cont�iner
            buttonContainer.Add(buttonElement);
        }
    }
}
