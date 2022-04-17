using GameSystems.Popup.Backend;
using TMPro;
using UnityEngine;

namespace GameSystems.Popup.Elements
{
    /// <summary>
    /// An Empty Popup Element, usually used as a divider.
    /// </summary>
    public class PopupElement : IPopupElement
    {
        #region Variables
        /// <summary>
        /// The width of the popup element.
        /// </summary>
        private float _width = 0;

        /// <summary>
        /// The height of the popup element.
        /// </summary>
        private float _height = 0;

        /// <summary>
        /// The parent of the popup element.
        /// </summary>
        public Transform Parent { get; set; }
        #endregion


        #region Getters and Setters
        /// <summary>
        /// Getter for The width of the popup element.
        /// </summary>
        public float Width => _width;

        /// <summary>
        /// Getter for The height of the popup element.
        /// </summary>
        public float Height => _height;
        #endregion


        #region Constructors
        public PopupElement(float preferredWidth, float preferredHeight)
        {
            _height = preferredHeight;
            _width = preferredWidth;
        }
        #endregion


        #region Functions
        /// <summary>
        /// Create this Popup Element at the given position.
        /// </summary>
        public GameObject Create(Vector2 position)
        {
            var element = new GameObject("PopupElement", typeof(RectTransform));
            var rect = element.GetComponent<RectTransform>();
            element.transform.SetParent(Parent);
            element.transform.position = position;
            rect.sizeDelta = new Vector3(Width, Height);

            return element;
        }
        #endregion
    }
}