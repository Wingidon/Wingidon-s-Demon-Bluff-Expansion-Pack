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
public class w_Sidekick : Role
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
        Il2CppSystem.Collections.Generic.List<Character> characters = Gameplay.CurrentCharacters;
        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        Characters charInst = Characters.Instance;
        Health health = PlayerController.PlayerInfo.health;
        int remainingHealth = 0;
        Il2CppSystem.Int32.TryParse(health.ToString(), out remainingHealth);
        string info = "Something went wrong.";
        if (remainingHealth < 5)
        {
            Il2CppSystem.Collections.Generic.List<Character> goodCharacters = Characters.Instance.FilterAlignmentCharacters(Gameplay.CurrentCharacters, EAlignment.Good);
            if (goodCharacters.Count > 1)
            {
                goodCharacters.Remove(charRef);
            }
            if (goodCharacters.Count == 0)
            {
                info = "I am confused";
            }
            else
            {
                Character myInfoTarget = goodCharacters[UnityEngine.Random.RandomRangeInt(0, goodCharacters.Count)];
                selection.Add(myInfoTarget);
                info = ConjureInfo(myInfoTarget.id, EAlignment.Good);
            }
        }
        else
        {
            Il2CppSystem.Collections.Generic.List<Character> evilCharacters = Characters.Instance.FilterAlignmentCharacters(Gameplay.CurrentCharacters, EAlignment.Evil);
            if (evilCharacters.Count > 1)
            {
                evilCharacters.Remove(charRef);
            }
            if (evilCharacters.Count == 0)
            {
                info = "I am confused";
            }
            else
            {
                Character myInfoTarget = evilCharacters[UnityEngine.Random.RandomRangeInt(0, evilCharacters.Count)];
                selection.Add(myInfoTarget);
                info = ConjureInfo(myInfoTarget.id, EAlignment.Evil);
            }
        }
        ActedInfo actedInfo = new ActedInfo(info, selection);
        onActed?.Invoke(new ActedInfo(info, selection));
    }
    public override void BluffAct(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Init)
        {
            // new wx_SavedScripts().DebugMessage($"Initialised {charRef.dataRef.characterName} at #{charRef.id}");
        }
        if (trigger != ETriggerPhase.Day) return;
        Il2CppSystem.Collections.Generic.List<Character> characters = Gameplay.CurrentCharacters;
        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        Characters charInst = Characters.Instance;
        Health health = PlayerController.PlayerInfo.health;
        int remainingHealth = 0;
        Il2CppSystem.Int32.TryParse(health.ToString(), out remainingHealth);
        string info = "Something went wrong.";
        if (remainingHealth < 5)
        {
            Il2CppSystem.Collections.Generic.List<Character> evilCharacters = Characters.Instance.FilterAlignmentCharacters(Gameplay.CurrentCharacters, EAlignment.Evil);
            if (evilCharacters.Count > 1)
            {
                evilCharacters.Remove(charRef);
            }
            if (evilCharacters.Count == 0)
            {
                info = "I am confused";
            }
            else
            {
                Character myInfoTarget = evilCharacters[UnityEngine.Random.RandomRangeInt(0, evilCharacters.Count)];
                selection.Add(myInfoTarget);
                info = ConjureInfo(myInfoTarget.id, EAlignment.Good);
            }
        }
        else
        {
            Il2CppSystem.Collections.Generic.List<Character> goodCharacters = Characters.Instance.FilterAlignmentCharacters(Gameplay.CurrentCharacters, EAlignment.Good);
            if (goodCharacters.Count > 1)
            {
                goodCharacters.Remove(charRef);
            }
            if (goodCharacters.Count == 0)
            {
                info = "I am confused";
            }
            else
            {
                Character myInfoTarget = goodCharacters[UnityEngine.Random.RandomRangeInt(0, goodCharacters.Count)];
                selection.Add(myInfoTarget);
                info = ConjureInfo(myInfoTarget.id, EAlignment.Evil);
            }
        }
        ActedInfo actedInfo = new ActedInfo(info, selection);
        onActed?.Invoke(new ActedInfo(info, selection));
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
    private string ConjureInfo(int character, EAlignment alignment)
    {
        string alignmentStr = "Good";
        if (alignment == EAlignment.Evil)
        {
            alignmentStr = "Evil";
        }
        string conjuredInfo = string.Format("#{0} is {1}", character, alignmentStr);
        return conjuredInfo;
    }
    public w_Sidekick() : base(ClassInjector.DerivedConstructorPointer<w_Sidekick>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
        action1 = new System.Action(CharacterPicked);
        action2 = new System.Action(StopPick);
        action3 = new System.Action(CharacterPickedLiar);
    }
    public w_Sidekick(System.IntPtr ptr) : base(ptr)
    {
        action1 = new System.Action(CharacterPicked);
        action2 = new System.Action(StopPick);
        action3 = new System.Action(CharacterPickedLiar);
    }
}