using System.Collections.Generic;
using System.Linq;
using GameSystems.Popup.Backend;
using UnityEngine;
using UnityEngine.UI;
using LayoutElement = GameSystems.Popup.Backend.LayoutElement;

namespace GameSystems.Popup.Layouts
{
    public class WideLayout : ILayout
    {
        #region Variables
        private List<LayoutElement> _elements = new List<LayoutElement>();
        private HorizontalLayoutGroup _downLayout;
        private Vector2 _downColumnSize;
        private RectTransform rect;
        private Vector4 _margin;
        private float _horizontalMargin;
        private float _verticalMargin;
        #endregion


        #region Getters and Setters
        public List<LayoutElement> GetElements() => _elements;

        public float GetTotalWidth()
        {
            float maxWidth = 0;

            var elements = _elements.ToList();
            for (int i = 1; i < elements.Count; i++)
            {
                maxWidth += elements[i].Element.Width;
            }

            _downColumnSize.x = maxWidth;
            return Mathf.Max(elements[0].Element.Width, maxWidth) + _verticalMargin;
        }

        public float GetTotalHeight()
        {
            _downColumnSize = new Vector2(_downColumnSize.x, 0);

            var elements = _elements.ToList();
            for (int i = 1; i < elements.Count; i++)
            {
                if (elements[i].Element.Height > _downColumnSize.y) _downColumnSize.y = elements[i].Element.Height;
            }


            var title = PopupManager.Instance.TitleRoot.GetComponent<RectTransform>();
            if (_downColumnSize.y == float.MinValue)
            {
                return elements[0].Element.Height + _verticalMargin + title.sizeDelta.y - title.anchoredPosition.y;
            }
            else
            {
                return elements[0].Element.Height + _downColumnSize.y + _verticalMargin + title.sizeDelta.y - title.anchoredPosition.y;
            }
        }
        #endregion


        #region Constructors
        public WideLayout(Vector4 margin)
        {
            _margin = margin;
            _horizontalMargin = (_margin.x + _margin.z);
            _verticalMargin = (_margin.y + _margin.w);
        }
        #endregion


        #region Functions
        public Vector2 AddElement(IPopupElement element)
        {
            _elements.Add(new LayoutElement
            {
                Element = element,
                Position = Vector2.zero,
            });

            RecalculatePositions();

            if (_elements.Count > 1)
            {
                element.Parent = _downLayout.transform;
            }
            else
            {
                element.Parent = PopupManager.Instance.BodyRoot.transform;
            }

            return _elements.Where(e => e.Element == element).FirstOrDefault().Position;
        }

        private void RecalculatePositions()
        {
            rect = _downLayout?.GetComponent<RectTransform>();
            var totalWidth = GetTotalWidth();
            var totalHeight = GetTotalHeight();
            var bodyRoot = PopupManager.Instance.BodyRoot.transform;
            var actionsRoot = PopupManager.Instance.ActionsRoot.GetComponent<RectTransform>();
            var popupRoot = PopupManager.Instance.PopupRoot.GetComponent<RectTransform>();
            var title = PopupManager.Instance.TitleRoot.GetComponent<RectTransform>();

            IPopupElement firstElement = _elements.ElementAt(0).Element;
            var furthestXPoint = bodyRoot.position.x - (totalWidth / 2);
            var furthestYPoint = bodyRoot.position.y - (totalHeight / 2);
            var xCenter = Mathf.Abs(furthestXPoint + firstElement.Width / 2 - popupRoot.localPosition.x);
            var yCenter = 0f;
            if (title.gameObject.activeInHierarchy)
            {
                yCenter = Mathf.Abs((totalHeight - (furthestYPoint + _downColumnSize.y + 7 + (title.sizeDelta.y - title.anchoredPosition.y))) / 2 - _margin.y - popupRoot.localPosition.y);
            }
            else if (!title.gameObject.activeInHierarchy)
            {
                yCenter = Mathf.Abs((totalHeight - furthestYPoint - _downColumnSize.y - 7) / 2 - _margin.y - popupRoot.localPosition.y);
            }

            var element = _elements.Where(e => e.Element == firstElement).First();
            element.Position = bodyRoot.TransformPoint(new Vector2(0, yCenter));

            if (_downLayout == null)
            {
                _downLayout = new GameObject("Wide Layout", typeof(HorizontalLayoutGroup)).GetComponent<HorizontalLayoutGroup>();
                _downLayout.transform.SetParent(bodyRoot);
                var layoutYCenter = furthestYPoint - (_downColumnSize.y / 2) - 5;

                _downLayout.transform.position = new Vector2(0, layoutYCenter);
                _downLayout.transform.localPosition = new Vector2(0, _downLayout.transform.localPosition.y);

                _downLayout.spacing = 4;

                rect = _downLayout.GetComponent<RectTransform>();
                rect.localPosition = new Vector2(rect.localPosition.x, rect.localPosition.y + actionsRoot.sizeDelta.y / 7 + _margin.w - popupRoot.localPosition.y);
            }

            rect.sizeDelta = _downColumnSize;
        }
        #endregion
    }
}