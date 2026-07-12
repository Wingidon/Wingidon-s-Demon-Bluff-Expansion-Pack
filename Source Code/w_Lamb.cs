using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using MelonLoader;
using System;
using static MelonLoader.MelonLogger;
using static MelonLoader.Modules.MelonModule;

namespace WingidonExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Lamb : Role
{
    public override ActedInfo GetInfo(Character charRef)
    {
        string info = "";
        Il2CppSystem.Collections.Generic.List<Character> allChars = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> allOutcasts = new Il2CppSystem.Collections.Generic.List<Character>();
        allChars = Gameplay.CurrentCharacters;
        foreach (Character c in Gameplay.CurrentCharacters)
        {
            if (c.GetCharacterType() == ECharacterType.Outcast)
            {
                allOutcasts.Add(c);
            }
        }
        if (allOutcasts.Contains(charRef))
        {
            allOutcasts.Remove(charRef);
        }
        allOutcasts = Characters.Instance.FilterCharacterType(allOutcasts, ECharacterType.Outcast);
        if (allOutcasts.Count > 0)
        {
            Character pickedOutcast = allOutcasts[UnityEngine.Random.Range(0, allOutcasts.Count)];
            UnityEngine.Debug.Log(string.Format("Lamb at #{2} found Outcast {0} at #{1}, using them as target.", pickedOutcast.GetRegisterAs().name, pickedOutcast.id, charRef.id));

            int dist1 = GetDistanceBetweenCharacters(charRef, pickedOutcast, allChars.Count);
            UnityEngine.Debug.Log(string.Format("Distance1 is {0}", dist1));
            int dist2 = GetDistanceBetweenCharacters(pickedOutcast, charRef, allChars.Count);
            UnityEngine.Debug.Log(string.Format("Distance2 is {0}", dist2));
            int trueDist = 0;
            if (dist1 < dist2)
            {
                trueDist = dist1;
            }
            else
            {
                trueDist = dist2;
            }
            UnityEngine.Debug.Log(string.Format("Shorter distance is {0}", trueDist));
            selection = Characters.Instance.GetCharactersAtRange(trueDist, charRef);

            info = ConjourInfo(pickedOutcast.GetRegisterAs().name, trueDist);
        }
        else
        {
            info = "There are no Outcasts";
        }
        ActedInfo newInfo = new ActedInfo(info, selection);
        return newInfo;
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        string info = "";
        Il2CppSystem.Collections.Generic.List<Character> allChars = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> trueInfo = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> allOutcasts = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> validTargets = new Il2CppSystem.Collections.Generic.List<Character>();
        allChars = Gameplay.CurrentCharacters;
        foreach (Character c in Gameplay.CurrentCharacters)
        {
            validTargets.Add(c);
            if (c.GetCharacterType() == ECharacterType.Outcast)
            {
                allOutcasts.Add(c);
            }
        }
        Il2CppSystem.Collections.Generic.List<CharacterData> deckOutcasts = new Il2CppSystem.Collections.Generic.List<CharacterData>();
        foreach (CharacterData character in Gameplay.Instance.GetScriptCharacters())
        {
            if (character.type == ECharacterType.Outcast)
            {
                deckOutcasts.Add(character);
            }
        }
        Il2CppSystem.Collections.Generic.List<string> misregisterOutcasts = new Il2CppSystem.Collections.Generic.List<string>();
        misregisterOutcasts.Add("Wretch_80988916");
        misregisterOutcasts.Add("Marionette_WING");
        bool noOutcastsValid = false;
        foreach (CharacterData character in deckOutcasts)
        {
            if (misregisterOutcasts.Contains(character.characterId))
            {
                noOutcastsValid = true;
            }
            break;
        }
        if (allOutcasts.Count == 0)
        {
            noOutcastsValid = false;
        }
        if (noOutcastsValid == false)
        {
            validTargets.Remove(charRef);
        }
        allOutcasts.Remove(charRef);
        Character pickedOutcast = charRef;
        if (allOutcasts.Count > 0)
        {
            pickedOutcast = allOutcasts[UnityEngine.Random.Range(0, allOutcasts.Count)];

            int dist11 = GetDistanceBetweenCharacters(charRef, pickedOutcast, allChars.Count);
            int dist12 = GetDistanceBetweenCharacters(pickedOutcast, charRef, allChars.Count);
            int trueDist = 0;
            if (dist11 < dist12)
            {
                trueDist = dist11;
            }
            else
            {
                trueDist = dist12;
            }
            trueInfo = Characters.Instance.GetCharactersAtRange(trueDist, charRef);
            foreach (Character c in trueInfo)
            {
                validTargets.Remove(c);
            }
        }
        Character trueTarget = validTargets[UnityEngine.Random.Range(0, validTargets.Count)];
        int dist21 = GetDistanceBetweenCharacters(charRef, trueTarget, allChars.Count);
        int dist22 = GetDistanceBetweenCharacters(trueTarget, charRef, allChars.Count);
        int trueDist2 = 0;
        if (dist21 < dist22)
        {
            trueDist2 = dist21;
        }
        else
        {
            trueDist2 = dist22;
        }
        selection = Characters.Instance.GetCharactersAtRange(trueDist2, charRef);
        if (allOutcasts.Count > 0)
        {
            info = ConjourInfo(pickedOutcast.GetRegisterAs().name, trueDist2);
        }
        else
        {
            info = ConjourInfo(deckOutcasts[UnityEngine.Random.RandomRange(0, deckOutcasts.Count)].name, trueDist2);
        }
        ActedInfo newInfo = new ActedInfo(info, selection);
        return newInfo;
    }
    public override string Description
    {
        get
        {
            return "Learn the distance from me to a particular Outcast.";
        }
    }
    public static int RoundValToInt(decimal val)
    {
        return (int)Math.Round(val);
    }
    public string ConjourInfo(string charName, int steps)
    {
        if (steps == 0)
            return "There are no Outcasts";
        if (steps == 1)
            return $"I am 1 card away from the {charName}";
        else
            return $"I am {steps} cards away from the {charName}";
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
    public int GetDistanceBetweenCharacters(Character char1, Character char2, int totalCharCount)
    {
        UnityEngine.Debug.Log(string.Format("Grabbing distance from #{0} to #{1}", char1.id, char2.id));
        int tempDist = char1.id - char2.id;
        UnityEngine.Debug.Log(string.Format("Outcome is {0}", tempDist));
        if (tempDist < 0)
        {
            UnityEngine.Debug.Log(string.Format("Number is negative, adding {0}", totalCharCount));
            tempDist = tempDist + totalCharCount;
        }
        return tempDist;
    }
    public w_Lamb() : base(ClassInjector.DerivedConstructorPointer<w_Lamb>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Lamb(IntPtr ptr) : base(ptr)
    {
    }
}


