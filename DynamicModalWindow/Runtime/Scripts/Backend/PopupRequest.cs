using System.Collections.Generic;
using UnityEngine;

namespace GameSystems.Popup.Backend
{
    /// <summary>
    /// The request that is passed to the popup Manager for showing a popup.
    /// </summary>
    public class PopupRequest
    {
        #region Variables
        /// <summary>
        /// The Position of the popup.
        /// </summary>
        private Vector2 _position;

        /// <summary>
        /// The layout to be used for determining the position of a layout.
        /// </summary>
        private ILayout _layout;

        /// <summary>
        /// The title of the popup.
        /// </summary>
        private string _title;

        /// <summary>
        /// The list of PopupActions, which will be used to create popupButtons.
        /// </summary>
        private List<PopupAction> _actions = new List<PopupAction>();

        /// <summary>
        /// The list of elements to create.
        /// </summary>
        private List<IPopupElement> _elements = new List<IPopupElement>();
        #endregion


        #region Getters and Setters
        /// <summary>
        /// Getter for The layout to be used for determining the position of a layout.
        /// </summary>
        public ILayout PopupLayout => _layout;

        /// <summary>
        /// The title of the popup.
        /// </summary>
        public string Title => _title;

        /// <summary>
        /// Getter for The list of PopupActions, which will be used to create popupButtons.
        /// </summary>
        public List<PopupAction> Actions => _actions;

        /// <summary>
        /// Getter for The list of elements to create.
        /// </summary>
        public List<IPopupElement> Elements => _elements;

        /// <summary>
        /// The Position of the popup.
        /// </summary>
        public Vector2 Position => _position;
        #endregion


        #region Constructors
        public PopupRequest(List<IPopupElement> elements, List<PopupAction> actions, ILayout layout, Vector2 localPosition, string title = "")
        {
            _title = title;
            _layout = layout;
            _actions = actions;
            _elements = elements;
            _position = localPosition;
        }
        #endregion


        #region Functions

        #endregion
    }
}