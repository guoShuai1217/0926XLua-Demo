/*
 *		Description: 
 *
 *		CreatedBy: guoShuai
 *
 *		DataTime: #DATE#
 *
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using XLua;

namespace XluaHotFixTest
{

    public class LuaMgr : MonoBehaviour
    {
        public static LuaMgr Instance;

        private LuaEnv luaenv;
        private void Awake()
        {
            Instance = this;
            luaenv = new LuaEnv();
            luaenv.AddLoader(MyLoader);
            luaenv.DoString("require 'hotfix'");
        }

        private byte[] MyLoader(ref string filepath)
        {
            string absPath = Application.streamingAssetsPath + "/" + filepath + ".lua.txt";
            return File.ReadAllBytes(absPath);
        }



        private void OnDestroy()
        {
            luaenv.Dispose();
        }


    }


}