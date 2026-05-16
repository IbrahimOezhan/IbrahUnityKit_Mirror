using Poke.UI;
using UnityEngine;

public class Menu_Element_Group : IMenuElement
{
    [SerializeField] private UI_Menu_Element_Group prefab;
    
    [SerializeField] private Margins            m_padding;
    [SerializeField] private Layout.LayoutDirection    m_direction;
    [SerializeField] private Layout.Justification      m_justifyContent;
    [SerializeField] private Layout.Alignment          m_alignContent;
    [SerializeField] private float              m_innerSpacing;
    [SerializeField] private bool               m_ignoreChildScale;
    
    public IMenuElementSpawnable Spawn()
    {
        Layout l = new Layout();
        
        
        return prefab;
    }
}