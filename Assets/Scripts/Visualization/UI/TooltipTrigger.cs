using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler 
{
    private Button button;
    private LocalizeStringEvent eventHandler;

    public void Awake()
    {
        button = this.gameObject.GetComponent<Button>();
        eventHandler = this.gameObject.GetComponent<LocalizeStringEvent>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Invoke(nameof(ShowTooltip), 0.5f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CancelInvoke(nameof(ShowTooltip));
        TooltipManager.Instance.HideTooltip();
    }

    private void ShowTooltip()
    {
        if (button.IsActive())
        {
            TooltipManager.Instance.ShowTooltip(eventHandler.StringReference.GetLocalizedString());
        }
    }
}
