#region

using System;
using System.Collections.Generic;
using System.Linq;
using IbrahKit.Input.Cursor;
using UnityEngine;
using UnityEngine.EventSystems;

#endregion

[Serializable]
public class Cursor_Controller_Receiver
{
    public HashSet<GameObject> GetReceivers(EventSystem system, Camera camera, Vector2 mousePos)
    {
        return !system.IsPointerOverGameObject()
            ? GetGameReceivers(camera, mousePos)
            : GetUIReceivers(system, mousePos);
    }

    public bool IsOverReceiver(EventSystem system, Camera camera, Vector2 mousePos)
    {
        if (!camera || !system) return false;

        return !system.IsPointerOverGameObject()
            ? IsOverGameReceiver(camera, mousePos)
            : IsOverUIReceiver(system, mousePos);
    }

    public HashSet<GameObject> GetGameReceivers(Camera camera, Vector2 mousePos)
    {
        List<GameObject> results = new();

        Vector2 mousePosWorld = camera.ScreenToWorldPoint(mousePos);

        RaycastHit2D hit2D = Physics2D.Raycast(mousePosWorld, Vector2.zero);

        if (hit2D.transform) results.Add(hit2D.transform.gameObject);

        return new HashSet<GameObject>(results);
    }

    public bool IsOverGameReceiver(Camera camera, Vector2 mousePos)
    {
        return GetGameReceivers(camera, mousePos).Any(x => x.GetComponent<ICursorReceiver>() != null);
    }

    public HashSet<GameObject> GetUIReceivers(EventSystem system, Vector2 mousePos)
    {
        PointerEventData pointerData = new(system)
        {
            position = mousePos
        };

        List<RaycastResult> results = new();

        system.RaycastAll(pointerData, results);

        return results.Select(x => x.gameObject).ToHashSet();
    }

    public bool IsOverUIReceiver(EventSystem system, Vector2 mousePos)
    {
        return GetUIReceivers(system, mousePos).Any(x => x.gameObject.GetComponent<ICursorReceiver>() != null);
    }
}