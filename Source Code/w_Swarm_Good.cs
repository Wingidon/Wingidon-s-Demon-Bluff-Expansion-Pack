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
        wx_SavedScripts sharedScripts = new();
        Il2CppSystem.Collections.Generic.List<Character> swarm = new();
        Il2CppSystem.Collections.Generic.List<Character> nonSwarm = new();
        Il2CppSystem.Collections.Generic.List<Character> selection = new();

        foreach (Character character in Gameplay.CurrentCharacters)
        {
            if (character.GetRegisterAs().characterId == "Swarm_Good_WING" || character.GetRegisterAs().characterId == "Swarm_Evil_WING") swarm.Add(character);
            else nonSwarm.Add(character);
        }
        sharedScripts.DebugMessage($"Good Swarm (#{charRef.id}) found the following Swarm: {sharedScripts.MentionEveryCharacterInList(swarm, "")}");
        sharedScripts.DebugMessage($"...and the following non-Swarm: {sharedScripts.MentionEveryCharacterInList(nonSwarm, "")}");

        if (swarm.Count < 2 || nonSwarm.Count < 2) return new ActedInfo("Something does not make sense");

        selection.Add(swarm[UnityEngine.Random.RandomRangeInt(0, swarm.Count)]);
        swarm.Remove(selection[0]);
        selection.Add(swarm[UnityEngine.Random.RandomRangeInt(0, swarm.Count)]);

        nonSwarm.Remove(selection[0]);
        nonSwarm.Remove(selection[1]);
        selection.Add(nonSwarm[UnityEngine.Random.RandomRangeInt(0, nonSwarm.Count)]);
        nonSwarm.Remove(selection[2]);
        selection.Add(nonSwarm[UnityEngine.Random.RandomRangeInt(0, nonSwarm.Count)]);

        selection = sharedScripts.SortList(selection);

        return new ActedInfo($"Two are Swarm:\n{sharedScripts.MentionEveryCharacterInList(selection, "")}", selection);
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


