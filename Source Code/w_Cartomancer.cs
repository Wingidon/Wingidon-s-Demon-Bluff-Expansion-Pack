using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using MelonLoader;
using System;
using Il2Cpp;

namespace WingidonExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Cartomancer : Role
{
    public override ActedInfo GetInfo(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<string> inPlayChars = new Il2CppSystem.Collections.Generic.List<string>();
        Il2CppSystem.Collections.Generic.List<string> outOfPlayChars = new Il2CppSystem.Collections.Generic.List<string>();
        foreach (Character character in Gameplay.CurrentCharacters)
        {
            inPlayChars.Add(character.GetRegisterAs().characterName);
        }
        foreach (CharacterData character in Gameplay.Instance.GetScriptCharacters())
        {
            if (!inPlayChars.Contains(character.characterName)) outOfPlayChars.Add(character.characterName);
        }

        if (outOfPlayChars.Count == 0)
        {
            foreach (CharacterData character in Gameplay.Instance.GetAllAscensionCharacters())
            {
                if (!inPlayChars.Contains(character.characterName)) outOfPlayChars.Add(character.characterName);
            }
        }

        if (inPlayChars.Contains("Cartomancer"))
        {
            while (inPlayChars.Contains("Cartomancer"))
            {
                inPlayChars.Remove("Cartomancer");
            }
        }
        if (outOfPlayChars.Contains("Cartomancer"))
        {
            while (outOfPlayChars.Contains("Cartomancer"))
            {
                outOfPlayChars.Remove("Cartomancer");
            }
        }
        if (inPlayChars.Count == 0)
        {
            inPlayChars.Add("Cartomancer");
        }

        if (outOfPlayChars.Count == 0)
        {
            outOfPlayChars.Add("Cartomancer"); // Had this trigger on an Overseer-Cartomancer where Cartomancer was the only not-in-play card. The only way this happens is if Cartomancer was the only not-in-play card and the previous statements removed it.
        }

        string inPlayChar = inPlayChars[UnityEngine.Random.RandomRangeInt(0, inPlayChars.Count)];
        string outOfPlayChar = outOfPlayChars[UnityEngine.Random.RandomRangeInt(0, outOfPlayChars.Count)];

        string line = $"One is in-play:\n{inPlayChar}, {outOfPlayChar}";
        wx_SavedScripts sharedScripts = new wx_SavedScripts();
        if (sharedScripts.PercentChance(50))
        {
            line = $"One is in-play:\n{outOfPlayChar}, {inPlayChar}";
        }

        ActedInfo actedInfo = new ActedInfo(line);
        return actedInfo;
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<string> inPlayChars = new Il2CppSystem.Collections.Generic.List<string>();
        Il2CppSystem.Collections.Generic.List<string> inPlayCharsPriority = new Il2CppSystem.Collections.Generic.List<string>();
        Il2CppSystem.Collections.Generic.List<string> outOfPlayChars = new Il2CppSystem.Collections.Generic.List<string>();
        Il2CppSystem.Collections.Generic.List<string> outOfPlayCharsPriority = new Il2CppSystem.Collections.Generic.List<string>();
        foreach (Character character in Gameplay.CurrentCharacters)
        {
            if (!inPlayChars.Contains(character.GetRegisterAs().characterName)) inPlayChars.Add(character.GetRegisterAs().characterName);
            if (!inPlayCharsPriority.Contains(character.GetRegisterAs().characterName)) inPlayCharsPriority.Add(character.GetRegisterAs().characterName);
        }
        foreach (Character character in Gameplay.CurrentCharacters)
        {
            if (character.bluff)
            {
                if (!inPlayChars.Contains(character.bluff.characterName) && !outOfPlayCharsPriority.Contains(character.bluff.characterName)) outOfPlayCharsPriority.Add(character.bluff.characterName); // Prefers to declare characters that someone is using as a bluff
                inPlayCharsPriority.Remove(character.bluff.characterName); // Prefers not to accuse bluffing characters of claiming something out-of-play.
            }
        }
        foreach (CharacterData character in Gameplay.Instance.GetScriptCharacters())
        {
            if (!inPlayChars.Contains(character.characterName) && !outOfPlayChars.Contains(character.characterName)) outOfPlayChars.Add(character.characterName);
        }



        if (inPlayChars.Contains("Cartomancer"))
        {
            while (inPlayChars.Contains("Cartomancer"))
            {
                inPlayChars.Remove("Cartomancer");
            }
        }
        if (outOfPlayChars.Contains("Cartomancer"))
        {
            while (outOfPlayChars.Contains("Cartomancer"))
            {
                outOfPlayChars.Remove("Cartomancer");
            }
        }
        if (inPlayCharsPriority.Contains("Cartomancer"))
        {
            while (inPlayCharsPriority.Contains("Cartomancer"))
            {
                inPlayCharsPriority.Remove("Cartomancer");
            }
        }
        if (outOfPlayCharsPriority.Contains("Cartomancer"))
        {
            while (outOfPlayCharsPriority.Contains("Cartomancer"))
            {
                outOfPlayCharsPriority.Remove("Cartomancer");
            }
        }

        wx_SavedScripts sharedScripts = new wx_SavedScripts();

        string name1 = "";
        string name2 = "";

        bool useInPlay = true;
        if (outOfPlayChars.Count >= 2 && sharedScripts.PercentChance(50))
        {
            useInPlay = false;
        }

        if (useInPlay)
        {
            if (inPlayCharsPriority.Count >= 2 && sharedScripts.PercentChance(80)) // If there's at least 2 priority characters, 80% chance to use those
            {
                name1 = inPlayCharsPriority[UnityEngine.Random.RandomRangeInt(0, inPlayCharsPriority.Count)];
                inPlayCharsPriority.Remove(name1);
                name2 = inPlayCharsPriority[UnityEngine.Random.RandomRangeInt(0, inPlayCharsPriority.Count)];
            }
            else
            {
                name1 = inPlayChars[UnityEngine.Random.RandomRangeInt(0, inPlayChars.Count)];
                inPlayChars.Remove(name1);
                name2 = inPlayChars[UnityEngine.Random.RandomRangeInt(0, inPlayChars.Count)];
            }
        }
        else
        {
            if (outOfPlayCharsPriority.Count >= 2 && sharedScripts.PercentChance(80)) // If there's at least 2 priority characters, 80% chance to use those
            {
                name1 = outOfPlayCharsPriority[UnityEngine.Random.RandomRangeInt(0, outOfPlayCharsPriority.Count)];
                outOfPlayCharsPriority.Remove(name1);
                name2 = outOfPlayCharsPriority[UnityEngine.Random.RandomRangeInt(0, outOfPlayCharsPriority.Count)];
            }
            else
            {
                name1 = outOfPlayChars[UnityEngine.Random.RandomRangeInt(0, outOfPlayChars.Count)];
                outOfPlayChars.Remove(name1);
                name2 = outOfPlayChars[UnityEngine.Random.RandomRangeInt(0, outOfPlayChars.Count)];
            }
        }

        string line = $"One is in-play:\n{name1}, {name2}";

        ActedInfo actedInfo = new ActedInfo(line);
        return actedInfo;
    }
    public override string Description
    {
        get
        {
            return "Learn an in-play role and an out-of-play role.";
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
    public w_Cartomancer() : base(ClassInjector.DerivedConstructorPointer<w_Cartomancer>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Cartomancer(IntPtr ptr) : base(ptr)
    {
    }
}


