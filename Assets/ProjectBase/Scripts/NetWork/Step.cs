using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBase
{
	public class Step
	{
		public int seq = 0;
		public string title = null;
		public string startTime = null;
		public string endTime = null;
		public string timeUsed = null;
		public string expectTime = null;
		public int maxScore = 0;
		public int score = 0;
		public int repeatCount = 0;
		public string evaluation = null;
		public string scoringModel = null;
		public string remarks = null;
		public string ext_data = null;
	}
}
