#region

using System.Collections.Generic;
using System.Linq;
using IbrahKit.UI.Generic;
using IbrahKit.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;

#endregion

namespace IbrahKit.UI.Selectable
{
    public class UI_Selectable_Controller_Navigation : UI_Selectable_Controller
    {
        public static List<UI_Selectable_Controller_Navigation> activeSelectables;
        [SerializeField] private bool firstSelectedCandidate;

        protected override void Init()
        {
            if (firstSelectedCandidate && UI_Selectable_Controller_State.currentlySelected == null)
            {
                GetSelectable().GetStateController().Select();
            }
        }

        public override void OnEnable()
        {
            activeSelectables.Add(this);
        }

        public override void OnDisable()
        {
            activeSelectables.Remove(this);
        }

        public void Navigate(InputAction.CallbackContext context)
        {
            UI_Canvas_Controller controllerCanvas =
                GetSelectable().transform.BetterGetComponentInParent<UI_Canvas_Controller>();


            Navigate(this, context.ReadValue<Vector2>(),
                controllerCanvas.GetCanvas(),
                activeSelectables.Where(x => x != this && x.GetSelectable().GetStateController().GetInteractable())
                    .ToList());
        }

        public static UI_Selectable_Controller_Navigation Navigate(UI_Selectable_Controller_Navigation current,
            Vector2 inputVector, Canvas canvas, IReadOnlyList<UI_Selectable_Controller_Navigation> activeSelectables)
        {
            if (inputVector.sqrMagnitude < 0.001f)
                return null;

            inputVector.Normalize();

            float bestScore = float.NegativeInfinity;

            UI_Selectable_Controller_Navigation best = null;

            RectTransform currentRT = current.GetSelectable().GetRectTransform();

            for (int i = 0; i < activeSelectables.Count; i++)
            {
                UI_Selectable_Controller_Navigation candidate = activeSelectables[i];

                if (candidate == current)
                    continue;

                if (!candidate.GetSelectable().GetStateController().GetInteractable())
                    continue;

                RectTransform candidateRT = candidate.GetSelectable().GetRectTransform();

                Vector2 from, to;

                // Direction-aware edge selection
                if (Mathf.Abs(inputVector.x) > Mathf.Abs(inputVector.y))
                {
                    // Horizontal navigation
                    if (inputVector.x > 0f)
                    {
                        from = currentRT.GetRightEdgeCenter(canvas);
                        to = candidateRT.GetLeftEdgeCenter(canvas);
                    }
                    else
                    {
                        from = currentRT.GetLeftEdgeCenter(canvas);
                        to = candidateRT.GetRightEdgeCenter(canvas);
                    }
                }
                else
                {
                    // Vertical navigation
                    if (inputVector.y > 0f)
                    {
                        from = currentRT.GetTopEdgeCenter(canvas);
                        to = candidateRT.GetBottomEdgeCenter(canvas);
                    }
                    else
                    {
                        from = currentRT.GetBottomEdgeCenter(canvas);
                        to = candidateRT.GetTopEdgeCenter(canvas);
                    }
                }

                Vector2 toCandidate = to - from;

                float alignment = Vector2.Dot(toCandidate.normalized, inputVector);

                if (alignment <= 0f) continue;

                float distance = toCandidate.magnitude;

                // Final score: strong direction preference + distance bias
                float score = alignment / (distance + 0.001f);

                if (score > bestScore)
                {
                    bestScore = score;
                    best = candidate;
                }
            }

            return best;
        }
    }
}