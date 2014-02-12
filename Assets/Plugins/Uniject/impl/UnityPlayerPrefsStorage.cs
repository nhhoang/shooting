//-----------------------------------------------------------------
//  Copyright 2013 Alex McAusland and Ballater Creations
//  All rights reserved
//  www.outlinegames.com
//-----------------------------------------------------------------
using System;
using UnityEngine;

namespace Uniject.Impl {
    public class UnityPlayerPrefsStorage : Uniject.IStorage {
        #region IStorage implementation
        public int GetInt (string key, int defaultValue) {
            return PlayerPrefs.GetInt(key, defaultValue);
        }
        public void SetInt (string key, int value) {
            PlayerPrefs.SetInt(key, value);
        }
        #endregion
    }
}

