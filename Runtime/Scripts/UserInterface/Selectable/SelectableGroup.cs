using IbrahKit;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectableGroup : MonoBehaviour
{
    private HashSet<UI_Selectable> selectables = new();

    [SerializeField] private bool deselectOnClickAnywhere;

    private void OnEnable()
    {
        Cursor_Input_Manager.Instance.OnLMB += OnLMB;
    }

    private void OnDisable()
    {
        Cursor_Input_Manager.Instance.OnLMB -= OnLMB;
    }

    public void OnLMB()
    {
        if (!deselectOnClickAnywhere) return;
        if (Cursor_Input_Manager.Instance.CursorOverUI(EventSystem.current)) return;
        foreach (var item in selectables)
        {
            item.DeSelect();
        }
    }

    public void Add(UI_Selectable selectable)
    {
        selectables.Add(selectable);
    }

    public void Remove(UI_Selectable selectable)
    {
        selectables.Remove(selectable);
    }

    public void OnSelect(UI_Selectable selected)
    {
        foreach (var item in selectables)
        {
            if (item != selected)
            {
                item.DeSelect();
            }
        }
    }
}