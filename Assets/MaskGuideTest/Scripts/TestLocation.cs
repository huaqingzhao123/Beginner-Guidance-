using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLocation : MonoBehaviour
{
    void Start()
    {
        var corners = new Vector3[4];
        //overlay情况下返回的是屏幕坐标
        (transform as RectTransform).GetWorldCorners(corners);
        Debug.LogErrorFormat("左下角:{0},左上角:{1},右上角:{2},右下角:{3}",corners[0],corners[1],corners[2],corners[3]);
        var canvas = transform.parent.GetComponent<Canvas>();
        Debug.LogErrorFormat("转换为画布中坐标后:左下角:{0},左上角:{1},右上角:{2},右下角:{3}",WorldToCanvasPos(canvas, corners[0]), 
            WorldToCanvasPos(canvas, corners[1]), WorldToCanvasPos(canvas, corners[2]), WorldToCanvasPos(canvas, corners[3]));
    }

    /// <summary>
    /// 世界坐标向画布坐标转换
    /// </summary>
    /// <param name="canvas">画布</param>
    /// <param name="world">世界坐标</param>
    /// <returns>返回画布上的二维坐标</returns>
    Vector2 WorldToCanvasPos(Canvas canvas, Vector3 world)
    {
        Vector2 position;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, world,null, out position);

        return position;
    }

}
