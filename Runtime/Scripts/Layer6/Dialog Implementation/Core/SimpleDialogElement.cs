#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using IbrahKit.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace IbrahKit.Dialog
{
    [Serializable]
    public class SimpleDialogElement : Dialog_Element
    {
        public enum SkipMode
        {
            SKIPABLE,
            NOTSKIPABLE
        }

        [SerializeField] private float charDelay;

        [SerializeField, ShowIf(nameof(skipMode), SkipMode.NOTSKIPABLE)]
        private float displayTime;

        [SerializeField] private SkipMode skipMode;

        private readonly string begginingPattern = "^\\[(.+)(?:=(.+))?\\]";

        private readonly string endingPattern = "\\[\\/(.+)\\]";

        public SkipMode GetSkipMode() => skipMode;

        public float GetCharDelay() => charDelay;

        public float GetDisplayTime() => displayTime;

        public List<Token> GetTokens()
        {
            string s = GetString();

            List<Token> tokens = new List<Token>();

            for (int i = 0; i < s.Length; i++)
            {
                Match m = Regex.Match(s, begginingPattern);

                if (m.Success)
                {
                    string startValue = m.Groups[1].Value;
                    string value = m.Groups[2].Value;

                    tokens.Add(new Token(startValue, value, "Open", i, m.Value.Length));
                }

                m = Regex.Match(s, endingPattern);

                if (m.Success)
                {
                    string endValue = m.Groups[1].Value;

                    tokens.Add(new Token(endValue, "", "Close", i, m.Value.Length));
                }
            }

            return tokens;
        }

        public bool Validate(List<Token> tokens)
        {
            int offset = 0;

            for (var i = 0; i < tokens.Count; i++)
            {
                if (tokens[i].Type != "Close") continue;

                if (tokens[i]._Token != tokens[i - offset]._Token)
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
                if (_tokens.Count == 0)
                {
                    action.Invoke(tokensInAffect, text);
                    break;
                }
                
                if (i == _tokens[0].Start)
                {
                    action.Invoke(tokensInAffect, cache);

                    if (_tokens[0].Type == "Open")
                    {
                        tokensInAffect.Push(_tokens[0]);
                    }
                    else if (_tokens[0].Type == "Close")
                    {
                        tokensInAffect.Pop();
                    }

                    i += _tokens[0].Length;
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

        public IEnumerator Process2(string text, List<Token> tokens, Func<Stack<Token>, string, IEnumerator> action)
        {
            string output = "";

            List<Token> _tokens = new List<Token>(tokens);
            Stack<Token> tokensInAffect = new Stack<Token>();

            string cache = "";

            for (int i = 0; i < text.Length; i++)
            {
                if (i == _tokens[0].Start)
                {
                    yield return action.Invoke(tokensInAffect, cache);

                    if (_tokens[0].Type == "Open")
                    {
                        tokensInAffect.Push(_tokens[0]);
                    }
                    else if (_tokens[0].Type == "Close")
                    {
                        tokensInAffect.Pop();
                    }

                    i += _tokens[0].Length;
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
        }

        public class Token
        {
            private int length;
            private DialogProcessor processor;
            private int start;
            private string token;
            private string type;
            private string value;

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
                    Attribute[] attributes = Attribute.GetCustomAttributes(type1);

                    foreach (Attribute attribute in attributes)
                    {
                        if (attribute is not DialogTagAttribute tag) continue;

                        if (tag.GetName() == token)
                        {
                            processor = Activator.CreateInstance(type1) as DialogProcessor;
                        }
                    }
                }
            }

            public string _Token { get; }

            public string Value
            {
                get { return value; }
            }

            public string Type
            {
                get { return type; }
            }

            public int Start
            {
                get { return start; }
            }

            public int Length
            {
                get { return length; }
            }

            public DialogProcessor Get()
            {
                return processor;
            }
        }
    }
}