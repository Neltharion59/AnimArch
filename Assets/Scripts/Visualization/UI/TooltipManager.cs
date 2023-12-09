using TMPro;
using UnityEngine;

public class TooltipManager : MonoBehaviour 
{
    public static TooltipManager Instance { get; private set; }
    public TextMeshProUGUI TooltipText;
    [SerializeField] private Camera mainCamera;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        Vector3 mousePosScreen = Input.mousePosition;
        RectTransform rectTransform = GetComponent<RectTransform>();

        if (mousePosScreen.y - 2*TooltipText.preferredHeight < 0)
        {
            rectTransform.pivot = new Vector2(rectTransform.pivot.x, (float)-0.1);
        }
        else
        {
            rectTransform.pivot = new Vector2(rectTransform.pivot.x, (float)1.5);
        }

        Vector3 mousePosWorld = mainCamera.ScreenToWorldPoint(new Vector3(mousePosScreen.x, mousePosScreen.y, Mathf.Abs(mainCamera.transform.position.z - transform.position.z)));
        
        transform.position = mousePosWorld;
    }

    public void ShowTooltip(string text)
    {
        Update();
        gameObject.SetActive(true);
        TooltipText.text = text;
    }

    public void HideTooltip()
    {
        if (!gameObject.activeSelf) 
        {
            return;
        }
        gameObject.SetActive(false);
        TooltipText.text = string.Empty;
    }
}
