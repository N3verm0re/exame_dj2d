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
            FindObjectOfType<LineRenderer>().enabled = true;
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, Input.mousePosition, myCanvas.worldCamera, out pos);
            FindObjectOfType<LineRenderer>().SetPosition(0, pos);
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
            this.transform.position = myCanvas.transform.TransformPoint(pos);
        }

        //Minion Controls
        if (this.CompareTag("PlayerMinion"))
        {
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, Input.mousePosition, myCanvas.worldCamera, out pos);
            FindObjectOfType<LineRenderer>().SetPosition(1, pos);
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
            FindObjectOfType<LineRenderer>().enabled = false;
            List<RaycastResult> targets = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, targets);

            foreach(RaycastResult target in targets)
            {
                Debug.Log($"Raycast Targeted {target.gameObject.name}");
                if (target.gameObject.CompareTag("EnemyMinion") && !this.GetComponent<MinionController>().isAsleep)
                {
                    this.GetComponent<MinionController>().isAsleep = true;
                    int targetAttack = target.gameObject.GetComponent<MinionController>().attack; //saving targets attack value in the case it dies before the trade is complete
                    target.gameObject.GetComponent<MinionController>().TakeDamage(this.GetComponent<MinionController>().attack);
                    this.GetComponent<MinionController>().TakeDamage(targetAttack);
                    break;
                }
            }
        }
    }
}
