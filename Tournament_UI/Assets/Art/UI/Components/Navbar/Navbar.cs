using UnityEngine;
using UnityEngine.UIElements;

public class ApplyMaterialToUI : MonoBehaviour
{

    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        var myButton = root.Q<Button>("Plus_button"); // Substitua pelo nome do seu botão
        myButton.text = "Clique Aqui!";

        var myButton1 = root.Q<Button>("Shadow_button"); // Substitua pelo nome do seu botão
        myButton.text = "Clique Aqui!";
    }
}   
