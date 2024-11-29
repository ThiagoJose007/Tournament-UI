using UnityEngine;
using UnityEngine.UIElements;

public class GarageScreen : MonoBehaviour
{
    public UIDocument uiDocument; // O UIDocument da cena principal
    public VisualTreeAsset navbarUXML; // O UXML da Navbar
    public VisualTreeAsset buttonCategoryUXML; // O UXML do bot�o

    // Informa��es para personalizar os bot�es
    private readonly string[] buttonTexts = { "Button 1", "Button 2", "Button 3", "Button 4", "Button 5", "Button 6" };
    private readonly string[] buttonLinks = { "Link1", "Link2", "Link3", "Link4", "Link5", "Link6" };
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
        buttonContainer.style.alignItems = Align.Center;
        buttonContainer.style.marginTop = 20;
        root.Add(buttonContainer);

        // Crie e personalize os bot�es
        for (int i = 0; i < buttonTexts.Length; i++)
        {
            var buttonElement = buttonCategoryUXML.CloneTree();

            // Personalize o texto do bot�o
            var buttonText = buttonElement.Q<Label>("ButtonText"); // Substitua pelo nome do Label no UXML
            if (buttonText != null)
                buttonText.text = buttonTexts[i];

            // Personalize a imagem do bot�o
            var buttonImage = buttonElement.Q<VisualElement>("ButtonImage"); // Substitua pelo nome da imagem no UXML
            if (buttonImage != null && buttonImages != null && i < buttonImages.Length)
            {
                var imageTexture = buttonImages[i].texture;
                buttonImage.style.backgroundImage = new StyleBackground(imageTexture);
            }

            // Adicione funcionalidade ao bot�o
            var button = buttonElement.Q<Button>("Button"); // Substitua pelo nome do bot�o no UXML
            if (button != null)
            {
                string link = buttonLinks[i]; // Copia o link para evitar problemas de closures
                button.clicked += () => OnButtonClicked(link);
            }

            // Adicione o bot�o ao cont�iner
            buttonContainer.Add(buttonElement);
        }
    }

    // A��o ao clicar no bot�o
    private void OnButtonClicked(string link)
    {
        Debug.Log($"Bot�o clicado! Link: {link}");
        // Aqui voc� pode implementar a navega��o ou a��o desejada
    }
}
