using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Visualization.UI
{
    public class ScrollableMethodList : MonoBehaviour
    {
        public GameObject MethodPrefabButton;
        public Transform ButtonParent;
        private readonly List<GameObject> Buttons = new();
        private List<string> Items = new();
        public ScrollableListState CurrentState { get; set; }
        private void Start()
        {   
            Items = new();
        }

        public void FillItems(List<string> items)
        {
            Items = new List<string>(items);
            Refresh();
        }

        public void Refresh()
        {
            foreach (GameObject button in Buttons)
            {
                Destroy(button); 
            }
            Buttons.Clear();

            ConstructButtons();
        }

        public void ClearItems()
        {
            foreach (GameObject button in Buttons)
            {
                Destroy(button); 
            }
            Buttons.Clear();
            Items.Clear();
        }

        private void ConstructButtons()
        {
            if (Items == null)
            {
                return;
            }
            foreach (string item in Items)
            {
                GameObject button = Instantiate(MethodPrefabButton, ButtonParent);
                button.GetComponentInChildren<TextMeshProUGUI>().text = item;
                
                CurrentState.HandleButtonClick(item, button.GetComponent<Button>());
                /*if(EditMode){
                    button.GetComponent<Button>().onClick.AddListener(() => MenuManager.Instance.SelectMethod(item));
                }else{
                    button.GetComponent<Button>().onClick.AddListener(() => MenuManager.Instance.SelectPlayMethod(item));
                }*/
                button.SetActive(true);
                Buttons.Add(button);
            }
        }
    }
    //TODO sprav z toho singletony
    //template method selectmethod a selectplaymethod presunut sem
    //spravit ine volanie pre kazdy state

    public abstract class ScrollableListState
    {
        public abstract void HandleButtonClick(string item, Button button);
    }

    public class EditModeState : ScrollableListState
    {
        public override void HandleButtonClick(string item, Button button)
        {
            button.onClick.AddListener(() => MenuManager.Instance.SelectMethod(item));
        }
    }

    public class PlayModeState : ScrollableListState
    {
        public override void HandleButtonClick(string item, Button button)
        {
            button.onClick.AddListener(() => MenuManager.Instance.SelectPlayMethod(item));
        }
    }
}