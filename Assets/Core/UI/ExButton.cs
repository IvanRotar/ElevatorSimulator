using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class ExButton : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    private Button button;
    public void OnPointerEnter(PointerEventData eventData)
    {
        button.image.color = button.colors.highlightedColor;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        button.image.color = button.colors.normalColor;
    }
    private void Start()
    {
        button = GetComponent<Button>();
    }
}
