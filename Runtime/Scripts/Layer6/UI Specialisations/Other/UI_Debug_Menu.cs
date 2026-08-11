#region

using System;
using IbrahKit.Debugging;
using IbrahKit.InfoCollector;
using IbrahKit.Input;
using IbrahKit.UI.Menu;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

#endregion

namespace IbrahKit.UI.Other
{
    public class UI_Debug_Menu : UI_Menu, ISelfValidator
    {
        [SerializeField] private UI_Modifier_Text_Modifier debugContent;

        [SerializeField, Required] private Key debugKey;

        private Action action;

        protected override void Awake()
        {
            base.Awake();

            action = () =>
            {
                IbrahDebug.Log("Toggle");
                GetStateController().Toggle();
            };

            if (Input_Shortcut_Manager.TryGet(out Input_Shortcut_Manager res))
            {
                res.RegisterAction(debugKey, action);
            }
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            if (Input_Shortcut_Manager.TryGet(out Input_Shortcut_Manager res))
            {
                res.UnregisterAction(debugKey, action);
            }
        }

        public void Validate(SelfValidationResult result)
        {
            if (debugContent == null)
            {
                result.AddError("Debug Content is null");
                return;
            }
        }

        protected override void MenuLifecycle()
        {
            base.MenuLifecycle();

            debugContent.GetStaticSetter().SetText(Info_Collection_Manager.GetInstance().GetInfoString());
        }
    }
}