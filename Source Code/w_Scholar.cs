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
public class w_Scholar : Role
{
    Character chRef;
    bool haveActed = false;
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
            return "Picks a character and Disguises as them";
        }
    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Init)
        {
            haveActed = false;
        }
        if (trigger != ETriggerPhase.Day || haveActed) return;
        chRef = charRef;
        CharacterPicker.Instance.StartPickCharacters(1, charRef);
        CharacterPicker.OnCharactersPicked += action1;
        CharacterPicker.OnStopPick += action2;
    }
    public override void BluffAct(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Init)
        {
            haveActed = false;
        }
        if (trigger != ETriggerPhase.Day || haveActed) return;
        chRef = charRef;
        CharacterPicker.Instance.StartPickCharacters(1, charRef);
        CharacterPicker.OnCharactersPicked += action3;
        CharacterPicker.OnStopPick += action2;
    }
    private void CharacterPicked()
    {
        CharacterPicker.OnCharactersPicked -= action1;
        CharacterPicker.OnStopPick -= action2;

        Il2CppSystem.Collections.Generic.List<Character> chars = new Il2CppSystem.Collections.Generic.List<Character>();
        chars.Add(CharacterPicker.PickedCharacters[0]);

        CharacterData myDisguise = chRef.dataRef;
        if (chars[0].bluff)
        {
            myDisguise = chars[0].bluff;
        }
        else
        {
            myDisguise = chars[0].dataRef;
        }
        if (myDisguise.startingAlignment == EAlignment.Evil) return;
        if (!myDisguise.bluffable) return;

        chRef.statuses.AddStatus(ECharacterStatus.HealthyBluff, charRef);
        OnActed(ETriggerPhase.Day, chRef, new ActedInfo("I am the Overseer"));
        chRef.GiveBluff(myDisguise);
        chRef.RevealBluff();
        haveActed = true;
        chRef.Act(ETriggerPhase.OnReveal);
        if (myDisguise.picking)
        {
            chRef.pickable.SetActive(true);
            chRef.pickableUses++;
        }
        else
        {
            chRef.Act(ETriggerPhase.Day);
        }
    }
    private void CharacterPickedLiar()
    {
        CharacterPicker.OnCharactersPicked -= action3;
        CharacterPicker.OnStopPick -= action2;

        Il2CppSystem.Collections.Generic.List<Character> chars = new Il2CppSystem.Collections.Generic.List<Character>();
        chars.Add(CharacterPicker.PickedCharacters[0]);

        CharacterData myDisguise = chRef.dataRef;
        if (chars[0].bluff)
        {
            myDisguise = chars[0].bluff;
        }
        else
        {
            myDisguise = chars[0].dataRef;
        }
        if (myDisguise.startingAlignment == EAlignment.Evil) return;

        chRef.statuses.statuses.Remove(ECharacterStatus.HealthyBluff);
        OnActed(ETriggerPhase.Day, chRef, new ActedInfo("I am the Overseer"));
        chRef.GiveBluff(myDisguise);
        chRef.RevealBluff();
        haveActed = true;
        chRef.Act(ETriggerPhase.OnReveal);
        if (myDisguise.picking)
        {
            chRef.pickable.SetActive(true);
            chRef.pickableUses++;
        }
        else
        {
            chRef.Act(ETriggerPhase.Day);
        }
    }
    private void StopPick()
    {
        CharacterPicker.OnCharactersPicked -= action1;
        CharacterPicker.OnStopPick -= action2;
        CharacterPicker.OnCharactersPicked -= action3;
    }
    public w_Scholar() : base(ClassInjector.DerivedConstructorPointer<w_Scholar>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
        action1 = new System.Action(CharacterPicked);
        action2 = new System.Action(StopPick);
        action3 = new System.Action(CharacterPickedLiar);
    }
    public w_Scholar(System.IntPtr ptr) : base(ptr)
    {
        action1 = new System.Action(CharacterPicked);
        action2 = new System.Action(StopPick);
        action3 = new System.Action(CharacterPickedLiar);
    }
    public override bool CheckIfCanBeKilled(Character charRef)
    {
        if (charRef.statuses.statuses.Contains(ECharacterStatus.HealthyBluff) && charRef.bluff)
            return charRef.bluff.role.CheckIfCanBeKilled(charRef);
        else
            return true;
    }
}