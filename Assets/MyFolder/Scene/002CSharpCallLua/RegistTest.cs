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

public class RegistTest : MonoBehaviour
{

    public class InfoData
    {
        public int Id;
        public string Title;
        public int State;
        public string Content;

        public InfoData(int id, string title, int state, string content)
        {
            this.Id = id;
            this.Title = title;
            this.State = state;
            this.Content = content;
        }
    }


    private List<InfoData> list = new List<InfoData>();
    public GameObject itemPrefab;
    public Transform parent;

    public Text txt_Show;
    void  Start()
    {
        list.Add ( new InfoData(1001, "牛刀小试", 0, "少侠我看你骨骼精奇，这有本如来神掌10块钱卖给你如何"));
        list.Add ( new InfoData(1002, "武器考验", 0, "试试这把无尽之刃吧,简直是神器"));
        list.Add ( new InfoData(1003, "技能传授", 0, "看我神威,无坚不摧"));
        list.Add ( new InfoData(1004, "修为精进", 0, "修为精进,一日千里,真是万中无一的绝世高手"));
        list.Add ( new InfoData(1005, "强化武器", 0, "一把无尽不大够,再来一把电刀如何..."));
        list.Add ( new InfoData(1006, "魔法抗性", 0, "现在你需要一件幽魂斗篷来保护自己"));
        list.Add (new InfoData(1007, "物理抗性", 0, "没有比荆棘之甲更能保护你了"));
        list.Add ( new InfoData(1008, "春哥馈赠", 0, "信春哥得永生 ,春哥宝甲送给你了,可以复活哦"));
        list.Add ( new InfoData(1009, "移速暴增", 0, "穿上轻灵之靴,健步如飞,日行千里"));


        for (int i = 0; i < list.Count; i++)
        {
            int index = i ;
            GameObject oo = Instantiate(itemPrefab);
            oo.transform.SetParent(parent);
            Toggle tog = oo.GetComponent<Toggle>();
            tog.group = parent.GetComponent<ToggleGroup>();
            tog.transform.Find("Label").GetComponent<Text>().text = list[index].Title;
            tog.onValueChanged.AddListener((isOn) =>
            {
                if (isOn)
                {
                    txt_Show.text = list[index].Content;
                }
            });


        }

    }



}