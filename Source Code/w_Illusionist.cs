using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppSystem;
using MelonLoader;
using System;
using System.ComponentModel.Design;
using UnityEngine;
using static Il2CppSystem.Collections.SortedList;

namespace WingidonExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Illusionist : Role
{
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Init)
        {
            // new wx_SavedScripts().DebugMessage($"Initialised {charRef.dataRef.characterName} at #{charRef.id}");
        }
        if (trigger == ETriggerPhase.Start)
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
                if (!disguising && character.alignment == EAlignment.Good && character.GetCharacterData().type == ECharacterType.Villager)
                {
                    newList.Add(character);
                }
            }
            Character targetChar = newList[UnityEngine.Random.RandomRangeInt(0, newList.Count)];
            CharacterData bluff = charRef.dataRef;
            Debug.Log(string.Format("Created Illusionist clone at #{0} using bluff of {1}", targetChar.id, bluff.characterId));
            targetChar.GiveBluff(bluff);
            targetChar.statuses.AddStatus(ECharacterStatus.MessedUpByEvil, charRef);


            Il2CppSystem.Collections.Generic.List<CharacterData> notInPlayCh = Gameplay.Instance.GetAscensionAllStartingCharacters();
            notInPlayCh = Characters.Instance.FilterNotInDeckCharactersUnique(notInPlayCh);
            notInPlayCh = Characters.Instance.FilterCharacterType(notInPlayCh, ECharacterType.Villager);

            CharacterData data = null;
            if (notInPlayCh.Count == 0)
            {
                notInPlayCh = Gameplay.Instance.GetAllAscensionCharacters();
                notInPlayCh = Characters.Instance.FilterCharacterType(notInPlayCh, ECharacterType.Villager);
            }

            data = notInPlayCh[UnityEngine.Random.Range(0, notInPlayCh.Count - 1)];

            Gameplay.Instance.AddScriptCharacter(data.type, data);

            notInPlayCh = Gameplay.Instance.GetAscensionAllStartingCharacters();
            notInPlayCh = Characters.Instance.FilterNotInDeckCharactersUnique(notInPlayCh);
            notInPlayCh = Characters.Instance.FilterCharacterType(notInPlayCh, ECharacterType.Minion);
            data = notInPlayCh[UnityEngine.Random.Range(0, notInPlayCh.Count - 1)];
            Gameplay.Instance.AddScriptCharacter(data.type, data);
            notInPlayCh.Remove(data);
        }
        if (trigger == ETriggerPhase.OnExecuted)
        {
            if (charRef.dataRef.characterId != "Illusionist_WING")
            {
                Health health = PlayerController.PlayerInfo.health;
                health.Damage(3);
            }
        }
    }
    public override void BluffAct(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Init)
        {
            // new wx_SavedScripts().DebugMessage($"Initialised {charRef.dataRef.characterName} at #{charRef.id}");
        }
        if (trigger == ETriggerPhase.Start)
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
                if (!disguising && character.alignment == EAlignment.Good && character.GetCharacterData().type == ECharacterType.Villager)
                {
                    newList.Add(character);
                }
            }
            Character targetChar = newList[UnityEngine.Random.RandomRangeInt(0, newList.Count)];
            CharacterData bluff = charRef.dataRef;
            Debug.Log(string.Format("Created Illusionist clone at #{0} using bluff of {1}", targetChar.id, bluff.characterId));
            targetChar.GiveBluff(bluff);
            targetChar.statuses.AddStatus(ECharacterStatus.MessedUpByEvil, charRef);
            targetChar.statuses.AddStatus(EmenveraxIllusion.emenveraxIllusion, charRef);


            Il2CppSystem.Collections.Generic.List<CharacterData> notInPlayCh = Gameplay.Instance.GetAscensionAllStartingCharacters();
            notInPlayCh = Characters.Instance.FilterNotInDeckCharactersUnique(notInPlayCh);
            notInPlayCh = Characters.Instance.FilterCharacterType(notInPlayCh, ECharacterType.Villager);

            CharacterData data = null;
            if (notInPlayCh.Count == 0)
            {
                notInPlayCh = Gameplay.Instance.GetAllAscensionCharacters();
                notInPlayCh = Characters.Instance.FilterCharacterType(notInPlayCh, ECharacterType.Villager);
            }

            data = notInPlayCh[UnityEngine.Random.Range(0, notInPlayCh.Count - 1)];

            Gameplay.Instance.AddScriptCharacter(data.type, data);

            notInPlayCh = Gameplay.Instance.GetAscensionAllStartingCharacters();
            notInPlayCh = Characters.Instance.FilterNotInDeckCharactersUnique(notInPlayCh);
            notInPlayCh = Characters.Instance.FilterCharacterType(notInPlayCh, ECharacterType.Minion);
            data = notInPlayCh[UnityEngine.Random.Range(0, notInPlayCh.Count - 1)];
            Gameplay.Instance.AddScriptCharacter(data.type, data);
            notInPlayCh.Remove(data);
        }
        if (trigger == ETriggerPhase.OnExecuted)
        {
            if (charRef.dataRef.characterId != "Illusionist_WING")
            {
                Health health = PlayerController.PlayerInfo.health;
                health.Damage(3);
            }
        }
    }
    public w_Illusionist() : base(ClassInjector.DerivedConstructorPointer<w_Illusionist>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Illusionist(System.IntPtr ptr) : base(ptr)
    {

    }
    public override CharacterData GetBluffIfAble(Character charRef)
    {
        return null;
    }
    public static class EmenveraxIllusion // Used to prevent Disguiser and Emenverax from affecting the same character.
    {
        public static ECharacterStatus emenveraxIllusion = (ECharacterStatus)542;
    }
}


