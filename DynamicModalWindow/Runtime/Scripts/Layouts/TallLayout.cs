using System.Collections.Generic;
using System.Linq;
using GameSystems.Popup.Backend;
using UnityEngine;
using UnityEngine.UI;

namespace GameSystems.Popup.Layouts
{
    public class TallLayout : ILayout
    {
        #region Variables
        private Dictionary<IPopupElement, Vector2> _elements = new Dictionary<IPopupElement, Vector2>();
        private VerticalLayoutGroup _rightLayout;
        private float _rightColumnWidth;
        private RectTransform rect;
        #endregion


        #region Getters and Setters
        public Dictionary<IPopupElement, Vector2> GetElements() => _elements;

        public float GetTotalWidth()
        {
            _rightColumnWidth = 0;

            var elements = _elements.Keys.ToList();
            for (int i = 1; i < elements.Count; i++)
            {
                if (elements[i].Width > _rightColumnWidth) _rightColumnWidth = elements[i].Width;
            }

            if (_rightColumnWidth == float.MinValue)
            {
                return elements[0].Width;
            }
            else
            {
                return elements[0].Width + _rightColumnWidth;
            }
        }

        public float GetTotalHeight()
        {
            float maxHeight = float.MinValue;

            var elements = _elements.Keys.ToList();
            for (int i = 1; i < elements.Count; i++)
            {
                maxHeight += elements[i].Height;
            }

            return Mathf.Max(elements[0].Height, maxHeight);
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
                element.Parent = _rightLayout.transform;
            }
            else
            {
                element.Parent = PopupManager.Instance.BodyRoot.transform;
            }

            return _elements[element];
        }

        private void RecalculatePositions()
        {
            rect = _rightLayout?.GetComponent<RectTransform>();
            var totalWidth = GetTotalWidth();
            var totalHeight = GetTotalHeight();
            var bodyRoot = PopupManager.Instance.BodyRoot.transform;
            var actionsRoot = PopupManager.Instance.ActionsRoot.GetComponent<RectTransform>();

            IPopupElement firstElement = _elements.Keys.ElementAt(0);
            var furthestXPoint = bodyRoot.position.x - (totalWidth / 2);
            var furthestYPoint = bodyRoot.position.y - (totalHeight / 2);
            var xCenter = furthestXPoint + firstElement.Width / 2;
            var yCenter = furthestYPoint + firstElement.Height / 2;
            _elements[firstElement] = new Vector2(xCenter, yCenter);

            if (_rightLayout == null)
            {
                _rightLayout = new GameObject("Tall Layout", typeof(VerticalLayoutGroup)).GetComponent<VerticalLayoutGroup>();
                _rightLayout.transform.SetParent(bodyRoot);
                var maxXPoint = furthestXPoint + firstElement.Width;
                var layoutXCenter = maxXPoint - (_rightColumnWidth / 2);

                _rightLayout.transform.position = new Vector2(layoutXCenter, 0);
                _rightLayout.transform.localPosition = new Vector2(_rightLayout.transform.localPosition.x, 0);

                rect = _rightLayout.GetComponent<RectTransform>();
                rect.localPosition = new Vector2(rect.localPosition.x, rect.localPosition.y + actionsRoot.sizeDelta.y / 7);
                rect.sizeDelta = new Vector2(rect.sizeDelta.x, rect.sizeDelta.y - actionsRoot.sizeDelta.y / 4);
            }

            rect.sizeDelta = new Vector2(_rightColumnWidth, totalHeight);
        }
        #endregion
    }
}