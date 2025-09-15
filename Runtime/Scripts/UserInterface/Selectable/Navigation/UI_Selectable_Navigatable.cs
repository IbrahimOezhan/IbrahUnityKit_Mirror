using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace IbrahKit
{
    public class UI_Selectable_Navigatable : UI_Selectable
    {
        private UI_Input input;

        public static List<UI_Selectable> activeSelectables;

        protected override void Awake()
        {
            base.Awake();

            input = new();
            input.Enable();
            input.Navigation.Move.performed += Navigate;
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            activeSelectables.Add(this);

            if(input != null) input.Enable();
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            activeSelectables.Remove(this);

            if (input != null) input.Disable();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            input.Navigation.Move.performed -= Navigate;
            input.Disable();
            input.Dispose();
        }

        public void Navigate(InputAction.CallbackContext context)
        {
            Navigate(this, context.ReadValue<Vector2>());
        }

        public static UI_Selectable Navigate(UI_Selectable current, Vector2 inputVector)
        {
            List<UI_Selectable> candidates = UI_Selectable_Navigatable.activeSelectables.Where(
                x => x != current && x.GetStateController().GetInteractable()).ToList();

            float highestScore = float.MinValue;

            UI_Selectable highestScoreSelectable = null;

            for (int i = 0; i < candidates.Count; i++)
            {
                Vector2 toCanditate = candidates[i].transform.position - current.transform.position;

                float alignment = Vector2.Dot(toCanditate.normalized,inputVector.normalized);

                if (alignment <= 0f) continue;

                float score = alignment / (toCanditate.magnitude + Mathf.Epsilon); 

                if(score > highestScore)
                {
                    highestScore = score;

                    highestScoreSelectable = candidates[i];
                }
            }

            return highestScoreSelectable;
        }

    }
}