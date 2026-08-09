#region

using System;
using System.Collections.Generic;
using System.Linq;
using IbrahKit.UI.Generic;
using IbrahKit.Utilities;
using Unity.Scripting.LifecycleManagement;
using UnityEngine;
using UnityEngine.InputSystem;

#endregion

namespace IbrahKit.UI.Selectable
{
    [Serializable]
    public partial class UI_Selectable_Controller_Navigation : UI_Selectable_Controller
    {
        [AutoStaticsCleanup] public static readonly List<UI_Selectable_Controller_Navigation> activeSelectables = new();

        [SerializeField] private bool firstSelectedCandidate;
        [SerializeField] private bool selectableCandidate = true;

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
            
            UI_Selectable_Controller_Navigation selectable = Navigate(this, context.ReadValue<Vector2>(),
                controllerCanvas.GetCanvas(),
                activeSelectables .Where( x => x != this && x.GetSelectable().GetStateController().GetInteractable() && x.selectableCandidate) .ToList());
            
            //TODO: Added this. Was not here before. CHeck if its correct now
            selectable.GetSelectable().GetStateController().Select();
        }

        public static UI_Selectable_Controller_Navigation Navigate(UI_Selectable_Controller_Navigation current,
            Vector2 inputVector, Canvas canvas, IReadOnlyList<UI_Selectable_Controller_Navigation> _activeSelectables)
        {
            if (inputVector.sqrMagnitude < 0.001f)
                return null;

            inputVector.Normalize();

            float bestScore = float.NegativeInfinity;

            UI_Selectable_Controller_Navigation best = null;

            RectTransform currentRT = current.GetSelectable().GetRectTransform();

            foreach (UI_Selectable_Controller_Navigation candidate in _activeSelectables)
            {
                if (candidate == current)
                    continue;

                if (!candidate.GetSelectable().GetStateController().GetInteractable())
                    continue;

                RectTransform candidateRT = candidate.GetSelectable().GetRectTransform();

                Vector2 from, to;

                if (Mathf.Abs(inputVector.x) > Mathf.Abs(inputVector.y))
                {
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