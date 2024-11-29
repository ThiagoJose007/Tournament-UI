using UnityEngine;
using UnityEngine.UIElements;

public class HoverExample : MonoBehaviour
{
    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        var myButton = root.Q<Button>("Category_Button"); // Substitua pelo nome do seu botão
        myButton.text = "Clique Aqui!";
    }
}
