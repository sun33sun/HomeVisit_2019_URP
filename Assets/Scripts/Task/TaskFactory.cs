using System;
using System.Collections.Generic;
using ProjectBase;

namespace HomeVisit.Task
{
    public class TaskFactory : Singleton<TaskFactory>
    {
        public string sceneName = null;
        public string homeType = null;

        private Dictionary<string, Dictionary<string, Func<BaseTask>>> taskDic;

        public TaskFactory()
        {
            this.sceneName = sceneName;
            this.homeType = homeType;
            taskDic = new Dictionary<string, Dictionary<string, Func<BaseTask>>>()
            {
                {
                    "传统",
                    new Dictionary<string, Func<BaseTask>>()
                        { { "ToBeDeveloped", () => new TraditionToBeDevelopedTask() } }
                },
            };
        }

        public BaseTask GetTask()
        {
            return taskDic[sceneName][homeType]();
        }

        public BaseTask GetTask(string sceneName, string homeType)
        {
            this.sceneName = sceneName;
            this.homeType = homeType;
            return taskDic[sceneName][homeType]();
        }
    }
}