using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public enum camViews
{
    Isometric,
    TopDown
}

public class CameraSystem : MonoBehaviour
{
    public float cameraPanSpeed = 50f;
    public float rotateCameraSpeed = 150f;
    public Vector3 defaultCameraPosition;
    public camViews cameraViews;

    public bool isPanable = true;
    public bool isRotatable = false;
    public bool useEdgeScrolling = false;
    public bool useDragPan = false;

    private TouchControls touchControls;
    private Vector2 dragStartPos;
    private bool isDragging = false;

    private void Awake()
    {
        // Enable Enhanced Touch Support for touch input
        EnhancedTouchSupport.Enable();
        touchControls = new TouchControls();

        // Setup touch gestures for panning and rotating
        touchControls.Touch.TouchPress.started += ctx => StartDrag(ctx);
        touchControls.Touch.TouchPress.canceled += ctx => EndDrag(ctx);
    }

    private void OnEnable()
    {
        touchControls.Enable();
    }

    private void OnDisable()
    {
        touchControls.Disable();
    }

    private void FixedUpdate()
    {
        PanCamera();
        RotateCamera();
        CameraDragPan();
    }

    private void StartDrag(InputAction.CallbackContext context)
    {
        if (useDragPan)
        {
            dragStartPos = Touch.activeTouches[0].screenPosition;
            isDragging = true;
        }
    }

    private void EndDrag(InputAction.CallbackContext context)
    {
        if (useDragPan)
        {
            isDragging = false;
        }
    }

    public void PanCamera()
    {
        if (isPanable)
        {
            Vector3 inputDir = Vector3.zero;

            // Keyboard input
            if (Keyboard.current.wKey.isPressed) inputDir.z = +1f;
            if (Keyboard.current.sKey.isPressed) inputDir.z = -1f;
            if (Keyboard.current.aKey.isPressed) inputDir.x = -1f;
            if (Keyboard.current.dKey.isPressed) inputDir.x = +1f;

            // Touch drag pan input
            if (isDragging)
            {
                var dragDelta = (Vector2)Touch.activeTouches[0].screenPosition - dragStartPos;
                inputDir.x = -dragDelta.x * 0.01f;
                inputDir.z = -dragDelta.y * 0.01f;
                dragStartPos = Touch.activeTouches[0].screenPosition;
            }

            Vector3 movDir = transform.forward * inputDir.z + transform.right * inputDir.x;
            transform.position += movDir * (cameraPanSpeed * Time.deltaTime);
        }
    }

    public void RotateCamera()
    {
        if (isRotatable)
        {
            float rotateDir = 0f;

            // Keyboard input
            if (Keyboard.current.qKey.isPressed) rotateDir = +1f;
            if (Keyboard.current.eKey.isPressed) rotateDir = -1f;

            // Multi-touch rotate input (two-finger twist)
            if (Touch.activeTouches.Count == 2)
            {
                var touch0 = Touch.activeTouches[0];
                var touch1 = Touch.activeTouches[1];
                var prevDistance = (touch0.screenPosition - touch0.delta).magnitude - (touch1.screenPosition - touch1.delta).magnitude;
                var currentDistance = (touch0.screenPosition - touch1.screenPosition).magnitude;
                rotateDir = prevDistance > currentDistance ? -1f : 1f;
            }

            transform.eulerAngles += new Vector3(0, rotateDir * rotateCameraSpeed * Time.deltaTime, 0);
        }
    }

    public void CameraDragPan()
    {
        if (useDragPan && isDragging)
        {
            // Dragging logic handled in PanCamera()
        }
    }
}
