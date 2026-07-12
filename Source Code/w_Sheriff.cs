using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using MelonLoader;
using System;
using Il2Cpp;

namespace WingidonExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Sheriff : Role
{
    public override ActedInfo GetInfo(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<string> characterDisguises = new Il2CppSystem.Collections.Generic.List<string>();
        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        foreach (Character character in Gameplay.CurrentCharacters)
        {
            if (CharacterHelper.CheckIfDisguisedAppearance(character)) // If the character is Registering as Disguised
            {
                if (character.bluff)
                {
                    characterDisguises.Add(character.bluff.characterName);
                }
                else
                {
                    characterDisguises.Add(character.dataRef.characterName);
                }
            }
        }
        string line = "info";
        if (characterDisguises.Count == 0)
        {
            line = "Something does not make sense";
        }
        else
        {
            string chosenBluff = characterDisguises[UnityEngine.Random.RandomRangeInt(0, characterDisguises.Count)];
            line = string.Format($"The {chosenBluff} is being used as a Disguise");
        }

        ActedInfo actedInfo = new ActedInfo(line, selection);
        return actedInfo;
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<string> characterDisguises = new Il2CppSystem.Collections.Generic.List<string>();
        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        foreach (Character character in Gameplay.CurrentCharacters)
        {
            if (character.bluff)
            {
                characterDisguises.Add(character.bluff.characterName);
            }
            else
            {
                characterDisguises.Add(character.dataRef.characterName);
            }
        }
        foreach (Character character in Gameplay.CurrentCharacters)
        {
            if (CharacterHelper.CheckIfDisguisedAppearance(character)) // If the character is Registering as Disguised
            {
                if (character.bluff)
                {
                    if (characterDisguises.Contains(character.bluff.characterName))
                    {
                        while (characterDisguises.Contains(character.bluff.characterName))
                        {
                            characterDisguises.Remove(character.bluff.characterName);
                        }
                    }
                }
                else
                {
                    if (characterDisguises.Contains(character.dataRef.characterName))
                    {
                        while (characterDisguises.Contains(character.dataRef.characterName))
                        {
                            characterDisguises.Remove(character.dataRef.characterName);
                        }
                    }
                }
            }
        }
        string line = "info";
        if (characterDisguises.Count == 0)
        {
            line = "Something does not make sense";
        }
        else
        {
            string chosenBluff = characterDisguises[UnityEngine.Random.RandomRangeInt(0, characterDisguises.Count)];
            line = string.Format($"The {chosenBluff} is being used as a Disguise");
        }

        ActedInfo actedInfo = new ActedInfo(line, selection);
        return actedInfo;
    }
    public override string Description
    {
        get
        {
            return "1 of 2 characters is Disguised.";
        }
    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Init)
        {
            // new wx_SavedScripts().DebugMessage($"Initialised {charRef.dataRef.characterName} at #{charRef.id}");
        }
        if (trigger == ETriggerPhase.Day)
        {
            onActed.Invoke(GetInfo(charRef));
        }
    }
    public override void BluffAct(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Init)
        {
            // new wx_SavedScripts().DebugMessage($"Initialised {charRef.dataRef.characterName} at #{charRef.id}");
        }
        if (trigger == ETriggerPhase.Day)
        {
            onActed.Invoke(GetBluffInfo(charRef));
        }
    }
    public w_Sheriff() : base(ClassInjector.DerivedConstructorPointer<w_Sheriff>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Sheriff(IntPtr ptr) : base(ptr)
    {
    }
}


