/****************************************************************************
 * 2023.12 ADMIN-20230222V
 ****************************************************************************/

using System.Collections.Generic;
using UnityEngine;
using QFramework;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using System.Threading;
using System.Linq;
using HomeVisit.Task;

namespace HomeVisit.UI
{
	public partial class SelectionWindow : UIElement
	{
		[SerializeField] OptionItem optionPrefab;
		[SerializeField] ToggleGroup toggleGroup;

		List<OptionItem> optionItems = new List<OptionItem>();

		CancellationTokenSource ctsEnable = null;

		private void OnEnable()
		{
			ctsEnable?.Dispose();
			ctsEnable = null;

			ctsEnable = new CancellationTokenSource();
		}

		private void OnDisable()
		{
			ctsEnable.Cancel();
		}

		public async UniTask<OptionData> WaitSelectionOptions(SelectionSO selectionSO)
		{
			//清理之前的选项
			foreach (var item in optionItems)
				Destroy(item.gameObject);
			optionItems.Clear();

			//创建新的选项
			foreach (var option in selectionSO.options)
			{
				OptionItem optionItem = Instantiate(optionPrefab, optionParent);
				optionItem.tmp.text = option.str;
				optionItem.tog.group = toggleGroup;
				optionItems.Add(optionItem);
			}
			LayoutRebuilder.ForceRebuildLayoutImmediate(optionParent);

			while (true)
			{
				await btnConfirm.OnClickAsync();
				for (int i = 0; i < optionItems.Count; i++)
				{
					if (optionItems[i].tog.isOn)
					{
						return selectionSO.options[i];
					}
				}
			}
		}

		protected override void OnBeforeDestroy()
		{
		}
	}
}