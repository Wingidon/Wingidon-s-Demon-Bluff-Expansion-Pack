using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using MelonLoader;
using System;
using static MelonLoader.MelonLaunchOptions;
using static MelonLoader.Modules.MelonModule;

namespace WingidonExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Prince : Role
{
    public override ActedInfo GetInfo(Character charRef)
    {
        ActedInfo checkedInfo = CheckIfUniqueCharacter(charRef);
        if (checkedInfo.desc != "False") return checkedInfo;
        Il2CppSystem.Collections.Generic.List<Character> characters = Gameplay.CurrentCharacters;
        System.Collections.Generic.List<Character> newList = new System.Collections.Generic.List<Character>();
        System.Collections.Generic.List<Character> newList2 = new System.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        Characters charInst = Characters.Instance;
        foreach (Character character in characters)
        {
            bool disguising = false;
            disguising = CharacterHelper.CheckIfDisguisedAppearance(character);
            if (!disguising)
            {
                newList.Add(character);
            }
            else
            {
                newList2.Add(character);
            }
        }
        string line = "info";
        if (newList.Count == 0 || newList2.Count == 0)
        {
            line = "Something does not make sense";
        }
        else
        {
            Character random = newList[UnityEngine.Random.RandomRangeInt(0, newList.Count)];
            selection.Add(random);
            Character random2 = newList2[UnityEngine.Random.RandomRangeInt(0, newList2.Count)];
            selection.Add(random2);
            if (random.id < random2.id)
            {
                line = string.Format("One is Disguised:\n#{0}, #{1}", random.id, random2.id);
            }
            else
            {
                line = string.Format("One is Disguised:\n#{0}, #{1}", random2.id, random.id);
            }
        }

        ActedInfo actedInfo = new ActedInfo(line, selection);
        return actedInfo;
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        ActedInfo checkedInfo = CheckIfUniqueCharacter(charRef);
        if (checkedInfo.desc != "False") return checkedInfo;
        Il2CppSystem.Collections.Generic.List<Character> characters = Gameplay.CurrentCharacters;
        System.Collections.Generic.List<Character> newList = new System.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        Characters charInst = Characters.Instance;
        foreach (Character character in characters)
        {
            bool disguising = false;
            disguising = CharacterHelper.CheckIfDisguisedAppearance(character);
            if (!disguising)
            {
                newList.Add(character);
            }
        }
        string line = "info";
        if (newList.Count < 2)
        {
            line = "Something does not make sense";
        }
        else
        {
            Character random = newList[UnityEngine.Random.RandomRangeInt(0, newList.Count)];
            selection.Add(random);
            newList.Remove(random);
            Character random2 = newList[UnityEngine.Random.RandomRangeInt(0, newList.Count)];
            selection.Add(random2);
            if (random.id < random2.id)
            {
                line = string.Format("One is Disguised:\n#{0}, #{1}", random.id, random2.id);
            }
            else
            {
                line = string.Format("One is Disguised:\n#{0}, #{1}", random2.id, random.id);
            }
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
            OnActed(ETriggerPhase.Day, charRef, GetInfo(charRef));
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
            OnActed(ETriggerPhase.Day, charRef, GetBluffInfo(charRef));
        }
    }



    public ActedInfo CheckIfUniqueCharacter(Character charRef) // Checks if the character is one that gives unique info
    {
        if (charRef.dataRef.characterId == "Captivator_scm")
        {
            Il2CppSystem.Collections.Generic.List<Character> disguisedChars = new Il2CppSystem.Collections.Generic.List<Character>();
            Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
            foreach (Character character in Gameplay.CurrentCharacters)
            {
                if (CharacterHelper.CheckIfDisguisedAppearance(character)) disguisedChars.Add(character);
            }
            if (disguisedChars.Count < 2) return new ActedInfo("Something does not make sense");
            selection.Add(disguisedChars[UnityEngine.Random.RandomRangeInt(0, disguisedChars.Count)]);
            disguisedChars.Remove(selection[0]);
            selection.Add(disguisedChars[UnityEngine.Random.RandomRangeInt(0, disguisedChars.Count)]);
            wx_SavedScripts sharedScripts = new wx_SavedScripts();
            selection = sharedScripts.SortList(selection);
            return new ActedInfo($"One is Disguised:\n#{selection[0].id}, #{selection[1].id}", selection);
        }
        return new ActedInfo("False");
    }
    public w_Prince() : base(ClassInjector.DerivedConstructorPointer<w_Prince>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Prince(IntPtr ptr) : base(ptr)
    {
    }
}


