using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppSystem;
using MelonLoader;
using System;
using System.ComponentModel.Design;
using UnityEngine;
using HarmonyLib;

namespace WingidonExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Chatterbox : Role
{
    public CharacterData[] allDatas = Il2CppSystem.Array.Empty<CharacterData>();
    public Character chatterPoisonTarget = new Character();
    public w_Chatterbox() : base(ClassInjector.DerivedConstructorPointer<w_Chatterbox>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Chatterbox(System.IntPtr ptr) : base(ptr)
    {

    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Init)
        {
            // new wx_SavedScripts().DebugMessage($"Initialised {charRef.dataRef.characterName} at #{charRef.id}");
        }
        if (trigger == ETriggerPhase.Day)
        {
            chatterPoisonTarget = charRef;
            Il2CppSystem.Collections.Generic.List<Character> unrevealedCharacters = Characters.Instance.FilterHiddenCharacters(Gameplay.CurrentCharacters);
            unrevealedCharacters = Characters.Instance.FilterAliveCharacters(unrevealedCharacters);
            unrevealedCharacters.Remove(charRef);
            if (unrevealedCharacters.Count == 0)
            {
                onActed.Invoke(new ActedInfo("All characters have been revealed"));
                return;
            }
            unrevealedCharacters = Characters.Instance.FilterCharacterType(unrevealedCharacters, ECharacterType.Villager);
            unrevealedCharacters = Characters.Instance.FilterAlignmentCharacters(unrevealedCharacters, EAlignment.Good);
            unrevealedCharacters = Characters.Instance.FilterCharacterMissingStatus(unrevealedCharacters, ECharacterStatus.Corrupted);
            if (unrevealedCharacters.Count == 0)
            {
                onActed.Invoke(GetInfo(charRef));
                return;
            }
            Character poisonTarget = unrevealedCharacters[UnityEngine.Random.RandomRangeInt(0, unrevealedCharacters.Count)];
            chatterPoisonTarget = poisonTarget;
            poisonTarget.statuses.AddStatus(ECharacterStatus.Corrupted, charRef);
            if (poisonTarget.dataRef.characterId == "Copycat_WING") poisonTarget.statuses.statuses.Remove(ECharacterStatus.HealthyBluff);
            onActed.Invoke(GetInfo(charRef));
        }
    }
    public override ActedInfo GetInfo(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        string info = "Info";
        Il2CppSystem.Collections.Generic.List<Character> unrevealedCharacters = Characters.Instance.FilterHiddenCharacters(Gameplay.CurrentCharacters);
        unrevealedCharacters = Characters.Instance.FilterAliveCharacters(unrevealedCharacters);
        unrevealedCharacters.Remove(charRef);

        unrevealedCharacters.Remove(chatterPoisonTarget);
        for (int i = 0; i < 2; i++)
        {
            if (unrevealedCharacters.Count != 0)
            {
                Character charAdd = unrevealedCharacters[UnityEngine.Random.RandomRangeInt(0, unrevealedCharacters.Count)];
                selection.Add(charAdd);
                unrevealedCharacters.Remove(charAdd);
            }
        }
        if (chatterPoisonTarget && chatterPoisonTarget != charRef)
        {
            selection.Add(chatterPoisonTarget);
        }
        else
        {
            if (unrevealedCharacters.Count != 0) selection.Add(unrevealedCharacters[UnityEngine.Random.RandomRangeInt(0, unrevealedCharacters.Count)]);
        }
        wx_SavedScripts sharedScripts = new wx_SavedScripts();
        selection = sharedScripts.SortList(selection);

        if (selection.Count == 0)
        {
            info = "All characters have been revealed";
        }
        else if (selection.Count == 1)
        {
            info = string.Format("I spoke to #{0}", selection[0].id);
        }
        else
        {
            info = $"I spoke to {sharedScripts.MentionEveryCharacterInList(selection, "or")}";
        }
        return new ActedInfo(info, selection);
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        return new ActedInfo("");
    }
    public override void BluffAct(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Init)
        {
            // new wx_SavedScripts().DebugMessage($"Initialised {charRef.dataRef.characterName} at #{charRef.id}");
        }
        if (trigger != ETriggerPhase.Day) return;
        onActed.Invoke(GetInfo(charRef));
    }
    public string ConjourInfo()
    {
        return "";
    }

    public ActedInfo GetRandomNonsense()
    {
        Il2CppSystem.Collections.Generic.List<string> randomNonsense = new Il2CppSystem.Collections.Generic.List<string>();
        ActedInfo returnInfo = new ActedInfo("");
        return returnInfo;
    }

    public override CharacterData GetBluffIfAble(Character charRef)
    {
        return null;
    }
}


