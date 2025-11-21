using IbrahKit;

public class UI_Interactive : Extension_Handler<UI_Interactive_Extension>, IMenuUpdate
{
    UI_Menu menu;

    private void Awake()
    {
        Init();
        if (IsInit()) ((IMenuUpdate)this).RegisterElement(menu);
    }

    protected void OnDisable()
    {
        Cleanup();
        if (IsInit()) ((IMenuUpdate)this).UnRegisterElement(menu);
    }

    public void OnMenuInit()
    {
        InitExtensions();
        RunExtensions();
    }

    public void OnMenuUpdate()
    {

    }

    public void Init()
    {
        ((IMenuUpdate)this).TryGetMenu(out menu);
    }

    public bool IsInit()
    {
        return menu != null;
    }

    public UI_Menu GetMenu()
    {
        return menu;
    }
}
