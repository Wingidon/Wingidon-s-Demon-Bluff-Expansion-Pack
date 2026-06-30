using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppSystem;
using MelonLoader;
using System;
using System.ComponentModel.Design;
using UnityEngine;

namespace ExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Empath : Role
{
    Character chRef;
    private Il2CppSystem.Action action1;
    private Il2CppSystem.Action action2;
    private Il2CppSystem.Action action3;
    public override ActedInfo GetInfo(Character charRef)
    {
        return new ActedInfo("", null);
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        return new ActedInfo("", null);
    }
    public override string Description
    {
        get
        {
            return "Learns which of two characters are more Trustworthy";
        }
    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger != ETriggerPhase.Day) return;
        chRef = charRef;
        CharacterPicker.Instance.StartPickCharacters(2, charRef);
        CharacterPicker.OnCharactersPicked += action1;
        CharacterPicker.OnStopPick += action2;
    }
    public override void BluffAct(ETriggerPhase trigger, Character charRef)
    {
        if (trigger != ETriggerPhase.Day) return;
        chRef = charRef;
        CharacterPicker.Instance.StartPickCharacters(2, charRef);
        CharacterPicker.OnCharactersPicked += action3;
        CharacterPicker.OnStopPick += action2;
    }
    private void CharacterPicked()
    {
        CharacterPicker.OnCharactersPicked -= action1;
        CharacterPicker.OnStopPick -= action2;

        Il2CppSystem.Collections.Generic.List<Character> chars = new Il2CppSystem.Collections.Generic.List<Character>();
        chars.Add(CharacterPicker.PickedCharacters[0]);
        chars.Add(CharacterPicker.PickedCharacters[1]);

        wx_SavedScripts sharedScripts = new wx_SavedScripts();
        chars = sharedScripts.SortList(chars);

        string info = "";

        float charOneTrust = sharedScripts.GetTrustworthiness(chars[0]);
        float charTwoTrust = sharedScripts.GetTrustworthiness(chars[1]);
        sharedScripts.DebugMessage($"Comparing trust of #{chars[0].id} ({charOneTrust}) and #{chars[1].id} ({charTwoTrust})");

        info = ConjureInfo(chars, "Equal");
        if (charOneTrust < charTwoTrust)
        {
            info = ConjureInfo(chars, "Second");
        }
        if (charOneTrust > charTwoTrust)
        {
            info = ConjureInfo(chars, "First");
        }

        sharedScripts.DebugMessage($"Final info: {info}");
        onActed?.Invoke(new ActedInfo(info, chars));
        Debug.Log($"{info}");
    }
    private void CharacterPickedLiar()
    {
        CharacterPicker.OnCharactersPicked -= action3;
        CharacterPicker.OnStopPick -= action2;

        Il2CppSystem.Collections.Generic.List<Character> chars = new Il2CppSystem.Collections.Generic.List<Character>();
        chars.Add(CharacterPicker.PickedCharacters[0]);
        chars.Add(CharacterPicker.PickedCharacters[1]);

        wx_SavedScripts sharedScripts = new wx_SavedScripts();
        chars = sharedScripts.SortList(chars);

        string info = "";

        float charOneTrust = sharedScripts.GetTrustworthiness(chars[0]);
        float charTwoTrust = sharedScripts.GetTrustworthiness(chars[1]);
        sharedScripts.DebugMessage($"Comparing trust of #{chars[0].id} ({charOneTrust}) and #{chars[1].id} ({charTwoTrust})");

        Il2CppSystem.Collections.Generic.List<string> fakeTrusts = new Il2CppSystem.Collections.Generic.List<string>();
        if (charOneTrust != charTwoTrust)
        {
            fakeTrusts.Add("Equal");
        }
        if (charOneTrust <= charTwoTrust)
        {
            fakeTrusts.Add("First");
            fakeTrusts.Add("First");
            fakeTrusts.Add("First");
            fakeTrusts.Add("First");
        }
        if (charOneTrust >= charTwoTrust)
        {
            fakeTrusts.Add("Second");
            fakeTrusts.Add("Second");
            fakeTrusts.Add("Second");
            fakeTrusts.Add("Second");
        }

        string chosenFake = sharedScripts.GetRandomItemOfList(fakeTrusts);

        info = ConjureInfo(chars, chosenFake);

        sharedScripts.DebugMessage($"Final (false) info: {info}");
        onActed?.Invoke(new ActedInfo(info, chars));
        Debug.Log($"{info}");
    }
    private void StopPick()
    {
        CharacterPicker.OnCharactersPicked -= action1;
        CharacterPicker.OnStopPick -= action2;
        CharacterPicker.OnCharactersPicked -= action3;
    }

    public string ConjureInfo(Il2CppSystem.Collections.Generic.List<Character> characters, string betterTrust)
    {
        // betterTrust = First/Second/Equal

        string info = $"#{characters[0].id} and #{characters[1].id} are equally Trustworthy";
        if (betterTrust == "First") info = $"#{characters[0].id} is more Trustworthy than #{characters[1].id}";
        if (betterTrust == "Second") info = $"#{characters[0].id} is less Trustworthy than #{characters[1].id}";
        return info;
    }

    public w_Empath() : base(ClassInjector.DerivedConstructorPointer<w_Empath>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
        action1 = new System.Action(CharacterPicked);
        action2 = new System.Action(StopPick);
        action3 = new System.Action(CharacterPickedLiar);
    }
    public w_Empath(System.IntPtr ptr) : base(ptr)
    {
        action1 = new System.Action(CharacterPicked);
        action2 = new System.Action(StopPick);
        action3 = new System.Action(CharacterPickedLiar);
    }
}