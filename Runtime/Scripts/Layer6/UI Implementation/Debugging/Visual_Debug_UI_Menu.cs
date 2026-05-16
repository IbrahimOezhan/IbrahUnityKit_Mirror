#region

using System;
using IbrahKit.Debugging;
using IbrahKit.InfoCollector;
using IbrahKit.Input;
using IbrahKit.UI;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

#endregion

namespace IbrahKit
{
    public class Visual_Debug_UI_Menu : UI_Menu, ISelfValidator
    {
        private UI_Interactive_Extension_Text_Setter textSetter;

        private Action action;

        [SerializeField] private UI_Interactive debugContent;

        [SerializeField, Required] private Key debugKey;

        protected override void Awake()
        {
            base.Awake();

            debugContent.TryGetExtension(out textSetter);

            action = () =>
            {
                IbrahDebug.Log("Toggle");
                GetStateController().Toggle();
            };

            if(Input_Shortcut_Manager.TryGet(out Input_Shortcut_Manager res))
            {
                res.RegisterAction(debugKey, action);
            }
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            if(Input_Shortcut_Manager.TryGet(out Input_Shortcut_Manager res))
            {
                res.UnregisterAction(debugKey, action);
            }
        }

        protected override void MenuLifecycle()
        {
            base.MenuLifecycle();

            textSetter.SetText(Info_Collection_Manager.GetInstance().GetInfoString());
        }

        public override void OnMenuEnabled()
        {
            base.OnMenuEnabled();

            debugContent.TryGetExtension(out textSetter);
        }

        public void Validate(SelfValidationResult result)
        {
            if (debugContent == null)
            {
                result.AddError("Debug Content is null");
                return;
            }

            if (!debugContent.TryGetExtension(out textSetter))
            {
                result.AddError("UI Interactive doesnt contain Text Setter");
            }
        }
    }
}