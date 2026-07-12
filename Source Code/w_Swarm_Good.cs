using Il2Cpp;
using Il2CppInterop.Runtime;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppSystem;
using MelonLoader;
using System;
using System.ComponentModel.Design;
using UnityEngine;
using static Il2CppSystem.Globalization.CultureInfo;

namespace WingidonExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Swarm_Good : Minion
{
    public CharacterData[] allDatas = Il2CppSystem.Array.Empty<CharacterData>();
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Init)
        {
            // new wx_SavedScripts().DebugMessage($"Initialised {charRef.dataRef.characterName} at #{charRef.id}");
        }
        if (trigger == ETriggerPhase.Start && !charRef.statuses.Contains(ECharacterStatus.BrokenAbility))
        {
            Il2CppSystem.Collections.Generic.List<Character> chars = new Il2CppSystem.Collections.Generic.List<Character>(Gameplay.CurrentCharacters.Pointer);
            Character pickedChar = new Character();
            for (int i = 0; i < 2; i++)
            {
                chars = Characters.Instance.FilterCharacterType(chars, ECharacterType.Villager);
                chars = Characters.Instance.FilterAlignmentCharacters(chars, EAlignment.Good);
                chars = Characters.Instance.FilterRealAlignmentCharacters(chars, EAlignment.Good);
                chars = Characters.Instance.FilterRealCharacterType(chars, ECharacterType.Villager);
                if (chars.Count > 0)
                {
                    pickedChar = chars[UnityEngine.Random.Range(0, chars.Count)];
                    foreach (Character c in Gameplay.CurrentCharacters)
                    {
                       if (c == pickedChar)
                        {
                            if (allDatas.Length == 0)
                            {
                                var loadedCharList = Resources.FindObjectsOfTypeAll(Il2CppType.Of<CharacterData>());
                                if (loadedCharList != null)
                               {
                                    allDatas = new CharacterData[loadedCharList.Length];
                                    for (int j = 0; j < loadedCharList.Length; j++)
                                    {
                                        allDatas[j] = loadedCharList[j]!.Cast<CharacterData>();
                                        c.statuses.AddStatus(ECharacterStatus.AlteredCharacter, charRef);
                                        c.statuses.AddStatus(ECharacterStatus.MessedUpByEvil, charRef);
                                    }
                                }
                            }
    
                            for (int j = 0; j < allDatas.Length; j++)
                            {
                                if (allDatas[j].characterId == "Swarm_Evil_WING")
                                {
                                    Gameplay.Instance.AddScriptCharacter(ECharacterType.Minion, allDatas[j]);
                                    if (c.GetRegisterAs().characterId != allDatas[j].characterId)
                                    {
                                        c.Init(allDatas[j]);
                                        break;
                                    }
                                }
                            }
                            break;
                        }
                    }
                }
            }
        }
        if (trigger == ETriggerPhase.Day)
        {
            this.onActed.Invoke(this.GetInfo(charRef));
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
            this.onActed.Invoke(this.GetBluffInfo(charRef));
        }
    }
    public override ActedInfo GetInfo(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<Character> characters = Gameplay.CurrentCharacters;
        System.Collections.Generic.List<Character> newList = new System.Collections.Generic.List<Character>();
        System.Collections.Generic.List<Character> newList2 = new System.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        Characters charInst = Characters.Instance;
        foreach (Character character in characters)
        {
            bool isSwarm = false;
            if (character.statuses != null)
            {
                isSwarm = (character.GetRegisterAs().name == "Swarm (Good)" || character.GetRegisterAs().name == "Swarm (Evil)" || character.GetRegisterAs().name == "Swarm");
            }
            if (!isSwarm)
            {
                newList.Add(character);
            }
            else
            {
                newList2.Add(character);
            }
        }
        string line = "Info";
        if (newList2.Count > 1 && newList.Count > 1)
        {
            Character random = newList[UnityEngine.Random.RandomRangeInt(0, newList.Count)];
            selection.Add(random);
            newList.Remove(random);
            Character random3 = newList[UnityEngine.Random.RandomRangeInt(0, newList.Count)];
            selection.Add(random3);
            Character random2 = newList2[UnityEngine.Random.RandomRangeInt(0, newList2.Count)];
            selection.Add(random2);
            newList2.Remove(random2);
            Character random4 = newList2[UnityEngine.Random.RandomRangeInt(0, newList2.Count)];
            selection.Add(random4);
            Il2CppSystem.Collections.Generic.List<Character> infoChars = new Il2CppSystem.Collections.Generic.List<Character>();
            // Time for a jank method of sorting this shit.
            for (int k = 0; k < 20; k++)
            {
                if (random.id == k)
                {
                    infoChars.Add(random);
                }
                if (random2.id == k)
                {
                    infoChars.Add(random2);
                }
                if (random3.id == k)
                {
                    infoChars.Add(random3);
                }
                if (random4.id == k)
                {
                    infoChars.Add(random4);
                }
            }
            line = string.Format("Two are Swarm:\n#{0}, #{1}, #{2}, #{3}", infoChars[0].id, infoChars[1].id, infoChars[2].id, infoChars[3].id);
        }
        else
        {
            //line = "I am the only Swarm.";
            line = "Something does not make sense";
            selection.Add(charRef);
        }

        ActedInfo actedInfo = new ActedInfo(line, selection);
        return actedInfo;
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();

        string line = string.Format("Something does not make sense");
        selection.Add(charRef);

        ActedInfo actedInfo = new ActedInfo(line, selection);
        return actedInfo;
    }
    public w_Swarm_Good() : base(ClassInjector.DerivedConstructorPointer<w_Swarm_Good>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Swarm_Good(System.IntPtr ptr) : base(ptr)
    {

    }
    public override CharacterData GetBluffIfAble(Character charRef)
    {
        return null;
    }
    }


