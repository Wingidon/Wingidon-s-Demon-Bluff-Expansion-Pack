using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppInterop.Runtime.Runtime;
using Il2CppSystem;
using MelonLoader;
using System;
using System.ComponentModel.Design;
using UnityEngine;
using Il2CppTMPro;
using HarmonyLib;

namespace WingidonExtraKeywords;
public class wx_KeywordPatch // Code by Skill Cycler, absolute legend!
{
    public string PatchTooltip(string tooltip)
    {
        string value = tooltip;
        if (value != null)
        {
            Il2CppSystem.Collections.Generic.List<string> replaceCheckStrings = new();
            Il2CppSystem.Collections.Generic.List<string> replaceReplacements = new();
            Il2CppSystem.Collections.Generic.List<string> replaceReplacementLink = new();
            Il2CppSystem.Collections.Generic.List<string> replaceColour = new();

            // Trust
            replaceCheckStrings.Add("Trustworthy");
            replaceReplacements.Add("Key1");
            replaceReplacementLink.Add("");
            replaceColour.Add("");

            replaceCheckStrings.Add("Trustworthiness");
            replaceReplacements.Add("Key2");
            replaceReplacementLink.Add("");
            replaceColour.Add("");

            replaceCheckStrings.Add("Trust");
            replaceReplacements.Add("Trust");
            replaceReplacementLink.Add("Link_T-rustworthy");
            replaceColour.Add("9999FF");

            replaceCheckStrings.Add("Key1");
            replaceReplacements.Add("Trustworthy");
            replaceReplacementLink.Add("Link_T-rustworthy");
            replaceColour.Add("9999FF");

            replaceCheckStrings.Add("Key2");
            replaceReplacements.Add("Trustworthiness");
            replaceReplacementLink.Add("Link_T-rustworthy");
            replaceColour.Add("9999FF");


            // Poison
            replaceCheckStrings.Add("LPoisoning");
            replaceReplacements.Add("Key1ing");
            replaceReplacementLink.Add("");
            replaceColour.Add("");
            replaceCheckStrings.Add("Poisoned");
            replaceReplacements.Add("Key1ed");
            replaceReplacementLink.Add("");
            replaceColour.Add("");
            replaceCheckStrings.Add("LPoisoner");
            replaceReplacements.Add("Key1er");
            replaceReplacementLink.Add("");
            replaceColour.Add("");
            replaceCheckStrings.Add("LPoisons");
            replaceReplacements.Add("Key1s");
            replaceReplacementLink.Add("");
            replaceColour.Add("");
            replaceCheckStrings.Add("LPoison");
            replaceReplacements.Add("Poison");
            replaceReplacementLink.Add("Link_P-oison");
            replaceColour.Add("3F8538");
            replaceCheckStrings.Add("Key1ing");
            replaceReplacements.Add("Poisoning");
            replaceReplacementLink.Add("Link_P-oison");
            replaceColour.Add("3F8538");
            replaceCheckStrings.Add("Key1ed");
            replaceReplacements.Add("Poisoned");
            replaceReplacementLink.Add("Link_P-oison");
            replaceColour.Add("3F8538");
            replaceCheckStrings.Add("Key1er");
            replaceReplacements.Add("Poisoner");
            replaceReplacementLink.Add("Link_P-oison");
            replaceColour.Add("3F8538");
            replaceCheckStrings.Add("Key1s");
            replaceReplacements.Add("Poisons");
            replaceReplacementLink.Add("Link_P-oison");
            replaceColour.Add("3F8538");


            // Misled/Hypnotised
            replaceCheckStrings.Add("Mislead");
            replaceReplacements.Add("Mislead");
            replaceReplacementLink.Add("Link_M-isled");
            replaceColour.Add("FF00AE");
            replaceCheckStrings.Add("Misled");
            replaceReplacements.Add("Misled");
            replaceReplacementLink.Add("Link_M-isled");
            replaceColour.Add("FF00AE");

            replaceCheckStrings.Add("Hypnotised");
            replaceReplacements.Add("Key1");
            replaceReplacementLink.Add("");
            replaceColour.Add("");
            replaceCheckStrings.Add("Hypnotized"); // MURICA
            replaceReplacements.Add("Key1z");
            replaceReplacementLink.Add("");
            replaceColour.Add("");
            replaceCheckStrings.Add("Hypnotise");
            replaceReplacements.Add("Hypnotise");
            replaceReplacementLink.Add("Link_H-ypnotised");
            replaceColour.Add("FF00AE");
            replaceCheckStrings.Add("Hypnotize");
            replaceReplacements.Add("Hypnotise");
            replaceReplacementLink.Add("Link_H-ypnotised");
            replaceColour.Add("FF00AE");
            replaceCheckStrings.Add("Key1z");
            replaceReplacements.Add("Hypnotised");
            replaceReplacementLink.Add("Link_H-ypnotised");
            replaceColour.Add("FF00AE");
            replaceCheckStrings.Add("Key1");
            replaceReplacements.Add("Hypnotised");
            replaceReplacementLink.Add("Link_H-ypnotised");
            replaceColour.Add("FF00AE");
            replaceCheckStrings.Add("Hypnosis");
            replaceReplacements.Add("Hypnosis");
            replaceReplacementLink.Add("Link_H-ypnotised");
            replaceColour.Add("FF00AE");




            bool shouldReplaceColour = false;
            bool shouldReplaceLink = false;
            for (int i = 0; i < replaceCheckStrings.Count; i++)
            {
                //MelonLogger.Msg($"Replacing {replaceCheckStrings[i]} with {replaceReplacements[i]}");
                shouldReplaceLink = (replaceReplacementLink[i] != "");
                shouldReplaceColour = (replaceColour[i] != "");
                string replacementValue = "";
                if (shouldReplaceLink) replacementValue = $"<link=\"{replaceReplacementLink[i]}\">";
                if (shouldReplaceColour) replacementValue += $"<color=#{replaceColour[i]}>";
                replacementValue += $"{replaceReplacements[i]}";
                if (shouldReplaceColour) replacementValue += $"</color>";
                if (shouldReplaceLink) replacementValue += $"</link>";
                if (value.Contains(replaceCheckStrings[i]))
                {
                    value = value.Replace(
                            $"{replaceCheckStrings[i]}",
                            $"{replacementValue.ToString()}"
                        );
                }
            }
            return value;
        }
        return "We've got problems!";
    }
    [HarmonyPatch(typeof(TextTooltipRecognizer), "GetTooltipInfo")]
    public static class TooltipPatch
    {
        static void Postfix(string linkID, ref TooltipInfo __result)
        {
            wx_KeywordPatch patcher = new();
            if (linkID == "Link_T-rustworthy")
            {
                __result = new TooltipInfo(
                    patcher.PatchTooltip($"A measure of how much you can Trust a character." +
                    $"\nVillagers, Outcasts, Minions and Demons are 5x, 3x, 3x and 1x as Trustworthy respectively." +
                    $"\nGood characters are 3x as Trustworthy.\nTruthful characters are 3x as Trustworthy." +
                    $"\nHonest characters are 2.5x as Trustworthy."),
                    "Trust",
                    new Color32(153, 153, 255, 255)
                );
            }
            if (linkID == "Link_P-oison")
            {
                __result = new TooltipInfo(
                    patcher.PatchTooltip($"Poisoned characters are Corrupted and act as such. After a specified number of Reveals, they die." +
                    $"\n\nRoles like the Alchemist can typically cure the Corruption, but can't stop the LPoison from killing the victim."),
                    "Poison",
                    new Color32(63, 133, 56, 255)
                );
            }
            if (linkID == "Link_M-isled")
            {
                __result = new TooltipInfo(
                    patcher.PatchTooltip($"This character has been Corrupted and turned Evil!" +
                    $"\n\nLike most Evil characters, this character will Lie, so finding them won't be too difficult. Just be careful of false-negatives when someone checks their Role or Type."),
                    "Misled",
                    new Color32(255, 0, 174, 255)
                );
            }
            if (linkID == "Link_H-ypnotised")
            {
                __result = new TooltipInfo(
                    patcher.PatchTooltip($"This character has been turned Evil!" +
                    $"\n\nUnlike most Evil characters, this character <b>will not Lie</b>, and this will make them very difficult to find. Use your manually-activated abilities carefully and wisely - alignment checkers are your only hope."),
                    "Hypnosis",
                    new Color32(255, 0, 174, 255)
                );
            }
        }
    }
}
