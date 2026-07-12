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
public class w_WardenNew : Role
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
        CharacterPicker.Instance.StartPickCharacters(4, charRef);
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
        CharacterPicker.Instance.StartPickCharacters(4, charRef);
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
        chars.Add(CharacterPicker.PickedCharacters[2]);
        chars.Add(CharacterPicker.PickedCharacters[3]);

        bool villPicked = false;
        bool outsPicked = false;
        bool minPicked = false;
        bool demPicked = false;


        wx_SavedScripts sharedScripts = new wx_SavedScripts();
        Il2CppSystem.Collections.Generic.List<ECharacterType> types = new Il2CppSystem.Collections.Generic.List<ECharacterType>();
        foreach (Character character in chars)
        {
            if (character.GetRegisterAs().type == ECharacterType.Villager)
            {
                villPicked = true;
            }
            if (character.GetRegisterAs().type == ECharacterType.Outcast)
            {
                outsPicked = true;
            }
            if (character.GetRegisterAs().type == ECharacterType.Minion)
            {
                minPicked = true;
            }
            if (character.GetRegisterAs().type == ECharacterType.Demon)
            {
                demPicked = true;
            }
        }

        int chosenTypes = 0;
        if (demPicked == true)
        {
            types.Add(ECharacterType.Demon);
            chosenTypes++;
        }
        if (minPicked == true)
        {
            types.Add(ECharacterType.Minion);
            chosenTypes++;
        }
        if (outsPicked == true && chosenTypes < 2)
        {
            types.Add(ECharacterType.Outcast);
            chosenTypes++;
        }
        if (villPicked == true && chosenTypes < 2)
        {
            types.Add(ECharacterType.Villager);
            chosenTypes++;
        }

        types = sharedScripts.SortList(types);
        chars = sharedScripts.SortList(chars);

        string info = "Among ";
        info += sharedScripts.MentionEveryCharacterInList(chars, "");
        info += " there is: ";
        info += sharedScripts.MentionEveryTypeInList(types, "and");
        onActed?.Invoke(new ActedInfo(info, chars));
    }
    private void CharacterPickedLiar()
    {

        CharacterPicker.OnCharactersPicked -= action3;
        CharacterPicker.OnStopPick -= action2;

        Il2CppSystem.Collections.Generic.List<Character> chars = new Il2CppSystem.Collections.Generic.List<Character>();
        chars.Add(CharacterPicker.PickedCharacters[0]);
        chars.Add(CharacterPicker.PickedCharacters[1]);
        chars.Add(CharacterPicker.PickedCharacters[2]);
        chars.Add(CharacterPicker.PickedCharacters[3]);

        bool villPicked = false;
        bool outsPicked = false;
        bool minPicked = false;
        bool demPicked = false;


        wx_SavedScripts sharedScripts = new wx_SavedScripts();
        Il2CppSystem.Collections.Generic.List<ECharacterType> types = new Il2CppSystem.Collections.Generic.List<ECharacterType>();
        foreach (Character character in chars)
        {
            if (character.GetRegisterAs().type == ECharacterType.Villager)
            {
                villPicked = true;
            }
            if (character.GetRegisterAs().type == ECharacterType.Outcast)
            {
                outsPicked = true;
            }
            if (character.GetRegisterAs().type == ECharacterType.Minion)
            {
                minPicked = true;
            }
            if (character.GetRegisterAs().type == ECharacterType.Demon)
            {
                demPicked = true;
            }
        }

        int chosenTypes = 0;
        if (demPicked == true)
        {
            types.Add(ECharacterType.Demon);
            chosenTypes++;
        }
        if (minPicked == true)
        {
            types.Add(ECharacterType.Minion);
            chosenTypes++;
        }
        if (outsPicked == true && chosenTypes < 2)
        {
            types.Add(ECharacterType.Outcast);
            chosenTypes++;
        }
        if (villPicked == true && chosenTypes < 2)
        {
            types.Add(ECharacterType.Villager);
            chosenTypes++;
        }

        Il2CppSystem.Collections.Generic.List<ECharacterType> mentionTypes = new Il2CppSystem.Collections.Generic.List<ECharacterType>();
        mentionTypes.Add(ECharacterType.Demon);
        mentionTypes.Add(ECharacterType.Minion);
        mentionTypes.Add(ECharacterType.Outcast);
        mentionTypes.Add(ECharacterType.Villager);
        mentionTypes.Remove(types[0]);
        bool canMentionOutcasts = false;
        foreach (Character character in chars)
        {
            if (character.GetCharacterBluffIfAble().type == ECharacterType.Outcast)
            {
                canMentionOutcasts = true;
                break;
            }
        }
        foreach (CharacterData character in Gameplay.Instance.GetScriptCharactersOfType(ECharacterType.Outcast))
        {
            if (character.usuallyDisguised)
            {
                canMentionOutcasts = true;
                break;
            }
        }
        if (!canMentionOutcasts) mentionTypes.Remove(ECharacterType.Outcast);
        if (mentionTypes.Count != 2)
        {
            while (mentionTypes.Count > 2) mentionTypes.Remove(mentionTypes[mentionTypes.Count - 1]);
        }

        mentionTypes = sharedScripts.SortList(mentionTypes);
        chars = sharedScripts.SortList(chars);
        string info = "Among ";
        info += sharedScripts.MentionEveryCharacterInList(chars, "");
        info += " there is: ";
        info += sharedScripts.MentionEveryTypeInList(mentionTypes, "and");
        onActed?.Invoke(new ActedInfo(info, chars));
    }

    public string ConjureInfo(Il2CppSystem.Collections.Generic.List<Character> characters, Il2CppSystem.Collections.Generic.List<ECharacterType> types, bool tie)
    {
        return "";
    }

    public Il2CppSystem.Collections.Generic.List<Character> SortList(Il2CppSystem.Collections.Generic.List<Character> list)
    {
        Il2CppSystem.Collections.Generic.List<Character> newList = new Il2CppSystem.Collections.Generic.List<Character>();
        for (int i = 0; i < 20; i++)
        {
            foreach (Character character in list)
            {
                if (character.id == i) newList.Add(character);
            }
        }
        return newList;
    }
    private void StopPick()
    {
        CharacterPicker.OnCharactersPicked -= action1;
        CharacterPicker.OnStopPick -= action2;
        CharacterPicker.OnCharactersPicked -= action3;
    }
    public w_WardenNew() : base(ClassInjector.DerivedConstructorPointer<w_WardenNew>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
        action1 = new System.Action(CharacterPicked);
        action2 = new System.Action(StopPick);
        action3 = new System.Action(CharacterPickedLiar);
    }
    public w_WardenNew(System.IntPtr ptr) : base(ptr)
    {
        action1 = new System.Action(CharacterPicked);
        action2 = new System.Action(StopPick);
        action3 = new System.Action(CharacterPickedLiar);
    }
}