using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using IPopupElement = GameSystems.Popup.Backend.IPopupElement;


namespace GameSystems.Popup.Elements
{
    public class HorizontalLayoutElement : IPopupElement
    {
        #region Variables
        private float _spacing;

        private float _width = 0;

        private float _height = 0;

        public Transform Parent { get; set; }

        private List<IPopupElement> _children = new List<IPopupElement>();
        #endregion


        #region Getters and Setters
        public float Width => _width;

        public float Height => _height;
        #endregion


        #region Functions
        public GameObject Create(Vector2 position)
        {
            var obj = new GameObject("HorizontalLayoutElement", typeof(RectTransform));
            var layout = obj.AddComponent<HorizontalLayoutGroup>();

            layout.childForceExpandHeight = false;
            layout.childForceExpandWidth = false;
            layout.childControlHeight = false;
            layout.childControlWidth = false;
            layout.spacing = _spacing;

            foreach (var child in _children)
            {
                child.Parent = layout.transform;
                var childClone = child.Create(Vector2.zero);
            }

            obj.transform.SetParent(Parent);

            return obj;
        }
        #endregion


        #region Constructors
        public HorizontalLayoutElement(float width, float height, float spacing, List<IPopupElement> children)
        {
            _width = width;
            _height = height;
            _spacing = spacing;
            _children = children;
        }

        public HorizontalLayoutElement(float width, float height, float spacing, params IPopupElement[] children)
        {
            _width = width;
            _height = height;
            _spacing = spacing;
            _children = children.ToList();
        }
        #endregion
    }
}