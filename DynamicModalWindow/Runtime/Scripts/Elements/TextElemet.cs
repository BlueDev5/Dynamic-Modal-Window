using GameSystems.Popup.Backend;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using LayoutElement = UnityEngine.UI.LayoutElement;

namespace GameSystems.Popup.Elements
{
    /// <summary>
    /// A Popup Element with text on it.
    /// </summary>
    public class TextElement : IPopupElement
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
        /// The size of the font of the text.
        /// </summary>
        private int _fontSize = 20;

        /// <summary>
        /// The the text to be written.
        /// </summary>
        private string _text;

        /// <summary>
        /// The parent of the popup element.
        /// </summary>
        public Transform Parent { get; set; }
        #endregion


        #region Getters and Setters
        /// <summary>
        /// Getter for the Width of the popup element.
        /// </summary>
        public float Width => _width;

        /// <summary>
        /// Getter for the Height of the popup element.
        /// </summary>
        public float Height => _height;
        #endregion


        #region Constructors
        public TextElement(string text, float preferredWidth, float preferredHeight, int fontSize = 20)
        {
            _fontSize = fontSize;
            _text = text;
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
            var element = new GameObject("TextElement", typeof(RectTransform));
            var rect = element.GetComponent<RectTransform>();
            element.transform.SetParent(Parent.transform);
            element.transform.position = position;
            rect.sizeDelta = new Vector3(Width, Height);

            var text = element.AddComponent<TextMeshProUGUI>();
            text.text = _text;
            text.alignment = TextAlignmentOptions.Midline;
            text.fontSize = _fontSize;
            var layoutElement = element.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = Width / 2;
            layoutElement.preferredHeight = Height / 2;

            return element;
        }
        #endregion
    }
}