using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MaskTest
{
    public class GameRoot : MonoBehaviour, IPointerClickHandler
    {
        public static GameRoot Instance
        {
            get; set;
        }
        public Transform Target
        {
            get { return _rectMaskControl.Target; }
        }
        CircleMaskControl _circleMaskControl;
        RectMaskControl _rectMaskControl;
        Transform[] _targets;
        private void Awake()
        {
            Instance = this;
        }
        private void Start()
        {
            _circleMaskControl = transform.Find("CircleMask").GetComponent<CircleMaskControl>();
            _rectMaskControl = transform.Find("RectMask").GetComponent<RectMaskControl>();
            //指向指定目标
            //_circleMaskControl.SetCurTarget();
            //此处采用矩形区域遮罩，圆形遮罩同理
            _rectMaskControl.SetCurTarget();
            _targets = _rectMaskControl.targets;
            for (int i = 0; i < _targets.Length; i++)
            {
                if (i != _targets.Length - 1)
                    _targets[i].GetComponent<Button>().onClick.AddListener(() =>
                    {
                        _rectMaskControl.CurTargetDone();
                        _rectMaskControl.SetCurTarget();
                    });
                else
                {
                    _targets[i].GetComponent<Button>().onClick.AddListener(() =>
                    {
#if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;

#else
                         Application.Quit();
#endif
                    });
                }
            }
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            PassEvent(eventData, ExecuteEvents.pointerClickHandler);
        }

        private void PassEvent<T>(PointerEventData pointerEventData, ExecuteEvents.EventFunction<T> eventFunction)
            where T : IEventSystemHandler
        {
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerEventData, results);
            GameObject current = pointerEventData.pointerCurrentRaycast.gameObject;
            for (int i = 0; i < results.Count; i++)
            {
                if (results[i].gameObject == Target)
                {
                    ExecuteEvents.Execute(results[i].gameObject, pointerEventData, eventFunction);
                }
            }
        }
    }

}

