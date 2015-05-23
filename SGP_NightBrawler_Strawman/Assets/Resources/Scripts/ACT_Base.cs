using UnityEngine;
using System.Collections;

[System.Serializable]
public class ACT_Base 
{

    public int Act_baseHP;          //The base HP of the current Actor
    public int Act_basePower;       //The base Power of the current Actor
    public int Act_baseSpeed;       //The base Speed of the current Actor
    public float Act_baseAspeed;      //The base Attack Speed modifier of the current Actor

    public int Act_currHP;          //The current HP of the Actor, can be modifie and change in play
    public int Act_currPower;       //The current Power of the Actor, can be modifie and change in play
    public int Act_currSpeed;       //The current Speed of the Actor, can be modifie and change in play
    public float Act_currAspeed;    //The current Attack Speed modifier of the Actor, can be modified and change in play dynamically woweee zowwy!

    public bool Act_facingRight = true;     //The direction the Actor is facing, use fro back attacks and shilds
    public bool Act_HasMod;                 //Does the Actor has a Modification acting on it
    public bool Act_ModIsBuff;              //If the Actor is afflicted is it a Buff or a DeBuff

    public int Act_HPLevel;                 //Current HP level;
    public int Act_PowerLevel;              //Current Power level;
    public int Act_SpeedLevel;              //Current Speed level;
    public int Act_AverageLevel;            //Average Of all levels;

    //Mutators
    public void SetCurrHP(int n_hp)
    {
        Act_currHP = n_hp;
    }
    public void SetCurrPower(int n_pwr)
    {
        Act_currPower = n_pwr;
    }
    public void SetCurrSpeed(int n_spd)
    {
        Act_currSpeed = n_spd;
    }
    public void SetCurrAttackSpeed(int n_spd)
    {
        Act_currAspeed = n_spd;
    }

    public void SetBaseHP(int n_hp)
    {
        Act_baseHP = n_hp;
    }
    public void SetBasePower(int n_pwr)
    {
        Act_basePower = n_pwr;
    }
    public void SetBaseSpeed(int n_spd)
    {
        Act_baseSpeed = n_spd;
    }
    public void SetBaseAttackSpeed(int n_spd)
    {
        Act_baseAspeed = n_spd;
    }

    //Interface
    public void RestoreToBaseHP()       //Restores current HP to its base value
    {
        Act_currHP = Act_baseHP;
    }
    public void RestoreToBasePower()    //Restores current Power to its base value
    {
        Act_currPower = Act_basePower;
    }
    public void RestoreToBaseSpeed()    //Restores current Speed to its base value
    {
        Act_currSpeed = Act_baseSpeed;
    }
    public int CalcAverageLvl()
    {
        Act_AverageLevel = (Act_HPLevel + Act_PowerLevel + Act_SpeedLevel) / 3;
        return Act_AverageLevel;
    }
}
