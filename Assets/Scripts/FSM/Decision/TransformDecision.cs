using UnityEngine;
using System;

[CreateAssetMenu(menuName = "PluggableSM/Decisions/Transform")]
public class TransformDecision : Decision
{
    public StateTransformMap[] map;
    public override bool Decide(StateController controller)
    {
        MarioStateController m = (MarioStateController)controller;
        // assume the same is named after one of the posisble values in MarioState
        // convert current state name into the MarioState enum using custom class EnumExtension
        MarioState toCompareState = EnumExtension.ParseEnum<MarioState>(m.currentState.name);

        // loop through state transform and see if it matches the current transformation we are looking for
        for (int i = 0; i < map.Length; i++)
        {
            if (toCompareState == map[i].fromState && m.currentPowerupType == map[i].powerupCollected)
            {
                return true;
            }
        }
        return false;
    }
}

[System.Serializable]
public struct StateTransformMap
{
    public MarioState fromState;
    public PowerupType powerupCollected;
}
