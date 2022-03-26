using GameSystems.Popup.Backend;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using LayoutElement = UnityEngine.UI.LayoutElement;

namespace GameSystems.Popup.Elements
{
    public class TextElement : IPopupElement
    {
        #region Variables
        private float _width = 0;
        private float _height = 0;
        private int _fontSize = 20;
        private string _text;
        public Transform Parent { get; set; }
        #endregion


        #region Getters and Setters
        public float Width => _width;
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
        public GameObject Create(Vector2 position, GameObject parent)
        {
            var element = new GameObject("TextElement", typeof(RectTransform));
            var rect = element.GetComponent<RectTransform>();
            element.transform.SetParent(parent.transform);
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