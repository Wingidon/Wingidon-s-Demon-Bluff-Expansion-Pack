using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppSystem;
using MelonLoader;
using System;
using System.ComponentModel.Design;
using UnityEngine;
using static MelonLoader.Modules.MelonModule;

namespace ExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_DevoutRework : Role
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
            return "Activates another character's ability again.";
        }
    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger != ETriggerPhase.Day) return;
        chRef = charRef;
        CharacterPicker.Instance.StartPickCharacters(1, charRef);
        CharacterPicker.OnCharactersPicked += action1;
        CharacterPicker.OnStopPick += action2;
    }
    public override void BluffAct(ETriggerPhase trigger, Character charRef)
    {
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
        string info = "info";
        // First, check if they're Revealed or not.
        Il2CppSystem.Collections.Generic.List<Character> revealedChars = Characters.Instance.FilterRevealedCharacters(Gameplay.CurrentCharacters);
        if (!revealedChars.Contains(pickedChars[0]))
        {
            //DenyInfo(chRef, "Unrevealed", pickedChars);
            //chRef.pickableUses = 1;
            return;
        }

        // Next, make them unable to choose themselves or other Devout claims
        /*
        if (pickedChars[0] = chRef)
        {
            DenyInfo(chRef, "Self", pickedChars);
            chRef.pickableUses = 1;
            return;
        }
        */
        if (pickedChars[0].GetCharacterBluffIfAble().characterId == "Devout_WING")
        {
            //DenyInfo(chRef, "Devout", pickedChars);
            //chRef.pickableUses = 1;
            return;
        }


        // Now, let's check if they're a Pick character,
        if (pickedChars[0].GetCharacterBluffIfAble().picking) // If they're a Pick role,
        {
            // Give them another use of their Pick ability.
            pickedChars[0].pickableUses += 1;
            pickedChars[0].pickable.SetActive(true);
        }
        else // If they aren't a Pick character,
        {
            // They act!
            pickedChars[0].Act(ETriggerPhase.OnReveal);
            pickedChars[0].Act(ETriggerPhase.Day);
        }

        info = $"I refreshed #{pickedChars[0].id}'s ability";
        chRef.ShowActed(new ActedInfo(info, pickedChars), ETriggerPhase.Day);
        Debug.Log($"{info}");
    }
    private void CharacterPickedLiar()
    {
        CharacterPicker.OnCharactersPicked -= action3;
        CharacterPicker.OnStopPick -= action2;

        Il2CppSystem.Collections.Generic.List<Character> pickedChars = new Il2CppSystem.Collections.Generic.List<Character>();
        pickedChars.Add(CharacterPicker.PickedCharacters[0]);
        string info = "info";
        // First, check if they're Revealed or not.
        Il2CppSystem.Collections.Generic.List<Character> revealedChars = Characters.Instance.FilterRevealedCharacters(Gameplay.CurrentCharacters);
        if (!revealedChars.Contains(pickedChars[0]))
        {
            //DenyInfo(chRef, "Unrevealed", pickedChars);
            //chRef.pickableUses += 1;
            return;
        }

        // Next, make them unable to choose themselves or other Devout claims
        /*
        if (pickedChars[0] = chRef)
        {
            DenyInfo(chRef, "Self", pickedChars);
            chRef.pickableUses = 1;
            return;
        }
        */
        if (pickedChars[0].GetCharacterBluffIfAble().characterId == "Devout_WING")
        {
            //DenyInfo(chRef, "Devout", pickedChars);
            //chRef.pickableUses += 1;
            return;
        }



        pickedChars[0].statuses.AddStatus(ECharacterStatus.Corrupted, chRef); // Corrupt them if Lying
        pickedChars[0].statuses.statuses.Remove(ECharacterStatus.HealthyBluff);
        
        // Now, let's check if they're a Pick character,
        if (pickedChars[0].GetCharacterBluffIfAble().picking) // If they're a Pick role,
        {
            // Give them another use of their Pick ability.
            pickedChars[0].pickableUses += 1;
            pickedChars[0].pickable.SetActive(true);
        }
        else // If they aren't a Pick character,
        {
            // They act!
            pickedChars[0].Act(ETriggerPhase.OnReveal);
            pickedChars[0].Act(ETriggerPhase.Day);
        }

        info = $"I refreshed #{pickedChars[0].id}'s ability";
        chRef.ShowActed(new ActedInfo(info, pickedChars), ETriggerPhase.Day);
    }
    private void StopPick()
    {
        CharacterPicker.OnCharactersPicked -= action1;
        CharacterPicker.OnStopPick -= action2;
        CharacterPicker.OnCharactersPicked -= action3;
    }
    public w_DevoutRework() : base(ClassInjector.DerivedConstructorPointer<w_DevoutRework>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
        action1 = new System.Action(CharacterPicked);
        action2 = new System.Action(StopPick);
        action3 = new System.Action(CharacterPickedLiar);
    }
    public w_DevoutRework(System.IntPtr ptr) : base(ptr)
    {
        action1 = new System.Action(CharacterPicked);
        action2 = new System.Action(StopPick);
        action3 = new System.Action(CharacterPickedLiar);
    }

    public static void DenyInfo(Character charRef, string reason, Il2CppSystem.Collections.Generic.List<Character> pickedChars)
    {
        string info = "Sorry, I can't do that";
        if (reason == "Unrevealed")
        {
            info = "I can't help you with unrevealed characters";
        }
        if (reason == "Self")
        {
            info = "Not sure what you're expecting me to do here";
        }
        if (reason == "Devout")
        {
            info = "I can't help them, sorry";
        }
        charRef.ShowActed(new ActedInfo(info, pickedChars), ETriggerPhase.Day);
    }
}