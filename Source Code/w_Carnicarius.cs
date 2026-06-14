using HarmonyLib;
using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppSystem;
using MelonLoader;
using System;
using System.ComponentModel.Design;
using UnityEngine;
using static ExpansionPack.w_Caedoccidere;

namespace ExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Carnicarius : Demon
{
    bool firstNight = true;
    bool firstDamage = false;
    public override Il2CppSystem.Collections.Generic.List<SpecialRule> GetRules()
    {
        Il2CppSystem.Collections.Generic.List<SpecialRule> sr = new Il2CppSystem.Collections.Generic.List<SpecialRule>();
        sr.Add(new NightModeRule(4));
        return sr;
    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Init)
        {
            firstNight = true;
            firstDamage = false;
        }    
        if (trigger == ETriggerPhase.Night)
        {
            if (firstNight == true)
            {
                firstNight = false;
                Act(ETriggerPhase.Night, charRef);
            }
            if (charRef.state == ECharacterState.Dead) return;
            Health health = PlayerController.PlayerInfo.health;

            // Define "valuable" roles
            Il2CppSystem.Collections.Generic.List<string> valuableCharacters_IDs = new Il2CppSystem.Collections.Generic.List<string>(); // IDs
            Il2CppSystem.Collections.Generic.List<int> valuableCharacters_Prio = new Il2CppSystem.Collections.Generic.List<int>(); // Priorities
            Il2CppSystem.Collections.Generic.List<bool> valuableCharacters_OnlyHidden = new Il2CppSystem.Collections.Generic.List<bool>(); // No point killing revealed characters and confirming their info
            // Vanilla:
            // 5x: Bishop, Empress, Jester, Oracle
            // 4x: Fortune Teller, Hunter, Knitter, Lover
            // 3x: Confessor, Dreamer, Enlightened, Judge, Poet, Scout, Slayer

            // This mod:
            // 5x: Gravekeeper
            // 4x: Arbiter, Arithmetician, Chiromancer, Gossip, Prince, Ranger, Good Swarm
            // 3x: Bloodseer, Forager, Introvert

            // Riddles:
            // 5x: Coach, Comedian, Recruiter
            // 4x: Swapper, Mathematician, Commander, Director, Lawyer
            // 3x: Riddler, Obsessor, Psychic, Weaver, Stylist, Governor, Surveyor, Pioneer
            valuableCharacters_IDs.Add("Bishop_58855542");
            valuableCharacters_OnlyHidden.Add(true);
            valuableCharacters_IDs.Add("Empress_13782227");
            valuableCharacters_OnlyHidden.Add(true);
            valuableCharacters_IDs.Add("Jester_41367606");
            valuableCharacters_OnlyHidden.Add(false);
            valuableCharacters_IDs.Add("Oracle_07039445");
            valuableCharacters_OnlyHidden.Add(true);
            valuableCharacters_IDs.Add("Plague Doctor_49312486");
            valuableCharacters_OnlyHidden.Add(true);
            valuableCharacters_IDs.Add("Gravekeeper_WING");
            valuableCharacters_OnlyHidden.Add(false);
            valuableCharacters_IDs.Add("Comedian_scm");
            valuableCharacters_OnlyHidden.Add(false);
            valuableCharacters_IDs.Add("Recruiter_scm");
            valuableCharacters_OnlyHidden.Add(true);
            valuableCharacters_Prio.Add(12);
            valuableCharacters_Prio.Add(12);
            valuableCharacters_Prio.Add(12);
            valuableCharacters_Prio.Add(12);
            valuableCharacters_Prio.Add(12);
            valuableCharacters_Prio.Add(12);
            valuableCharacters_Prio.Add(12);
            valuableCharacters_Prio.Add(12);

            valuableCharacters_IDs.Add("Fortune Teller_74565681");
            valuableCharacters_OnlyHidden.Add(false);
            valuableCharacters_IDs.Add("Hunter_93427887");
            valuableCharacters_OnlyHidden.Add(true);
            valuableCharacters_IDs.Add("Knitter_32352172");
            valuableCharacters_OnlyHidden.Add(true);
            valuableCharacters_IDs.Add("Investigator_34015277");
            valuableCharacters_OnlyHidden.Add(false);
            valuableCharacters_IDs.Add("Lover_91302708");
            valuableCharacters_OnlyHidden.Add(true);
            valuableCharacters_IDs.Add("Arbiter_WING");
            valuableCharacters_OnlyHidden.Add(false);
            valuableCharacters_IDs.Add("Arithmetician_WING");
            valuableCharacters_OnlyHidden.Add(true);
            valuableCharacters_IDs.Add("Chiromancer_WING");
            valuableCharacters_OnlyHidden.Add(true);
            valuableCharacters_IDs.Add("Gossip_WING");
            valuableCharacters_OnlyHidden.Add(false);
            valuableCharacters_IDs.Add("Prince_WING");
            valuableCharacters_OnlyHidden.Add(true);
            valuableCharacters_IDs.Add("Ranger_WING");
            valuableCharacters_OnlyHidden.Add(true);
            valuableCharacters_IDs.Add("Swarm_Good_WING");
            valuableCharacters_OnlyHidden.Add(true);
            valuableCharacters_IDs.Add("Swapper_scm");
            valuableCharacters_OnlyHidden.Add(false);
            valuableCharacters_IDs.Add("Mathematician_scm");
            valuableCharacters_OnlyHidden.Add(true);
            valuableCharacters_IDs.Add("Commander_scm");
            valuableCharacters_OnlyHidden.Add(false);
            valuableCharacters_IDs.Add("Director_scm");
            valuableCharacters_OnlyHidden.Add(true);
            valuableCharacters_IDs.Add("Lawyer_scm");
            valuableCharacters_OnlyHidden.Add(true);
            valuableCharacters_IDs.Add("Coach_scm");
            valuableCharacters_OnlyHidden.Add(false);
            valuableCharacters_Prio.Add(8);
            valuableCharacters_Prio.Add(8);
            valuableCharacters_Prio.Add(8);
            valuableCharacters_Prio.Add(8);
            valuableCharacters_Prio.Add(8);
            valuableCharacters_Prio.Add(8);
            valuableCharacters_Prio.Add(8);
            valuableCharacters_Prio.Add(8);
            valuableCharacters_Prio.Add(8);
            valuableCharacters_Prio.Add(8);
            valuableCharacters_Prio.Add(8);
            valuableCharacters_Prio.Add(8);
            valuableCharacters_Prio.Add(8);
            valuableCharacters_Prio.Add(8);
            valuableCharacters_Prio.Add(8);
            valuableCharacters_Prio.Add(8);
            valuableCharacters_Prio.Add(8);
            valuableCharacters_Prio.Add(8);

            valuableCharacters_IDs.Add("Confessor_18741708");
            valuableCharacters_OnlyHidden.Add(true);
            valuableCharacters_IDs.Add("Dreamer_32014895");
            valuableCharacters_OnlyHidden.Add(false);
            valuableCharacters_IDs.Add("Enlightened_62576217");
            valuableCharacters_OnlyHidden.Add(true);
            valuableCharacters_IDs.Add("Judge_87202475");
            valuableCharacters_OnlyHidden.Add(false);
            valuableCharacters_IDs.Add("Gossip_85354100");
            valuableCharacters_OnlyHidden.Add(true);
            valuableCharacters_IDs.Add("Scout_88081716");
            valuableCharacters_OnlyHidden.Add(true);
            valuableCharacters_IDs.Add("Gambler_42592744");
            valuableCharacters_OnlyHidden.Add(false);
            valuableCharacters_IDs.Add("Bloodseer_WING");
            valuableCharacters_OnlyHidden.Add(false);
            valuableCharacters_IDs.Add("Forager_WING");
            valuableCharacters_OnlyHidden.Add(false);
            valuableCharacters_IDs.Add("Introvert_WING");
            valuableCharacters_OnlyHidden.Add(true);
            valuableCharacters_IDs.Add("Riddler_scm");
            valuableCharacters_OnlyHidden.Add(true);
            valuableCharacters_IDs.Add("Obsessor_scm");
            valuableCharacters_OnlyHidden.Add(true);
            valuableCharacters_IDs.Add("Psychic_scm");
            valuableCharacters_OnlyHidden.Add(true);
            valuableCharacters_IDs.Add("Weaver_scm");
            valuableCharacters_OnlyHidden.Add(true);
            valuableCharacters_IDs.Add("Stylist_scm");
            valuableCharacters_OnlyHidden.Add(false);
            valuableCharacters_IDs.Add("Governor_ehm");
            valuableCharacters_OnlyHidden.Add(true);
            valuableCharacters_IDs.Add("Surveyor_ehm");
            valuableCharacters_OnlyHidden.Add(true);
            valuableCharacters_IDs.Add("Pioneer_ehm");
            valuableCharacters_OnlyHidden.Add(true);
            valuableCharacters_Prio.Add(4);
            valuableCharacters_Prio.Add(4);
            valuableCharacters_Prio.Add(4);
            valuableCharacters_Prio.Add(4);
            valuableCharacters_Prio.Add(4);
            valuableCharacters_Prio.Add(4);
            valuableCharacters_Prio.Add(4);
            valuableCharacters_Prio.Add(4);
            valuableCharacters_Prio.Add(4);
            valuableCharacters_Prio.Add(4);
            valuableCharacters_Prio.Add(4);
            valuableCharacters_Prio.Add(4);
            valuableCharacters_Prio.Add(4);
            valuableCharacters_Prio.Add(4);
            valuableCharacters_Prio.Add(4);
            valuableCharacters_Prio.Add(4);
            valuableCharacters_Prio.Add(4);
            valuableCharacters_Prio.Add(4);


            int corruptMult = 2; // Multiplier for Pure characters, because leaving Corrupted characters alive is good I think
            int outsBluffMult = 4; // Multiplier for unrevealed face-up Outcasts.
            int copyBluffMult = 7; // Multiplier for roles that're being used as Disguises by roles like Doppelganger, Zealot, Copycat, Turncoat, etc
            Il2CppSystem.Collections.Generic.List<string> backupRoles = new Il2CppSystem.Collections.Generic.List<string>(); // Roles to back up by doing this
            backupRoles.Add("Doppleganger_52694042");
            backupRoles.Add("Zealot_WING");
            backupRoles.Add("Turncoat_WING");
            backupRoles.Add("Copycat_WING");
            Il2CppSystem.Collections.Generic.List<string> usefulOutcasts = new Il2CppSystem.Collections.Generic.List<string>(); // Outcasts that would be better kept alive and are therefore excluded from the relevant multiplier
            usefulOutcasts.Add("Bombardier_79093372");
            usefulOutcasts.Add("Chatterbox_WING");
            Il2CppSystem.Collections.Generic.List<string> copyBluffs = new Il2CppSystem.Collections.Generic.List<string>();
            foreach (Character character in Gameplay.CurrentCharacters)
            {
                if (backupRoles.Contains(character.dataRef.characterId) && character.bluff)
                {
                    copyBluffs.Add(character.bluff.characterId);
                }
            }


            Il2CppSystem.Collections.Generic.List<Character> revealedChars = Characters.Instance.FilterRevealedCharacters(Gameplay.CurrentCharacters);

            Il2CppSystem.Collections.Generic.List<Character> validTargets = new Il2CppSystem.Collections.Generic.List<Character>();
            // Now let's choose our targets.
            Il2CppSystem.Collections.Generic.List<string> killReasons = new Il2CppSystem.Collections.Generic.List<string>();
            for (int i = 0; i < 100; i++)
            {
                killReasons.Add("");
            }
            string killReason = "";
            int multiplier = 1;
            int listNum = 0;
            foreach (Character character in Gameplay.CurrentCharacters)
            {
                killReason = $"#{character.id} was my target. They were Good.";
                multiplier = 1;
                if (character.GetRegisterAlignment() == EAlignment.Good && character.GetRealAlignment() == EAlignment.Good && character.state != ECharacterState.Dead && !checkIfDead(character)) // Only look at them if they're Good (and alive, for that matter)
                {
                    if (!character.statuses.Contains(ECharacterStatus.Corrupted))
                    {
                        multiplier *= corruptMult; // Pure characters are more likely to be killed
                        killReason += " They were Pure.";
                    }
                    if (character.GetCharacterType() == ECharacterType.Outcast && character.dataRef.type == ECharacterType.Outcast && !character.bluff && !revealedChars.Contains(character) && !usefulOutcasts.Contains(character.dataRef.characterId))
                    {
                        multiplier *= outsBluffMult; // Face-up Outcasts are more likely to be killed
                        killReason += " They were an unrevealed face-up Outcast.";
                    }
                    if (copyBluffs.Contains(character.dataRef.characterId) && !revealedChars.Contains(character))
                    {
                        killReason += " I was backing up someone's bluff.";
                        multiplier *= copyBluffMult;
                    }
                    if (valuableCharacters_IDs.Contains(character.dataRef.characterId))
                    {
                        listNum = valuableCharacters_IDs.IndexOf(character.dataRef.characterId);
                        if ((valuableCharacters_OnlyHidden[listNum] == true && !revealedChars.Contains(character)) || valuableCharacters_OnlyHidden[listNum] == false)
                        {
                            multiplier *= valuableCharacters_Prio[listNum] * 2;
                            if (valuableCharacters_Prio[listNum] != 1) killReason += $" They were a valuable target with a priority level of {valuableCharacters_Prio[listNum]}.";
                        }
                    }
                    killReason += $" In total, my choice to kill them had a weight of {multiplier}.";
                }
                else
                {
                    multiplier = 0;
                }
                if (character.statuses.Contains(ECharacterStatus.UnkillableByDemon))
                {
                    multiplier = 0;
                }
                if (multiplier != 0)
                {
                    for (int i = 0; i < multiplier; i++)
                    {
                        validTargets.Add(character);
                    }
                }
                killReasons[character.id] = killReason;
            }


            //wx_SavedScripts sharedScripts = new wx_SavedScripts();
            if (firstDamage)
            {
                health.Damage(2);
            }
            else
            {
                firstDamage = true;
            }
            if (!(validTargets.Count == 0))
            {
                Character myTarget = validTargets[UnityEngine.Random.Range(0, validTargets.Count)];
                myTarget.statuses.AddStatus(ECharacterStatus.KilledByEvil, charRef);
                myTarget.statuses.AddStatus(ECharacterStatus.MessedUpByEvil, charRef);
                if (revealedChars.Contains(myTarget)) myTarget.statuses.AddStatus(CarniKill.carniKill, charRef);
                myTarget.KillByDemon(charRef);
                myTarget.pickable.SetActive(false);
                MelonLogger.Msg($"Killed the {myTarget.dataRef.characterName} at #{myTarget.id}");
                //MelonLogger.Msg($"The target list was: {sharedScripts.MentionEveryCharacterInList(validTargets, "")}");
                MelonLogger.Msg($"The reason was as follows: {killReasons[myTarget.id]}");
            }
        }
    }
    public w_Carnicarius() : base(ClassInjector.DerivedConstructorPointer<w_Carnicarius>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Carnicarius(System.IntPtr ptr) : base(ptr)
    {

    }
    public override CharacterData GetBluffIfAble(Character charRef)
    {
        CharacterData bluff = Characters.Instance.GetRandomUniqueVillagerBluff();
        Gameplay.Instance.AddScriptCharacterIfAble(bluff.type, bluff);

        return bluff;
    }


    public bool checkIfDead(Character character)
    {
        if (character.GetState() == ECharacterState.Dead) return true;
        if (character.statuses.Contains(ECharacterStatus.KilledByEvil)) return true;
        if (character.statuses.Contains(CarniKill.carniKill)) return true;
        return false;
    }

    public static class CarniKill
    {
        public static ECharacterStatus carniKill = (ECharacterStatus)311814931;
        [HarmonyPatch(typeof(Character), nameof(Character.ShowDescription))]
        public static class ChangeKillByDemonText
        {
            public static void Postfix(Character __instance)
            {
                if (__instance.killedByDemon && __instance.statuses.Contains(carniKill))
                {
                    HintInfo info = new HintInfo();
                    info.text = "Killed by a <color=#FF9999>Demon</color>.\nCannot use abilities.\nTrue Role is not revealed.";
                    UIEvents.OnShowHint.Invoke(info, __instance.hintPivot);
                }
            }
        }
    }


}


