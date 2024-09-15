using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DodeAnimation : MonoBehaviour
{
    [Header("Initial Position")]
    [SerializeField] private Vector3 initPos;
    [SerializeField] private Quaternion initQ;
    [SerializeField] private GameObject mainObject; // to manipulate for position
    [SerializeField] private Transform wayPointPosition;


    [Header("Sides tracking and manipulation")]
    [SerializeField] private List<GameObject> sides;

    [SerializeField] private GameObject selectedSide;

    [SerializeField] private CloneDode cloneDode;

    [Header("External Animation")]
    [SerializeField] private AnimateOnKeyPress dodeOpenAnimation;
    [SerializeField] private PathFollower pathFollower; // to move the camera

    [Header("Background Image")]
    [SerializeField] private GameObject transitionBackgroundObject;
    [SerializeField] private Canvas transitionBackgroundCanvas;
    [SerializeField] private Image transitionBackgroundImage;

    public Color newColor;

    [Header("Camera Stuff")]
    [SerializeField] private Camera cameraToZoom;
    [SerializeField] private CinemachineVirtualCamera cm1;
    [SerializeField] private CinemachineVirtualCamera cm2;
    [SerializeField] private Transform rootObjectCenter;

    private void Start()
    {
        initQ = mainObject.transform.localRotation;
        initPos = mainObject.transform.localPosition;

        transitionBackgroundImage = transitionBackgroundObject.GetComponent<Image>();

        cameraToZoom = Camera.main;
    }

    public void SetSide(GameObject side)
    {
        if (side != null)
        {
            selectedSide = side;

            if (selectedSide.TryGetComponent<SideRevamp>(out SideRevamp selectedSideRevamp))
            {
                if (!selectedSideRevamp.IsDefaultRoot())
                {
                    Material rootMaterial = selectedSideRevamp.GetRoot();

                    List<Material> sidesMaterials = selectedSideRevamp.GetSides();

                    // illusion of clone dode taking over to switch materials before animation starts
                    // cloneDode.gameObject.SetActive(true);

                    // List<GameObject> cloneDodeSides = cloneDode.GetSides();

                    ApplyTextures(rootMaterial, sidesMaterials);

                    // step1: move the main dice to look at the camera (goes the same for clone)
                    if (mainObject.transform.rotation != initQ)
                    {
                        // initQ = mainObject.transform.localRotation;

                        mainObject.transform.localPosition = initPos;
                        mainObject.transform.localRotation = initQ;
                    }

                }

                // Open dode animation
                // zoom in
                // StartCoroutine(RotateDode());
                StartCoroutine(TransitionAndLoadScene(selectedSideRevamp));

                // load additive scene
                // transition to background (make this object more transparent)

                // scene transition
                // SceneManager.LoadScene(selectedSideRevamp.GetTargetScene());

                // SingletonSceneLoader.Instance.PrepareNextScene(selectedSideRevamp.GetTargetScene());
            }

        }
    }

    private void ApplyTextures(Material rootMaterial, List<Material> sidesMaterials)
    {
        // root
        sides[10].GetComponent<MeshRenderer>().sharedMaterial = rootMaterial;

        for (int i = 0; i < sides.Count; i++)
        {
            if (i == 10)
                continue;

            sides[i].GetComponent<SkinnedMeshRenderer>().sharedMaterial = sidesMaterials[i];
        }
    }

    private IEnumerator TransitionAnimation(Sprite transitionImage)
    {
        dodeOpenAnimation.StartAnimation();
        yield return new WaitForSeconds(3f);

        // pathFollower.SetMoving(true);
        // pathFollower.StartSegment();
        // yield return StartCoroutine(ZoomCameraToCenter());
        cm2.gameObject.SetActive(true);

        yield return new WaitForSeconds(3f);

        transitionBackgroundObject.SetActive(true);
        Color tempColor = transitionBackgroundImage.color;
        // transitionBackgroundImage.color = new Color(0, 0, 0, 0);
        transitionBackgroundImage.sprite = transitionImage;

        float elapsedTime = 0f;
        float animDuration = 6f;
        while (elapsedTime < animDuration)
        {

            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / animDuration);

            // Update alpha
            newColor = transitionBackgroundImage.color;
            newColor.a = Mathf.Lerp(0, 0.3f, t);
            transitionBackgroundImage.color = newColor;

            // Update plane distance
            transitionBackgroundCanvas.planeDistance = Mathf.Lerp(500, 0.3f, t);

            yield return null;
        }

        yield return new WaitForSeconds(1f);
        elapsedTime = 0f;
        animDuration = 3f;
        while (elapsedTime < animDuration)
        {

            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / animDuration);

            // Update alpha
            newColor = transitionBackgroundImage.color;
            newColor.a = Mathf.Lerp(0.3f, 1f, t);
            transitionBackgroundImage.color = newColor;

            // // Update plane distance
            // transitionBackgroundCanvas.planeDistance = Mathf.Lerp(500, 19, t);

            yield return null;
        }

        yield return null;
    }

    private IEnumerator TransitionAndLoadScene(SideRevamp selectedSideRevamp)
    {
        yield return StartCoroutine(TransitionAnimation(selectedSideRevamp.GetBackgroundImage()));

        // Load the new scene after the animation/coroutine is complete
        SingletonSceneLoader.Instance.PrepareNextScene(selectedSideRevamp.GetTargetScene());
    }

    private IEnumerator RotateDode()
    {
        Quaternion startRotation = mainObject.transform.localRotation;
        Vector3 startPosition = mainObject.transform.localPosition;

        float time = 0;

        while (time < 0.1f)
        {
            // Calculate the fraction of the duration that has passed
            time += Time.deltaTime;
            float fraction = time / 0.1f;

            // Interpolate between the start and initial rotations
            // mainObject.transform.rotation = Quaternion.Lerp(startRotation, initQ, fraction);
            // mainObject.transform.localposition = Vector3.Lerp(startPosition, wp1.position, fraction);
            mainObject.transform.localRotation = Quaternion.Lerp(startRotation, initQ, fraction);
            mainObject.transform.localPosition = Vector3.Lerp(startPosition, initPos, fraction);

            // Wait for the next frame
            yield return null;
        }

        // Ensure the rotation is exactly set to the initial rotation at the end
        // mainObject.transform.position = wp1.position;
        // mainObject.transform.rotation = initQ;

        mainObject.transform.localPosition = initPos;
        mainObject.transform.localRotation = initQ;

        yield return null;
        // this.gameObject.SetActive(false);
    }

    public void AddToSidesList(GameObject side)
    {
        sides.Add(side);
    }
}
