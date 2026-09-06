#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using IbrahKit.Dialog;
using IbrahKit.UI.Menu;
using IbrahKit.UI.Modifier;
using IbrahKit.UI.Selectable;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.Events;

#endregion

public class UI_Dialog_Menu : UI_Menu
{
    [SerializeField] private UI_Selectable prefab;

    [SerializeField] private Transform choiceContainer;

    [SerializeField] private UI_Modifier_Text_Setter textSetter;

    private readonly List<UI_Selectable> elements = new();

    private void Cleanup()
    {
        textSetter.SetText("");

        int length = elements.Count;

        for (int i = 0; i < length; i++)
        {
            Destroy(elements[i].gameObject);
        }

        elements.Clear();
    }

    public void DisplayChoices(SimpleDialogChoice[] choices, Action<SimpleDialogChoice> onClick)
    {
        Cleanup();

        foreach (SimpleDialogChoice ch in choices)
        {
            UI_Selectable selectable = Instantiate(prefab, choiceContainer);

            SimpleDialogChoice choice = ch;

            selectable.GetStateController().GetOnPressSuccess().AddListener(() => { onClick.Invoke(choice); });

            elements.Add(selectable);
        }
    }

    // ReSharper disable once UnusedParameter.Global
    public IEnumerator DisplayText(SimpleDialogElement element, UnityEvent onClick)
    {
        Cleanup();

        List<SimpleDialogElement.Token> tokens = element.GetTokens();

        yield return element.Process2(element.GetString(), tokens, OnStringReceive);

        yield break;

        IEnumerator OnStringReceive(Stack<SimpleDialogElement.Token> tokens, string s)
        {
            textSetter.SetText("");

            StringBuilder stringBuilder = new();

            bool skip = false;

            onClick.AddListener(OnClick);

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
                stringBuilder.Append(c);
                textSetter.SetText(stringBuilder.ToString());

                if (!skip) yield return new WaitForSeconds(delay);
            }

            skip = false;

            if (element.GetSkipMode() == SimpleDialogElement.SkipMode.SKIPABLE) yield return new WaitUntil(() => skip);
            else yield return new WaitForSeconds(element.GetDisplayTime());

            onClick.RemoveListener(OnClick);

            Debug.Log(stringBuilder);

            yield break;

            void OnClick()
            {
                if (element.GetSkipMode() == SimpleDialogElement.SkipMode.SKIPABLE) skip = true;
            }
        }
    }
}