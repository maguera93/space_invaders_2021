using UnityEngine;
using UnityEngine.EventSystems;

namespace MAG.Popups
{
    public class InputsButton : MonoBehaviour, IUpdateSelectedHandler, IPointerDownHandler, IPointerUpHandler
    {
        public delegate void InputAction();
        public InputAction onHold;

        private bool isPressed = false;

        public void OnUpdateSelected(BaseEventData eventData)
        {
            if (isPressed)
                onHold();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            isPressed = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            isPressed = false;
        }
    }
}