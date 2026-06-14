using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using MelonLoader;
using System;
using static MelonLoader.MelonLogger;
using static MelonLoader.Modules.MelonModule;

namespace ExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Ranger : Role
{
    public override ActedInfo GetInfo(Character charRef)
    {
        string info = "";
        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        int dist = 0;
        int checkDist = 0;
        foreach (Character c in Gameplay.CurrentCharacters)
        {
            if (c.GetRegisterAlignment() == EAlignment.Evil)
            {
                checkDist = GetDistanceBetweenCharacters(charRef, c, Gameplay.CurrentCharacters.Count);
                if (checkDist > dist) dist = checkDist;
            }
        }
        selection = Characters.Instance.GetCharactersAtRange(dist, charRef);
        info = ConjourInfo(dist);
        ActedInfo newInfo = new ActedInfo(info, selection);
        return newInfo;
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        string info = "";
        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> evils = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> allChars = new Il2CppSystem.Collections.Generic.List<Character>();
        int dist = 0;
        int checkDist = 0;
        foreach (Character c in Gameplay.CurrentCharacters)
        {
            allChars.Add(c);
            if (c.GetRegisterAlignment() == EAlignment.Evil)
            {
                evils.Add(c);
                checkDist = GetDistanceBetweenCharacters(charRef, c, Gameplay.CurrentCharacters.Count);
                if (checkDist > dist) dist = checkDist;
            }
        }
        int lieDist = 0;
        Character charCheck = new Character();
        foreach (Character character in Characters.Instance.GetCharactersAtRange(dist, charRef))
        {
            allChars.Remove(character);
        }
        allChars.Remove(charRef);
        for (int i = 0; i < evils.Count; i++)
        {
            if (allChars.Count != 0)
            {
                charCheck = allChars[UnityEngine.Random.RandomRangeInt(0, allChars.Count)];
                allChars.Remove(charCheck);
                checkDist = GetDistanceBetweenCharacters(charRef, charCheck, Gameplay.CurrentCharacters.Count);
                if (checkDist > lieDist) lieDist = checkDist;
            }
        }
        selection = Characters.Instance.GetCharactersAtRange(lieDist, charRef);
        info = ConjourInfo(lieDist);
        ActedInfo newInfo = new ActedInfo(info, selection);
        return newInfo;
    }
    public override string Description
    {
        get
        {
            return "Learn the distance from me to my furthest Evil.";
        }
    }
    public static int RoundValToInt(decimal val)
    {
        return (int)Math.Round(val);
    }
    public string ConjourInfo(int steps)
    {
        //if (steps > 20)
        //    return $"There are no Outcasts.";
        if (steps == 1)
            return $"I am 1 card away from my furthest Evil";
        else if (steps == 0)
            return "This village confuses me";
        else
            return $"I am {steps} cards away from my furthest Evil";
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
    public int GetDistanceBetweenCharacters(Character char1, Character char2, int totalCharCount)
    {
        int tempDist = char1.id - char2.id;
        if (tempDist < 0)
        {
            tempDist = tempDist + totalCharCount;
        }
        int tempDist2 = char2.id - char1.id;
        if (tempDist2 < 0)
        {
            tempDist2 = tempDist2 + totalCharCount;
        }
        if (tempDist > tempDist2)
        {
            return tempDist2;
        }
        return tempDist;
    }
    public w_Ranger() : base(ClassInjector.DerivedConstructorPointer<w_Ranger>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Ranger(IntPtr ptr) : base(ptr)
    {
    }
}


