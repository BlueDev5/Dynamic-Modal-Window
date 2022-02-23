using GameSystems.Popup.Backend;
using GameSystems.Popup.Frontend;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameSystems.Popup
{
    public class PopupManager : MonoBehaviour
    {
        #region Singleton
        private static PopupManager _instance;
        public static PopupManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = GameObject.FindObjectOfType<PopupManager>();
                    if (_instance == null)
                    {
                        _instance = new GameObject("PopupManager Instance", typeof(PopupManager)).GetComponent<PopupManager>();
                    }
                }
                return _instance;
            }
        }
        #endregion

        #region Variables
        [SerializeField] private RectTransform _popupRoot;
        [SerializeField] private GameObject _actionsRoot;
        [SerializeField] private GameObject _bodyRoot;
        [SerializeField] private PopupButton _actionButton;
        [SerializeField] private GameObject _titleRoot;
        [SerializeField] private TMP_Text _title;
        private HorizontalLayoutGroup _actionsGroup;

        private bool _isOpen = false;
        #endregion


        #region Getters and Setters
        public GameObject BodyRoot => _bodyRoot;

        public GameObject ActionsRoot => _actionsRoot;
        #endregion


        #region Unity Calls

        #endregion


        #region Functions
        public void ShowPopup(PopupRequest request)
        {
            if (_isOpen) return;

            _popupRoot.gameObject.SetActive(true);

            _title.text = request.Title;
            _titleRoot.gameObject.SetActive(request.Title != "");

            foreach (var element in request.Elements)
            {
                var position = request.PopupLayout.AddElement(element);
            }

            request.Elements.ForEach(element =>
                element.Create(request.PopupLayout.GetElements()[element], element.Parent.gameObject));

            _popupRoot.sizeDelta = new Vector2(request.PopupLayout.GetTotalWidth(), request.PopupLayout.GetTotalHeight());

            foreach (var action in request.Actions)
            {
                var button = Instantiate<PopupButton>(_actionButton, _actionsRoot.transform);
                button.Init(action);
            }

            _bodyRoot.transform.localPosition = request.Position;

            _isOpen = true;
        }

        public void HidePopup()
        {
            if (!_isOpen) return;

            _popupRoot.gameObject.SetActive(false);
            foreach (var child in _bodyRoot.transform.GetComponentsInChildren<Transform>(true))
            {
                if (child == _bodyRoot.transform) continue;
                Destroy(child.gameObject);
            }

            foreach (var child in _actionsRoot.transform.GetComponentsInChildren<Transform>(true))
            {
                if (child == _actionsRoot.transform) continue;
                Destroy(child.gameObject);
            }

            _isOpen = false;
        }
        #endregion
    }
}