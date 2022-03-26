using System.Collections.Generic;
using System.Linq;
using GameSystems.Popup.Backend;
using UnityEngine;
using UnityEngine.UI;
using LayoutElement = GameSystems.Popup.Backend.LayoutElement;

namespace GameSystems.Popup.Layouts
{
    public class TallLayout : ILayout
    {
        #region Variables
        private List<LayoutElement> _elements = new List<LayoutElement>();
        private VerticalLayoutGroup _rightLayout;
        private Vector2 _rightColumnSize = new Vector2();
        private RectTransform rect;
        private Vector4 _margin;
        private float _horizontalMargin;
        private float _verticalMargin;
        #endregion


        #region Getters and Setters
        public List<LayoutElement> GetElements() => _elements;

        public float GetTotalWidth()
        {
            _rightColumnSize.x = 0;

            var elements = _elements.ToList();
            for (int i = 1; i < elements.Count; i++)
            {
                if (elements[i].Element.Width > _rightColumnSize.x) _rightColumnSize.x = elements[i].Element.Width;
            }

            return elements[0].Element.Width + _rightColumnSize.x + _horizontalMargin + 5;
        }

        public float GetTotalHeight()
        {
            float maxHeight = 0;

            var elements = _elements.ToList();
            for (int i = 1; i < elements.Count; i++)
            {
                maxHeight += elements[i].Element.Height;
            }

            var actionsRoot = PopupManager.Instance.ActionsRoot.GetComponent<RectTransform>();
            var title = PopupManager.Instance.TitleRoot.GetComponent<RectTransform>();
            float titleHeight = title.sizeDelta.y - title.anchoredPosition.y;
            float actionsRootHeight = actionsRoot.sizeDelta.y + actionsRoot.anchoredPosition.y;

            _rightColumnSize.y = maxHeight;
            return Mathf.Max(elements[0].Element.Height, maxHeight) + _verticalMargin + titleHeight + actionsRootHeight;
        }
        #endregion


        #region Constructors
        public TallLayout(Vector4 margin)
        {
            _margin = margin;
            _horizontalMargin = _margin.x + _margin.z;
            _verticalMargin = _margin.y + _margin.w;
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
                element.Parent = _rightLayout.transform;
            }
            else
            {
                element.Parent = PopupManager.Instance.BodyRoot.transform;
            }

            return _elements.Where(e => e.Element == element).FirstOrDefault().Position;
        }

        private void RecalculatePositions()
        {
            rect = _rightLayout?.GetComponent<RectTransform>();
            var totalWidth = GetTotalWidth();
            var totalHeight = GetTotalHeight();
            var bodyRoot = PopupManager.Instance.BodyRoot.transform;
            var actionsRoot = PopupManager.Instance.ActionsRoot.GetComponent<RectTransform>();
            var title = PopupManager.Instance.TitleRoot.GetComponent<RectTransform>();
            var popupRoot = PopupManager.Instance.PopupRoot;

            IPopupElement firstElement = _elements.ElementAt(0).Element;
            var furthestXPoint = bodyRoot.position.x - (totalWidth / 2);
            var furthestYPoint = bodyRoot.position.y - (totalHeight / 2);
            var xCenter = furthestXPoint - firstElement.Width + _margin.x + 12 - popupRoot.localPosition.x;
            var yCenter = 0f;
            if (title.gameObject.activeInHierarchy)
            {
                yCenter = (title.sizeDelta.y - title.anchoredPosition.y) - (actionsRoot.sizeDelta.y + actionsRoot.anchoredPosition.y) + _margin.w - _margin.y - popupRoot.localPosition.y;
            }
            else if (!title.gameObject.activeInHierarchy)
            {
                yCenter = actionsRoot.sizeDelta.y + actionsRoot.anchoredPosition.y + _margin.w - _margin.y - popupRoot.localPosition.y;
            }

            var element = _elements.Where(e => e.Element == firstElement).FirstOrDefault();
            element.Position = bodyRoot.TransformPoint(new Vector2(xCenter, yCenter));
            element.Scale = new Vector2(element.Element.Width + _margin.x, element.Element.Height + _margin.y);

            if (_rightLayout == null)
            {
                _rightLayout = new GameObject("Tall Layout", typeof(VerticalLayoutGroup)).GetComponent<VerticalLayoutGroup>();
                _rightLayout.transform.SetParent(bodyRoot);
                var maxXPoint = furthestXPoint + firstElement.Width;
                var layoutXCenter = maxXPoint - (_rightColumnSize.x / 2);

                _rightLayout.spacing = 4;

                _rightLayout.transform.position = new Vector2(layoutXCenter, 0);
                _rightLayout.transform.localPosition = new Vector2(_rightLayout.transform.localPosition.x, 0);

                rect = _rightLayout.GetComponent<RectTransform>();

                var layoutYCenter = 0f;
                if (title.gameObject.activeInHierarchy)
                {
                    layoutYCenter = (title.sizeDelta.y - title.anchoredPosition.y) - (actionsRoot.sizeDelta.y + actionsRoot.anchoredPosition.y) + _margin.w - _margin.y - popupRoot.localPosition.y;
                }
                else if (!title.gameObject.activeInHierarchy)
                {
                    layoutYCenter = actionsRoot.sizeDelta.y - actionsRoot.anchoredPosition.y + _margin.w - _margin.y - popupRoot.localPosition.y;
                }
                rect.localPosition = new Vector2(rect.localPosition.x + 10, layoutYCenter);
            }

            rect.sizeDelta = new Vector2(_rightColumnSize.x, _rightColumnSize.y);
        }
        #endregion
    }
}