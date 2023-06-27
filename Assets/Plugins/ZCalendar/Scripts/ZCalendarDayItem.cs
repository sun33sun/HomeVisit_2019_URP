using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Text;
namespace ZTools
{
    public class ZCalendarDayItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public GameObject imgBk;
        public GameObject rangeBk;
        public Text txt;
        public Button btn;
        public Text lunarTxt;
        [HideInInspector]
        public ZCalendarController zCalendarController;
        private bool isCanClick = true;
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public DateTime dateTime;
        private bool isOn = false;
        public bool IsOn
        {
            set
            {
                if (isOn != value || isOn)
                {
                    isOn = value;
                    imgBk?.SetActive(value);
                    if (value)
                    {
                        if (!zCalendarController.IsInRange)
                        {
                            zCalendarController.zCalendar.DayClick(this);
                        }
                        if (zCalendarController.zCalendarModel.rangeCalendar)
                        {
                            zCalendarController.ChangeRangeType(this);
                        }
                        if (zCalendarController.zCalendarModel.isPopupCalendar && zCalendarController.isInit)
                        {
                            zCalendarController.Hide();
                        }
                    }
                }
            }
            get { return isOn; }
        }
        public bool IsOnWithOutEvent
        {
            set
            {
                if (isOn != value)
                {
                    isOn = value;
                    imgBk?.SetActive(value);
                }
            }
        }
        private bool isRange;
        public bool IsRange
        {
            set
            {
                if (isRange != value)
                {
                    isRange = value;
                    rangeBk?.SetActive(value);
                }
            }
            get { return isRange; }
        }
        Color greyColor;
        /// <summary>
        /// 初始化日期
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <param name="day">日</param>
        /// <param name="nowTime"></param>
        /// <param name="crtDay">当前天</param>
        public void Init(DateTime dateTime, DateTime crtDay)
        {
            isRange = rangeBk.activeInHierarchy;
            isOn = imgBk.activeInHierarchy;
            IsOnWithOutEvent = false;
            IsRange = false;
            this.dateTime = dateTime;
            this.Year = dateTime.Year;
            this.Month = dateTime.Month;
            this.Day = dateTime.Day;
            txt.text = Day.ToString("00");

            if (!zCalendarController.zCalendarModel.rangeCalendar)
            {
                IsOn = (DateTime.Compare(dateTime, crtDay) == 0);
            }
            else
            {
                zCalendarController.zCalendar.RangeTimeEvent += RangeTimeEvent;
            }
            isCanClick = !zCalendarController.zCalendarModel.isStaticCalendar;
            greyColor = zCalendarController.greyColor.a == 0 ? new Color(txt.color.r, txt.color.g, txt.color.b, 0.1f) : zCalendarController.greyColor;
            if (!zCalendarController.zCalendarModel.isStaticCalendar)
            {
                btn.onClick.AddListener(() =>
                {
                    IsOn = true;
                });
                zCalendarController.zCalendar.ChoiceDayEvent += ChangeState;
            }
            if (!zCalendarController.zCalendarModel.isUnexpiredTimeCanClick)
                IsUnexpiredTime(zCalendarController.nowTime, dateTime);
            if (zCalendarController.zCalendarModel.autoFillDate)
            {
                IsCrtMonth(zCalendarController.Month);
            }
            if (zCalendarController.zCalendarModel.lunar)
            {
                lunarTxt.gameObject.SetActive(true);
                SolarToLunar(dateTime);
            }
        }
        /// <summary>
        /// 关闭可点击权限
        /// </summary>
        public void CloseClickAble()
        {
            isRange = rangeBk.activeInHierarchy;
            isOn = imgBk.activeInHierarchy;
            IsOn = false;
            txt.text = "";
            enabled = false;
            IsOnWithOutEvent = false;
            IsRange = false;
        }
        /// <summary>
         /// 判断是否在选择区间内的时间
         /// </summary>
        public void IsRangeDayItem(ZCalendarDayItem d1, ZCalendarDayItem d2)
        {
            RangeTimeEvent(d1, d2);
            if (DateTime.Compare(d1.dateTime, dateTime) == 0 || DateTime.Compare(d2.dateTime, dateTime) == 0)
            {
                IsOnWithOutEvent = true;
            }
        }
        /// <summary>
        /// 判断当前是否在区域选择时间内
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        void RangeTimeEvent(ZCalendarDayItem d1, ZCalendarDayItem d2)
        {
            if (DateTime.Compare(d1.dateTime, dateTime) < 0 && DateTime.Compare(d2.dateTime, dateTime) > 0)
            {
                IsRange = true;
            }
        }
        /// <summary>
        /// 改变当前状态
        /// </summary>
        void ChangeState(ZCalendarDayItem dayItem)
        {
            if (dayItem != this)
            {
                IsOn = false;
                IsRange = false;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!isOn && isCanClick)
            {
                imgBk.SetActive(true);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!isOn && isCanClick)
            {
                imgBk.SetActive(false);
            }
        }
        /// <summary>
        /// 判断是否超过了今天的时间
        /// </summary>
        void IsUnexpiredTime(DateTime time, DateTime crtTime)
        {
            int compNum = DateTime.Compare(time, crtTime);
            if (compNum < 0)
            {
                btn.interactable = false;
                isCanClick = false;
                txt.color = greyColor;
                lunarTxt.color = greyColor;
            }
        }
        /// <summary>
        /// 判断是否为本月日期
        /// </summary>
        void IsCrtMonth(int time)
        {
            if (time != Month)
            {
                btn.interactable = false;
                isCanClick = false;
                txt.color = greyColor;
                lunarTxt.color = greyColor;
            }
        }
        /// <summary>
        /// 显示农历日期
        /// </summary>
        /// <param name="time"></param>
        void SolarToLunar(DateTime dt)
        {
            int year = zCalendarController.cncld.GetYear(dt);
            int flag = zCalendarController.cncld.GetLeapMonth(year);
            int month = zCalendarController.cncld.GetMonth(dt);
            if (flag > 0)
            {
                if (flag == month)
                {
                    //闰月
                    month--;
                }
                else if (month > flag)
                {
                    month--;
                }
            }
            int day = zCalendarController.cncld.GetDayOfMonth(dt);
            lunarTxt.text = (day == 1) ? GetLunarMonth(month) : GetLunarDay(day);
            //Debug.Log($"{year}-{(month.ToString().Length == 1 ? "0" + month : month + "")}-{(day.ToString().Length == 1 ? "0" + day : day + "")}");
        }
        /// <summary>
        /// 获取农历月
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        string GetLunarMonth(int month)
        {
            if (month < 13 && month > 0)
            {
                return $"{zCalendarController.lunarMonths[month - 1]}月";
            }

            throw new ArgumentOutOfRangeException("无效的月份!");
        }
        /// <summary>
        /// 获取农历年
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        string GetLunarDay(int day)
        {
            if (day > 0 && day < 32)
            {
                if (day != 20 && day != 30)
                {
                    return string.Concat(zCalendarController.lunarDaysT[(day - 1) / 10], zCalendarController.lunarDays[(day - 1) % 10]);
                }
                else
                {
                    return string.Concat(zCalendarController.lunarDays[(day - 1) / 10], zCalendarController.lunarDaysT[1]);
                }
            }
            throw new ArgumentOutOfRangeException("无效的日!");
        }
        private void OnDestroy()
        {
            if (!zCalendarController.zCalendarModel.isStaticCalendar)
            {
                zCalendarController.zCalendar.ChoiceDayEvent -= ChangeState;
            }
            zCalendarController.zCalendar.RangeTimeEvent -= RangeTimeEvent;
        }
    }
}
