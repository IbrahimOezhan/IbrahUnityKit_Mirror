using System.Collections.Generic;
using IbrahKit.UI;
using Poke.UI;
using Sirenix.OdinInspector;
using UnityEngine;

public class Menu_Item_Tree_Definition : Menu_Item_Base
{
    [SerializeField] private Margins            m_padding;
    [SerializeField] private Layout.LayoutDirection    m_direction;
    [SerializeField] private Layout.Justification      m_justifyContent;
    [SerializeField] private Layout.Alignment          m_alignContent;
    [SerializeField] private float              m_innerSpacing;
    [SerializeField] private bool               m_ignoreChildScale;
    
    [TabGroup("Menu Items", order: -1), Tooltip("List of predefined menu items."), SerializeReference]
    private List<Menu_Item_Base> listMenuItems = new();

    protected override bool Spawn(RectTransform parent, UI_Menu menu, out GameObject go)
    {
        GameObject gameObject = new GameObject();
        gameObject.AddComponent<RectTransform>();
        Layout l = gameObject.AddComponent<Layout>();
        l.AlignContent = m_alignContent;
        l.JustifyContent = m_justifyContent;
        l.Direction = m_direction;
        l.InnerSpacing = m_innerSpacing;
        l.IgnoreChildScale = m_ignoreChildScale;
        l.Padding = m_padding;
        go = gameObject;
        
        return true;
    }
}
