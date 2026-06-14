using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using MelonLoader;
using System;
using Il2Cpp;

namespace ExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Sheriff : Role
{
    public override ActedInfo GetInfo(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<Character> characters = Gameplay.CurrentCharacters;
        System.Collections.Generic.List<Character> newList = new System.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        Characters charInst = Characters.Instance;
        foreach (Character character in characters)
        {
            bool disguising = false;
            if (character.statuses != null)
            {
                disguising = character.bluff;
            }
            if (disguising)
            {
                newList.Add(character);
            }
        }
        string line = "info";
        if (newList.Count == 0)
        {
            line = "This village confuses me";
        }
        else
        {
            Character random = newList[UnityEngine.Random.RandomRangeInt(0, newList.Count)];
            line = string.Format("The {0} is being used as a Disguise", random.bluff.name.ToString());
        }

        ActedInfo actedInfo = new ActedInfo(line, selection);
        return actedInfo;
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<Character> characters = Gameplay.CurrentCharacters;
        System.Collections.Generic.List<Character> newList = new System.Collections.Generic.List<Character>();
        System.Collections.Generic.List<Character> newList2 = new System.Collections.Generic.List<Character>();
        System.Collections.Generic.List<CharacterData> bluffsList = new System.Collections.Generic.List<CharacterData>();
        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        Characters charInst = Characters.Instance;
        foreach (Character character in characters)
        {
            bool disguising = false;
            if (character.statuses != null)
            {
                disguising = character.bluff;
            }
            if (disguising)
            {
                newList.Add(character);
            }
            else
            {
                newList2.Add(character);
                if (character.GetRegisterAlignment() == EAlignment.Good) bluffsList.Add(character.GetRegisterAs());
            }
        }
        string line = "info";
        if (bluffsList.Count == 0)
        {
            line = "This village confuses me";
        }
        else
        {
            foreach (Character character in newList)
            {
                if (bluffsList.Contains(character.bluff))
                {
                    while (bluffsList.Contains(character.bluff))
                    {
                        bluffsList.Remove(character.bluff);
                    }
                }
            }
            CharacterData random = bluffsList[UnityEngine.Random.RandomRangeInt(0, bluffsList.Count)];
            line = string.Format("The {0} is being used as a Disguise", random.name.ToString());
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
        if (trigger == ETriggerPhase.Day)
        {
            onActed.Invoke(GetInfo(charRef));
        }
    }
    public override void BluffAct(ETriggerPhase trigger, Character charRef)
    {
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


