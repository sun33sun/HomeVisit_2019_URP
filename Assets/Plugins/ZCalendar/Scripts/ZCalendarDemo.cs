/*
 * JacobKay --20220903
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZTools;
/// <summary>
/// 使用示例
/// </summary>
public class ZCalendarDemo : MonoBehaviour
{
    public ZCalendar zCalendar;
    // Start is called before the first frame update
    void Start()
    {
        zCalendar.UpdateDateEvent += ZCalendar_UpdateDateEvent;
        zCalendar.ChoiceDayEvent += ZCalendar_ChoiceDayEvent;
        zCalendar.RangeTimeEvent += ZCalendar_RangeTimeEvent;
        zCalendar.CompleteEvent += ZCalendar_CompleteEvent;
        //zCalendar.Init();
        //zCalendar.Init(System.DateTime.Now);
        //zCalendar.Init("2022-02-02");
        //zCalendar.Show();
        //zCalendar.Hide();
    }
    /// <summary>
    /// 加载结束
    /// </summary>
    private void ZCalendar_CompleteEvent()
    {
        Debug.Log("ZCalendar加载结束");
        if (null != zCalendar.CrtTime)
        {
            Debug.Log($"当前时间{zCalendar.CrtTime.Day}");
        }
    }

    /// <summary>
    /// 区间时间
    /// </summary>
    /// <param name="arg1"></param>
    /// <param name="arg2"></param>
    private void ZCalendar_RangeTimeEvent(ZCalendarDayItem arg1, ZCalendarDayItem arg2)
    {
        Debug.Log($"选择的时间区间：{arg1.Day}到{arg2.Day}");
    }

    /// <summary>
    /// 获取选择的日期
    /// </summary>
    /// <param name="obj"></param>
    private void ZCalendar_ChoiceDayEvent(ZCalendarDayItem obj)
    {
        Debug.Log($"选择的日期：{obj.Day}");
    }

    /// <summary>
    /// 切换月份时，可拿到每一天的item对象
    /// </summary>
    /// <param name="obj"></param>
    private void ZCalendar_UpdateDateEvent(ZCalendarDayItem obj)
    {
        Debug.Log($"加载日期：{obj.Day}");
    }
    private void OnDestroy()
    {
        zCalendar.UpdateDateEvent -= ZCalendar_UpdateDateEvent;
        zCalendar.ChoiceDayEvent -= ZCalendar_ChoiceDayEvent;
        zCalendar.RangeTimeEvent -= ZCalendar_RangeTimeEvent;
        zCalendar.CompleteEvent -= ZCalendar_CompleteEvent;
    }
}
