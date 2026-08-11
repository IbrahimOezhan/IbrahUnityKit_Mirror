#region

using System;
using System.Collections.Generic;
using System.Linq;
using IbrahKit.Input.Cursor;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

#endregion

namespace IbrahKit.Input.Cursor
{
    [Serializable]
    public class Cursor_Controller_Receiver
    {
        [SerializeField, ReadOnly] private List<GameObject> receivers = new();
        
        public void Run(Camera camera, Vector2 mousePos)
        {
            receivers = GameRaycastTargets(EventSystem.current, camera, mousePos).ToList();
        }
        
        public bool IsOverIReceiver(HashSet<GameObject> objects)
        {
            return objects.Any(x => x.GetComponent<ICursorReceiver>() != null);
        }
        
        public HashSet<GameObject> GameRaycastTargets(EventSystem system, Camera camera, Vector2 mousePos)
        {
            return !system.IsPointerOverGameObject()
                ? GetGameRaycastTargets(camera, mousePos)
                : GetUIRaycastTargets(system, mousePos);
        }

        public HashSet<GameObject> GetGameRaycastTargets(Camera camera, Vector2 mousePos)
        {
            List<GameObject> results = new();

            Vector2 mousePosWorld = camera.ScreenToWorldPoint(mousePos);

            RaycastHit2D hit2D = Physics2D.Raycast(mousePosWorld, Vector2.zero);

            if (hit2D.transform) results.Add(hit2D.transform.gameObject);

            return new HashSet<GameObject>(results);
        }

        public HashSet<GameObject> GetUIRaycastTargets(EventSystem system, Vector2 mousePos)
        {
            PointerEventData pointerData = new(system)
            {
                position = mousePos
            };

            List<RaycastResult> results = new();

            system.RaycastAll(pointerData, results);

            return results.Select(x => x.gameObject).ToHashSet();
        }
    }
}