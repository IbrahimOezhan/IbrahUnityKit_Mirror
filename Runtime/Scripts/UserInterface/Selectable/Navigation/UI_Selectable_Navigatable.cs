using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace IbrahKit
{
    public class UI_Selectable_Navigatable : UI_Selectable
    {
        //[TabGroup("Navigation Settings"), SerializeField]
        //private UI_Selectable up;

        //[TabGroup("Navigation Settings"), SerializeField]
        //private UI_Selectable down;

        //[TabGroup("Navigation Settings"), SerializeField]
        //private UI_Selectable left;

        //[TabGroup("Navigation Settings"), SerializeField]
        //private UI_Selectable right;

        //[TabGroup("Navigation Settings"), SerializeField]
        //private RectTransform rect;

        //[TabGroup("Navigation Settings"), SerializeField]
        //private float alignmentTolerance = 0.1f;

        //protected override void Awake()
        //{
        //    if (rect == null) rect = GetComponent<RectTransform>();
        //}

        //protected override void OnEnable()
        //{
        //    base.OnEnable();


        //    //if (UI_Navigation_Manager.Instance != null)
        //    //{
        //    //    UI_Navigation_Manager.Instance.AddSelectable(this);

        //    //    UI_Navigation_Manager.Instance.UpdateSelectables();
        //    //}

        //    Visualize();
        //}

        //protected override void OnDisable()
        //{
        //    SetState(UI_SELECTABLE_STATE.NONE);

        //    DeSelect();

        //    //if (UI_Navigation_Manager.Instance != null)
        //    //{
        //    //    UI_Navigation_Manager.Instance.RemoveSelectable(this);

        //    //    UI_Navigation_Manager.Instance.UpdateSelectables();
        //    //}
        //}

        //public void SetupNavigation(List<UI_Selectable> list)
        //{
        //    float minLeftDist = Mathf.Infinity;
        //    float minRightDist = Mathf.Infinity;
        //    float minUpDist = Mathf.Infinity;
        //    float minDownDist = Mathf.Infinity;

        //    float minDiagLeftDist = Mathf.Infinity;
        //    float minDiagRightDist = Mathf.Infinity;
        //    float minDiagUpDist = Mathf.Infinity;
        //    float minDiagDownDist = Mathf.Infinity;

        //    Vector2 currentPos = transform.position;

        //    foreach (var point in list)
        //    {
        //        if (point.transform == transform) continue;

        //        Vector2 direction = (Vector2)point.transform.position - currentPos;
        //        float dist = Vector2.Distance(currentPos, point.transform.position);

        //        // === Left Navigation ===
        //        if (direction.x < 0)
        //        {
        //            float distX = Mathf.Abs(direction.x);

        //            if (Mathf.Abs(direction.y) <= alignmentTolerance)
        //            {
        //                if (distX < minLeftDist)
        //                {
        //                    minLeftDist = distX;
        //                    left = point;
        //                }
        //            }
        //            else if (Mathf.Abs(direction.y) < Mathf.Abs(direction.x) && dist < minDiagLeftDist)
        //            {
        //                minDiagLeftDist = dist;
        //                left = left == null ? point : left;
        //            }
        //        }

        //        // === Right Navigation ===
        //        if (direction.x > 0)
        //        {
        //            float distX = Mathf.Abs(direction.x);

        //            if (Mathf.Abs(direction.y) <= alignmentTolerance)
        //            {
        //                if (distX < minRightDist)
        //                {
        //                    minRightDist = distX;
        //                    right = point;
        //                }
        //            }
        //            else if (Mathf.Abs(direction.y) < Mathf.Abs(direction.x) && dist < minDiagRightDist)
        //            {
        //                minDiagRightDist = dist;
        //                right = right == null ? point : right;
        //            }
        //        }

        //        // === Up Navigation ===
        //        if (direction.y > 0)
        //        {
        //            float distY = Mathf.Abs(direction.y);

        //            if (Mathf.Abs(direction.x) <= alignmentTolerance)
        //            {
        //                if (distY < minUpDist)
        //                {
        //                    minUpDist = distY;
        //                    up = point;
        //                }
        //            }
        //            else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y) && dist < minDiagUpDist)
        //            {
        //                minDiagUpDist = dist;
        //                up = up == null ? point : up;
        //            }
        //        }

        //        // === Down Navigation ===
        //        if (direction.y < 0)
        //        {
        //            float distY = Mathf.Abs(direction.y);

        //            if (Mathf.Abs(direction.x) <= alignmentTolerance)
        //            {
        //                if (distY < minDownDist)
        //                {
        //                    minDownDist = distY;
        //                    down = point;
        //                }
        //            }
        //            else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y) && dist < minDiagDownDist)
        //            {
        //                minDiagDownDist = dist;
        //                down = down == null ? point : down;
        //            }
        //        }
        //    }
        //}

        //public UI_Selectable Navigation(Vector2 direction)
        //{
        //    if (direction != Vector2.zero)
        //    {
        //        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        //        {
        //            if (direction.x < 0)
        //            {
        //                return left;
        //            }
        //            else
        //            {
        //                return right;
        //            }
        //        }
        //        else
        //        {
        //            if (direction.y < 0)
        //            {
        //                return down;
        //            }
        //            else
        //            {
        //                return up;
        //            }
        //        }
        //    }

        //    return null;
        //}
    }
}