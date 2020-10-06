using UnityEngine;
using UnityEngine.UI;

public class MenuItem : MonoBehaviour
{
    [HideInInspector] public Image img; //to use the image information when in SettingsMenuController
    [HideInInspector] public Transform trans;
    [SerializeField] private SettingsMenuController SettingsMenuController;
    private int _index;
    private Button _button;

    private void Awake()
    {
        img = GetComponent<Image>();
        trans = transform;
        _index = trans.GetSiblingIndex() - 1;
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnItemClick);
    }

    public void OnItemClick()
    {
        SettingsMenuController.OnItemClick(_index);
    }
}