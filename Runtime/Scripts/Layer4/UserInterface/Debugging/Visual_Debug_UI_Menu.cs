#region

using System;
using IbrahKit.Debugging;
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
        private UI_Interative_Extension_Text_Setter textSetter;

        private Action action;

        [SerializeField] private UI_Interactive debugContent;

        [SerializeField, Required] private Key debugKey;

        protected override void Awake()
        {
            base.Awake();

            debugContent.TryGet(out textSetter);

            action = () =>
            {
                IbrahDebug.Log("Toggle");
                GetStateController().Toggle();
            };

            Debug.Log(debugKey);

            Input_Shortcut_Manager.GetInstance().RegisterAction(debugKey, action);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            Input_Shortcut_Manager.GetInstance().UnregisterAction(debugKey, action);
        }

        protected override void MenuLifecycle()
        {
            base.MenuLifecycle();

            textSetter.SetText(Lifecycle_Diagnostics_Manager.GetInstance().GetLifecycleContent());
        }

        public override void OnMenuEnabled()
        {
            base.OnMenuEnabled();

            debugContent.TryGet(out textSetter);
        }

        public void Validate(SelfValidationResult result)
        {
            if (debugContent == null)
            {
                result.AddError("Debug Content is null");
                return;
            }

            if (!debugContent.TryGet(out textSetter))
            {
                result.AddError("UI Interactive doesnt contain Text Setter");
            }
        }
    }
}