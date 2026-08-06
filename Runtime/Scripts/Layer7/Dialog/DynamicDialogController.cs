using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicDialogController : MonoBehaviour
{
    public void StartDialog(SimpleDialogNode node)
    {
        
    }

    public void Next()
    {
        
    }

    private IEnumerator Display(SimpleDialogElement element)
    {
        List<SimpleDialogElement.Token> tokens = element.GetTokens();
        
        element.Process(element.GetString(),tokens , (Stack<SimpleDialogElement.Token> t,string s) =>
        {
            element.GetCharDelay();
        });
        
        yield break;
    }

    private void SetText(string text)
    {
        
    }
}
