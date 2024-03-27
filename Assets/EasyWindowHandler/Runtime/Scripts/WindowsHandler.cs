using System;
using UnityEngine;
using System.Linq;

namespace WindowHandler
{
    /// <summary>
    /// handler open/close windows
    /// </summary>
    [RequireComponent(typeof(WindowsContainer))]
    public class WindowsHandler : MonoBehaviour
    {
        /// <summary>
        /// Event - open window
        /// </summary>
        public static event Action<Window> onShowWindow = delegate { };

        /// <summary>
        /// Event - close window
        /// </summary>
        public static event Action<Window> onCloseWindow = delegate { };

        [SerializeField]
        private Window startWindow;

        private WindowsContainer WindowsContainer;

        private void Awake()
        {
            WindowsContainer = GetComponent<WindowsContainer>();

            foreach (var windowData in WindowsContainer.Windows)
            {
                windowData.onRequestShowWindow += OpenWindow;
                windowData.onRequestCloseWindow += CloseWindow;

                CloseWindow(windowData);
            }
        }

        private void Start() => OpenWindow(startWindow);

        private void OnDestroy()
        {
            foreach (var windowData in WindowsContainer.Windows)
            {
                windowData.onRequestShowWindow -= OpenWindow;
                windowData.onRequestCloseWindow -= CloseWindow;
            }
        }

        /// <summary>
        /// Open window
        /// </summary>
        /// <param name="inputWindowData"></param>
        public void OpenWindow(Window inputWindowData) => ActionWindow(inputWindowData, true);

        /// <summary>
        /// Close window
        /// </summary>
        public void CloseWindow(Window inputWindowData) => ActionWindow(inputWindowData, false);

        private void ActionWindow(Window inputWindow, bool state)
        {
            WindowsContainer.Windows.FirstOrDefault(windowData => windowData.NameWindow == inputWindow.NameWindow).gameObject.SetActive(state);
            ;
            if (state)
            {
                onShowWindow.Invoke(inputWindow);
            }
            else
            {
                onCloseWindow.Invoke(inputWindow);
            }

        }
    }
}
