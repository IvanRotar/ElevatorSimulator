using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonControll : MonoBehaviour
{
    [SerializeField] private UIPanel Lister;
    private Button button;
    void Start()
    {
        button = GetComponent<Button>();
        Lister = GetComponentInParent<UIPanel>();
        button.onClick.AddListener(OnClickBtn);
    }
    private void OnClickBtn()
    {
        if (Lister != null)
        {
            Lister.SendMessage(gameObject.name + "OnClick", null, SendMessageOptions.DontRequireReceiver);
        }
    }
}