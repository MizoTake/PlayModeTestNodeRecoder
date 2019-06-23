using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TestTools;

namespace PlayModeTestNodeRecorder
{
    public abstract class NodeTestScript : IMonoBehaviourTest
    {
        public bool IsTestFinished { get; private set; }

        protected void Touch (Vector2 position)
        {
            var eventDataCurrent = new PointerEventData(EventSystem.current)
            {
                position = position
            };

            var raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrent, raycastResults);
            var target = raycastResults.FirstOrDefault(x => x.gameObject.GetComponent<Collider>() != null).gameObject?.GetComponent<Collider>() ?? null;
            var point = raycastResults.First(x => x.gameObject.GetComponent<IPointerClickHandler>() != null).gameObject.GetComponent<IPointerClickHandler>();
            if (target)
            {
                target.SendMessage("OnMouseDown", null, SendMessageOptions.DontRequireReceiver);
            }
            else
            {
                point.OnPointerClick(eventDataCurrent);
            }
        }
    }
}