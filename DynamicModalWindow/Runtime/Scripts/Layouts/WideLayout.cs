using System.Collections.Generic;
using System.Linq;
using GameSystems.Popup.Backend;
using UnityEngine;
using UnityEngine.UI;

namespace GameSystems.Popup.Layouts
{
    public class WideLayout : ILayout
    {
        #region Variables
        private Dictionary<IPopupElement, Vector2> _elements = new Dictionary<IPopupElement, Vector2>();
        private HorizontalLayoutGroup _downLayout;
        private float _downColumnHeight;
        private RectTransform rect;
        #endregion


        #region Getters and Setters
        public Dictionary<IPopupElement, Vector2> GetElements() => _elements;

        public float GetTotalWidth()
        {
            float maxWidth = 0;

            var elements = _elements.Keys.ToList();
            for (int i = 1; i < elements.Count; i++)
            {
                maxWidth += elements[i].Width;
            }

            return Mathf.Max(elements[0].Width, maxWidth);
        }

        public float GetTotalHeight()
        {
            _downColumnHeight = 0;

            var elements = _elements.Keys.ToList();
            for (int i = 1; i < elements.Count; i++)
            {
                if (elements[i].Height > _downColumnHeight) _downColumnHeight = elements[i].Height;
            }

            if (_downColumnHeight == float.MinValue)
            {
                return elements[0].Height;
            }
            else
            {
                return elements[0].Height + _downColumnHeight;
            }
        }
        #endregion


        #region Constructors

        #endregion


        #region Functions
        public Vector2 AddElement(IPopupElement element)
        {
            _elements.Add(element, Vector2.zero);

            RecalculatePositions();

            if (_elements.Count > 1)
            {
                element.Parent = _downLayout.transform;
            }
            else
            {
                element.Parent = PopupManager.Instance.BodyRoot.transform;
            }

            return _elements[element];
        }

        private void RecalculatePositions()
        {
            rect = _downLayout?.GetComponent<RectTransform>();
            var totalWidth = GetTotalWidth();
            var totalHeight = GetTotalHeight();
            var bodyRoot = PopupManager.Instance.BodyRoot.transform;
            var actionsRoot = PopupManager.Instance.ActionsRoot.GetComponent<RectTransform>();

            IPopupElement firstElement = _elements.Keys.ElementAt(0);
            var furthestXPoint = bodyRoot.position.x - (totalWidth / 2);
            var furthestYPoint = bodyRoot.position.y - (totalHeight / 2);
            var xCenter = furthestXPoint + firstElement.Width / 2;
            var yCenter = (totalHeight - (furthestYPoint + _downColumnHeight)) / 2;

            _elements[firstElement] = bodyRoot.TransformPoint(new Vector2(0, yCenter));

            if (_downLayout == null)
            {
                _downLayout = new GameObject("Wide Layout", typeof(HorizontalLayoutGroup)).GetComponent<HorizontalLayoutGroup>();
                _downLayout.transform.SetParent(bodyRoot);
                var layoutYCenter = furthestYPoint - (_downColumnHeight / 2);

                _downLayout.transform.position = new Vector2(0, layoutYCenter);
                _downLayout.transform.localPosition = new Vector2(0, _downLayout.transform.localPosition.y);

                rect = _downLayout.GetComponent<RectTransform>();
                rect.localPosition = new Vector2(rect.localPosition.x, rect.localPosition.y + actionsRoot.sizeDelta.y / 7);
                rect.sizeDelta = new Vector2(rect.sizeDelta.x, rect.sizeDelta.y - actionsRoot.sizeDelta.y / 4);
            }

            rect.sizeDelta = new Vector2(totalWidth, _downColumnHeight);
        }
        #endregion
    }
}