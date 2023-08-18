using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject picturePrefab; // Prefab of the picture frame
    public GameObject Frame; 
    public Transform pictureSpawnPoint; // The position to spawn the picture
    public AnimationClip slideAnimation; // Reference to the slide animation clip

    public Material RenderMaterial;
    public Material WhiteMaterial;

    public Camera renderCamera; // The camera rendering into the RenderTexture
    public RenderTexture renderTexture; // The RenderTexture to capture from

    private bool isTakingPhoto = false;
    private bool isCameraHolded = false;

    public InputActionAsset InputActionButton;
    InputAction Button;

    private void Start() 
    {
        var GameActionMap = InputActionButton.FindActionMap("VrButton");

        Button = GameActionMap.FindAction("PhotoInteract");

        Button.performed += ButtonPress;
        Button.Enable();
    }

    void ButtonPress(InputAction.CallbackContext context)
    {
        if (!isTakingPhoto && isCameraHolded)
        {
            Debug.Log("Taking photo");
            StartCoroutine(TakePhoto());
        }
    }

    private void Update()
    {
        // Assuming you're using a VR button press event to trigger the camera
        if (!isTakingPhoto && isCameraHolded)
        {
            //StartCoroutine(TakePhoto());
        }
    }

    public void SetCamera(bool CameraState) 
    {
        if (CameraState)
        {
            Frame.GetComponent<MeshRenderer>().material = RenderMaterial;
        }
        else 
        {
            Frame.GetComponent<MeshRenderer>().material = WhiteMaterial;
        }
        isCameraHolded = CameraState;
    }

    private IEnumerator TakePhoto()
    {
        isTakingPhoto = true;

        // Capture a screenshot
        yield return new WaitForEndOfFrame();

        GameObject picture = Instantiate(picturePrefab, pictureSpawnPoint.position, pictureSpawnPoint.rotation);

        Texture2D dynamicTexture = new Texture2D(1920, 1080);

        // Populate the Texture2D with color data (for example, a red color)
        Color[] pixels = new Color[1920 * 1080];
        Color fillColor = Color.red;
        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i] = fillColor;
        }
        dynamicTexture.SetPixels(pixels);
        dynamicTexture.Apply();


        picture.GetComponentInChildren<Renderer>().material.mainTexture = dynamicTexture;

        // Trigger the sliding animation
        Animation animation = picture.GetComponent<Animation>();
        animation.Play();

        yield return new WaitForSeconds(1.5f);
        // Trigger printing animation or effect if needed (via animation events)

        isTakingPhoto = false;
    }
}
