/*
 *		Description: 
 *
 *		CreatedBy: guoShuai
 *
 *		DataTime: #DATE#
 *
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XLua;

namespace XluaHotFixTest
{
    [Hotfix]
    public class ShowHotfix : MonoBehaviour
    {
        public Transform cube;

        private Text txt;

        private void Start()
        {
            txt = transform.Find("txt").GetComponent<Text>();
        }

        [LuaCallCSharp]
        public void ShowFix()
        {
            txt.text = "我没有热更";
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ShowFix();
            }
        }
    }
}