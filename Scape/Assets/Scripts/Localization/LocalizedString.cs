using System;
using Sirenix.Serialization;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Localization
{

    [Serializable]
    public class LocalizedString
    {

        public string tag;

        public string GetText()
        {
            //TODO implement retrieve localized text
            return tag;
        }
    }
}