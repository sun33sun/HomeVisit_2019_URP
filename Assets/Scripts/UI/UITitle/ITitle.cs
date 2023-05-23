using UnityEngine;

namespace HomeVisit.UI
{
    public interface ITitle
    {
        int GetScore();

        bool GetState();

        void CheckTitle();

        void Reset();

        void SetInteractable(bool newState);

        bool GetInteractable();
    }
}


