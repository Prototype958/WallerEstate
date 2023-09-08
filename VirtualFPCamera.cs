using UnityEngine;
using Cinemachine;

public class VirtualFPCamera : CinemachineExtension
{
    private InputMaster input;

    private Vector3 _startingRotation;
    [SerializeField] private float _clamp = 90f;
    [SerializeField] private float _lookSensitivity = 3f;

    protected override void Awake()
    {
        input = new InputMaster();
        input.Enable();

        _startingRotation = transform.localRotation.eulerAngles;

        base.Awake();
    }

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        // Temp work around to stop cinemachine error spam in edit mode
        // inputMaster not properly accessible during edit mode
        // cinemachine wants it available for live preview
        if (Application.isPlaying)
        {
            if (vcam.Follow)
            {
                if (stage == CinemachineCore.Stage.Aim)
                {
                    Vector2 deltaInput = input.Player.Look.ReadValue<Vector2>();
                    _startingRotation.x += deltaInput.x * _lookSensitivity * Time.deltaTime;
                    _startingRotation.y += deltaInput.y * _lookSensitivity * Time.deltaTime;
                    _startingRotation.y = Mathf.Clamp(_startingRotation.y, -_clamp, _clamp);

                    state.RawOrientation = Quaternion.Euler(-_startingRotation.y, _startingRotation.x, 0f);
                    this.transform.rotation = Quaternion.Euler(-_startingRotation.y, _startingRotation.x, 0f);
                }
            }
        }
        else
            return;

    }
}
