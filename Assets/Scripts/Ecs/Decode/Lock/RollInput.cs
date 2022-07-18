using System;
using System.Collections;
using Decode;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Decode
{
	/// <summary>
	/// 通过滚轮输入
	/// 挂在锁上
	/// 通过结束
	/// </summary>
	
	public struct Roll
	{
		public enum AnswerType
		{
			Rotation,
			Index
		}
		public MouseRollControl rollControl;
		public AnswerType type;
		[ShowIf("type",AnswerType.Rotation)]
		public float answerFloat;
		
		[ShowIf("type",AnswerType.Index)]
		public int answerIndex;
	}
	[RequireComponent(typeof(AudioSource))]
#if UNITY_EDITOR
	[ExecuteAlways]
#endif
	public class RollInput : DecodeBaseInput
	{
		public Roll[] rolls;
		public string name;
		
#if UNITY_EDITOR
		private void OnValidate()
		{
			foreach (var roll in rolls)
			{
				if (roll.rollControl)
					roll.rollControl.input = this;
			}
		}
		
#endif

		public override void CheckInput()
		{
			foreach (var roll in rolls)
			{
				
				switch (roll.type)
				{
					case Roll.AnswerType.Index:
						var index = (roll.rollControl.index + roll.rollControl.total) % roll.rollControl.total;
						if (roll.answerIndex != index)
						{
							return;
						}
						break;
					case Roll.AnswerType.Rotation:
						var angle = roll.rollControl.currentAngel;
						print(angle);
						angle %= 360;
						if (angle > 180)
							angle -= 360;
						if (angle < -180)
							angle += 360;
						if (Mathf.Abs((angle - roll.answerFloat)) > 0.1f)
						{
							return;
						}
						break;
				}
				
			}
			//DoActions();
		}


		//public override void DoActions()
		//{
		//	foreach (var action in actions)
		//	{
		//		action.CheckDoAction();
		//	}
		//}
	}
}