using UnityEngine;

namespace HomeVisit.UI
{
    public interface ITitle
    {
        void InitData(TitleData titleData);
        int GetScore();

        bool GetExamResult();

        void CheckTitle();

        void Reset();

        void SetInteractable(bool newState);

        bool GetInteractable();
    }
}


