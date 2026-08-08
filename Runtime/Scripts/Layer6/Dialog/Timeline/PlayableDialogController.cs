#region

using System.Collections.Generic;
using IbrahKit.Dialog;
using Sirenix.Utilities;

#endregion

public static class PlayableDialogController
{
    public static string Get(SimpleDialogElement[] elements, double time, double duration)
    {
        double totalTime = 0;

        foreach (SimpleDialogElement element in elements)
        {
            List<SimpleDialogElement.Token> tokens = element.GetTokens();

            element.Process(element.GetString(), tokens, (t, s) =>
            {
                double delay = element.GetCharDelay();

                t.ForEach(x =>
                {
                    if (x.Get() is DialogSpeedProcessor _)
                    {
                        delay *= float.Parse(x.Value);
                    }
                });

                totalTime += s.Length * delay;
            });
        }

        double counter = 0;

        double divident = totalTime / duration;

        string text = "";

        foreach (SimpleDialogElement element in elements)
        {
            text = "";

            bool done = false;

            List<SimpleDialogElement.Token> tokens = element.GetTokens();

            element.Process(element.GetString(), tokens, (stack, str) =>
            {
                if (!done)
                {
                    Process(stack, str);
                }
            });

            continue;

            void Process(Stack<SimpleDialogElement.Token> stack, string s)
            {
                float delay = element.GetCharDelay();

                stack.ForEach(x =>
                {
                    if (x.Get() is DialogSpeedProcessor _)
                    {
                        delay *= float.Parse(x.Value);
                    }
                });

                double cache = counter;

                foreach (char c in s)
                {
                    text += c;

                    cache += (delay / divident);

                    if (!(cache >= time)) continue;

                    done = true;

                    return;
                }

                cache += (element.GetDisplayTime() / divident);

                if (cache >= time)
                {
                    done = true;
                    return;
                }

                counter = cache;
            }
        }

        return text;
    }
}