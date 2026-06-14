using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using MelonLoader;
using System;
using UnityEngine;

namespace ExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Chiromancer : Role
{
    public override ActedInfo GetInfo(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<Character> allCharacters = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> evilCharacters = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        foreach (Character character in Gameplay.CurrentCharacters)
        {
            if (character != charRef)
            {
                allCharacters.Add(character);
            }
        }
        evilCharacters = Characters.Instance.FilterAlignmentCharacters(allCharacters, EAlignment.Evil);
        Character chosenEvil = evilCharacters[UnityEngine.Random.RandomRangeInt(0, evilCharacters.Count)];
        allCharacters.Remove(chosenEvil);
        Character goodieOneShoe = allCharacters[UnityEngine.Random.RandomRangeInt(0, allCharacters.Count)];
        allCharacters.Remove(goodieOneShoe);
        Character goodieTwoShoes = allCharacters[UnityEngine.Random.RandomRangeInt(0, allCharacters.Count)];
        selection.Add(chosenEvil);
        selection.Add(goodieOneShoe);
        selection.Add(goodieTwoShoes);

        Il2CppSystem.Collections.Generic.List<string> info = new Il2CppSystem.Collections.Generic.List<string>();

        for (int i = 0; i < Gameplay.CurrentCharacters.Count + 1; i++) // The +1 is just to be safe. This is a jank method of sorting everything out.
        {
            if (i == chosenEvil.id)
            {
                info.Add(chosenEvil.id.ToString());
                info.Add(chosenEvil.GetRegisterAs().name.ToString());
            }
            if (i == goodieOneShoe.id)
            {
                info.Add(goodieOneShoe.id.ToString());
                info.Add(GetFalseDreamerInfo(goodieOneShoe).name.ToString());
            }
            if (i == goodieTwoShoes.id)
            {
                info.Add(goodieTwoShoes.id.ToString());
                info.Add(GetFalseDreamerInfo(goodieTwoShoes).name.ToString());
            }
        }
        string line = string.Format("One is correct:\n#{0} {6} {1}\n#{2} {7} {3}\n#{4} {8} {5}", info[0], info[1], info[2], info[3], info[4], info[5], CheckIfThe(info[1]), CheckIfThe(info[3]), CheckIfThe(info[5]));
        ActedInfo actedInfo = new ActedInfo(line, selection);
        return actedInfo;
    }
    public override ActedInfo GetBluffInfo(Character charRef) // Rewriting the whole code to make the bluff info what now?
    {
        Il2CppSystem.Collections.Generic.List<Character> allCharacters = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> evilCharacters = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        foreach (Character character in Gameplay.CurrentCharacters)
        {
            if (character != charRef)
            {
                allCharacters.Add(character);
            }
        }
        Character chosenEvil = allCharacters[UnityEngine.Random.RandomRangeInt(0, allCharacters.Count)];
        allCharacters.Remove(chosenEvil);
        Character goodieOneShoe = allCharacters[UnityEngine.Random.RandomRangeInt(0, allCharacters.Count)];
        allCharacters.Remove(goodieOneShoe);
        Character goodieTwoShoes = allCharacters[UnityEngine.Random.RandomRangeInt(0, allCharacters.Count)];
        selection.Add(chosenEvil);
        selection.Add(goodieOneShoe);
        selection.Add(goodieTwoShoes);

        Il2CppSystem.Collections.Generic.List<string> info = new Il2CppSystem.Collections.Generic.List<string>();

        for (int i = 0; i < Gameplay.CurrentCharacters.Count + 1; i++) // The +1 is just to be safe. This is a jank method of sorting everything out.
        {
            if (i == chosenEvil.id)
            {
                info.Add(chosenEvil.id.ToString());
                info.Add(GetFalseDreamerInfo(chosenEvil).name.ToString());
            }
            if (i == goodieOneShoe.id)
            {
                info.Add(goodieOneShoe.id.ToString());
                info.Add(GetFalseDreamerInfo(goodieOneShoe).name.ToString());
            }
            if (i == goodieTwoShoes.id)
            {
                info.Add(goodieTwoShoes.id.ToString());
                info.Add(GetFalseDreamerInfo(goodieTwoShoes).name.ToString());
            }
        }
        string line = string.Format("One is correct:\n#{0} {6} {1}\n#{2} {7} {3}\n#{4} {8} {5}", info[0], info[1], info[2], info[3], info[4], info[5], CheckIfThe(info[1]), CheckIfThe(info[3]), CheckIfThe(info[5]));
        ActedInfo actedInfo = new ActedInfo(line, selection);
        return actedInfo;
    }
    public override string Description
    {
        get
        {
            return "Learn 3 characters and which Evil role each is, but only one is correct.";
        }
    }
    public string CheckIfThe(string Name)
    {
        Il2CppSystem.Collections.Generic.List<string> pluralNamesV = new Il2CppSystem.Collections.Generic.List<string>(); // There may be many of this character, they start with a vowel. #X is an Y.
        Il2CppSystem.Collections.Generic.List<string> pluralNamesC = new Il2CppSystem.Collections.Generic.List<string>(); // There may be many of this character, they start with a consonant. #X is a Y.
        Il2CppSystem.Collections.Generic.List<string> demonNames = new Il2CppSystem.Collections.Generic.List<string>(); // This character has an actual name. #X is Y.
        // Minions
        demonNames.Add("Swarm");
        pluralNamesV.Add("Acolyte");
        pluralNamesC.Add("Fanatic");
        pluralNamesC.Add("Mastermind"); // From Skill Cycler's Riddles mod
        pluralNamesC.Add("Zealot");

        // Vanilla
        demonNames.Add("Baa");
        demonNames.Add("Lilis");
        demonNames.Add("Pooka");

        // Wingidon's Expansion Pack
        demonNames.Add("Agmeres");
        demonNames.Add("Caedoccidere");
        demonNames.Add("Carnicarius");
        demonNames.Add("Emenverax");
        demonNames.Add("Iris");
        demonNames.Add("Leviathan");
        demonNames.Add("Magnere");
        demonNames.Add("Mendaverte");
        demonNames.Add("Praesect");
        demonNames.Add("Sanguitaurus");
        demonNames.Add("Specularus");
        demonNames.Add("Tenecaligo");
        demonNames.Add("Venelum");
        demonNames.Add("Veniyon");
        demonNames.Add("Vidiyon");
        demonNames.Add("Viciyon");

        // Role Ideas Collection
        demonNames.Add("Death");

        // ExtraRandomized
        demonNames.Add("Better Baa");

        // CarlzVilliagePack
        pluralNamesC.Add("Hydra");
        demonNames.Add("Pestilence");

        // CSK's Expansion Pack
        demonNames.Add("Belias");
        if (pluralNamesV.Contains(Name))
        {
            return "is an";
        }
        if (pluralNamesC.Contains(Name))
        {
            return "is a";
        }
        if (demonNames.Contains(Name))
        {
            return "is";
        }
        return "is the";
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
    private CharacterData GetFalseDreamerInfo(Character character)
    {
        UnityEngine.Debug.Log(string.Format("Getting false Dreamer info on #{0}", character.id));
        Il2CppSystem.Collections.Generic.List<CharacterData> evilCharacters = new Il2CppSystem.Collections.Generic.List<CharacterData>();
        foreach (CharacterData c in Gameplay.Instance.GetScriptCharactersOfAlignment(EAlignment.Evil))
        {
            if (character.GetRegisterAs() != c)
            {
                UnityEngine.Debug.Log(string.Format("Adding {0} to Chiromancer list", c.name.ToString()));
                evilCharacters.Add(c);
            }
        }
        CharacterData chosenCharacter = new CharacterData();
        if (evilCharacters.Count != 0)
        {
            chosenCharacter = evilCharacters[UnityEngine.Random.RandomRangeInt(0, evilCharacters.Count)];
            UnityEngine.Debug.Log(string.Format("Chose to use {0}", chosenCharacter.name.ToString()));
        }
        else
        {
            UnityEngine.Debug.Log(string.Format("Why are there no other Evils in the deck?"));
            evilCharacters = Characters.Instance.FilterAlignmentCharacters(Gameplay.Instance.GetAllAscensionCharacters(), EAlignment.Evil);
            for (int i = 0; i < evilCharacters.Count; i++)
            {
                UnityEngine.Debug.Log(string.Format("Added {0} to Chiromancer list.", evilCharacters[i].name.ToString()));
                if (character.GetRegisterAs() == evilCharacters[i])
                {
                    evilCharacters.Remove(evilCharacters[i]);
                    UnityEngine.Debug.Log(string.Format("Nevermind, removed {0} from Chiromancer list.", evilCharacters[i].name.ToString()));
                    i -= 1;
                }
            }
            chosenCharacter = evilCharacters[UnityEngine.Random.RandomRangeInt(0, evilCharacters.Count)];
        }
        return chosenCharacter;
    }
    public w_Chiromancer() : base(ClassInjector.DerivedConstructorPointer<w_Chiromancer>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Chiromancer(IntPtr ptr) : base(ptr)
    {
    }
}


