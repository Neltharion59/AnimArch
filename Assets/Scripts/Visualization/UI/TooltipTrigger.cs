using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler 
{
    public Button button;
    public LocalizeStringEvent eventHandler;
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
