using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Questionnaire : MonoBehaviour
{
	[SerializeField] List<Button> btns;
	Button leftBtn;
	Button midBtn;
	Button rightBtn;

	List<int> xList;
	float animTime = 0.3f;
	Vector3 sScale = new Vector3(0.6f, 0.6f, 1);

	void Start()
	{
		leftBtn = btns[0];
		midBtn = btns[1];
		rightBtn = btns[2];

		for (int i = 0; i < btns.Count; i++)
		{
			int index = i;
			btns[i].onClick.AddListener(() => {
				PlayAnim(index);
			});
		}
	}

	void PlayAnim(int index)
	{
		//重置所有值
		DOTween.KillAll();
		Vector3 tempPos = leftBtn.transform.position;
		Button oldLeftBtn = leftBtn;
		tempPos.x = -390;
		oldLeftBtn.transform.localPosition = tempPos;
		Button oldMidBtn = midBtn;
		tempPos.x = 10;
		oldMidBtn.transform.localPosition = tempPos;
		Button oldRightBtn = rightBtn;
		tempPos.x = 410;
		oldRightBtn.transform.localPosition = tempPos;

		Button btn = btns[index];
		switch ((int)(btn.transform.localPosition.x))
		{
			case -390:
				DoLM(oldLeftBtn);
				DoMR(oldMidBtn);
				DoRL(oldRightBtn);
				break;
			case 410:
				DoRM(oldRightBtn);
				DoML(oldMidBtn);
				DoLR(oldLeftBtn);
				break;
		}
	}

	//点击左侧
	void DoLM(Button btn)
	{
		Transform t = btn.transform;
		Sequence s = DOTween.Sequence();
		s.Append(t.DOScale(Vector3.one, animTime));
		s.Insert(0, t.DOLocalMoveX(10, animTime));
		midBtn = btn;
		s.Play();
	}
	void DoMR(Button btn)
	{
		Transform t = btn.transform;
		Sequence s = DOTween.Sequence();
		s.Append(t.DOScale(sScale, animTime));
		s.Insert(0, t.DOLocalMoveX(410, animTime));
		rightBtn = btn;
		s.Play();
	}
	void DoRL(Button btn)
	{
		Transform t = btn.transform;
		Sequence s = DOTween.Sequence();
		s.Append(t.DOLocalMoveX(710, animTime / 3));
		s.InsertCallback(animTime / 3, () =>
		{
			DOTween.Kill(t);
			Vector3 newPos = t.localPosition;
			newPos.x = -710;
			t.position = newPos;
			t.DOLocalMoveX(-390, animTime);
		});
		s.Play();
		leftBtn = btn;
	}
	//点击右侧
	void DoRM(Button btn)
	{
		Transform t = btn.transform;
		Sequence s = DOTween.Sequence();
		s.Append(t.DOScale(Vector3.one, animTime));
		s.Insert(0, t.DOLocalMoveX(10, animTime));
		midBtn = btn;
		s.Play();
	}
	void DoML(Button btn)
	{
		Transform t = btn.transform;
		Sequence s = DOTween.Sequence();
		s.Append(t.DOScale(sScale, animTime));
		s.Insert(0, t.DOLocalMoveX(-390, animTime));
		leftBtn = btn;
		s.Play();
	}
	void DoLR(Button btn)
	{
		Transform t = btn.transform;
		Sequence s = DOTween.Sequence();
		s.Append(t.DOLocalMoveX(-710, animTime / 3));
		s.InsertCallback(animTime / 3, () =>
		{
			DOTween.Kill(t);
			Vector3 newPos = t.position;
			newPos.x = 710;
			t.position = newPos;
			t.DOLocalMoveX(410, animTime);
		});
		s.Play();
		rightBtn = btn;
	}

	public void InitPos()
	{
		DOTween.KillAll();
		leftBtn = btns[0];
		midBtn = btns[1];
		rightBtn = btns[2];

		Vector3 tempPos = leftBtn.transform.position;
		tempPos.x = -390;
		leftBtn.transform.position = tempPos;
		tempPos.x = 10;
		midBtn.transform.position = tempPos;
		tempPos.x = 410;
		rightBtn.transform.position = tempPos;
	}
}

