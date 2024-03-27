using System.Collections.Generic;
using UnityEngine;

namespace WindowHandler
{

    /// <summary>
    /// Container containing all available windows
    /// </summary>
    public class WindowsContainer : MonoBehaviour
    {
        /// <summary>
        /// Current windows
        /// </summary>
        public IReadOnlyList<Window> Windows => windows;

        [SerializeField]
        private List<Window> windows = new();

    }
}
