using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeVisit.UI
{
	public enum TitleType
	{
		SingleTitle, MultipleTitle, JudgeTitle, OutlineTitle
	}
	public class TitleData
	{
		public TitleType type;
		public string strTitle;
		public int rightIndex;
		public List<bool> rightIndexs;
		public List<string> strOptions;
		public int score;
	}
}
