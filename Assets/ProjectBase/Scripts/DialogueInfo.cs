using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeVisit
{
	public class Dialogue
	{
		public string[] keywords;
		public string parent;
		public string imgPath;
	}
	public class DialogueInfo
	{
		public string dialogueName = "";
		public List<Dialogue> dialogues = new List<Dialogue>();
	}
}
