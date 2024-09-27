using UnityEngine;
using UnityEngine.InputSystem;

public class TouchManager : MonoBehaviour
{
    #region Events

    public delegate void StartTouch(Vector2 position, float time);
    public event StartTouch OnStartTouch;
    
    public delegate void EndTouch(Vector2 position, float time);
    public event EndTouch OnEndTouch;
    #endregion
    
    private TouchInput touchControls;
    public Camera cam;

    public void Awake()
    {
        touchControls = new TouchInput();
    }

    private void OnEnable()
    {
        touchControls.Enable();
    }

    private void OnDisable()
    {
        touchControls.Disable();
    }

    public void Start()
    {
        touchControls.Touch.TouchPress.started += context => StartTouchPrimary(context);
        touchControls.Touch.TouchPress.canceled += context => EndTouchPrimary(context);
        
    }

    public void StartTouchPrimary(InputAction.CallbackContext context)
    {
        if (OnStartTouch != null) OnStartTouch(Utils.ScreenToWorld(cam,touchControls.Touch.PrimaryTouchPosition.ReadValue<Vector2>()),(float)context.startTime); 
        if (OnEndTouch != null) OnEndTouch(Utils.ScreenToWorld(cam,touchControls.Touch.PrimaryTouchPosition.ReadValue<Vector2>()),(float)context.time); 
        Debug.Log("Touch pressed");
    }
    public void EndTouchPrimary(InputAction.CallbackContext context)
    {
        Debug.Log("Touch ended");
    }

    public Vector2 PrimaryPosition()
    {
        return Utils.ScreenToWorld(cam, touchControls.Touch.PrimaryTouchPosition.ReadValue<Vector2>());
    }
}
