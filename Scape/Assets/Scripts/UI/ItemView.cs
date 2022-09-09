using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI
{
    public class ItemView : MonoBehaviour
    {

        public TextMeshProUGUI indicator;
        public Image icon;
        public GameObject highlight;

        public void SetSelected(bool value)
        {
            highlight.SetActive(value);
        }

        public void Clear()
        {
            indicator.text = "";
            icon = null;
            highlight.SetActive(false);
        }

    }

}