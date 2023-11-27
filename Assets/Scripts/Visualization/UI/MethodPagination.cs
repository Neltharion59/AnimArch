using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Visualization.UI
{
    public class MethodPagination
    {
        private GameObject ButtonUp;
        private GameObject ButtonDown;
        private List<GameObject> Buttons;
        private List<string> Items;
        private int CurrentPage = 0;

        private int PageSize
        {
            get
            {
                return Buttons.Count;
            }
        }

        public MethodPagination(List<GameObject> buttons)
        {
            this.ButtonUp = null;
            this.ButtonDown = null;
            this.Buttons = buttons;
            this.Items = new();
            this.CurrentPage = 0;

            ConstructButtons();
        }

        public void FillItems(List<string> items)
        {
            this.Items = items;
            this.CurrentPage = 0;
            Refresh();
        }

        public void Refresh()
        {
            foreach (GameObject button in Buttons)
            {
                button.SetActive(false);
            }

            ButtonDown.GetComponent<Button>().interactable
                = (PageSize * (CurrentPage + 1)) < Items.Count;
            ButtonUp.GetComponent<Button>().interactable = CurrentPage > 0;

            for
            (
                int i = 0;
                i < PageSize && (CurrentPage * PageSize + i) < Items.Count;
                i++
            )
            {
                Buttons[i].GetComponentInChildren<TMP_Text>().text
                    = Items[CurrentPage * PageSize + i] + "()";
                Buttons[i].SetActive(true);
            }
        }

        public string GetSelectedItem(int btnIndex)
        {
            return Items[btnIndex + CurrentPage * Buttons.Count()];
        }

        private void ConstructButtons()
        {
            GameObject firstMethodButton = Buttons.First();
            GameObject paginationButtonTemplate = GameObject.Find("Button3D");
            
            ButtonUp
                = GameObject.Instantiate
                (
                    paginationButtonTemplate, firstMethodButton.transform.position + new Vector3(15, 3, 0),
                    firstMethodButton.transform.rotation, firstMethodButton.transform.parent
                );
            ButtonUp.name = "MethodPaginationUpBtn";
            ButtonUp.GetComponent<Button>().onClick.AddListener(() => { CurrentPage--; Refresh(); });
            ButtonUp.GetComponent<Button>().navigation = new Navigation() { mode = Navigation.Mode.None };
            ButtonUp.GetComponentInChildren<TMP_Text>().SetText("UP");
            ButtonUp.SetActive(true);

            ButtonDown
                = GameObject.Instantiate
                (
                    paginationButtonTemplate, firstMethodButton.transform.position + new Vector3(15, -27, 0),
                    firstMethodButton.transform.rotation, firstMethodButton.transform.parent
                );
            ButtonDown.name = "MethodPaginationDownBtn";
            ButtonDown.GetComponent<Button>().onClick.AddListener(() => { CurrentPage++; Refresh(); });
            ButtonDown.GetComponent<Button>().navigation = new Navigation() { mode = Navigation.Mode.None };
            ButtonDown.GetComponent<RectTransform>().sizeDelta *= new Vector2(2, 1);
            ButtonDown.GetComponentInChildren<TMP_Text>().SetText("DOWN");
            ButtonDown.SetActive(true);
        }
    }
}