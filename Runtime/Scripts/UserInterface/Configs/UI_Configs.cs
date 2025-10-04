using IbrahKit;
using UnityEngine;

[System.Serializable]
public class UI_Configs
{
    [SerializeField] private BoolOverride<UI_Audio_Config_SO> audio;
    [SerializeField] private BoolOverride<UI_Fitter_Config_SO> fitter;
    [SerializeField] private BoolOverride<UI_Layout_Config_SO> layout;
    [SerializeField] private BoolOverride<UI_Menu_Config_SO> menu;
    [SerializeField] private BoolOverride<UI_Styling_Config_SO> style;

    private bool GetAudio(out UI_Audio_Config_SO result)
    {
        result = audio.Get();
        return result != null;
    }

    public bool GetFitter(out UI_Fitter_Config_SO result)
    {
        result = fitter.Get();
        return result != null;
    }

    public bool GetLayout(out UI_Layout_Config_SO result)
    {
        result = layout.Get();
        return result != null;
    }

    public bool GetMenu(out UI_Menu_Config_SO result)
    {
        result = menu.Get();
        return result != null;
    }

    public bool GetStyle(out UI_Styling_Config_SO result)
    {
        result = style.Get();
        return result != null;
    }

    public static UI_Configs[] GetConfigs(Transform t)
    {
        IConfig[] iConfigs = t.BetterGetComponentsInParents<IConfig>(true);

        bool found = UI_Config_Manager.TryGet(out UI_Config_Manager result);

        UI_Configs[] uiConfigs = new UI_Configs[iConfigs.Length + (found ? 1: 0)];

        for (int i = 0; i < iConfigs.Length; i++)
        {
            uiConfigs[i] = iConfigs[i].GetConfigs();
        }

        if(found)
        {
            uiConfigs[^1] = result.GetConfigs();
        }

        return uiConfigs;
    }

    public static bool GetAudio(UI_Configs[] configs, out UI_Audio_Config_SO result)
    {
        result = null;
        for (int i = 0; i < configs.Length; i++)
            if (configs[i].GetAudio(out result))
                return true;
        return false;
    }

    public static bool GetFitter(UI_Configs[] configs, out UI_Fitter_Config_SO result)
    {
        result = null;
        for (int i = 0; i < configs.Length; i++)
            if (configs[i].GetFitter(out result))
                return true;
        return false;
    }

    public static bool GetLayout(UI_Configs[] configs, out UI_Layout_Config_SO result)
    {
        result = null;
        for (int i = 0; i < configs.Length; i++)
            if (configs[i].GetLayout(out result))
                return true;
        return false;
    }

    public static bool GetMenu(UI_Configs[] configs, out UI_Menu_Config_SO result)
    {
        result = null;
        for (int i = 0; i < configs.Length; i++)
            if (configs[i].GetMenu(out result))
                return true;
        return false;
    }

    public static bool GetStyle(UI_Configs[] configs, out UI_Styling_Config_SO result)
    {
        result = null;
        for (int i = 0; i < configs.Length; i++)
            if (configs[i].GetStyle(out result))
                return true;
        return false;
    }

}