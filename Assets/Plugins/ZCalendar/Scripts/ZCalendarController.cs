/*
 * Created by JacobKay - 2022.08.24
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ZTools
{
    public class ZCalendarController
    {
        public int Year { set; get; }
        public int Month { set; get; }
        public int Day { set; get; }
        /// <summary>
        /// 当前是否在区间选择状态
        /// </summary>
        private bool isInRange = false;
        public bool IsInRange { get { return isInRange; } }
        private string week;
        private DateTime now;
        private int days;
        /// <summary>
        /// 当前选中的位置
        /// </summary>
        public Vector3 pos;
        private int lastMonthDays;
        private int nextMonthDays;
        public ZCalendar zCalendar;
        public ZCalendarModel zCalendarModel;
        public DateTime nowTime = DateTime.Today;
        private int lastMonthEmptyDays;
        bool isShow = true;
        public bool isInit = false;
        /// <summary>
        /// 保存文字颜色
        /// </summary>
        public Color greyColor;

        public System.Globalization.ChineseLunisolarCalendar cncld = new System.Globalization.ChineseLunisolarCalendar();
        /// <summary>
        /// 农历月
        /// </summary>
        public string[] lunarMonths = { "正", "二", "三", "四", "五", "六", "七", "八", "九", "十", "十一", "腊" };

        public string[] lunarDaysT = { "初", "十", "廿", "三" };

        /// <summary>
        /// 农历日
        /// </summary>
        public string[] lunarDays = { "一", "二", "三", "四", "五", "六", "七", "八", "九", "十" };
        DateTime monthFirstDay;
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="date"></param>
        public void Init()
        {
            zCalendarModel.zCalendarController = this;
            zCalendarModel.Init();
            if (zCalendarModel.isStaticCalendar) return;
            // 动态日历，可关闭
            if (zCalendarModel.isPopupCalendar)
            {
                zCalendarModel.btnClose.onClick.AddListener(() =>
                {
                    Hide();
                });
            }
            zCalendarModel.btnLastYear.onClick.AddListener(LastYear);
            zCalendarModel.btnNextYear.onClick.AddListener(NextYear);
            zCalendarModel.btnLastMonth.onClick.AddListener(LastMonth);
            zCalendarModel.btnNextMonth.onClick.AddListener(NextMonth);
        }

        /// <summary>
        /// 按照规定时间初始化日历
        /// </summary>
        public void InitDate(DateTime date)
        {
            now = date;
            DestroyAllChildren();
            UpdateYear();
            UpdateMonth();
            UpdateDays();
            UpdateData();
            if (!isInit)
            {
                isInit = true;
                zCalendar.DateComplete();
            }
        }
        void LastYear()
        {
            now = now.AddYears(-1);
            DestroyAllChildren();
            UpdateYear();
            UpdateMonth();
            UpdateDays();
            UpdateData();
        }
        void NextYear()
        {
            now = now.AddYears(1);
            DestroyAllChildren();
            UpdateYear();
            UpdateMonth();
            UpdateDays();
            UpdateData();
        }
        void LastMonth()
        {
            now = now.AddMonths(-1);
            DestroyAllChildren();
            UpdateYear();
            UpdateMonth();
            UpdateDays();
            UpdateData();
        }
        void NextMonth()
        {
            now = now.AddMonths(1);
            DestroyAllChildren();
            UpdateYear();
            UpdateMonth();
            UpdateDays();
            UpdateData();
        }

        List<ZCalendarDayItem> dayItemList = new List<ZCalendarDayItem>();

        /// <summary>
        /// 如果是区间日历，选择时间时，需要判断当前日期选择状态
        /// </summary>
        /// <returns></returns>
        public void ChangeRangeType(ZCalendarDayItem dayItem)
        {
            isInRange = !isInRange;
            if (dayItemList.Count >= 2)
            {
                dayItemList.Clear();
            }
            if (dayItemList.Count == 0)
            {
                dayItemList.Add(dayItem);
            }
            else
            {
                int compare = DateTime.Compare(dayItemList[0].dateTime, dayItem.dateTime);
                if (compare <= 0)
                {
                    dayItemList.Add(dayItem);
                }
                else
                {
                    dayItemList.Insert(0, dayItem);
                }
            }
            if (!isInRange)
            {
                zCalendar.RangeCalendar(dayItemList[0], dayItemList[1]);
            }
        }
        /// <summary>
        /// 显示日历
        /// </summary>
        public void Show()
        {
            if (pos != null && !isShow)
            {
                isShow = true;
                zCalendar.transform.localPosition = pos;
            }
        }
        /// <summary>
        /// 隐藏日历
        /// </summary>
        public void Hide()
        {
            if (isShow && !isInRange)
            {
                isShow = false;
                //Debug.Log("hide");
                zCalendar.transform.localPosition = new Vector3(pos.x, 5000, pos.z);
            }
        }
        /// <summary>
        /// 查询年数据
        /// </summary>
        void UpdateYear()
        {
            Year = now.Year;
        }
        /// <summary>
        /// 查询月数据
        /// </summary>
        void UpdateMonth()
        {
            Month = int.Parse(now.Month.ToString("00"));
        }
        /// <summary>
        /// 返回要查询那天
        /// </summary>
        /// <returns></returns>
        void UpdateDays()
        {
            days = DateTime.DaysInMonth(Year, Month);
            if (Day == 0)
            {
                Day = now.Day;
            }
            else if (Day > days)
            {
                Day = days;
            }
        }
        /// <summary>
        /// 更新显示月份
        /// </summary>
        void UpdateData()
        {
            zCalendarModel.SetTimeTxt(Year, Month);
            FillLastMonth();
            for (int i = 0; i < days; i++)
            {
                AddDayItem(monthFirstDay.AddDays(i));
            }
            FillNextMonth();
        }
        /// <summary>
        /// 自动填充上个月内容
        /// </summary>
        void FillLastMonth()
        {
            monthFirstDay = new DateTime(Year, Month, 1);
            lastMonthEmptyDays = GetLastMonthDays();
            if (zCalendarModel.autoFillDate)
            {
                for (int i = lastMonthEmptyDays; i > 0; i--)
                {
                    AddDayItem(monthFirstDay.AddDays(-i));
                }
            }
            else
            {
                for (int i = 0; i < lastMonthEmptyDays; i++)
                {
                    ZCalendarDayItem dayItem = zCalendarModel.Instantiate();
                    dayItem.zCalendarController = this;
                    dayItem.CloseClickAble();
                }
            }
        }
        /// <summary>
        /// 添加下个月的时间
        /// </summary>
        void FillNextMonth()
        {
            int nextMonthDays = 42 - (lastMonthEmptyDays + days);
            if (nextMonthDays != 0)
            {
                if (zCalendarModel.autoFillDate)
                {
                    DateTime lastDay = monthFirstDay.AddDays(days);
                    for (int i = 0; i < nextMonthDays; i++)
                    {
                        AddDayItem(lastDay.AddDays(i));
                    }
                }
            }
        }
        /// <summary>
        /// 添加日期对象
        /// </summary>
        void AddDayItem(DateTime dateTime)
        {
            ZCalendarDayItem dayItem = zCalendarModel.Instantiate();
            dayItem.zCalendarController = this;
            dayItem.Init(dateTime, nowTime);
            zCalendar.UpdateDate(dayItem);
            if (!isInRange && dayItemList.Count > 0)
            {
                dayItem.IsRangeDayItem(dayItemList[0], dayItemList[1]);
            }
        }
        /// <summary>
        /// 判断上一个月有几天
        /// </summary>
        /// <returns></returns>
        int GetLastMonthDays()
        {
            string firstWeek = new DateTime(Year, Month, 1).DayOfWeek.ToString();
            return (int)Enum.Parse(typeof(DayOfWeek), firstWeek);
        }
        /// <summary>
        /// 删除所有内容
        /// </summary>
        void DestroyAllChildren()
        {
            List<Transform> lst = new List<Transform>();
            int count = zCalendarModel.dayContent.childCount;
            for (int i = 0; i < count; i++)
            {
                Transform child = zCalendarModel.dayContent.GetChild(i);
                lst.Add(child);
            }
            for (int i = 0; i < lst.Count; i++)
            {
                MonoBehaviour.Destroy(lst[i].gameObject);
            }
        }
    }
}
