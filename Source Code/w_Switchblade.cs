using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppSystem;
using MelonLoader;
using System;
using System.ComponentModel.Design;
using UnityEngine;
using HarmonyLib;

namespace ExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Switchblade : Role
{
    int alignmentTimer = 0;
    bool haveKilled = false;
    bool haveActed = false;
    int cycleLength = 3;
    Character killVictim = null;
    public w_Switchblade() : base(ClassInjector.DerivedConstructorPointer<w_Switchblade>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Switchblade(System.IntPtr ptr) : base(ptr)
    {

    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Init)
        {
            alignmentTimer = 0;
            haveKilled = false;
            haveActed = false;
            killVictim = charRef;
            cycleLength = 3;
            // new wx_SavedScripts().DebugMessage($"Initialised {charRef.dataRef.characterName} at #{charRef.id}");
        }
        if (trigger == wx_SavedScripts.w_AnyRevealPatch.AnyReveal)
        {
            if (charRef.GetState() == ECharacterState.Dead) return;
            alignmentTimer++;
            if (alignmentTimer < cycleLength) return;
            alignmentTimer -= cycleLength;
            wx_SavedScripts sharedScripts = new wx_SavedScripts();
            Il2CppSystem.Collections.Generic.List<Character> aliveNeighbours = Characters.Instance.GetAdjacentAliveCharacters(charRef);
            if (aliveNeighbours.Count != 2)
            {
                sharedScripts.DebugMessage("Switchblade found less than 2 alive neighbours, not acting");
                return;
            }
            sharedScripts.DebugMessage($"Switchblade preparing to act, found #{aliveNeighbours[0].id} and #{aliveNeighbours[1].id} as alive neighbours");

            Character chosenNeighbour = aliveNeighbours[UnityEngine.Random.RandomRangeInt(0, aliveNeighbours.Count)];
            charRef.statuses.AddStatus(ECharacterStatus.HealthyBluff, charRef);
            charRef.ChangeAlignment(chosenNeighbour.GetRegisterAlignment());

            if (charRef.alignment == EAlignment.Evil)
            {
                if (!charRef.statuses.Contains(TergiStatus.w_tergiEvil)) charRef.statuses.AddStatus(TergiStatus.w_tergiEvil, charRef);
                charRef.statuses.statuses.Remove(TergiStatus.w_tergiGood);
            }
            else
            {
                if (!charRef.statuses.Contains(TergiStatus.w_tergiGood)) charRef.statuses.AddStatus(TergiStatus.w_tergiGood, charRef);
                charRef.statuses.statuses.Remove(TergiStatus.w_tergiEvil);
            }
            CheckGameEnd(charRef);

            Il2CppSystem.Collections.Generic.List<Character> validKillTargets = new Il2CppSystem.Collections.Generic.List<Character>();
            if (aliveNeighbours[0].GetRegisterAlignment() == EAlignment.Good && aliveNeighbours[0].GetRealAlignment() == EAlignment.Good) validKillTargets.Add(aliveNeighbours[0]);
            if (aliveNeighbours[1].GetRegisterAlignment() == EAlignment.Good && aliveNeighbours[1].GetRealAlignment() == EAlignment.Good) validKillTargets.Add(aliveNeighbours[1]);
            if (validKillTargets.Count == 0 || haveKilled) return;
            Character killTarget = validKillTargets[UnityEngine.Random.RandomRangeInt(0, validKillTargets.Count)];
            if (sharedScripts.PercentChance(36))
            {
                Health health = PlayerController.PlayerInfo.health;
                health.Damage(1);
                killVictim = killTarget;
                haveKilled = true;
                killTarget.pickable.SetActive(false);
                killTarget.pickableUses = 0;
                killTarget.Reveal();
                killTarget.onReveal.Invoke();
                killTarget.RevealReal();
                killTarget.RevealAllReal();
                killTarget.statuses.AddStatus(ECharacterStatus.KilledByEvil, charRef);
                killTarget.statuses.AddStatus(SwitchbladeKill.switchbladeKill, charRef);
                killTarget.KillByDemon(charRef);
                if (haveActed) charRef.Act(ETriggerPhase.Day);
            }
        }
        if (trigger == ETriggerPhase.Day)
        {
            haveActed = true;
            if (!haveKilled) OnActed(ETriggerPhase.Day, charRef, GetOneLiner());
            else OnActed(ETriggerPhase.Day, charRef, GetTargetedOneLiner(killVictim));
        }
    }
    public override ActedInfo GetInfo(Character charRef)
    {

        return GetOneLiner();
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        return GetOneLiner();
    }
    public override void BluffAct(ETriggerPhase trigger, Character charRef)
    {
        Act(trigger, charRef);
    }
    public string ConjourInfo()
    {
        return "";
    }


    public void CheckGameEnd(Character charRef)
    {
        bool gameEnd = true;
        foreach (Character character in Gameplay.CurrentCharacters)
        {
            if (character.alignment == EAlignment.Evil && character.GetState() != ECharacterState.Dead)
            {
                gameEnd = false;
                break;
            }
        }
        if (gameEnd)
        {
            charRef.RevealReal();
            charRef.KillByDemon(charRef);
        }
    }

    public ActedInfo GetOneLiner()
    {
        Il2CppSystem.Collections.Generic.List<string> oneLiner = new Il2CppSystem.Collections.Generic.List<string>();
        oneLiner.Add("Sharp as a blade...");
        oneLiner.Add("Let's switch it up a little");
        oneLiner.Add("Let's turn it up a notch");
        oneLiner.Add("Bring it on");
        oneLiner.Add("You want some?");
        oneLiner.Add("Dark as the night and swift as the wind");
        oneLiner.Add("You wouldn't dare");
        ActedInfo returnInfo = new ActedInfo(oneLiner[UnityEngine.Random.RandomRangeInt(0, oneLiner.Count)]);
        return returnInfo;
    }

    public ActedInfo GetTargetedOneLiner(Character target)
    {
        Il2CppSystem.Collections.Generic.List<string> oneLiner = new Il2CppSystem.Collections.Generic.List<string>();
        oneLiner.Add($"Bad decision, #{target.id}");
        oneLiner.Add($"You wouldn't dare, #{target.id}");
        oneLiner.Add($"#{target.id} needs to stop getting in my way");
        oneLiner.Add($"#{target.id} was in my way");
        oneLiner.Add($"#{target.id} shut up, good lord!");
        oneLiner.Add($"I did warn you, #{target.id}");
        oneLiner.Add($"For hell's sake, shut up, #{target.id}!");
        oneLiner.Add($"Hey #{target.id}? Do me a favour and quiet down, yeah?");
        oneLiner.Add($"You're really getting on my nerves, #{target.id}");
        oneLiner.Add($"I killed Good at #{target.id}");
        oneLiner.Add($"#{target.id} deserved that, by the way");
        ActedInfo returnInfo = new ActedInfo(oneLiner[UnityEngine.Random.RandomRangeInt(0, oneLiner.Count)]);
        return returnInfo;
    }

    public override CharacterData GetBluffIfAble(Character charRef)
    {
        return null;
    }
}
public static class SwitchbladeKill
{
    public static ECharacterStatus switchbladeKill = (ECharacterStatus)1923920382;
    [HarmonyPatch(typeof(Character), nameof(Character.ShowDescription))]
    public static class ChangeKillByDemonText
    {
        public static void Postfix(Character __instance)
        {
            if (__instance.killedByDemon && __instance.statuses.Contains(switchbladeKill))
            {
                HintInfo info = new HintInfo();
                info.text = "Killed by the <color=#99FF99>Switch</color><color=#FF9999>blade</color>\nCannot use abilities.";
                UIEvents.OnShowHint.Invoke(info, __instance.hintPivot);
            }
        }
    }
}


