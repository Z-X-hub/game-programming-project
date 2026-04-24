using UnityEngine;
using UnityEngine.UI;

public class SolarSystemController : MonoBehaviour
{
    public static SolarSystemController Instance { get; private set; }

    [Header("Scene")]
    public Camera sceneCamera;
    public Transform defaultLookTarget;
    public AudioSource audioSource;

    [Header("UI")]
    public GameObject infoPanel;
    public Text factText;
    public Text progressText;
    public Text checklistText;
    public Button returnButton;

    [Header("Camera Motion")]
    public float moveSpeed = 4.5f;
    public float rotateSpeed = 6f;

    private Vector3 homePosition;
    private Quaternion homeRotation;
    private SolarBody selectedBody;
    private readonly System.Collections.Generic.HashSet<string> visitedBodies = new System.Collections.Generic.HashSet<string>();

    private void Awake()
    {
        Instance = this;

        if (sceneCamera == null)
        {
            sceneCamera = Camera.main;
        }

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    private void Start()
    {
        if (sceneCamera != null)
        {
            homePosition = sceneCamera.transform.position;
            homeRotation = sceneCamera.transform.rotation;
        }

        if (returnButton != null)
        {
            returnButton.onClick.AddListener(ReturnHome);
            returnButton.gameObject.SetActive(false);
        }

        SetFactText("Space Mission\nVisit Earth and the Moon, then try the other objects.");
        UpdateProgressText();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
        {
            ReturnHome();
        }

        UpdateCamera();
    }

    public void SelectBody(SolarBody body)
    {
        if (selectedBody != null)
        {
            selectedBody.ClearHighlight();
        }

        selectedBody = body;
        visitedBodies.Add(selectedBody.displayName);
        selectedBody.Highlight(1.4f);

        SetFactText(selectedBody.displayName + "\n" + selectedBody.childFriendlyFact);
        UpdateProgressText();

        if (returnButton != null)
        {
            returnButton.gameObject.SetActive(true);
        }

        if (audioSource != null && selectedBody.clickSound != null)
        {
            audioSource.PlayOneShot(selectedBody.clickSound, 0.7f);
        }
    }

    public void ReturnHome()
    {
        if (selectedBody != null)
        {
            selectedBody.ClearHighlight();
        }

        selectedBody = null;
        SetFactText("Space Mission\nVisit Earth and the Moon, then try the other objects.");
        UpdateProgressText();

        if (returnButton != null)
        {
            returnButton.gameObject.SetActive(false);
        }
    }

    private void UpdateCamera()
    {
        if (sceneCamera == null)
        {
            return;
        }

        Vector3 targetPosition = homePosition;
        Quaternion targetRotation = homeRotation;

        if (selectedBody != null)
        {
            Vector3 lookPoint = selectedBody.transform.position;
            Vector3 outward = (lookPoint - Vector3.zero).normalized;

            if (outward.sqrMagnitude < 0.01f)
            {
                outward = new Vector3(0f, 0.3f, -1f).normalized;
            }

            targetPosition = lookPoint - outward * selectedBody.cameraDistance + Vector3.up * selectedBody.cameraHeight;
            targetRotation = Quaternion.LookRotation(lookPoint - targetPosition, Vector3.up);
        }
        else if (defaultLookTarget != null)
        {
            targetRotation = Quaternion.LookRotation(defaultLookTarget.position - targetPosition, Vector3.up);
        }

        sceneCamera.transform.position = Vector3.Lerp(sceneCamera.transform.position, targetPosition, Time.deltaTime * moveSpeed);
        sceneCamera.transform.rotation = Quaternion.Slerp(sceneCamera.transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
    }

    private void SetFactText(string message)
    {
        if (infoPanel != null && !infoPanel.activeSelf)
        {
            infoPanel.SetActive(true);
        }

        if (factText != null)
        {
            factText.text = message;
        }
    }

    private void UpdateProgressText()
    {
        bool earthVisited = visitedBodies.Contains("Earth");
        bool moonVisited = visitedBodies.Contains("Moon");
        bool marsVisited = visitedBodies.Contains("Mars");
        bool sunVisited = visitedBodies.Contains("Sun");

        if (progressText != null)
        {
            progressText.text = "Visited: " + visitedBodies.Count + "/4";
        }

        if (checklistText != null)
        {
            checklistText.text =
                Mark(earthVisited) + " Earth discovery\n" +
                Mark(moonVisited) + " Moon discovery\n" +
                Mark(visitedBodies.Count > 0) + " Close-up view\n" +
                Mark(visitedBodies.Count > 0) + " Sparkle and sound\n" +
                Mark(sunVisited || marsVisited) + " Bonus visit";
        }
    }

    private static string Mark(bool complete)
    {
        return complete ? "[x]" : "[ ]";
    }
}
