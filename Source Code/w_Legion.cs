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

namespace WingidonExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Legion : Role
{
    public override Il2CppSystem.Collections.Generic.List<SpecialRule> GetRules()
    {
        Il2CppSystem.Collections.Generic.List<SpecialRule> sr = new Il2CppSystem.Collections.Generic.List<SpecialRule>();
        sr.Add(new NightModeRule(4));
        return sr;
    }
    Il2CppSystem.Collections.Generic.List<string> GetJinxedIDs()
    {
        Il2CppSystem.Collections.Generic.List<string> jinxedIDs = new Il2CppSystem.Collections.Generic.List<string>();
        jinxedIDs.Add("Bishop_58855542");
        jinxedIDs.Add("Empress_13782227");

        jinxedIDs.Add("Confectioner_scm");
        jinxedIDs.Add("Captivator_scm");
        jinxedIDs.Add("Hypnotist_scm");
        jinxedIDs.Add("Chatterbox_WING");
        jinxedIDs.Add("Marionette_WING");
        jinxedIDs.Add("Mutant_WING");
        jinxedIDs.Add("Renegade_WING");
        jinxedIDs.Add("Switchblade_WING");
        jinxedIDs.Add("Tergiversator_WING");
        jinxedIDs.Add("Wretch_80988916");

        jinxedIDs.Add("Baron_04539999");
        jinxedIDs.Add("Mezepheles_09511163");
        jinxedIDs.Add("Cryptid_WING");
        jinxedIDs.Add("Ritualist_WING");
        jinxedIDs.Add("Saboteur_WING");
        jinxedIDs.Add("Snake Charmer_WING");
        jinxedIDs.Add("Swarm_Good_WING");
        jinxedIDs.Add("Undying_WING");

        return jinxedIDs;
    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Init)
        {
            // new wx_SavedScripts().DebugMessage($"Initialised {charRef.dataRef.characterName} at #{charRef.id}");
        }
        if (trigger == ETriggerPhase.Start)
        {
            wx_SavedScripts sharedScripts = new wx_SavedScripts();
            Il2CppSystem.Collections.Generic.List<CharacterData> underlingRoles = sharedScripts.GetUnderlingDatas(charRef);
            Gameplay.Instance.AddScriptCharacterIfAble(underlingRoles[0].type, underlingRoles[0]);
            Gameplay.Instance.AddScriptCharacterIfAble(underlingRoles[1].type, underlingRoles[1]);
            Gameplay.Instance.AddScriptCharacterIfAble(underlingRoles[2].type, underlingRoles[2]);
            Il2CppSystem.Collections.Generic.List<string> jinxedIDs = GetJinxedIDs();

            foreach (Character character in Gameplay.CurrentCharacters)
            {
                if (jinxedIDs.Contains(character.dataRef.characterId))
                {
                    sharedScripts.DebugMessage($"Agmeres found {character.dataRef.characterName} at #{character.id}, replacing with underling");
                    int underlingID = 0;
                    if (character.dataRef.type == ECharacterType.Villager) underlingID = 0;
                    if (character.dataRef.type == ECharacterType.Outcast) underlingID = 1;
                    if (character.dataRef.type == ECharacterType.Minion) underlingID = 2;
                    character.Init(underlingRoles[underlingID]);
                }
            }
        }
        if (trigger == ETriggerPhase.Night)
        {
            Il2CppSystem.Collections.Generic.List<Character> aliveGoods = new Il2CppSystem.Collections.Generic.List<Character>();
            aliveGoods = Characters.Instance.FilterAliveCharacters(Gameplay.CurrentCharacters);
            aliveGoods = Characters.Instance.FilterRealAlignmentCharacters(aliveGoods, EAlignment.Good);
            Health health = PlayerController.PlayerInfo.health;
            LegionLoss.checkExe(charRef);
            if (charRef.state == ECharacterState.Dead) return;
            //health.Damage(2);
            health.AddMaxHp(-2);
        }
        if (trigger == ETriggerPhase.AfterRoundStart)
        {
            Il2CppSystem.Collections.Generic.List<Character> aliveGoods = new Il2CppSystem.Collections.Generic.List<Character>();
            aliveGoods = Characters.Instance.FilterAliveCharacters(Gameplay.CurrentCharacters);
            aliveGoods = Characters.Instance.FilterRealAlignmentCharacters(aliveGoods, EAlignment.Good);
            Health health = PlayerController.PlayerInfo.health;
            //health.Damage(2);
            health.AddMaxHp(-2);
            wx_SavedScripts sharedScripts = new wx_SavedScripts();
            Il2CppSystem.Collections.Generic.List<CharacterData> underlingRoles = sharedScripts.GetUnderlingDatas(charRef);
            Gameplay.Instance.AddScriptCharacterIfAble(underlingRoles[0].type, underlingRoles[0]);
            Gameplay.Instance.AddScriptCharacterIfAble(underlingRoles[1].type, underlingRoles[1]);
            Gameplay.Instance.AddScriptCharacterIfAble(underlingRoles[2].type, underlingRoles[2]);
            foreach (Character character in Gameplay.CurrentCharacters)
            {
                if (character.bluff)
                {
                    if (GetJinxedIDs().Contains(character.bluff.characterId))
                    {
                        int underlingID = 0;
                        if (character.bluff.type == ECharacterType.Outcast) underlingID = 1;
                        sharedScripts.DebugMessage($"Agmeres discovered that #{character.id} has an invalid bluff of {character.bluff.characterName}, replacing with {underlingRoles[underlingID].characterName}");
                        character.GiveBluff(underlingRoles[underlingID]);
                    }
                }
            }
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
        bool goodDies = false;
        bool legionInPlay = false;
        foreach (Character character in Gameplay.CurrentCharacters)
        {
            if (character.alignment == EAlignment.Good && character.state != ECharacterState.Dead)
            {
                UnityEngine.Debug.Log(string.Format("Found an alive Good character at #{0}", character.id));
                goodLives = true;
            }
            if (character.alignment == EAlignment.Good && character.state == ECharacterState.Dead)
            {
                goodDies = true;
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
        if (!goodLives && legionInPlay && !noHealth && goodDies)
        {
            UnityEngine.Debug.Log("No Good characters live. Initiating Agmeres loss.");
            health.Damage(999999);
            //ch.RevealAllReal();
            //getWinCons();
            //legionLose();
        }
    }
    public static void legionLose()
    {
        legionLoss.SetActive(true);
        winConditions.Lose();
    }
}


