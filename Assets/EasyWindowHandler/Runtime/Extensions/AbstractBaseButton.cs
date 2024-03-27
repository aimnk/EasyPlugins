using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Abstract button listener
/// </summary>
[RequireComponent(typeof(Button))]
public abstract class AbstractBaseButton : MonoBehaviour
{
    protected Button button;

    protected void Awake() => button = GetComponent<Button>();

    protected void OnEnable() => button.onClick.AddListener(OnClickButton);

    protected void OnDisable() => button.onClick.RemoveListener(OnClickButton);

    protected abstract void OnClickButton();

}
