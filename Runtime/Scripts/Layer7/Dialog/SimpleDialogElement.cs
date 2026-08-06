using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using IbrahKit.Dialog;
using IbrahKit.Utilities;
using UnityEngine;


public class SimpleDialogElement : Dialog_Element
{
    public enum SkipMode
    {
        SKIPABLE,
        NOTSKIPABLE
    }

    private string begginingPattern = "^\\[(.+)(?:=(.+))?\\]";
    private string endingPattern = "\\[\\/(.+)\\]";

    [SerializeReference] private float charDelay;
    
    [SerializeReference] private SkipMode skipMode;

    public float GetCharDelay() => charDelay;
    
    public List<Token> GetTokens()
    {
        string s = GetString();
        
        List<Token> tokens = new List<Token>();
        
        for (int i = 0; i < s.Length; i++)
        {
            Match m  = Regex.Match(s, begginingPattern);

            if (m.Success)
            {
                string startValue = m.Groups[1].Value;
                string value =m.Groups[2].Value;

                tokens.Add(new Token(startValue,value, "Open", i, m.Value.Length));
            }
            
            m  = Regex.Match(s, endingPattern);

            if (m.Success)
            {
                string endValue = m.Groups[1].Value;

                tokens.Add(new Token(endValue,"", "Close", i, m.Value.Length));
            }
        }
        
        return tokens;
    }

    public bool Validate(List<Token> tokens)
    {
        int offset = 0;

        for (var i = 0; i < tokens.Count; i++)
        {
            if (tokens[i].type != "Close") continue;
            
            if (tokens[i].token != tokens[i - offset].token)
            {
                return false;
            }

            offset++;
        }

        return true;
    }

    public string Process(string text, List<Token> tokens, Action<Stack<Token>, string> action)
    {
        string output = "";

        List<Token> _tokens = new List<Token>(tokens);
        Stack<Token> tokensInAffect = new Stack<Token>();

        string cache = "";
        
        for (int i = 0; i < text.Length; i++)
        {
            if(i == _tokens[0].start)
            {
                action.Invoke(tokensInAffect,cache);
                
                if (_tokens[0].type == "Open")
                {
                    tokensInAffect.Push(_tokens[0]);
                }
                else if (_tokens[0].type == "Close")
                {
                    tokensInAffect.Pop();
                }
                
                i+= _tokens[0].length;
                _tokens.RemoveAt(0);
                cache = "";
                
                continue;
            }

            string append = text[i].ToString();

            for (var i1 = 0; i1 < tokensInAffect.Count; i1++)
            {
                append = tokensInAffect.ElementAt(i1).Get().Process(append);
            }
            
            output += append;
            cache += append;
        }
        
        return output;
    }

    public class Token
    {
        private string token;
        private string value;
        private string type;
        private int start;
        private int length;
        private DialogProcessor processor;

        public Token(string token, string value, string type, int start, int length)
        {
            this.token = token;
            this.value = value;
            this.type = type;
            this.start = start;
            this.length = length;

            IEnumerable<Type> types = Type_Utilities.GetSubTypes(typeof(DialogProcessor));
            
            foreach (Type type1 in types)
            {
                Attribute[] attributes = System.Attribute.GetCustomAttributes(type1);
                for (var i = 0; i < attributes.Length; i++)
                {
                    if (attributes[i] is DialogTagAttribute tag)
                    {
                        if (tag.GetName() == token)
                        {
                            processor = Activator.CreateInstance(type1) as DialogProcessor;
                        }
                    }
                }
            }
            
        }

        public DialogProcessor Get()
        {
            return null;
        }
    }
}
