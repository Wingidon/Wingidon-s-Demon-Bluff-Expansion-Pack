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
public class w_Warden : Role
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

        int villagerPicked = 0;
        int outcastPicked = 0;
        int minionPicked = 0;
        int demonPicked = 0;
        foreach (Character character in chars)
        {
            if (character.GetCharacterType() == ECharacterType.Villager)
            {
                villagerPicked++;
            }
            if (character.GetCharacterType() == ECharacterType.Outcast)
            {
                outcastPicked++;
            }
            if (character.GetCharacterType() == ECharacterType.Minion)
            {
                minionPicked++;
            }
            if (character.GetCharacterType() == ECharacterType.Demon)
            {
                demonPicked++;
            }
        }
        int savedPickAmount = 0;
        string savedPickType = "Villagers";
        bool picksEqual = false;
        savedPickAmount = villagerPicked;
        if (outcastPicked > savedPickAmount)
        {
            savedPickType = "Outcasts";
            savedPickAmount = outcastPicked;
        }
        if (minionPicked > savedPickAmount)
        {
            savedPickType = "Minions";
            savedPickAmount = minionPicked;
        }
        if (demonPicked > savedPickAmount)
        {
            savedPickType = "Demons";
            savedPickAmount = demonPicked;
        }
        if (villagerPicked == savedPickAmount && savedPickType != "Villagers") picksEqual = true;
        if (outcastPicked == savedPickAmount && savedPickType != "Outcasts") picksEqual = true;
        if (minionPicked == savedPickAmount && savedPickType != "Minions") picksEqual = true;
        if (demonPicked == savedPickAmount && savedPickType != "Demons") picksEqual = true;
        chars = SortList(chars);
        string info = ConjureInfo(chars, savedPickType, picksEqual);
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
        chars.Add(CharacterPicker.PickedCharacters[2]);
        chars.Add(CharacterPicker.PickedCharacters[3]);

        int villagerPicked = 0;
        int outcastPicked = 0;
        int minionPicked = 0;
        int demonPicked = 0;
        foreach (Character character in chars)
        {
            if (character.GetCharacterType() == ECharacterType.Villager)
            {
                villagerPicked++;
            }
            if (character.GetCharacterType() == ECharacterType.Outcast)
            {
                outcastPicked++;
            }
            if (character.GetCharacterType() == ECharacterType.Minion)
            {
                minionPicked++;
            }
            if (character.GetCharacterType() == ECharacterType.Demon)
            {
                demonPicked++;
            }
        }
        int savedPickAmount = 0;
        string savedPickType = "Villagers";
        bool picksEqual = false;
        savedPickAmount = villagerPicked;
        if (outcastPicked > savedPickAmount)
        {
            savedPickType = "Outcasts";
            savedPickAmount = outcastPicked;
        }
        if (minionPicked > savedPickAmount)
        {
            savedPickType = "Minions";
            savedPickAmount = minionPicked;
        }
        if (demonPicked > savedPickAmount)
        {
            savedPickType = "Demons";
            savedPickAmount = demonPicked;
        }
        if (villagerPicked == savedPickAmount && savedPickType != "Villagers") picksEqual = true;
        if (outcastPicked == savedPickAmount && savedPickType != "Outcasts") picksEqual = true;
        if (minionPicked == savedPickAmount && savedPickType != "Minions") picksEqual = true;
        if (demonPicked == savedPickAmount && savedPickType != "Demons") picksEqual = true;
        string trueInfo = "PLACEHOLDER (You shouldn't see this!)";
        if (picksEqual == true)
        {
            trueInfo = "Tie";
        }
        else
        {
            trueInfo = savedPickType;
        }

        Il2CppSystem.Collections.Generic.List<string> falseCharacterTypes = new Il2CppSystem.Collections.Generic.List<string>();
        if (trueInfo != "Tie")
        {
            falseCharacterTypes.Add("Tie");
            falseCharacterTypes.Add("Tie");
            falseCharacterTypes.Add("Tie");
        }
        if (trueInfo != "Villagers")
        {
            falseCharacterTypes.Add("Villagers");
            falseCharacterTypes.Add("Villagers");
            falseCharacterTypes.Add("Villagers");
            falseCharacterTypes.Add("Villagers");
            falseCharacterTypes.Add("Villagers");
            falseCharacterTypes.Add("Villagers");
            falseCharacterTypes.Add("Villagers");
            falseCharacterTypes.Add("Villagers");
            falseCharacterTypes.Add("Villagers");
            falseCharacterTypes.Add("Villagers");
        }
        if (trueInfo != "Outcasts")
        {
            falseCharacterTypes.Add("Outcasts");
            falseCharacterTypes.Add("Outcasts");
            falseCharacterTypes.Add("Outcasts");
            falseCharacterTypes.Add("Outcasts");
            falseCharacterTypes.Add("Outcasts");
        }
        if (trueInfo != "Minions")
        {
            falseCharacterTypes.Add("Minions");
            falseCharacterTypes.Add("Minions");
            falseCharacterTypes.Add("Minions");
            falseCharacterTypes.Add("Minions");
            falseCharacterTypes.Add("Minions");
        }
        /*if (trueInfo != "Demons")
        {
            falseCharacterTypes.Add("Demons");
            falseCharacterTypes.Add("Demons");
        }*/
        string falseInfo = falseCharacterTypes[UnityEngine.Random.RandomRangeInt(0, falseCharacterTypes.Count)];
        if (falseInfo == "Tie")
        {
            picksEqual = true;
        }
        else
        {
            picksEqual = false;
        }
        chars = SortList(chars);
        string info = ConjureInfo(chars, falseInfo, picksEqual);
        onActed?.Invoke(new ActedInfo(info, chars));
        Debug.Log($"{info}");
    }

    public string ConjureInfo(Il2CppSystem.Collections.Generic.List<Character> characters, string type, bool tie)
    {
        if (tie == true)
        {
            return string.Format("Among #{0}, #{1}, #{2}, #{3}, multiple types are tied for most numerous", characters[0].id, characters[1].id, characters[2].id, characters[3].id);
        }
        return string.Format("Among #{0}, #{1}, #{2}, #{3}, the {4} are the most numerous", characters[0].id, characters[1].id, characters[2].id, characters[3].id, type);
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
    public w_Warden() : base(ClassInjector.DerivedConstructorPointer<w_Warden>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
        action1 = new System.Action(CharacterPicked);
        action2 = new System.Action(StopPick);
        action3 = new System.Action(CharacterPickedLiar);
    }
    public w_Warden(System.IntPtr ptr) : base(ptr)
    {
        action1 = new System.Action(CharacterPicked);
        action2 = new System.Action(StopPick);
        action3 = new System.Action(CharacterPickedLiar);
    }
}