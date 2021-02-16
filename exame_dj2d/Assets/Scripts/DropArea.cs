using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropArea : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log($"PointerEnter on {this.name}");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log($"PointerExit on {this.name}");
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log($"{eventData.pointerDrag.name} was dropped on {this.name}");

        CardController card = eventData.pointerDrag.GetComponent<CardController>();
        if (card != null && card.CompareTag("PlayerCard") && card.manaCost <= Manager.Instance.playerMana && GameObject.FindGameObjectsWithTag("PlayerMinion").Length < 7)
        {
            Manager.Instance.playerMana -= card.manaCost;
            card.PlayCard();
        }
    }
}
