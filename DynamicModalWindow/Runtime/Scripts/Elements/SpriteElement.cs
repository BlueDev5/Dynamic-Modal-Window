using GameSystems.Popup.Backend;
using UnityEngine;
using UnityEngine.UI;

namespace GameSystems.Popup.Elements
{
    /// <summary>
    /// An Popup Element with a sprite on it
    /// </summary>
    public class SpriteElement : IPopupElement
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
        /// The sprite of the popup element.
        /// </summary>
        private Sprite _sprite;

        /// <summary>
        /// The tit of the sprite.
        /// </summary>
        private Color _tint;

        /// <summary>
        /// The parent of the popup element.
        /// </summary>
        /// <value></value>
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
        public SpriteElement(float preferredWidth, float preferredHeight, Sprite texture2D, Color? tint = null)
        {
            Color tintColor = tint == null ? Color.white : (Color)tint;
            _tint = tintColor;
            _sprite = texture2D;
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
            var element = new GameObject("TextureElement", typeof(RectTransform));
            var rect = element.GetComponent<RectTransform>();
            element.transform.SetParent(Parent.transform);
            element.transform.position = position;
            rect.sizeDelta = new Vector3(Width, Height);

            var image = element.AddComponent<Image>();
            image.sprite = _sprite;
            image.color = _tint;

            return element;
        }
        #endregion
    }
}