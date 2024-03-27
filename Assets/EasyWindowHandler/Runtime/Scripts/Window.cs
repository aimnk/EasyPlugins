using System;
using UnityEngine;

namespace WindowHandler
{
    public class Window : MonoBehaviour
    {
        public string NameWindow => nameWindow;

        [SerializeField]
        private string nameWindow;

        public event Action<Window> onRequestShowWindow = delegate { };

        public event Action<Window> onRequestCloseWindow = delegate { };

        public void RequestSetWindow(bool state)
        {
            if (state)
            {
                onRequestShowWindow.Invoke(this);
            }
            else
            {
                onRequestCloseWindow.Invoke(this);
            }

        }
    }
}
