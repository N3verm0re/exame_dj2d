using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AutoSpacing : MonoBehaviour
{
    public float baseSpacing = -270f;
    public float step = 5f;
    float spacing;
    HorizontalLayoutGroup layoutGroup;

    private void Awake()
    {
        layoutGroup  = this.GetComponent<HorizontalLayoutGroup>();
    }
    void Update()
    {
        GameObject[] cards = GameObject.FindGameObjectsWithTag("PlayerCard");
        spacing = baseSpacing - (cards.Length * step);
        
        if (spacing <= -400f)
        {
            spacing = -390f;
        }

        layoutGroup.spacing = spacing;
    }
}
