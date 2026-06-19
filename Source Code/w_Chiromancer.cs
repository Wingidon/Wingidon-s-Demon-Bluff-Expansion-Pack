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
        ActedInfo checkedInfo = CheckIfUniqueCharacter(charRef);
        if (checkedInfo.desc != "False") return checkedInfo;
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
        string line = string.Format("One is correct:\n#{0} is {6}{1}\n#{2} is {7}{3}\n#{4} is {8}{5}", info[0], info[1], info[2], info[3], info[4], info[5], CheckIfThe(info[1]), CheckIfThe(info[3]), CheckIfThe(info[5]));
        ActedInfo actedInfo = new ActedInfo(line, selection);
        return actedInfo;
    }
    public override ActedInfo GetBluffInfo(Character charRef) // Rewriting the whole code to make the bluff info what now?
    {
        ActedInfo checkedInfo = CheckIfUniqueCharacter(charRef);
        if (checkedInfo.desc != "False") return checkedInfo;
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
        string line = string.Format("One is correct:\n#{0} is {6}{1}\n#{2} is {7}{3}\n#{4} is {8}{5}", info[0], info[1], info[2], info[3], info[4], info[5], CheckIfThe(info[1]), CheckIfThe(info[3]), CheckIfThe(info[5]));
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
        return new wx_SavedScripts().CheckIfThe(Name);
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



    public ActedInfo CheckIfUniqueCharacter(Character charRef) // Checks if the character is one that gives unique info
    {
        if (charRef.dataRef.characterId == "Captivator_scm")
        {
            Il2CppSystem.Collections.Generic.List<Character> evilChars = new Il2CppSystem.Collections.Generic.List<Character>();
            evilChars = Characters.Instance.FilterAlignmentCharacters(Gameplay.CurrentCharacters, EAlignment.Evil);
            if (evilChars.Count < 2) return new ActedInfo("Something does not make sense");
            ActedInfo returnInfo = new ActedInfo("");
            Il2CppSystem.Collections.Generic.List<Character> allCharacters = new Il2CppSystem.Collections.Generic.List<Character>();
            Il2CppSystem.Collections.Generic.List<Character> evilCharacters = new Il2CppSystem.Collections.Generic.List<Character>();
            Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
            wx_SavedScripts sharedScripts = new wx_SavedScripts();
            foreach (Character character in Gameplay.CurrentCharacters)
            {
                if (character != charRef)
                {
                    allCharacters.Add(character);
                }
            }
            selection.Add(evilCharacters[UnityEngine.Random.RandomRangeInt(0, evilCharacters.Count)]);
            evilCharacters.Remove(selection[0]);
            allCharacters.Remove(selection[0]);
            selection.Add(evilCharacters[UnityEngine.Random.RandomRangeInt(0, evilCharacters.Count)]);
            allCharacters.Remove(selection[1]);
            selection.Add(allCharacters[UnityEngine.Random.RandomRangeInt(0, allCharacters.Count)]);

            selection = sharedScripts.SortList(selection);

            Il2CppSystem.Collections.Generic.List<string> chosenInfo = new Il2CppSystem.Collections.Generic.List<string>();
            foreach (Character character in selection)
            {
                if (character.GetRegisterAlignment() == EAlignment.Evil)
                {
                    chosenInfo.Add(character.GetRegisterAs().characterName);
                }
                else
                {
                    chosenInfo.Add(GetFalseDreamerInfo(character).characterName);
                }
            }

            string infoString = "One is correct:\n"
                + $"#{selection[0].id} is {CheckIfThe(chosenInfo[0])}{chosenInfo[0]}"
                + $"#{selection[1].id} is {CheckIfThe(chosenInfo[1])}{chosenInfo[1]}"
                + $"#{selection[2].id} is {CheckIfThe(chosenInfo[2])}{chosenInfo[2]}";

            returnInfo.desc = infoString;
            returnInfo.characters = selection;
            return returnInfo;
        }
        return new ActedInfo("False");
    }
    public w_Chiromancer() : base(ClassInjector.DerivedConstructorPointer<w_Chiromancer>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Chiromancer(IntPtr ptr) : base(ptr)
    {
    }
}


