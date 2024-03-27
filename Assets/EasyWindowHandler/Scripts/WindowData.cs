using UnityEngine;

namespace WindowHandler
{
    [CreateAssetMenu(fileName = "WindowData", menuName = "WindowHandler/Create/WindowData")]
    public class WindowData : ScriptableObject
    {
        public string NameWindow => nameWindow;

        public Window Window => window;

        [SerializeField]
        private string nameWindow;

        [SerializeField]
        private Window window;
    }
}
