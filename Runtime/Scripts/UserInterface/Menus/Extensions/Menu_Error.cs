using UnityEngine;
using UnityEngine.UI;

namespace IbrahKit
{
    public class Menu_Error : UI_Menu_Basic
    {
        [SerializeField] private UI_Localization errorText;
        [SerializeField] private Button continueButton;

        public static Menu_Error ErrorMenu;

        protected override void Awake()
        {
            ErrorMenu = this;
            base.Awake();
        }

        public void ShowError(string _key, bool _allowContinue, UI_Menu_Basic _nextMenu, params string[] _string)
        {
            errorText.SetKeyParam(_key, _string);
            continueButton.gameObject.SetActive(_allowContinue);
            continueButton.onClick.AddListener(() => MenuTransition(_nextMenu));
        }
    }
}