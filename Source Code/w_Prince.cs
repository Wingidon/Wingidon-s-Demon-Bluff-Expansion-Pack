using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using MelonLoader;
using System;
using static MelonLoader.MelonLaunchOptions;
using static MelonLoader.Modules.MelonModule;

namespace ExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Prince : Role
{
    public override ActedInfo GetInfo(Character charRef)
    {
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
            line = "This village confuses me";
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
            line = "This village confuses me";
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
        if (trigger == ETriggerPhase.Day)
        {
            OnActed(ETriggerPhase.Day, charRef, GetInfo(charRef));
        }
    }
    public override void BluffAct(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Day)
        {
            OnActed(ETriggerPhase.Day, charRef, GetBluffInfo(charRef));
        }
    }
    public w_Prince() : base(ClassInjector.DerivedConstructorPointer<w_Prince>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Prince(IntPtr ptr) : base(ptr)
    {
    }
}


