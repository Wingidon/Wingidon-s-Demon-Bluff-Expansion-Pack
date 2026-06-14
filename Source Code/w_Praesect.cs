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

namespace ExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Praesect : Demon
{
    public CharacterData[] allDatas = Il2CppSystem.Array.Empty<CharacterData>();
    public Il2CppSystem.Collections.Generic.List<Il2Cpp.CharacterData> scriptCharacters = Gameplay.Instance.GetScriptCharacters();
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Start)
        {
            Il2CppSystem.Collections.Generic.List<Character> chars = new Il2CppSystem.Collections.Generic.List<Character>(Gameplay.CurrentCharacters.Pointer);
            Character pickedChar = new Character();
                chars = Characters.Instance.FilterCharacterType(chars, ECharacterType.Villager);
                chars = Characters.Instance.FilterAlignmentCharacters(chars, EAlignment.Good);
                if (chars.Count > 0)
                {
                    pickedChar = chars[UnityEngine.Random.Range(0, chars.Count)];
                    chars.Remove(pickedChar);
                    Character pickedChar2 = chars[UnityEngine.Random.Range(0, chars.Count)];
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
                            if (allDatas[j].characterId == "Acolyte_WING")
                            {
                                if (c.GetRegisterAs().characterId != allDatas[j].characterId)
                                {
                                    c.Init(allDatas[j]);
                                }
                            }
                        }
                    }
                    if (c == pickedChar2)
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
                            if (allDatas[j].characterId == "Zealot_WING")
                            {
                                if (c.GetRegisterAs().characterId != allDatas[j].characterId)
                                {
                                    c.Init(allDatas[j]);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    public override void ActOnDied(Character charRef)
    {
        if (charRef.statuses.Contains(ECharacterStatus.KilledByEvil)) return;
        Health health = PlayerController.PlayerInfo.health;
        int aliveAcolytes = 0;
        foreach (Character character in Characters.Instance.FilterAliveCharacters(Gameplay.CurrentCharacters))
        {
            if (character.dataRef.characterId == "Acolyte_WING" || character.dataRef.characterId == "Zealot_WING" || character.dataRef.characterId == "Fanatic_WING")
            {
                aliveAcolytes++;
            }
        }
        health.Damage(aliveAcolytes * 2);
    }
    public w_Praesect() : base(ClassInjector.DerivedConstructorPointer<w_Praesect>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Praesect(System.IntPtr ptr) : base(ptr)
    {

    }
    public override CharacterData GetBluffIfAble(Character charRef)
    {
        CharacterData bluff = Characters.Instance.GetRandomUniqueVillagerBluff();
        bluff = Characters.Instance.GetRandomUniqueVillagerBluff();
        Gameplay.Instance.AddScriptCharacterIfAble(bluff.type, bluff);
        return bluff;
    }
    }


