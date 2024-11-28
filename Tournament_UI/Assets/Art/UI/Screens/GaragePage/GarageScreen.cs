using UnityEngine;
using UnityEngine.UIElements;

public class GarageScreen : MonoBehaviour
{
    public UIDocument uiDocument; // O UIDocument da cena principal
    public VisualTreeAsset navbarUXML; // O UXML da Navbar

    void Start()
    {
        // Obtenha o rootVisualElement da cena
        var root = uiDocument.rootVisualElement;

        // Carregue o UXML da Navbar
        VisualElement navbar = navbarUXML.CloneTree();

        // Adicione a Navbar ao layout principal
        root.Add(navbar);

    }
}
