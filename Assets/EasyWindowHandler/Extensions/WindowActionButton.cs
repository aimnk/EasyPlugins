using UnityEngine;

namespace WindowHandler
{
    public class WindowActionButton : AbstractBaseButton
    {
        [SerializeField]
        protected TypeAction typeAction;

        [SerializeField]
        private Window window = default;

        protected override void OnClickButton() => window.RequestSetWindow(typeAction == TypeAction.OpenWindow ? true : false);

        protected enum TypeAction
        {
            OpenWindow,
            CloseWindow
        }
    }
}
