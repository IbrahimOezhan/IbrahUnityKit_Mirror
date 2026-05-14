#region

using System;
using System.Collections.Generic;
using UnityEngine;

#endregion

namespace IbrahKit.Effects
{
    public class Effect_Manager : MonoBehaviour
    {
        protected Dictionary<string, List<Effect_BaseC>> effects = new();

        public Action<Effect_BaseC> OnEffectAdded;

        public Action<Effect_BaseC> OnEffectRemoved;

        public static Effect_Manager Instance;

        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            foreach (var entries in effects)
            {
                foreach (var value in entries.Value)
                {
                    value.Run();
                }
            }
        }

        public void Add(params Effect_BaseC[] effects)
        {
            for (int i = 0; i < effects.Length; i++)
            {
                Add(effects[i]);
            }
        }

        public void Add(List<Effect_BaseC> effects)
        {
            effects.ForEach(x => Add(x));
        }

        public void Add(Effect_BaseC effect)
        {
            if (effects.TryGetValue(effect.GetKey(), out List<Effect_BaseC> result))
            {
                if (result.Contains(effect))
                {
                    return;
                }

                int indexToInsert = 0;

                for (int i = 0; i < result.Count; i++)
                {
                    if (effect.CompareTo(result[i]) > 0)
                    {
                        indexToInsert = i + 1;
                    }
                }

                result.Insert(indexToInsert, effect);

                Interpret(result);

                OnAdd(effect);

                OnEffectAdded?.Invoke(effect);
            }
            else
            {
                List<Effect_BaseC> insert = new List<Effect_BaseC>() { effect };

                effects.Add(effect.GetKey(), insert);

                Interpret(insert);

                OnAdd(effect);

                OnEffectAdded?.Invoke(effect);
            }
        }

        protected virtual void OnAdd(Effect_BaseC effect)
        {
        }

        public void Remove(List<Effect_BaseC> effects)
        {
            effects.ForEach(x => Remove(x));
        }

        public void Remove(Effect_BaseC effect)
        {
            if (effects.TryGetValue(effect.GetKey(), out List<Effect_BaseC> result))
            {
                if (!result.Contains(effect))
                {
                    return;
                }

                result.Remove(effect);

                Interpret(result);

                if (result.Count == 0)
                {
                    effects.Remove(effect.GetKey());
                }

                OnRemove(effect);

                OnEffectRemoved?.Invoke(effect);
            }
            else
            {
                return;
            }
        }

        protected virtual void OnRemove(Effect_BaseC effect)
        {
        }

        public virtual void Interpret(List<Effect_BaseC> effects)
        {
        }

        public Dictionary<string, List<Effect_BaseC>> GetEffects()
        {
            return effects;
        }

        public int GetEffectCount(string key)
        {
            if (effects.TryGetValue(key, out List<Effect_BaseC> result))
            {
                return result.Count;
            }

            return 0;
        }

        public bool Contains(List<Effect_BaseC> list)
        {
            for (var i = 0; i < list.Count; i++)
            {
                if (Contains(list[i]))
                {
                    return true;
                }
            }

            return false;
        }

        public bool Contains(Effect_BaseC effect)
        {
            if (effects.TryGetValue(effect.GetKey(), out List<Effect_BaseC> result))
            {
                if (result.Contains(effect))
                {
                    return true;
                }

                return false;
            }
            else
            {
                return false;
            }
        }
    }
}