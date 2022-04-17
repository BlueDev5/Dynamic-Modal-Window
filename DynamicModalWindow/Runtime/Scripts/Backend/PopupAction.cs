using System;
using UnityEngine;


namespace GameSystems.Popup.Backend
{
    /// <summary>
    /// Represents a button action for the popup.
    /// </summary>
    public class PopupAction
    {
        #region Variables
        /// <summary>
        /// The text that will be written to the Button.
        /// </summary>
        private string _buttonText;

        /// <summary>
        /// The method to call when the Button is pressed.
        /// </summary>
        private Action _clickCallback;

        /// <summary>
        /// The background color of the Button.
        /// </summary>
        private Color _backgroundColor;
        #endregion


        #region Getters and Setters
        /// <summary>
        /// The text that will be written to the Button.
        /// </summary>
        public string ButtonText => _buttonText;

        /// <summary>
        /// Getter for The method to call when the Button is pressed.
        /// </summary>
        public Action ClickCallback => _clickCallback;

        /// <summary>
        /// Getter for The background color of the Button.
        /// </summary>
        public Color BackgroundColor => _backgroundColor;
        #endregion


        #region Constructors
        public PopupAction(string buttonText, Action clickCallback, Color backgroundColor)
        {
            _buttonText = buttonText;
            _clickCallback = clickCallback;
            _backgroundColor = backgroundColor;
        }
        #endregion


        #region Functions

        #endregion
    }
}