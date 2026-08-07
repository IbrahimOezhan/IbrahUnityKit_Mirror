using System;
using System.Collections;
using System.Collections.Generic;
using Codice.Client.Common.FsNodeReaders;
using IbrahKit.Dialog;
using IbrahKit.UI.Menu;
using IbrahKit.UI.Modifier;
using IbrahKit.UI.Selectable;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.UI;

public class UI_Dialog_Choice_Menu : UI_Menu
{
    [SerializeField] private UI_Selectable prefab;

    [SerializeField] private Transform parent;
    
    private List<UI_Selectable> elements = new();

    [SerializeField] private UI_Modifier_Extension_Text_Setter setter;
    
    public void Init(SimpleDialogChoice[] choices, Action<SimpleDialogChoice> onClick)
    {
        int length = elements.Count;

        for (int i = 0; i < length; i++)
        {
            Destroy(elements[i].gameObject);
        }
        
        elements.Clear();
        
        foreach (SimpleDialogChoice ch in choices)
        {
            UI_Selectable selectable = Instantiate(prefab, parent);
            
            SimpleDialogChoice choice = ch;
            
            selectable.GetStateController().GetOnPressSuccess().AddListener(() =>
            {
                onClick.Invoke(choice);
            });
            
            elements.Add(selectable);
        }
    }

    // ReSharper disable once UnusedParameter.Global
    public IEnumerator DisplayText(SimpleDialogElement element, Action onClick)
    {
        List<SimpleDialogElement.Token> tokens = element.GetTokens();

        yield return element.Process2(element.GetString(), tokens, OnStringReceive);
        
        yield break;

        IEnumerator OnStringReceive(Stack<SimpleDialogElement.Token> tokens, string s)
        {
            setter.SetText("");
            
            bool skip = false;

            onClick += OnClick;
            
            float delay = element.GetCharDelay();

            tokens.ForEach(x =>
            {
                if (x.Get() is DialogSpeedProcessor _)
                {
                    delay *= float.Parse(x.Value);
                }
            });
            
            foreach (char c in s)
            {
                setter.AppendText(c);
                
                if(!skip) yield return new WaitForSeconds(delay);
            }
            
            skip = false;
            
            if(element.GetSkipMode() == SimpleDialogElement.SkipMode.SKIPABLE) yield return new WaitUntil(() => skip);
            else yield return new WaitForSeconds(element.GetDisplayTime());

            onClick -= OnClick;
            
            yield break;
            
            void OnClick()
            {
               if(element.GetSkipMode() == SimpleDialogElement.SkipMode.SKIPABLE) skip = true;
            }
        }
    }


}
