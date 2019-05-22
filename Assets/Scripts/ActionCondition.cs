﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UtilityAI
{
    [CreateAssetMenu(fileName = "ActionCondition", menuName = "Condition/ActionCondition", order = 1)]
    public class ActionCondition : Condition
    {
        public Action action;

        // Use this for initialization
        public override bool CheckCondition(Agent agent)
        {
            if (action == agent.currentAction)
                return true;
            action.commitmentToAction = false;
            return false;
        }

        // Update is called once per frame
        public override void UpdateNeedsUI(Agent agent)
        {
            if (agent.currentAction.withinRangeOfTarget)
            {
                foreach (string need in needsAffected)
                {
                    changeInNeedUI = agent.needBars[need].fillAmount;
                    agent.SetNeed(agent.GetNeed(need), changeInNeedUI);
                    if (agent.needBars.ContainsKey(need))
                    {
                        if (multiplier > 0 && agent.needBars[need].fillAmount < 1)
                        {
                            changeInNeedUI += multiplier * Time.deltaTime;
                            agent.needBars[need].fillAmount = changeInNeedUI;
                            agent.SetNeed(agent.GetNeed(need), changeInNeedUI);
                        }
                        else if (multiplier < 0 && agent.needBars[need].fillAmount > 0)
                        {
                            changeInNeedUI += multiplier * Time.deltaTime;
                            agent.needBars[need].fillAmount = changeInNeedUI;
                            agent.SetNeed(agent.GetNeed(need), changeInNeedUI);
                        }
                    }
                }
            }
        }

        public override void Awake()
        {
            changeInNeedUI = 1;
        }

        public override void Exit(Agent agent)
        {
        }
    }
}
