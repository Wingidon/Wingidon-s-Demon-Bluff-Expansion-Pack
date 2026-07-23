using HarmonyLib;
using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppSystem;
using MelonLoader;
using System;
using System.ComponentModel.Design;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace WingidonExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Masquerade : Role
{
    public w_Masquerade() : base(ClassInjector.DerivedConstructorPointer<w_Masquerade>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Masquerade(System.IntPtr ptr) : base(ptr)
    {

    }
    public override CharacterData GetBluffIfAble(Character charRef)
    {
        wx_SavedScripts sharedScripts = new wx_SavedScripts();
        if (sharedScripts.PercentChance(50)) return sharedScripts.GetOverrideDuplicateBluff(charRef);
        else
            return sharedScripts.GetOverrideNotInPlayBluff(charRef, true);
    }
    public string ConjourInfo()
    {
        return "";
    }


    public override int GetDamageToYou()
    {
        return 2;
    }

    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Init)
        {
            new wx_SavedScripts().DebugMessage($"Masquerade initialised in seat {charRef.id}");
        }
    }
    public override void BluffAct(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Init)
        {
            new wx_SavedScripts().DebugMessage($"Masquerade BluffAct-initialised in seat {charRef.id}");
        }
    }

    public override ActedInfo GetInfo(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<Character> evilChars = Characters.Instance.FilterAlignmentCharacters(Gameplay.CurrentCharacters, EAlignment.Evil);
        if (evilChars.Count == 0) return new ActedInfo("You got me!\n\nSomething does not make sense");
        Il2CppSystem.Collections.Generic.List<Character> selection = new();
        selection.Add(evilChars[UnityEngine.Random.RandomRangeInt(0, evilChars.Count)]);
        return new ActedInfo($"You got me!\n\n#{selection[0].id} is Evil", selection);
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        return new ActedInfo($"You got me!\n\nI am dizzy");
    }
    public override bool CheckIfCanBeKilled(Character charRef)
    {
        if (charRef.alignment == EAlignment.Evil) return true;
        wx_SavedScripts sharedScripts = new();
        if (charRef.statuses.Contains(ECharacterStatus.Corrupted))
        {
            charRef.actedInfos.Add(new ActedInfo($"I am dizzy"));
            charRef.ShowActed(charRef.actedInfos[charRef.actedInfos.Count - 1], ETriggerPhase.Day);
            return true;
        }
        if (charRef.statuses.Contains(MasqKill.spentMasquerade)) return false;
        charRef.statuses.AddStatus(MasqKill.spentMasquerade, charRef);
        charRef.Reveal();
        charRef.RevealAllReal();
        sharedScripts.DebugMessage($"Masquerade at #{charRef.id} activating.");
        Il2CppSystem.Collections.Generic.List<Character> evilChars = new();
        Il2CppSystem.Collections.Generic.List<Character> lastStandChars = new();
        Il2CppSystem.Collections.Generic.List<Character> lastStandPunisherChars = new();
        Il2CppSystem.Collections.Generic.List<string> lastStandIDs = sharedScripts.GetLastStandIDs();
        Il2CppSystem.Collections.Generic.List<string> lastStandPunisherIDs = sharedScripts.GetLastStandPunisherIDs(false);
        foreach (Character character in Characters.Instance.FilterAliveCharacters(Gameplay.CurrentCharacters))
        {
            if (character.GetRegisterAlignment() == EAlignment.Evil)
            {
                if (lastStandIDs.Contains(character.dataRef.characterId)) lastStandChars.Add(character);
                else if (lastStandPunisherIDs.Contains(character.dataRef.characterId)) lastStandPunisherChars.Add(character);
                else evilChars.Add(character);
            }
        }
        if (evilChars.Count != 0)
        {
            Character target = sharedScripts.GetRandomItemOfList(evilChars);
            sharedScripts.DebugMessage($"Attacked #{target.id}!");
            target.statuses.AddStatus(MasqKill.masqueradeKill, charRef);
            target.Reveal();
            target.onReveal.Invoke();
            target.RevealReal();
            target.RevealAllReal();
            target.KillByDemon(charRef);
            charRef.actedInfos.Add(sharedScripts.ReturnInfoWithSingleSelection($"{GetGotString()}\n\n{GetKillString(target)}", target));
            charRef.ShowActed(charRef.actedInfos[charRef.actedInfos.Count - 1], ETriggerPhase.Day);
        }
        else
        {
            if (lastStandPunisherChars.Count == 0)
            {
                if (lastStandChars.Count == 0)
                {
                    sharedScripts.DebugMessage("Couldn't find anyone Evil!");
                    charRef.actedInfos.Add(new ActedInfo($"{GetGotString()}\n\nThis village is too Good to find anyone Evil!"));
                    charRef.ShowActed(charRef.actedInfos[charRef.actedInfos.Count - 1], ETriggerPhase.Day);
                }
                else
                {
                    Character target = sharedScripts.GetRandomItemOfList(lastStandChars);
                    sharedScripts.DebugMessage($"Attacked #{target.id}!");
                    target.statuses.AddStatus(MasqKill.masqueradeKill, charRef);
                    target.Reveal();
                    target.onReveal.Invoke();
                    target.RevealReal();
                    target.RevealAllReal();
                    target.KillByDemon(charRef);
                    charRef.actedInfos.Add(sharedScripts.ReturnInfoWithSingleSelection($"{GetGotString()}\n\n{GetKillString(target)}", target));
                    charRef.ShowActed(charRef.actedInfos[charRef.actedInfos.Count - 1], ETriggerPhase.Day);
                }
            }
            else
            {
                Character target = sharedScripts.GetRandomItemOfList(lastStandPunisherChars);
                sharedScripts.DebugMessage($"Attacked #{target.id}!");
                target.statuses.AddStatus(MasqKill.masqueradeKill, charRef);
                target.Reveal();
                target.onReveal.Invoke();
                target.RevealReal();
                target.RevealAllReal();
                target.KillByDemon(charRef);
                charRef.actedInfos.Add(sharedScripts.ReturnInfoWithSingleSelection($"{GetGotString()}\n\n{GetKillString(target)}", target));
                charRef.ShowActed(charRef.actedInfos[charRef.actedInfos.Count - 1], ETriggerPhase.Day);
            }
        }
        return false;
    }


    public static string GetGotString()
    {
        Il2CppSystem.Collections.Generic.List<string> returnStrings = new();
        returnStrings.Add("You got me!");
        returnStrings.Add("Alright, fine, you win");
        returnStrings.Add("Ah damnit");
        returnStrings.Add("Well played!");
        returnStrings.Add("Alright, sure!");
        returnStrings.Add("It's go-time!");
        returnStrings.Add("Ready for this?");
        returnStrings.Add("Nice shot!");
        return returnStrings[UnityEngine.Random.RandomRangeInt(0, returnStrings.Count)];
    }
    public static string GetKillString(Character target)
    {
        Il2CppSystem.Collections.Generic.List<string> returnStrings = new();
        returnStrings.Add($"I shot #{target.id}");
        returnStrings.Add($"I stabbed #{target.id}");
        returnStrings.Add($"I struck #{target.id}");
        returnStrings.Add($"I attacked #{target.id}");
        returnStrings.Add($"I killed Evil at #{target.id}");
        returnStrings.Add($"I slammed #{target.id}");
        returnStrings.Add($"I whacked #{target.id}");
        return returnStrings[UnityEngine.Random.RandomRangeInt(0, returnStrings.Count)];
    }


    public static class MasqKill
    {
        public static ECharacterStatus spentMasquerade = (ECharacterStatus)1916514201;
        public static ECharacterStatus masqueradeKill = (ECharacterStatus)1916514202;
        [HarmonyPatch(typeof(Character), nameof(Character.ShowDescription))]
        public static class ChangeKillByDemonText
        {
            public static void Postfix(Character __instance)
            {
                if (__instance.killedByDemon && __instance.statuses.Contains(masqueradeKill))
                {
                    HintInfo info = new HintInfo();
                    info.text = "Killed by the <color=#C080FF>Masquerade</color>!";
                    UIEvents.OnShowHint.Invoke(info, __instance.hintPivot);
                }
            }
        }
    }
}


