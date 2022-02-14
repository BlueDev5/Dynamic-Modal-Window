using GameSystems.Popup.Backend;
using UnityEngine;
using UnityEngine.UI;

namespace GameSystems.Popup.Elements
{
    public class SpriteElement : IPopupElement
    {
        #region Variables
        private float _width = 0;
        private float _height = 0;
        private Sprite _sprite;
        private Color _tint;
        public Transform Parent { get; set; }
        #endregion


        #region Getters and Setters
        public float Width => _width;
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
        public GameObject Create(Vector2 position, GameObject parent)
        {
            var element = new GameObject("TextureElement", typeof(RectTransform));
            var rect = element.GetComponent<RectTransform>();
            element.transform.SetParent(parent.transform);
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