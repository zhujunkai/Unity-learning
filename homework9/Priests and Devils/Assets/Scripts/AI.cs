using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {
	public Queue<LinkedList<AIState>> statequeue=new Queue<LinkedList<AIState>>();
	bool findway(LinkedList<AIState> a)
	{
		AIState temp = a.Last.Value;
		if (temp.ld > temp.lp && temp.lp != 0) return false;
		if (temp.lp > temp.ld && temp.lp != 3) return false;
		if (temp.lp == 0 && temp.ld == 0) return true;
		if (temp.boatstate)//right
		{
			if (temp.lp <= 2)
			{
				AIState newtemp = new AIState(temp.lp + 1, temp.ld , false);
				LinkedList<AIState> newlist = new LinkedList<AIState>(a);
				newlist.AddLast(newtemp);
				statequeue.Enqueue(newlist);
				if (temp.lp <= 1)
				{
					AIState newtemp1 = new AIState(temp.lp + 2, temp.ld , false);
					LinkedList<AIState> newlist1 = new LinkedList<AIState>(a);
					newlist1.AddLast(newtemp1);
					statequeue.Enqueue(newlist1);
				}
			}
			if(temp.ld <= 2)
			{
				AIState newtemp = new AIState(temp.lp, temp.ld + 1, false);
				LinkedList<AIState> newlist = new LinkedList<AIState>(a);
				newlist.AddLast(newtemp);
				statequeue.Enqueue(newlist);
				if (temp.ld <= 1)
				{
					AIState newtemp1 = new AIState(temp.lp, temp.ld + 2, false);
					LinkedList<AIState> newlist1 = new LinkedList<AIState>(a);
					newlist1.AddLast(newtemp1);
					statequeue.Enqueue(newlist1);
				}
			}
			if(temp.lp <=2&& temp.ld <= 2)
			{
				AIState newtemp = new AIState(temp.lp + 1, temp.ld + 1,false);
				LinkedList<AIState> newlist = new LinkedList<AIState>(a);
				newlist.AddLast(newtemp);
				statequeue.Enqueue(newlist);
			}
		}
		else//left
		{
			if (temp.lp>=1)
			{
				AIState newtemp = new AIState(temp.lp - 1, temp.ld, true);
				LinkedList<AIState> newlist = new LinkedList<AIState>(a);
				newlist.AddLast(newtemp);
				statequeue.Enqueue(newlist);
				if (temp.lp >= 2)
				{
					AIState newtemp1 = new AIState(temp.lp - 2, temp.ld, true);
					LinkedList<AIState> newlist1 = new LinkedList<AIState>(a);
					newlist1.AddLast(newtemp1);
					statequeue.Enqueue(newlist1);
				}
			}
			if (temp.ld >=1)
			{
				AIState newtemp = new AIState(temp.lp, temp.ld - 1, true);
				LinkedList<AIState> newlist = new LinkedList<AIState>(a);
				newlist.AddLast(newtemp);
				statequeue.Enqueue(newlist);
				if (temp.ld >= 2)
				{
					AIState newtemp1 = new AIState(temp.lp, temp.ld - 2, true);
					LinkedList<AIState> newlist1 = new LinkedList<AIState>(a);
					newlist1.AddLast(newtemp1);
					statequeue.Enqueue(newlist1);
				}
			}
			if (temp.lp >=1 && temp.ld >=1)
			{
				AIState newtemp = new AIState(temp.lp - 1, temp.ld - 1, true);
				LinkedList<AIState> newlist = new LinkedList<AIState>(a);
				newlist.AddLast(newtemp);
				statequeue.Enqueue(newlist);
			}
		}
		return false;
	}
	public LinkedList<AIState> findnextState(AIState AI_inputState)
	{
		statequeue.Clear();
		LinkedList<AIState> temp = new LinkedList<AIState>();
		temp.AddLast(AI_inputState);
		statequeue.Enqueue(temp);
		int num = 0;
		while (statequeue.Count != 0)
		{
			LinkedList<AIState> testlist=statequeue.Dequeue();
			if (findway(testlist))
			{
				return testlist;
			}
			num++;
			if (num > 100000) {
				Debug.Log(statequeue.Count);
				break;
			}
		}
		return null;
	}
}
public class AIState
{
	public int lp;
	public int ld;
	public bool boatstate;//false=left,true=right
	public AIState(int lp,int ld,bool boatstate)
	{
		this.lp = lp;
		this.ld = ld;
		this.boatstate = boatstate;
	}
}
