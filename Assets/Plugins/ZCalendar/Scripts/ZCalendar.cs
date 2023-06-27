using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ZTools
{
    [RequireComponent(typeof(ZCalendarModel))]
    public class ZCalendar : MonoBehaviour
    {
        /// <summary>
        /// 数据更新时，可获取到每一个日期，并对其进行操作
        /// </summary>
        public event Action<ZCalendarDayItem> UpdateDateEvent;
        /// <summary>
        /// 可以获取到点击的某一天
        /// </summary>
        public event Action<ZCalendarDayItem> ChoiceDayEvent;
        /// <summary>
        /// 选择区间时间事件
        /// </summary>
        public event Action<ZCalendarDayItem, ZCalendarDayItem> RangeTimeEvent;
        /// <summary>
        /// 日历加载结束
        /// </summary>
        public event Action CompleteEvent;
        /// <summary>
        /// 获取当前选中的天对象
        /// </summary>
        public ZCalendarDayItem CrtTime { get; set; }
        /// <summary>
        /// model
        /// </summary>
        private ZCalendarModel zCalendarModel;
        /// <summary>
        /// controller
        /// </summary>
        private ZCalendarController zCalendarController;
        /// <summary>
        /// 入口
        /// </summary>
        private void Start()
        {
            zCalendarModel = this.GetComponent<ZCalendarModel>();
            zCalendarController = new ZCalendarController()
            {
                zCalendar = this,
                zCalendarModel = zCalendarModel,
                pos = this.transform.localPosition
            };
            zCalendarController.Init();
            // 开启时自动初始化
            if (zCalendarModel.awake2Init)
            {
                Init();
            }
        }
        /// <summary>
        /// 按照现在时间初始化
        /// </summary>
        public void Init()
        {
            zCalendarController.InitDate(DateTime.Now);
        }
        /// <summary>
        /// 按照DateTime格式初始化日历
        /// </summary>
        public void Init(DateTime dateTime)
        {
            zCalendarController.InitDate(dateTime);
        }
        /// <summary>
        /// 按照YYYY-MM-DD格式初始化日历
        /// </summary>
        public void Init(string dateTime)
        {
            string[] dateTimes = dateTime.Split('-');
            zCalendarController.InitDate(new DateTime(int.Parse(dateTimes[0]), int.Parse(dateTimes[1]), int.Parse(dateTimes[2])));
        }
        /// <summary>
        /// 显示弹窗
        /// </summary>
        public void Show()
        {
            zCalendarController.Show();
        }
        /// <summary>
        /// 隐藏弹窗
        /// </summary>
        public void Hide()
        {
            zCalendarController.Hide();
        }

        /// <summary>
        /// 切换时间
        /// </summary>
        /// <param name="obj"></param>
        [Obsolete("事件触发器，请使用UpdateDateEvent获取切换月份时加载的时间对象")]
        public void UpdateDate(ZCalendarDayItem obj)
        {
            if (null != UpdateDateEvent)
            {
                UpdateDateEvent.Invoke(obj);
            }
        }
        /// <summary>
        /// 日期点击
        /// </summary>
        [Obsolete("事件触发器，请使用ChoiceDayEvent获取当前选择的时间")]
        public void DayClick(ZCalendarDayItem dayItem)
        {
            if (null != ChoiceDayEvent)
            {
                ChoiceDayEvent.Invoke(dayItem);
            }
            CrtTime = dayItem;
        }
        /// <summary>
        /// 加载结束
        /// </summary>
        [Obsolete("事件触发器，请使用CompleteEvent获取日历加载完成事件")]
        public void DateComplete()
        {
            if (null != CompleteEvent)
            {
                CompleteEvent.Invoke();
            }
        }
        /// <summary>
        /// 区间日期选择
        /// </summary>
        /// <param name="firstDay"></param>
        /// <param name="secondDay"></param>
        [Obsolete("事件触发器，请使用RangeTimeEvent获取区间时间")]
        public void RangeCalendar(ZCalendarDayItem firstDay, ZCalendarDayItem secondDay )
        {
            if (null != RangeTimeEvent)
            {
                RangeTimeEvent.Invoke(firstDay, secondDay);
            }
        }
        private void OnDestroy()
        {
            zCalendarController = null;
            GC.Collect();
        }
    }
}
