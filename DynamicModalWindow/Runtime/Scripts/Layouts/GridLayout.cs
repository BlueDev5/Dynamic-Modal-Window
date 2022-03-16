using System;
using System.Collections.Generic;
using System.Linq;
using GameSystems.Popup.Backend;
using UnityEngine;
using UnityEngine.UI;
using LayoutElement = GameSystems.Popup.Backend.LayoutElement;

namespace GameSystems.Popup.Layouts
{
    public class GridLayout : ILayout
    {
        #region Variables
        private List<LayoutElement> _elements = new List<LayoutElement>();
        private HorizontalLayoutGroup[] _rows;

        private Vector4 _margin = Vector4.zero;
        private float _horizontalMargin;
        private float _verticalMargin;
        private Vector2 _spacing;

        private int _maxRows = 1;
        private int _maxColumns = 1;
        #endregion


        #region Getters and Setters
        public List<LayoutElement> GetElements() => _elements;

        public float GetTotalHeight()
        {
            float maxHeight = 0;

            for (int i = 0; i < _maxRows; i++)
            {
                var height = GetRowHeight(i);
                // if (height > maxHeight) maxHeight = height;
                maxHeight += height;
            }

            var title = PopupManager.Instance.TitleRoot.GetComponent<RectTransform>();
            var actions = PopupManager.Instance.ActionsRoot.GetComponent<RectTransform>();
            return title.gameObject.activeInHierarchy ? maxHeight + (title.sizeDelta.y - title.anchoredPosition.y) + _verticalMargin : maxHeight + _verticalMargin;
        }

        public float GetTotalWidth()
        {
            float maxWidth = 0;

            for (int i = 0; i < _maxRows; i++)
            {
                var width = GetRowWidth(i);
                if (width > maxWidth) maxWidth = width;
            }

            return maxWidth + _horizontalMargin;
        }
        #endregion


        #region Constructors
        public GridLayout(Vector4 margin, Vector2 spacing, int maxRows = 1, int maxColumns = 1)
        {
            _maxRows = maxRows;
            _maxColumns = maxColumns;

            _elements = new List<LayoutElement>();
            _rows = new HorizontalLayoutGroup[_maxRows];

            _margin = margin;
            _horizontalMargin = (_margin.x + _margin.z);
            _verticalMargin = (_margin.y + _margin.w);
            _spacing = spacing;

            CreateRows();
        }
        #endregion


        #region Functions
        public Vector2 AddElement(IPopupElement element)
        {
            var addedElement = new LayoutElement
            {
                Element = element,
                Position = new Vector2(),
            };

            _elements.Add(addedElement);

            RecalculatePositions();

            var elementIndex = _elements.IndexOf(addedElement);
            var rowIndex = elementIndex / _maxColumns;
            element.Parent = _rows[rowIndex].transform;

            return _elements.Where(e => e.Element == element).FirstOrDefault().Position;
        }

        private void RecalculatePositions()
        {
            var height = GetTotalHeight();
            var width = GetTotalWidth();
            var bodyRoot = PopupManager.Instance.BodyRoot.transform;
            var title = PopupManager.Instance.TitleRoot.GetComponent<RectTransform>();
            var actions = PopupManager.Instance.ActionsRoot.GetComponent<RectTransform>();

            var furthestYPoint = bodyRoot.localPosition.y + (height / 2);

            for (int i = 0; i < _maxRows; i++)
            {
                var rowHeight = GetRowHeight(i);
                var rowWidth = GetRowWidth(i);

                var rect = _rows[i].GetComponent<RectTransform>();
                rect.sizeDelta = new Vector2(rowWidth, rowHeight);

                var rectYPosition = furthestYPoint - (rowHeight / 2);

                if (title.gameObject.activeInHierarchy)
                {
                    rect.localPosition = new Vector3((_margin.z - _margin.x) / 2, rectYPosition - title.sizeDelta.y + title.anchoredPosition.y + actions.sizeDelta.y / 7 + (_margin.w - _margin.y) / 2 - _spacing.y * i);
                }
                else if (!title.gameObject.activeInHierarchy)
                {
                    rect.localPosition = new Vector3((_margin.z - _margin.x) / 2, rectYPosition + actions.sizeDelta.y / 7 + (_margin.w - _margin.y) / 2 + _spacing.y);
                }

                furthestYPoint -= rowHeight;
            }
        }

        private void CreateRows()
        {
            for (int i = 0; i < _maxRows; i++)
            {
                var row = new GameObject($"Row {i}", typeof(HorizontalLayoutGroup));
                row.transform.SetParent(PopupManager.Instance.BodyRoot.transform);

                var rect = row.GetComponent<RectTransform>();
                var layoutGroup = row.GetComponent<HorizontalLayoutGroup>();

                layoutGroup.childForceExpandHeight = false;
                layoutGroup.childForceExpandWidth = false;
                layoutGroup.childControlHeight = false;
                layoutGroup.childControlWidth = false;
                layoutGroup.spacing = _spacing.x;

                rect.localPosition = Vector2.zero;

                _rows[i] = layoutGroup;
            }
        }

        private float GetRowWidth(int rowIndex)
        {
            var startIndex = rowIndex * _maxColumns;
            var endIndex = rowIndex * _maxColumns + _maxColumns;
            float width = 0;

            for (int i = startIndex; i < endIndex && i < _elements.Count; i++)
            {
                width += _elements[i].Element.Width + _spacing.x;
            }

            return width;
        }

        private float GetRowHeight(int rowIndex)
        {
            var startIndex = rowIndex * _maxColumns;
            var endIndex = rowIndex * _maxColumns + _maxColumns;
            float height = 0;

            for (int i = startIndex; i < endIndex && i < _elements.Count; i++)
            {
                // height += _elements[i].Element.Height;
                if (_elements[i].Element.Height > height) height = _elements[i].Element.Height;
            }

            return height;
        }
        #endregion
    }
}