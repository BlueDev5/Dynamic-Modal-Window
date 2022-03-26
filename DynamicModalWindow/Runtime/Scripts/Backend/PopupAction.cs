using System;
using UnityEngine;


namespace GameSystems.Popup.Backend
{
    public class PopupAction
    {
        #region Variables
        private string _buttonText;
        private Action _clickCallback;
        private Color _backgroundColor;
        #endregion


        #region Getters and Setters
        public string ButtonText => _buttonText;
        public Action ClickCallback => _clickCallback;
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