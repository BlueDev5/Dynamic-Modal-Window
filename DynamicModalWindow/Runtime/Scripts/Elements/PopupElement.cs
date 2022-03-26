using GameSystems.Popup.Backend;
using TMPro;
using UnityEngine;

namespace GameSystems.Popup.Elements
{
    public class PopupElement : IPopupElement
    {
        #region Variables
        private float _width = 0;
        private float _height = 0;
        public Transform Parent { get; set; }
        #endregion


        #region Getters and Setters
        public float Width => _width;
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
        public GameObject Create(Vector2 position, GameObject parent)
        {
            var element = new GameObject("PopupElement", typeof(RectTransform));
            var rect = element.GetComponent<RectTransform>();
            element.transform.SetParent(parent.transform);
            element.transform.position = position;
            rect.sizeDelta = new Vector3(Width, Height);

            return element;
        }
        #endregion
    }
}