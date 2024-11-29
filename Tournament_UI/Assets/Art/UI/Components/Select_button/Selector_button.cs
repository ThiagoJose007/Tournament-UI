using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Selector_button : EditorWindow
{
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;

    [MenuItem("Window/UI Toolkit/Selector_button")]
    public static void ShowExample()
    {
        Selector_button wnd = GetWindow<Selector_button>();
        wnd.titleContent = new GUIContent("Selector_button");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // VisualElements objects can contain other VisualElement following a tree hierarchy.
        VisualElement label = new Label("Hello World! From C#");
        root.Add(label);

        // Instantiate UXML
        VisualElement labelFromUXML = m_VisualTreeAsset.Instantiate();
        root.Add(labelFromUXML);
    }
}
