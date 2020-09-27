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



public class MyCSharpCallLua : MonoBehaviour
{

    string str = @"
        a = 10

        b = 'Are you OK?'

        c = true 

        d = {
           f1 = 12, f2 = 34, 
           1, 2, 3,
           add = function(self, a, b) 
              print('d.add called')
              return a + b 
           end
        }

e = function()

    print('无参无返回值')
end


function f(a, b)

    print('a', a,'b', b)
	return a,b

end


function get_E()

	return e

end";


    [CSharpCallLua]
    public interface IGetD
    {
        int f1 { get; set; }
        int f2 { get; set; }
        int add(int numA, int numB);
    }

    [CSharpCallLua]
    public delegate int GetF(int a, int b, out int c);

    [CSharpCallLua]
    public delegate System.Action GetE();

    private void Start()
    {
        LuaEnv env = new LuaEnv();
        env.DoString(str);

        int a = env.Global.Get<int>("a");
        string b = env.Global.Get<string>("b");
        bool c = env.Global.Get<bool>("c");

        IGetD id = env.Global.Get<IGetD>("d");
        print("修改前: " + id.f1 + "--" + id.f2);
        id.f1 = 100;
        print("修改后 : " + id.f1);
        print("add : " + id.add(10, 20));

        System.Action e = env.Global.Get<System.Action>("e");
        e();

        GetF f = env.Global.Get<GetF>("f");
        int tmpB = 0;
        int tmpA = f(100, 200, out tmpB);
        Debug.LogWarning("a= " + tmpA + ", b= " + tmpB);

        GetE tmpE = env.Global.Get<GetE>("get_E");
        // e = tmpE();
        //e();
        tmpE();

       // env.Dispose();
    }












}