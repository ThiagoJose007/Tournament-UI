using UnityEngine;
using UnityEngine.UIElements;

public class Items_category : MonoBehaviour
{
    public VisualTreeAsset itemCategoryUXML;  // Refer�ncia ao arquivo UXML do item de skin
    public VisualElement buttonContainer;     // Cont�iner onde os itens ser�o exibidos

    // Arrays de dados din�micos para as skins
    public Sprite[] carImages;
    public Sprite[] carEffects;
    public Sprite[] rarityImages;
    public string[] rarityTexts;
    public Sprite[] itemBackgrounds;
    void Start()
    {
        // Verificar se as refer�ncias est�o configuradas
        if (itemCategoryUXML == null || buttonContainer == null)
        {
            Debug.LogError("UXML ou ButtonContainer n�o atribu�dos corretamente.");
            return;
        }

        if (carImages.Length == 0 || carEffects.Length == 0 || rarityImages.Length == 0 || rarityTexts.Length == 0 || itemBackgrounds.Length == 0)
        {
            Debug.LogError("Arrays de dados de skin est�o vazios.");
            return;
        }

        CreateSkinItems();
    }

    // M�todo para criar e atualizar os itens de skin
    public void CreateSkinItems()
    {
        Debug.Log("Iniciando a cria��o dos itens de skin...");

        int skinCount = carImages.Length;

        // Criar os itens de skin a partir do UXML
        for (int i = 0; i < skinCount; i++)
        {
            var skinItemElement = itemCategoryUXML.CloneTree();
            if (skinItemElement == null)
            {
                Debug.LogError($"Falha ao clonar o UXML do item {i}");
                continue;
            }

            skinItemElement.name = "skin-item-" + i;

            // Atualizar as imagens e textos dos elementos
            var carImageElement = skinItemElement.Q<VisualElement>("Car_Img");
            if (carImageElement != null)
                carImageElement.style.backgroundImage = new StyleBackground(carImages[i].texture);

            var carEffectElement = skinItemElement.Q<VisualElement>("Car_effect");
            if (carEffectElement != null)
                carEffectElement.style.backgroundImage = new StyleBackground(carEffects[i].texture);

            var rarityImageElement = skinItemElement.Q<VisualElement>("Img_rarity");
            if (rarityImageElement != null)
                rarityImageElement.style.backgroundImage = new StyleBackground(rarityImages[i].texture);

            var rarityTextElement = skinItemElement.Q<Label>("Text_rarity");
            if (rarityTextElement != null)
                rarityTextElement.text = rarityTexts[i];

            var backgroundElement = skinItemElement.Q<VisualElement>("Item");
            if (backgroundElement != null)
                backgroundElement.style.backgroundImage = new StyleBackground(itemBackgrounds[i].texture);

            // Adicionar ao cont�iner de bot�es
            buttonContainer.Add(skinItemElement);
        }

        Debug.Log("Itens de skin criados com sucesso.");
    }
}
