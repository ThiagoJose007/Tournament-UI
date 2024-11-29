using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemContainer : MonoBehaviour
{
    // Referência ao Prefab do item de skin (deve ser um GameObject que tenha UI Toolkit)
    public VisualTreeAsset skinItemPrefab;  // Arraste o Prefab aqui pelo Inspector

    // Lista de imagens que serão usadas nos botões (categorias)
    public Sprite[] Images;  // Imagens para as categorias (Skins, Wheels, etc.)

    // Texto para cada botão (categoria)
    private readonly string[] buttonTexts = { "Skins", "Color", "Wheels", "Accessories", "Bumper", "Spoiler" };

    private VisualElement itemContainer;

    void Start()
    {
        // Referência ao container onde os itens serão adicionados (isso seria um VisualElement na UI do seu Canvas)
        itemContainer = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("ItemContainer");

        // Teste: Adicionar uma nova skin
        AddNewSkin("Car1", "Rare", "Assets/Images/Car1.png", "Assets/Images/Background1.png", "Assets/Images/Effect1.png");
    }

    // Função para adicionar uma nova skin ao inventário
    private void AddNewSkin(string carName, string rarity, string carImagePath, string backgroundImagePath, string effectImagePath)
    {
        // Instanciando o item de skin do Prefab usando VisualTreeAsset
        VisualElement skinItem = skinItemPrefab.CloneTree();  // Clona o UXML do prefab

        // Alterando os valores do item de skin
        var carImage = skinItem.Q<Image>("Car_img");
        var rarityImage = skinItem.Q<Image>("Img_rarity");
        var carEffect = skinItem.Q<Image>("Car_effect");
        var rarityText = skinItem.Q<Label>("text_rarity");
        var itemBackground = skinItem.Q<VisualElement>("Item");

        // Definindo as imagens e texto
        carImage.image = AssetDatabase.LoadAssetAtPath<Texture2D>(carImagePath);  // Definindo imagem do carro
        rarityImage.image = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Images/" + rarity + "Rarity.png");  // Definindo imagem da raridade
        carEffect.image = AssetDatabase.LoadAssetAtPath<Texture2D>(effectImagePath);  // Definindo efeito de fundo
        rarityText.text = rarity;  // Definindo texto de raridade

        // Definindo imagem de fundo
        var bgTexture = AssetDatabase.LoadAssetAtPath<Texture2D>(backgroundImagePath);
        itemBackground.style.backgroundImage = new StyleBackground(bgTexture);

        // Adicionando o item de skin ao container
        itemContainer.Add(skinItem);
    }

    // Função para configurar os botões (categorias)
    private void ConfigureButtons()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        // Crie um contêiner para os botões
        var buttonContainer = new VisualElement
        {
            style =
            {
                flexDirection = FlexDirection.Column,
                alignItems = Align.FlexStart,
                paddingLeft = 30,
                marginTop = 150
            }
        };
        root.Add(buttonContainer);

        // Crie e personalize os botões
        for (int i = 0; i < buttonTexts.Length; i++)
        {
            var buttonElement = new Button();  // Cria um botão novo
            buttonElement.text = buttonTexts[i];  // Define o texto do botão

            // Atribui a imagem à categoria
            if (i < Images.Length)
            {
                buttonElement.style.backgroundImage = new StyleBackground(Images[i].texture);
            }

            // Adiciona o botão ao contêiner
            buttonContainer.Add(buttonElement);

            // Ação de clique (aqui você pode adicionar a lógica para instanciar itens relacionados)
            int index = i; // Captura o valor de i corretamente
            buttonElement.RegisterCallback<ClickEvent>(evt =>
            {
                // Exemplo de ação ao clicar no botão (aqui você pode chamar uma função para instanciar os itens dessa categoria)
                Debug.Log($"Botão {buttonTexts[index]} clicado.");
            });
        }
    }
}
