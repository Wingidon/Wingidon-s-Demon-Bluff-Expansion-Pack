using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppSystem;
using MelonLoader;
using System;
using System.ComponentModel.Design;
using UnityEngine;

namespace WingidonExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Matchmaker : Role
{
    Character chRef;
    private Il2CppSystem.Action action1;
    private Il2CppSystem.Action action2;
    private Il2CppSystem.Action action3;
    public override ActedInfo GetInfo(Character charRef)
    {
        return new ActedInfo("", null);
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        return new ActedInfo("", null);
    }
    public override string Description
    {
        get
        {
            return "Learns if a character is a Villager.";
        }
    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Init)
        {
            // new wx_SavedScripts().DebugMessage($"Initialised {charRef.dataRef.characterName} at #{charRef.id}");
        }
        if (trigger != ETriggerPhase.Day) return;
        chRef = charRef;
        CharacterPicker.Instance.StartPickCharacters(2, charRef);
        CharacterPicker.OnCharactersPicked += action1;
        CharacterPicker.OnStopPick += action2;
    }
    public override void BluffAct(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Init)
        {
            // new wx_SavedScripts().DebugMessage($"Initialised {charRef.dataRef.characterName} at #{charRef.id}");
        }
        if (trigger != ETriggerPhase.Day) return;
        chRef = charRef;
        CharacterPicker.Instance.StartPickCharacters(2, charRef);
        CharacterPicker.OnCharactersPicked += action3;
        CharacterPicker.OnStopPick += action2;
    }
    private void CharacterPicked()
    {
        CharacterPicker.OnCharactersPicked -= action1;
        CharacterPicker.OnStopPick -= action2;

        Il2CppSystem.Collections.Generic.List<Character> chars = new Il2CppSystem.Collections.Generic.List<Character>();
        chars.Add(CharacterPicker.PickedCharacters[0]);
        chars.Add(CharacterPicker.PickedCharacters[1]);

        /*
        Can detect characters that share...
        - Priority 1: Roles
          - Role
        - Priority 2: Alignment & Trust
          - Alignment
          - Type
          - Honesty
          - Truthfulness
        - Priority 3: Status
          - Corruption
          - Misregistration
          - Confusion (CSK's Mod)
         */

        Il2CppSystem.Collections.Generic.List<string> sharedEffects = new Il2CppSystem.Collections.Generic.List<string>();
        // Priority 1: Roles
        if (chars[0].GetRegisterAs().characterId == chars[1].GetRegisterAs().characterId) sharedEffects.Add("their Role");

        // Priority 2: Trust
        if (sharedEffects.Count == 0)
        {
            if (chars[0].GetRegisterAlignment() == chars[1].GetRegisterAlignment()) sharedEffects.Add("their Alignment");
            if (chars[0].GetRegisterAs().type == chars[1].GetRegisterAs().type) sharedEffects.Add("their Type");
            if (CharacterHelper.CheckLyingAppearance(chars[0]) == CharacterHelper.CheckLyingAppearance(chars[1])) sharedEffects.Add("their Truthfulness");
            if (CharacterHelper.CheckIfDisguisedAppearance(chars[0]) == CharacterHelper.CheckIfDisguisedAppearance(chars[1])) sharedEffects.Add("their Honesty");
        }

        // Priority 3: Status
        if (sharedEffects.Count == 0)
        {
            if (chars[0].statuses.Contains(ECharacterStatus.Corrupted) == chars[1].statuses.Contains(ECharacterStatus.Corrupted)) sharedEffects.Add("their Purity");
            if ((chars[0].dataRef != chars[0].GetRegisterAs()) && (chars[1].dataRef != chars[1].GetRegisterAs())) sharedEffects.Add("their ability to Register falsely");
        }

        if (sharedEffects.Count == 0) sharedEffects.Add("nothing with each other!");

        string chosenShare = sharedEffects[UnityEngine.Random.RandomRangeInt(0, sharedEffects.Count)];

        wx_SavedScripts sharedScripts = new wx_SavedScripts();
        chars = sharedScripts.SortList(chars);
        string info = $"#{chars[0].id} and #{chars[1].id} share {chosenShare}";


        onActed?.Invoke(new ActedInfo(info, chars));
        Debug.Log($"{info}");
    }
    private void CharacterPickedLiar()
    {
        CharacterPicker.OnCharactersPicked -= action3;
        CharacterPicker.OnStopPick -= action2;

        Il2CppSystem.Collections.Generic.List<Character> chars = new Il2CppSystem.Collections.Generic.List<Character>();
        chars.Add(CharacterPicker.PickedCharacters[0]);
        chars.Add(CharacterPicker.PickedCharacters[1]);

        /*
        Can detect characters that share...
        - Priority 1: Roles
          - Role
        - Priority 2: Alignment & Trust
          - Alignment
          - Type
          - Honesty
          - Truthfulness
        - Priority 3: Status
          - Corruption
          - Misregistration
          - Confusion (CSK's Mod)
         */


        Il2CppSystem.Collections.Generic.List<string> sharedEffects = new Il2CppSystem.Collections.Generic.List<string>();
        // Priority 1: Roles
        // if (chars[0].GetRegisterAs().characterId == chars[1].GetRegisterAs().characterId) sharedEffects.Add("their Role"); Role is far too obvious of a Lie

        // Priority 2: Trust
        if (sharedEffects.Count == 0)
        {
            if (chars[0].GetRegisterAlignment() != chars[1].GetRegisterAlignment()) sharedEffects.Add("their Alignment");
            if (chars[0].GetRegisterAs().type != chars[1].GetRegisterAs().type) sharedEffects.Add("their Type");
            if (CharacterHelper.CheckLyingAppearance(chars[0]) != CharacterHelper.CheckLyingAppearance(chars[1])) sharedEffects.Add("their Truthfulness");
            if (CharacterHelper.CheckIfDisguisedAppearance(chars[0]) != CharacterHelper.CheckIfDisguisedAppearance(chars[1])) sharedEffects.Add("their Honesty");
        }

        // Priority 3: Status
        if (sharedEffects.Count == 0)
        {
            if (chars[0].statuses.Contains(ECharacterStatus.Corrupted) != chars[1].statuses.Contains(ECharacterStatus.Corrupted)) sharedEffects.Add("their Purity");
            // if ((chars[0].dataRef != chars[0].GetRegisterAs()) && (chars[1].dataRef != chars[1].GetRegisterAs())) sharedEffects.Add("their ability to Register falsely"); This will pretty much always be wrong.
        }

        if (sharedEffects.Count == 0) sharedEffects.Add("nothing with each other!");

        string chosenShare = sharedEffects[UnityEngine.Random.RandomRangeInt(0, sharedEffects.Count)];

        wx_SavedScripts sharedScripts = new wx_SavedScripts();
        chars = sharedScripts.SortList(chars);
        string info = $"#{chars[0].id} and #{chars[1].id} share {chosenShare}";


        onActed?.Invoke(new ActedInfo(info, chars));
        Debug.Log($"{info}");
    }
    private void StopPick()
    {
        CharacterPicker.OnCharactersPicked -= action1;
        CharacterPicker.OnStopPick -= action2;
        CharacterPicker.OnCharactersPicked -= action3;
    }
    public w_Matchmaker() : base(ClassInjector.DerivedConstructorPointer<w_Matchmaker>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
        action1 = new System.Action(CharacterPicked);
        action2 = new System.Action(StopPick);
        action3 = new System.Action(CharacterPickedLiar);
    }
    public w_Matchmaker(System.IntPtr ptr) : base(ptr)
    {
        action1 = new System.Action(CharacterPicked);
        action2 = new System.Action(StopPick);
        action3 = new System.Action(CharacterPickedLiar);
    }
}