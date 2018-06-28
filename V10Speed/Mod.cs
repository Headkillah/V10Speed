/* 
 * Copyright(c) 2015 Alexander Dzhoganov
 * Copyright(c) 2018 Thomas Rohloff <v10lator@myway.de>
 * 
 * Licensed under the MIT
 */

using ColossalFramework.UI;
using ICities;
using UnityEngine;

namespace V10Speed
{

    public class Mod : IUserMod
    {

        public string Name
        {
            get { return "V10Speed 0.1"; }
        }

        public string Description
        {
            get { return "More speed options"; }
        }

    }
    public class ModLoad : LoadingExtensionBase
    {

        private Hook hook;
        
        public override void OnLevelLoaded(LoadMode mode)
        {
            var uiView = GameObject.FindObjectOfType<UIView>();
            hook = uiView.gameObject.AddComponent<Hook>();
        }

        public override void OnLevelUnloading()
        {
            GameObject.Destroy(hook);
        }
    }

}
