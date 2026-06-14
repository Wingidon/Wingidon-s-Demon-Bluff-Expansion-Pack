using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppSystem;
using MelonLoader;
using System;
using System.ComponentModel.Design;
using UnityEngine;

namespace ExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Forager : Role
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

        Il2CppSystem.Collections.Generic.List<Character> chars = new Il2CppSystem.Collections.Generic.List<Character>();
        chars.Add(CharacterPicker.PickedCharacters[0]);

        string info = $"";
        if (chars[0].GetCharacterData().type == ECharacterType.Villager)
        {
            info = string.Format("#{0} is a Villager", chars[0].id);
        }
        else
        {
            Il2CppSystem.Collections.Generic.List<string> possibleInfo = new Il2CppSystem.Collections.Generic.List<string>();
//            possibleInfo.Add(string.Format("#{0} is not a Villager.", chars[0].id));
            possibleInfo.Add(string.Format("#{0} could be a lemon", chars[0].id));
            possibleInfo.Add(string.Format("#{0} could be a cabbage", chars[0].id));
            possibleInfo.Add(string.Format("#{0} could be a peanut", chars[0].id));
            possibleInfo.Add(string.Format("#{0} could be a potato", chars[0].id));
            possibleInfo.Add(string.Format("#{0} might be stupid", chars[0].id));
            possibleInfo.Add(string.Format("#{0} might be a moron", chars[0].id));
            possibleInfo.Add(string.Format("#{0} might be an idiot", chars[0].id));
            possibleInfo.Add(string.Format("#{0} might be dumb", chars[0].id));
            string newString = possibleInfo[UnityEngine.Random.RandomRangeInt(0, possibleInfo.Count)];
            info = newString;
        }

        onActed?.Invoke(new ActedInfo(info, chars));
        Debug.Log($"{info}");
    }
    private void CharacterPickedLiar()
    {
        CharacterPicker.OnCharactersPicked -= action3;
        CharacterPicker.OnStopPick -= action2;

        Il2CppSystem.Collections.Generic.List<Character> chars = new Il2CppSystem.Collections.Generic.List<Character>();
        chars.Add(CharacterPicker.PickedCharacters[0]);

        string info = $"";
        if (chars[0].GetCharacterData().type != ECharacterType.Villager)
        {
            info = string.Format("#{0} is a Villager", chars[0].id);
        }
        else
        {
            Il2CppSystem.Collections.Generic.List<string> possibleInfo = new Il2CppSystem.Collections.Generic.List<string>();
 //           possibleInfo.Add(string.Format("#{0} is not a Villager.", chars[0].id));
                       possibleInfo.Add(string.Format("#{0} could be a lemon", chars[0].id));
                       possibleInfo.Add(string.Format("#{0} could be a cabbage", chars[0].id));
                       possibleInfo.Add(string.Format("#{0} could be a peanut", chars[0].id));
                       possibleInfo.Add(string.Format("#{0} could be a potato", chars[0].id));
                       possibleInfo.Add(string.Format("#{0} might be stupid", chars[0].id));
                       possibleInfo.Add(string.Format("#{0} might be a moron", chars[0].id));
                       possibleInfo.Add(string.Format("#{0} might be an idiot", chars[0].id));
                       possibleInfo.Add(string.Format("#{0} might be dumb", chars[0].id));
            string newString = possibleInfo[UnityEngine.Random.RandomRangeInt(0, possibleInfo.Count)];
            info = newString;
        }

        onActed?.Invoke(new ActedInfo(info, chars));
        Debug.Log($"{info}");
    }
    private void StopPick()
    {
        CharacterPicker.OnCharactersPicked -= action1;
        CharacterPicker.OnStopPick -= action2;
        CharacterPicker.OnCharactersPicked -= action3;
    }
    public w_Forager() : base(ClassInjector.DerivedConstructorPointer<w_Forager>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
        action1 = new System.Action(CharacterPicked);
        action2 = new System.Action(StopPick);
        action3 = new System.Action(CharacterPickedLiar);
    }
    public w_Forager(System.IntPtr ptr) : base(ptr)
    {
        action1 = new System.Action(CharacterPicked);
        action2 = new System.Action(StopPick);
        action3 = new System.Action(CharacterPickedLiar);
    }
}