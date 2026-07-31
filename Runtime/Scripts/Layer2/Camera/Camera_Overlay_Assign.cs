#region

using System.Collections.Generic;
using System.Linq;
using IbrahKit.Debugging;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

#endregion

namespace IbrahKit
{
    /// <summary>
    ///     Auto assigns the camera on the same game object as overlay to the Camera.main
    /// </summary>
    [RequireComponent(typeof(Camera))]
    public class Camera_Overlay_Assign : MonoBehaviour
    {
        [SerializeField] private int priority;

        private Camera overlayCam;

        private void Awake()
        {
            overlayCam = GetComponent<Camera>();

            SceneManager.sceneLoaded += SceneLoaded;
        }

        private void Start()
        {
            Assign();
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= SceneLoaded;
        }

        private void SceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Assign();
        }

        private void Assign()
        {
            overlayCam.GetUniversalAdditionalCameraData().renderType = CameraRenderType.Overlay;

            Camera cam = Camera.main;

            if (cam == null)
            {
                IbrahDebug.LogWarning("No camera with tag MainCamera found");
                return;
            }

            UniversalAdditionalCameraData baseCameraData = cam.GetUniversalAdditionalCameraData();

            List<Camera> list = baseCameraData.cameraStack;

            if (!list.Contains(overlayCam))
            {
                overlayCam.allowMSAA = cam.allowMSAA;
                overlayCam.allowHDR = cam.allowHDR;
                overlayCam.targetDisplay = cam.targetDisplay;
                overlayCam.rect = cam.rect;
                overlayCam.clearFlags = CameraClearFlags.Depth;
                overlayCam.targetTexture = null;

                overlayCam.GetUniversalAdditionalCameraData().renderType = CameraRenderType.Overlay;

                list.Add(overlayCam);

                list.Sort((a, b) =>
                    a.TryGetComponent(out Camera_Overlay_Assign overlayA)
                        ? b.TryGetComponent(out Camera_Overlay_Assign overlayB)
                            ? overlayA.priority.CompareTo(overlayB.priority)
                            : 1
                        : 0);
            }

            baseCameraData.cameraStack.RemoveAll(x => !list.Distinct().Contains(x));
        }
    }
}