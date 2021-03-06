using UnityEngine;

public class powersA_util_debug : MonoBehaviour
{
    [Header("Links")]
    public GameObject FPSCounter;
    public Camera cam;

    [Header("Debug Options")]
    public bool displayFPS;
    public bool displayColliders;
    public bool wireframeMode;

    // Update is called once per frame
    void Update()
    {
        FPSCounter.SetActive(displayFPS); //set fps counter activate based on display fps bool

        if (displayColliders) cam.cullingMask |= (1 << 6);
        else cam.cullingMask = ~(1 << 6);
    }

    void OnPreRender()
    {
        GL.wireframe = wireframeMode; //show wireframes if wireframe mode is active
    }

    private void OnPostRender()
    {
        GL.wireframe = wireframeMode; //show wireframes if wireframe mode is active
    }
}
