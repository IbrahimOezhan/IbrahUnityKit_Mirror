#region

using System;
using IbrahKit.Override;
using IbrahKit.Utilities;
using UnityEngine;

#endregion

[Serializable]
public class First_Person_Look
{
    private float _cinemachineTargetPitch;

    //private float _rotationVelocity;

    private const float lookThreshold = 0.01f;

    private Override_Struct<float> rotationSpeed;

    [SerializeField] private float leftClamp;

    [SerializeField] private float rightClamp = 360;

    [SerializeField] private float topClamp = 89;

    [SerializeField] private float bottomClamp = -89;

    [SerializeField] private float baseRotationSpeed = 1.0f;

    [SerializeField] private Transform cinemachineCameraTarget;

    [SerializeField] private bool clampX;

    private float _cinemachineTargetYaw;

    public void Init()
    {
        rotationSpeed = new(baseRotationSpeed, new OverrideReplace<float>());
    }

    public void Look(Vector2 input, Transform cameraTarget, float deltaTime)
    {
        if (input.sqrMagnitude < lookThreshold)
            return;

        float speed = rotationSpeed.GetValue();

        // Pitch (Kamera hoch/runter)
        _cinemachineTargetPitch += input.y * speed * deltaTime;
        _cinemachineTargetPitch = Math_Utilities.ClampAngle(
            _cinemachineTargetPitch,
            bottomClamp,
            topClamp
        );

        cinemachineCameraTarget.localRotation =
            Quaternion.Euler(_cinemachineTargetPitch, 0f, 0f);

        // Yaw (Spieler links/rechts) � LOKAL
        _cinemachineTargetYaw += input.x * speed * deltaTime;
        if (clampX) _cinemachineTargetYaw = Mathf.Clamp(_cinemachineTargetYaw, leftClamp, rightClamp);

        cameraTarget.localRotation =
            Quaternion.Euler(0f, _cinemachineTargetYaw, 0f);
    }

    public void SyncFromTransforms(Transform cameraTarget)
    {
        // Yaw from player
        Vector3 yawEuler = cameraTarget.localEulerAngles;
        if (yawEuler.y > 180f) yawEuler.y -= 360f;
        _cinemachineTargetYaw = yawEuler.y;

        // Pitch from camera target
        Vector3 pitchEuler = cinemachineCameraTarget.localEulerAngles;
        if (pitchEuler.x > 180f) pitchEuler.x -= 360f;
        _cinemachineTargetPitch = pitchEuler.x;
    }


    //public void Look(Vector2 input, Transform cameraTarget, float deltaTime)
    //{
    //    if (input.sqrMagnitude >= lookThreshold)
    //    {
    //        _cinemachineTargetPitch += input.y * rotationSpeed.GetValue() * deltaTime;

    //        _rotationVelocity = input.x * rotationSpeed.GetValue() * deltaTime;

    //        _cinemachineTargetPitch = Math_Utilities.ClampAngle(_cinemachineTargetPitch, bottomClamp, topClamp);

    //        cinemachineCameraTarget.localRotation = Quaternion.Euler(_cinemachineTargetPitch, 0.0f, 0.0f);

    //        Quaternion playerRotation = cameraTarget.rotation * Quaternion.Euler(0.0f, _rotationVelocity, 0.0f);

    //        cameraTarget.rotation = ClampPlayerRotation(playerRotation);
    //    }
    //}

    private Quaternion ClampPlayerRotation(Quaternion rotation)
    {
        Vector3 eulerRotation = rotation.eulerAngles;

        if (eulerRotation.x > 180f) eulerRotation.x -= 360f;
        if (eulerRotation.y > 180f) eulerRotation.y -= 360f;

        eulerRotation.y = Mathf.Clamp(eulerRotation.y, leftClamp, rightClamp);

        return Quaternion.Euler(eulerRotation);
    }

    public Override_Struct<float> GetRotationSpeed()
    {
        return rotationSpeed;
    }
}
