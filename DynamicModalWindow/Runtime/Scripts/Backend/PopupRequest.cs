using System.Collections.Generic;


namespace GameSystems.Popup.Backend
{
    public class PopupRequest
    {
        #region Variables
        private ILayout _layout;
        private string _title;
        private List<PopupAction> _actions = new List<PopupAction>();
        private List<IPopupElement> _elements = new List<IPopupElement>();
        #endregion


        #region Getters and Setters
        public ILayout PopupLayout => _layout;
        public string Title => _title;
        public List<PopupAction> Actions => _actions;
        public List<IPopupElement> Elements => _elements;
        #endregion


        #region Constructors
        public PopupRequest(List<IPopupElement> elements, List<PopupAction> actions, ILayout layout, string title = "")
        {
            _title = title;
            _layout = layout;
            _actions = actions;
            _elements = elements;
        }
        #endregion


        #region Functions

        #endregion
    }
}