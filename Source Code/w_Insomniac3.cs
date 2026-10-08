using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppSystem;
using MelonLoader;
using System;
using System.ComponentModel.Design;
using UnityEngine;
using static MelonLoader.Modules.MelonModule;

namespace WingidonExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Insomniac3 : Role
{
    Character chRef;
    private Il2CppSystem.Action action1;
    private Il2CppSystem.Action action2;
    private Il2CppSystem.Action action3;
    bool haveActed = false;
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
            return "Starts revealed. Activates an unrevealed character's ability.";
        }
    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.AfterRoundStart)
        {
            if (haveActed) return;
            haveActed = true;
            charRef.Reveal();
            charRef.onReveal.Invoke();
            charRef.ChangeState(ECharacterState.Alive);
            charRef.pickable.SetActive(true);
            charRef.pickableUses = 1;
        }
        if (trigger != ETriggerPhase.Day) return;
        chRef = charRef;
        CharacterPicker.Instance.StartPickCharacters(1, charRef);
        CharacterPicker.OnCharactersPicked += action1;
        CharacterPicker.OnStopPick += action2;
    }
    public override void BluffAct(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.AfterRoundStart)
        {
            if (haveActed) return;
            haveActed = true;
            charRef.Reveal();
            charRef.onReveal.Invoke();
            charRef.ChangeState(ECharacterState.Alive);
            charRef.pickable.SetActive(true);
            charRef.pickableUses = 1;
        }
        if (trigger != ETriggerPhase.Day) return;
        chRef = charRef;
        CharacterPicker.Instance.StartPickCharacters(1, charRef);
        CharacterPicker.OnCharactersPicked += action3;
        CharacterPicker.OnStopPick += action2;
    }
    private void CharacterPicked()
    {
        CharacterPicker.OnCharactersPicked -= action1;
        CharacterPicker.OnStopPick -= action2;

        Il2CppSystem.Collections.Generic.List<Character> pickedChars = new Il2CppSystem.Collections.Generic.List<Character>();
        pickedChars.Add(CharacterPicker.PickedCharacters[0]);

        if (Characters.Instance.FilterAliveCharacters(Gameplay.CurrentCharacters).Contains(pickedChars[0]))
        {
            if (Characters.Instance.FilterHiddenCharacters(Gameplay.CurrentCharacters).Contains(pickedChars[0]))
            {
                pickedChars[0].Reveal();
                pickedChars[0].onReveal.Invoke();
                pickedChars[0].ChangeState(ECharacterState.Alive);
            }
            wx_SavedScripts sharedScripts = new();
            if (!pickedChars[0].GetCharacterBluffIfAble().picking)
            {
                pickedChars[0].Act(ETriggerPhase.Day);
            }
            else
            {
                pickedChars[0].pickable.SetActive(true);
                pickedChars[0].pickableUses = 1;
            }
            OnActed(ETriggerPhase.Day, chRef, sharedScripts.ReturnInfoWithSingleSelection($"I activated #{pickedChars[0].id}", pickedChars[0]));
        }
        else return;
    }
    private void CharacterPickedLiar()
    {
        CharacterPicker.OnCharactersPicked -= action3;
        CharacterPicker.OnStopPick -= action2;

        Il2CppSystem.Collections.Generic.List<Character> pickedChars = new Il2CppSystem.Collections.Generic.List<Character>();
        pickedChars.Add(CharacterPicker.PickedCharacters[0]);

        if (Characters.Instance.FilterAliveCharacters(Gameplay.CurrentCharacters).Contains(pickedChars[0]))
        {
            if (pickedChars[0].GetRegisterAlignment() == EAlignment.Good)
            {
                pickedChars[0].statuses.AddStatus(ECharacterStatus.Corrupted, chRef);
                if (pickedChars[0].statuses.Contains(ECharacterStatus.Corrupted)) pickedChars[0].statuses.statuses.Remove(ECharacterStatus.HealthyBluff);
            }
            if (Characters.Instance.FilterHiddenCharacters(Gameplay.CurrentCharacters).Contains(pickedChars[0]))
            {
                pickedChars[0].Reveal();
                pickedChars[0].onReveal.Invoke();
                pickedChars[0].ChangeState(ECharacterState.Alive);
            }
            pickedChars[0].Act(wx_SavedScripts.w_AnyRevealPatch.SelfReveal);
            if (!pickedChars[0].GetCharacterBluffIfAble().picking)
            {
                pickedChars[0].Act(ETriggerPhase.Day);
            }
            else
            {
                pickedChars[0].pickable.SetActive(true);
                pickedChars[0].pickableUses += 1;
            }
            OnActed(ETriggerPhase.Day, chRef, new wx_SavedScripts().ReturnInfoWithSingleSelection($"I activated #{pickedChars[0].id}", pickedChars[0]));
        }
        else return;
    }
    private void StopPick()
    {
        CharacterPicker.OnCharactersPicked -= action1;
        CharacterPicker.OnStopPick -= action2;
        CharacterPicker.OnCharactersPicked -= action3;
    }
    public w_Insomniac3() : base(ClassInjector.DerivedConstructorPointer<w_Insomniac3>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
        action1 = new System.Action(CharacterPicked);
        action2 = new System.Action(StopPick);
        action3 = new System.Action(CharacterPickedLiar);
    }
    public w_Insomniac3(System.IntPtr ptr) : base(ptr)
    {
        action1 = new System.Action(CharacterPicked);
        action2 = new System.Action(StopPick);
        action3 = new System.Action(CharacterPickedLiar);
    }
}