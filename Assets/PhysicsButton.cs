using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PhysicsButton : MonoBehaviour
{
    [SerializeField] private float threshold = 0.05f;
    [SerializeField] private float deadZone = 0.025f;

    private bool _isPressed;
    private Vector3 _startPos;
    private ConfigurableJoint _joint;

    public UnityEvent onPressed, onReleased;
    public Material glowMat;
    public Material regMat;
    public GameObject button;

    void Start()
    {
        _startPos = transform.localPosition;
        _joint = GetComponent<ConfigurableJoint>();
}

void Update()
    {
        if (!_isPressed && GetValue() + threshold >= 1)
            Pressed();
        if (_isPressed && GetValue() - threshold <= 0)
            Released();
    }

    private float GetValue()
    {
        var value = Vector3.Distance(_startPos, transform.localPosition) / _joint.linearLimit.limit;

        if (Mathf.Abs(value) < deadZone)
            value = 0;

        return Mathf.Clamp(value, -1f, 1f);
    }

    private void Pressed()
    {
        _isPressed = true;
        onPressed.Invoke();
        //Debug.Log("Pressed.");
        ButtonGlow();
        Invoke("ButtonNormal", 2);

        // Make button unable to be pressed
        ConfigurableJoint PhysButton = GetComponentInChildren<ConfigurableJoint>();
        SoftJointLimit newLimit = new SoftJointLimit();
        newLimit.limit = 0f;
        PhysButton.linearLimit = newLimit;

        // turn off button interactability while elevator doors close and open        
        SwapEnabled();
        Invoke("SwapEnabled", (float) 3.5);

        newLimit.limit = 0.01f;
        PhysButton.linearLimit = newLimit;
    }

    private void SwapEnabled()
    {
        enabled = !enabled;
    }

    private void Released()
    {
        _isPressed = false;
        onReleased.Invoke();
        Debug.Log("Released.");
    }
    private void ButtonGlow()
    {
        Renderer renderer = button.GetComponent<Renderer>();
        Material oriMat = renderer.material;
        renderer.material = glowMat;
    }

    private void ButtonNormal()
    {
        Renderer renderer = button.GetComponent<Renderer>();
        renderer.material = regMat;
    }
}