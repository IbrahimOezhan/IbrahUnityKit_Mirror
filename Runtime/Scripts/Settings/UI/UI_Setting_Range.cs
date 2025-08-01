using Sirenix.OdinInspector;
using UnityEngine;

namespace IbrahKit
{
    public class UI_Setting_Range : UI_Setting
    {
        [TabGroup("UI"), SerializeField] private UI_Selectable sub;
        [TabGroup("UI"), SerializeField] private UI_Selectable add;

        public override bool Initialize()
        {
            if (initialized) return true;

            bool result = base.Initialize();

            if (!result) return false;

            if (sub != null)
            {
                sub.OnClickEvent.RemoveAllListeners();
                sub.OnClickEvent.AddListener(() => AddValue(-setting.GetStep()));
            }
            else
            {
                Debug.LogWarning("Sub Selectable is null");
            }

            if (add != null)
            {
                add.OnClickEvent.RemoveAllListeners();
                add.OnClickEvent.AddListener(() => AddValue(setting.GetStep()));
            }
            else
            {
                Debug.LogWarning("Add Selectable is null");
            }

            return true;
        }

        public override void UpdateUI()
        {
            if (!TryInit()) return;

            base.UpdateUI();

            if (sub != null)
            {
                sub.SetInteractable((setting.GetValue() > setting.GetValueRange().x) || setting.GetLoop());
            }

            if (add != null)
            {
                add.SetInteractable((setting.GetValue() < setting.GetValueRange().y) || setting.GetLoop());
            }
        }
    }
}