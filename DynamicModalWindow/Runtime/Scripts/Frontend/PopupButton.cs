using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GameSystems.Popup.Frontend
{
    [RequireComponent(typeof(Button), typeof(Image))]
    public class PopupButton : MonoBehaviour
    {
        #region Variables
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;
        #endregion


        #region Getters and Setters

        #endregion


        #region Unity Calls
        private void Awake()
        {
            if (_button == null) _button = GetComponent<Button>();
            if (_image == null) _image = GetComponent<Image>();
            if (_text == null) _text = GetComponent<TMP_Text>();
        }
        #endregion


        #region Functions
        public void Init(GameSystems.Popup.Backend.PopupAction action)
        {
            _text.text = action.ButtonText;

            UnityAction unityAction = new UnityAction(action.ClickCallback);
            UnityAction closePopupAction = new UnityAction(() => PopupManager.Instance.HidePopup());
            _button.onClick.AddListener(unityAction);
            _button.onClick.AddListener(closePopupAction);

            _image.color = action.BackgroundColor;
        }
        #endregion
    }
}