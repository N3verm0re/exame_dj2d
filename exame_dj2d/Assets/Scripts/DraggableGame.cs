using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DraggableGame : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject DragAreaPanel;
    public GameObject PlayerHand;
    public Canvas myCanvas;

    void Start()
    {
        DragAreaPanel = GameObject.FindGameObjectWithTag("DragArea");
        PlayerHand = GameObject.FindGameObjectWithTag("PlayerHand");
        myCanvas = GameObject.FindObjectOfType<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log($"Began dragging {this.name}");
        
        //Card Controls
        if (this.CompareTag("PlayerCard"))
        {
            this.transform.SetParent(DragAreaPanel.transform);
            this.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }

        //Minion Controls
        if (this.CompareTag("PlayerMinion"))
        {

        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log($"Dragging {this.name}");
        
        //Card Controls
        if (this.CompareTag("PlayerCard"))
        {
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, Input.mousePosition, myCanvas.worldCamera, out pos);
            transform.position = myCanvas.transform.TransformPoint(pos);
        }

        //Minion Controls
        if (this.CompareTag("PlayerMinion"))
        {

        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log($"Stopped dragging {this.name}");
        
        //Card Controls
        if (this.CompareTag("PlayerCard"))
        {
            this.transform.SetParent(PlayerHand.transform);
            this.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }

        //Minion Controls
        if (this.CompareTag("PlayerMinion"))
        {

        }
    }
}
