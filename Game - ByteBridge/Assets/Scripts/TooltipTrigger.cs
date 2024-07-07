using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
   public string header;
   [Multiline()]
   public string content;

   private Coroutine routine;
   public void OnPointerEnter(PointerEventData eventData)
   {
      routine = StartCoroutine(delayedShow());
   }

   private IEnumerator delayedShow()
   {
      yield return new WaitForSeconds(0.5f);
      TooltipSystem.Show(content,header);
   }

   public void OnPointerExit(PointerEventData eventData)
   {
      StopCoroutine(routine);
      TooltipSystem.Hide();
   }
}