using HarmonyLib;
using Il2Cpp;
using Il2CppFIMSpace.Basics;
using Il2CppInterop.Runtime;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppSystem;
using Il2CppSystem.Collections.Generic;
using Il2CppTMPro;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Diagnostics;
using static Il2CppSystem.Collections.SortedList;

namespace ExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Legion : Role
{
    public override Il2CppSystem.Collections.Generic.List<SpecialRule> GetRules()
    {
        Il2CppSystem.Collections.Generic.List<SpecialRule> sr = new Il2CppSystem.Collections.Generic.List<SpecialRule>();
        sr.Add(new NightModeRule(4));
        return sr;
    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Start)
        {
            if (charRef.state == ECharacterState.Dead) return;
            Health health = PlayerController.PlayerInfo.health;
            //health.Damage(2);
            health.AddMaxHp(-2);
        }
        if (trigger == ETriggerPhase.Night)
        {
            if (charRef.state == ECharacterState.Dead) return;
            Health health = PlayerController.PlayerInfo.health;
            //health.Damage(2);
            health.AddMaxHp(-2);
        }
    }
    public w_Legion() : base(ClassInjector.DerivedConstructorPointer<w_Legion>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Legion(System.IntPtr ptr) : base(ptr)
    {

    }
    public override CharacterData GetBluffIfAble(Character charRef)
    {
        CharacterData bluff = Characters.Instance.GetRandomUniqueVillagerBluff();
        Gameplay.Instance.AddScriptCharacterIfAble(bluff.type, bluff);

        return bluff;
    }
}

[HarmonyPatch(typeof(Character), nameof(Character.Act))]
public static class legionCheckExecution
{
    public static void Postfix(Character __instance, ETriggerPhase trigger)
    {
        if (trigger == ETriggerPhase.OnExecuted && __instance.alignment == EAlignment.Good)
        {
            LegionLoss.checkExe(__instance);
        }
    }
}
public static class LegionLoss
{
    public static WinConditions winConditions;
    public static GameObject legionLoss;
    public static void getWinCons()
    {
        GameObject winCon = GameObject.Find("Game/Gameplay/Content/WinConditions");
        winConditions = winCon.GetComponent<WinConditions>();
        System.Action<Character> action = new System.Action<Character>(checkExe);
        GameplayEvents.OnCharacterKilled += action;
        legionLoss = GameObject.Instantiate(winConditions.autoLose);
        GameObject legionLossNote = legionLoss.transform.FindChild("Note/Text (TMP)").gameObject;
        TextMeshProUGUI legionLossText = legionLossNote.GetComponent<TextMeshProUGUI>();
        legionLossText.text = "<color=red>Agmeres Wins</color>\n\nAll Good characters have died.";
    }
    public static void checkExe(Character ch)
    {
        UnityEngine.Debug.Log("Checking if Good should lose due to Agmeres...");
        bool goodLives = false;
        bool legionInPlay = false;
        foreach (Character character in Gameplay.CurrentCharacters)
        {
            if (character.alignment == EAlignment.Good && character.state != ECharacterState.Dead)
            {
                UnityEngine.Debug.Log(string.Format("Found an alive Good character at #{0}", character.id));
                goodLives = true;
            }
            if (character.dataRef.name.ToString() == "Agmeres")
            {
                legionInPlay = true;
            }
        }
        Health health = PlayerController.PlayerInfo.health;
        int healthCount = health.value.GetValue();
        bool noHealth = false;
        if (healthCount <= 0) noHealth = true;
        if (!goodLives && legionInPlay && !noHealth)
        {
            UnityEngine.Debug.Log("No Good characters live. Initiating Agmeres loss.");
            ch.RevealAllReal();
            getWinCons();
            legionLose();
        }
    }
    public static void legionLose()
    {
        legionLoss.SetActive(true);
        winConditions.Lose();
    }
}


