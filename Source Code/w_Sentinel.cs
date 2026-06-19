using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using MelonLoader;
using System;
using Il2Cpp;

namespace ExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Sentinel : Role
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
            bool targetCorrupted = false;
            if (character.statuses.statuses.Contains(ECharacterStatus.Corrupted))
            {
                targetCorrupted = true;
            }
            if (!targetCorrupted)
            {
                newList.Add(character);
            }
            else
            {
                newList2.Add(character);
            }
        }
        string line = "info";
        if (newList2.Count == 0)
        {
            line = "There are no Corrupted characters";
        }
        else
        {
            Character random = newList[UnityEngine.Random.RandomRangeInt(0, newList.Count)];
            selection.Add(random);
            Character random2 = newList2[UnityEngine.Random.RandomRangeInt(0, newList2.Count)];
            selection.Add(random2);
            if (random.id < random2.id)
            {
                line = string.Format("One is Corrupted:\n#{0}, #{1}", random.id, random2.id);
            }
            else
            {
                line = string.Format("One is Corrupted:\n#{0}, #{1}", random2.id, random.id);
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
        System.Collections.Generic.List<Character> newList2 = new System.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        Characters charInst = Characters.Instance;
        foreach (Character character in characters)
        {
            bool targetCorrupted = false;
            if (character.statuses.statuses.Contains(ECharacterStatus.Corrupted))
            {
                newList2.Add(character);
                targetCorrupted = true;
            }
            if (!targetCorrupted)
            {
                newList.Add(character);
            }
        }
        string line = "info";
        if (newList.Count <= 1)
        {
            line = "There are no Corrupted characters";
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
                line = string.Format("One is Corrupted:\n#{0}, #{1}", random.id, random2.id);
            }
            else
            {
                line = string.Format("One is Corrupted:\n#{0}, #{1}", random2.id, random.id);
            }

        }

            ActedInfo actedInfo = new ActedInfo(line, selection);
        return actedInfo;
    }
    public override string Description
    {
        get
        {
            return "1 of 2 characters is Corrupted.";
        }
    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Day)
        {
            this.onActed.Invoke(this.GetInfo(charRef));
        }
    }
    public override void BluffAct(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Day)
        {
            this.onActed.Invoke(this.GetBluffInfo(charRef));
        }
    }
    public w_Sentinel() : base(ClassInjector.DerivedConstructorPointer<w_Sentinel>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Sentinel(IntPtr ptr) : base(ptr)
    {
    }

    public ActedInfo CheckIfUniqueCharacter(Character charRef) // Checks if the character is one that gives unique info
    {
        if (charRef.dataRef.characterId == "Hypnotist_scm")
        {
            return new ActedInfo("There are no Corrupted characters");
        }
        if (charRef.dataRef.characterId == "Captivator_scm")
        {
            Il2CppSystem.Collections.Generic.List<Character> corruptedChars = new Il2CppSystem.Collections.Generic.List<Character>();
            corruptedChars = Characters.Instance.FilterCharacterContainsStatus(Gameplay.CurrentCharacters, ECharacterStatus.Corrupted);
            if (corruptedChars.Count < 2) return new ActedInfo("Something does not make sense");
            ActedInfo returnInfo = new ActedInfo("");
            returnInfo.characters.Add(corruptedChars[UnityEngine.Random.RandomRangeInt(0, corruptedChars.Count)]);
            corruptedChars.Remove(returnInfo.characters[0]);
            returnInfo.characters.Add(corruptedChars[UnityEngine.Random.RandomRangeInt(0, corruptedChars.Count)]);
            wx_SavedScripts sharedScripts = new wx_SavedScripts();
            returnInfo.characters = sharedScripts.SortList(returnInfo.characters);
            returnInfo.desc = $"One is Corrupted:\n#{returnInfo.characters[0].id}, #{returnInfo.characters[1].id}";
        }
        return new ActedInfo("False");
    }

    public bool randomPercent(float chance)
    {
        return false;
    }
}


