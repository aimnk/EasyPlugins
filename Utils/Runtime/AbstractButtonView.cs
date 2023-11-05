using UnityEngine;
using UnityEngine.UI;

namespace EasyPlugins.Utils
{
    /// <summary>
    /// Basic button click Listener
    /// </summary>
    [RequireComponent(typeof(Button))]
    public abstract class AbstractButtonView : MonoBehaviour
    {
        protected Button button;

        private void Reset() => button = GetComponent<Button>();

        protected virtual void Awake()
        {
            if (button == null)
                button = GetComponent<Button>();
            
            button.onClick.AddListener(OnButtonClicked);
        }

        protected virtual void OnDestroy() => button.onClick.RemoveListener(OnButtonClicked);

        protected abstract void OnButtonClicked();
    }
}