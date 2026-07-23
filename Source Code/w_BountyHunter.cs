using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using MelonLoader;
using System;
using static UnityEngine.GraphicsBuffer;

namespace WingidonExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_BountyHunter : Role
{
    CharacterData bounty = new CharacterData();
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.AfterRoundStart)
        {
            wx_SavedScripts sharedScripts = new wx_SavedScripts();
            sharedScripts.DebugMessage($"Bounty Hunter at #{charRef.id} acting AfterRoundStart");
            Il2CppSystem.Collections.Generic.List<CharacterData> possibleBounties = new Il2CppSystem.Collections.Generic.List<CharacterData>();
            foreach (Character character in Gameplay.CurrentCharacters)
            {
                if (GetValidTypes().Contains(character.GetRegisterAs().type) && character.GetRegisterAlignment() == EAlignment.Evil)
                {
                    possibleBounties.Add(character.GetRegisterAs());
                }
            }
            if (possibleBounties.Count == 0)
            {
                bounty = null;
                sharedScripts.DebugMessage($"Bounty Hunter at #{charRef.id} found no bounties! What's going on?");
            }
            else
            {
                sharedScripts.DebugMessage($"Possible bounties: {sharedScripts.MentionEveryRoleInList(possibleBounties, "")}");
                bounty = possibleBounties[UnityEngine.Random.RandomRangeInt(0, possibleBounties.Count)];
                sharedScripts.DebugMessage($"Bounty Hunter at #{charRef.id} hunting {bounty.characterName}");
            }
        }
        if (trigger == wx_SavedScripts.w_AnyRevealPatch.SelfReveal)
        {
            wx_SavedScripts sharedScripts = new wx_SavedScripts();
            sharedScripts.DebugMessage($"Bounty Hunter at #{charRef.id} acting on SelfReveal");
            if (bounty == null)
            {
                OnActed(ETriggerPhase.Day, charRef, new ActedInfo("Something does not make sense"));
                charRef.pickable.SetActive(false);
                charRef.pickableUses = 0;
            }
            else
            {
                OnActed(ETriggerPhase.Day, charRef, new ActedInfo($"I'm hunting {sharedScripts.CheckIfThe(bounty.characterName)}{bounty.characterName}"));
            }
        }
        if (trigger == ETriggerPhase.Day)
        {
            wx_SavedScripts sharedScripts = new wx_SavedScripts();
            Health health = PlayerController.PlayerInfo.health;
            if (bounty == null)
            {
                charRef.pickable.SetActive(false);
                charRef.pickableUses = 0;
                OnActed(ETriggerPhase.Day, charRef, new ActedInfo("Something does not make sense"));
            }
            else
            {
                Il2CppSystem.Collections.Generic.List<Character> bountyCharacters = new();
                foreach (Character character in Gameplay.CurrentCharacters)
                {
                    if (character.GetRegisterAlignment() == EAlignment.Evil && GetValidTypes().Contains(character.GetRegisterAs().type) && character.GetRegisterAs().characterName == bounty.characterName)
                    {
                        bountyCharacters.Add(character);
                    }
                }
                if (bountyCharacters.Count == 0)
                {
                    charRef.pickable.SetActive(false);
                    charRef.pickableUses = 0;
                    OnActed(ETriggerPhase.Day, charRef, new ActedInfo($"Seems like {sharedScripts.CheckIfThe(bounty.characterName)}{bounty.characterName} fled from town"));
                }
                else
                {
                    Character target = bountyCharacters[UnityEngine.Random.RandomRangeInt(0, bountyCharacters.Count)];
                    health.Damage(2);
                    OnActed(ETriggerPhase.Day, charRef, sharedScripts.ReturnInfoWithSingleSelection($"#{target.id} is {sharedScripts.CheckIfThe(bounty.characterName)}{bounty.characterName}", target));
                }
            }
        }
    }
    public override void BluffAct(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.AfterRoundStart)
        {
            wx_SavedScripts sharedScripts = new wx_SavedScripts();
            sharedScripts.DebugMessage($"Bounty Hunter at #{charRef.id} bluff-acting AfterRoundStart");
            Il2CppSystem.Collections.Generic.List<CharacterData> possibleBounties = new();
            /*
            foreach (CharacterData character in Gameplay.Instance.GetScriptCharactersOfAlignment(EAlignment.Evil))
            {
                if (GetValidTypes().Contains(character.type)) possibleBounties.Add(character);
            }
            foreach (CharacterData character in sharedScripts.GetPossibleHiddenRoles())
            {
                if (GetValidTypes().Contains(character.type) && character.startingAlignment == EAlignment.Evil) possibleBounties.Add(character);
            }
            */
            foreach (Character character in Gameplay.CurrentCharacters)
            {
                if (GetValidTypes().Contains(character.GetRegisterAs().type) && character.GetRegisterAlignment() == EAlignment.Evil)
                {
                    possibleBounties.Add(character.GetRegisterAs());
                }
            }
            if (possibleBounties.Count == 0)
            {
                bounty = null;
                sharedScripts.DebugMessage($"Bounty Hunter at #{charRef.id} found no bounties! What's going on?");
            }
            else
            {
                sharedScripts.DebugMessage($"Possible bounties: {sharedScripts.MentionEveryRoleInList(possibleBounties, "")}");
                bounty = possibleBounties[UnityEngine.Random.RandomRangeInt(0, possibleBounties.Count)];
                sharedScripts.DebugMessage($"Bounty Hunter at #{charRef.id} bluff-hunting {bounty.characterName}");
            }
        }
        if (trigger == wx_SavedScripts.w_AnyRevealPatch.SelfReveal)
        {
            wx_SavedScripts sharedScripts = new wx_SavedScripts();
            sharedScripts.DebugMessage($"Bounty Hunter at #{charRef.id} bluff-acting on SelfReveal");
            if (bounty == null)
            {
                OnActed(ETriggerPhase.Day, charRef, new ActedInfo("Something does not make sense"));
                charRef.pickable.SetActive(false);
                charRef.pickableUses = 0;
            }
            else
            {
                OnActed(ETriggerPhase.Day, charRef, new ActedInfo($"I'm hunting {sharedScripts.CheckIfThe(bounty.characterName)}{bounty.characterName}"));
            }
        }
        if (trigger == ETriggerPhase.Day)
        {
            wx_SavedScripts sharedScripts = new wx_SavedScripts();
            Health health = PlayerController.PlayerInfo.health;
            if (bounty == null)
            {
                charRef.pickable.SetActive(false);
                charRef.pickableUses = 0;
                OnActed(ETriggerPhase.Day, charRef, new ActedInfo("Something does not make sense"));
            }
            else
            {
                Il2CppSystem.Collections.Generic.List<Character> bountyCharacters = new();
                Il2CppSystem.Collections.Generic.List<Character> nonBountyCharacters = new();
                foreach (Character character in Gameplay.CurrentCharacters)
                {
                    if (character.GetRegisterAlignment() == EAlignment.Evil && GetValidTypes().Contains(character.GetRegisterAs().type) && character.GetRegisterAs().characterName == bounty.characterName)
                    {
                        bountyCharacters.Add(character);
                    }
                    else
                    {
                        nonBountyCharacters.Add(character);
                    }
                }
                nonBountyCharacters.Remove(charRef);
                if (nonBountyCharacters.Count == 0)
                {
                    charRef.pickable.SetActive(false);
                    charRef.pickableUses = 0;
                    OnActed(ETriggerPhase.Day, charRef, new ActedInfo($"Seems like {sharedScripts.CheckIfThe(bounty.characterName)}{bounty.characterName} fled from town"));
                }
                else
                {
                    Character target = nonBountyCharacters[UnityEngine.Random.RandomRangeInt(0, nonBountyCharacters.Count)];
                    health.Damage(2);
                    OnActed(ETriggerPhase.Day, charRef, sharedScripts.ReturnInfoWithSingleSelection($"#{target.id} is {sharedScripts.CheckIfThe(bounty.characterName)}{bounty.characterName}", target));
                }
            }
        }
    }
    public override string Description
    {
        get
        {
            return "Learn an in-play character. Activate me to take damage & Learn who it is.";
        }
    }

    public void ForceAct(Character charRef, ActedInfo info)
    {
        charRef.actedInfos.Add(info);
        charRef.ShowActed(info, ETriggerPhase.Day);
    }

    public Il2CppSystem.Collections.Generic.List<ECharacterType> GetValidTypes()
    {
        Il2CppSystem.Collections.Generic.List<ECharacterType> returnList = new Il2CppSystem.Collections.Generic.List<ECharacterType>();
        returnList.Add(ECharacterType.Minion);
        returnList.Add(ECharacterType.Demon);
        return returnList;
    }
    public w_BountyHunter() : base(ClassInjector.DerivedConstructorPointer<w_BountyHunter>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_BountyHunter(IntPtr ptr) : base(ptr)
    {
    }
}


