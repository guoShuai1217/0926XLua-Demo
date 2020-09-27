/*
 * Tencent is pleased to support the open source community by making xLua available.
 * Copyright (C) 2016 THL A29 Limited, a Tencent company. All rights reserved.
 * Licensed under the MIT License (the "License"); you may not use this file except in compliance with the License. You may obtain a copy of the License at
 * http://opensource.org/licenses/MIT
 * Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the specific language governing permissions and limitations under the License.
*/

using UnityEngine;
using System.Collections;
using System;
using XLua;
using System.Collections.Generic;

namespace Tutorial
{
	[LuaCallCSharp]
	public class BaseClass
	{
        /// <summary>
        /// 静态方法
        /// 本类调用 : CS.Tutorial.BaseClass.BSFunc()
        /// 子类调用 : CS.Tutorial.DerivedClass.BSFunc()
        /// </summary>
		public static void BSFunc()
		{
			Debug.Log("Derived Static Func, BSF = " + BSF);
		}

        /// <summary>
        /// 静态字段
        /// 本类调用 : CS.Tutorial.BaseClass.BSF = 2 ,      print(CS.Tutorial.BaseClass.BSF)
        /// 子类调用 : CS.Tutorial.DerivedClass.BSF =3 ,    print(CS.Tutorial.DerivedClass.BSF)
        /// </summary>
		public static int BSF = 1;

        /// <summary>
        /// 成员方法
        /// 本类调用 : local BaseClass = CS.Tutorial.BaseClass , local bc = BaseClass(), bc:BMFunc()   注意 :
        /// 子类调用 : local DerivedClass = CS.Tutorial.DerivedClass , local der = DerivedClass(), der:BMFunc() 注意 : 
        /// </summary>
		public void BMFunc()
		{
			Debug.Log("Derived Member Func, BMF = " + BMF);
		}


        /// <summary>
        /// 成员属性
        /// 本类调用 : local BaseClass = CS.Tutorial.BaseClass , local bc = BaseClass(), bc.BMF = 10 ,print(bc.BMF) 
        /// 子类调用 : local DerivedClass = CS.Tutorial.DerivedClass , local der = DerivedClass(), der.BMF = 10,print(der.BMF) 
        /// </summary>
        public int BMF { get; set; }
	}

    /// <summary>
    /// 结构体 
    /// tab1={ x =1,y = "老王" }
    /// </summary>
	public struct Param1
	{
		public int x;
		public string y;
	}

    /// <summary>
    /// 全局枚举 
    /// CS.Tutorial.TestEnum.E1
    /// </summary>
	[LuaCallCSharp]
	public enum TestEnum
	{
		E1,
		E2
	}


	[LuaCallCSharp]
	public class DerivedClass : BaseClass
	{
        /// <summary>
        /// 类里面的枚举
        /// CS.Tutorial.DerivedClass.TestEnumInner.E3
        /// </summary>
		[LuaCallCSharp]
		public enum TestEnumInner
		{
			E3,
			E4
		}

        /// <summary>
        /// 成员方法
        /// local DerivedClass = CS.Tutorial.DerivedClass   --找到DerivedClass类
        /// local der = DerivedClass()                      --new一个对象
        /// der:DMFunc()                                    --调用成员方法 , 注意 : 
        /// </summary>
		public void DMFunc()
		{
			Debug.Log("Derived Member Func, DMF = " + DMF);
		}

        /// <summary>
        /// 成员属性
        /// local der = CS.Tutorial.DerivedClass()
        /// der.DMF = 10
        /// print(der.DMF)
        /// </summary>
		public int DMF { get; set; }

        /// <summary>
        /// 多个返回值成员方法 
        /// 输入参数 : ref 算输入 , out不算 
        /// 返回值 : 函数返回值算第一个,ref 算 , out也算 
        /// local der = CS.Tutorial.DerivedClass()
        /// doubleResult,intResult,stringResult,outFunc = der:ComplexFunc({x=100,y="老王"},12,function() print("luafunc")  end)
        /// 第一个返回值是 double类型 , 第二个是int ,第三个是string , 第四个是Action
        /// </summary>
		public double ComplexFunc(Param1 p1, ref int p2, out string p3, Action luafunc, out Action csfunc)
		{
			Debug.Log("P1 = {x=" + p1.x + ",y=" + p1.y + "},p2 = " + p2);
			luafunc();
			p2 = p2 * p1.x;
			p3 = "hello " + p1.y;
			csfunc = () =>
			{
				Debug.Log("csharp callback invoked!");
			};
			return 1.23;
		}

        /// <summary>
        /// 重载成员方法
        /// local der = CS.Tutorial.DerivedClass()
        /// der:TestFunc(100)
        /// </summary>
        /// <param name="i"></param>
		public void TestFunc(int i)
		{
			Debug.Log("TestFunc(int i)");
		}

        /// <summary>
        ///  重载成员方法
        /// local der = CS.Tutorial.DerivedClass()
        /// der:TestFunc("老王")
        /// </summary>
        /// <param name="i"></param>
        public void TestFunc(string i)
		{
			Debug.Log("TestFunc(string i)");
		}

        /// <summary>
        /// 操作符..这个我没用过
        /// local der1 = CS.Tutorial.DerivedClass()
        /// local der2 = CS.Tutorial.DerivedClass()
        /// der1.DMF = 50
        /// der2.DMF = 200
        /// print('(der1 + der2).DMF = ', (der1 + der2).DMF)
        /// </summary>
		public static DerivedClass operator +(DerivedClass a, DerivedClass b)
		{
            DerivedClass ret = new DerivedClass();
			ret.DMF = a.DMF + b.DMF;
			return ret;
		}

        /// <summary>
        /// 带默认值的成员方法
        /// local der = CS.Tutorial.DerivedClass()
        /// der:DefaultValueFunc(100,"老王","奥利给")
        /// </summary>
        public void DefaultValueFunc(int a = 100, string b = "cccc", string c = null)
		{
			UnityEngine.Debug.Log("DefaultValueFunc: a=" + a + ",b=" + b + ",c=" + c);
		}

        /// <summary>
        /// 可变参数的成员方法
        /// local der = CS.Tutorial.DerivedClass()
        /// der:DefaultValueFunc(100,"老王","奥利给","任意输入")
        /// </summary>
        public void VariableParamsFunc(int a, params string[] strs)
		{
			UnityEngine.Debug.Log("VariableParamsFunc: a =" + a);
			foreach (var str in strs)
			{
				UnityEngine.Debug.Log("str:" + str);
			}
		}

        /// <summary>
        /// 枚举类型
        /// local der = CS.Tutorial.DerivedClass()
        /// local tmpEnum = der:EnumTestFunc(CS.Tutorial.TestEnum.E1)
        /// 
        /// 扩展 : 
        /// print(tmpEnum, tmpEnum == CS.Tutorial.TestEnum.E2)
        /// 
        /// 把数值或字符串强转成 枚举
        /// print(CS.Tutorial.TestEnum.__CastFrom(1) , CS.Tutorial.TestEnum.__CastFrom('E2'))
        /// 
        /// TestEnumInner枚举在 DerivedClass类里面
        /// print(CS.Tutorial.DerivedClass.TestEnumInner.E3)
        /// </summary>
		public TestEnum EnumTestFunc(TestEnum e)
		{
			Debug.Log("EnumTestFunc: e=" + e);
			return TestEnum.E2;
		}

        /// <summary>
        /// 委托类型 
        /// local der = CS.Tutorial.DerivedClass()
        /// 直接调用就可以 : der.TestDelegate('Are you OK ?') 
        /// 
        /// local function lua_callback (str)
        ///     print(str) 
        /// end 
        /// 
        /// -- 绑定委托
        /// der.TestDelegate = der.TestDelegate  + lua_callback 
        /// der.TestDelegate("绑定了委托")
        /// 
        /// --移除委托
        /// der.TestDelegate = der.TestDelegate - lua_callback
        /// der.TestDelegate("移除了委托")
        /// </summary>
		public Action<string> TestDelegate = (param) =>
		{
			Debug.Log("TestDelegate in c#:" + param);
		};


        /// <summary>
        /// 事件
        /// 
        /// local function luacallback1()
        ///     print("事件 1 的回调")
        /// end 
        /// 
        /// local function luacallback2()
        ///     print("事件 2 的回调")
        /// end 
        /// 
        /// local der = CS.Tutorial.DerivedClass()
        /// der:TestEvent('+',luacallback1)     --绑定事件
        /// der:CallEvent()                     --调用
        /// 
        /// der:TestEvent('+',luacallback2)     --绑定事件
        /// der:CallEvent()                     --调用
        /// 
        /// der:TestEvent('-',luacallback1)     --移除事件
        /// der:TestEvent('-',luacallback2)     --移除事件
        /// </summary>
		public event Action TestEvent;

		public void CallEvent()
		{
			TestEvent();
		}

        /// <summary>
        /// 64位支持
        /// local der = CS.Tutorial.DerivedClass()
        /// local tmpResult = der:TestLong(11)
        /// print(type(tmpResult),tmpResult,tmpResult+100,tmpResult + 10000)
        /// </summary>
		public ulong TestLong(long n)
		{
			return (ulong)(n + 1);
		}

        // 添加组件 
        // local gm = CS.UnityEngine.GameObject("oo")
        // gm:AddComponent(typeof(CS.UnityEngine.ParticleSystem))


    
		class InnerCalc : ICalc
		{
			public int add(int a, int b)
			{
				return a + b;
			}

			public int id = 100;
		}

        /// <summary>
        /// local der = CS.Toturial.DerivedClass()
        /// local tmpCalc = der:GetCalc()
        /// tmpCalc:add(1,2)
        /// -- assert(a,b) a是要检查是否有错误的一个参数，b是a错误时抛出的信息。第二个参数b是可选的。
        /// assert(tmpCalc.id = 100)
        /// -- 强转
        /// cast(tmpCalc,typeof(CS.Tutorial.ICalc))
        /// assert(tmpCalc.add(1,2))
        /// assert(tmpCalc.id = 200)
        /// </summary>
		public ICalc GetCalc()
		{
			return new InnerCalc();
		}

        /// <summary>
        /// 泛型，不直接支持，可以通过扩展方法
        /// </summary>
		public void GenericMethod<T>()
		{
			Debug.Log("GenericMethod<" + typeof(T) + ">");
		}
	}

	[LuaCallCSharp]
	public interface ICalc
	{
		int add(int a, int b);
	}

    /// <summary>
    /// 扩展
    /// </summary>
	[LuaCallCSharp]
	public static class DerivedClassExtensions
    {
		public static int GetSomeData(this DerivedClass obj)
		{
			Debug.Log("GetSomeData ret = " + obj.DMF);
			return obj.DMF;
		}

		public static int GetSomeBaseData(this BaseClass obj)
		{
			Debug.Log("GetSomeBaseData ret = " + obj.BMF);
			return obj.BMF;
		}

		public static void GenericMethodOfString(this DerivedClass obj)
		{
			obj.GenericMethod<string>();
		}
	}
}

public class LuaCallCs : MonoBehaviour
{
	LuaEnv luaenv = null;
	string script = @"
        function demo()
            --new C#对象
            local newGameObj = CS.UnityEngine.GameObject()
            local newGameObj2 = CS.UnityEngine.GameObject('helloworld')
            print(newGameObj, newGameObj2)
        
            --访问静态属性，方法
            local GameObject = CS.UnityEngine.GameObject
            print('UnityEngine.Time.deltaTime:', CS.UnityEngine.Time.deltaTime) --读静态属性
            CS.UnityEngine.Time.timeScale = 0.5 --写静态属性
            print('helloworld', GameObject.Find('helloworld')) --静态方法调用

            --访问成员属性，方法
            local DerivedClass = CS.Tutorial.DerivedClass
            local testobj = DerivedClass()
            testobj.DMF = 1024--设置成员属性
            print(testobj.DMF)--读取成员属性
            testobj:DMFunc()--成员方法

            --基类属性，方法
            print(DerivedClass.BSF)--读基类静态属性
            DerivedClass.BSF = 2048--写基类静态属性
            DerivedClass.BSFunc();--基类静态方法
            print(testobj.BMF)--读基类成员属性
            testobj.BMF = 4096--写基类成员属性
            testobj:BMFunc()--基类方法调用

            --复杂方法调用
            local ret, p2, p3, csfunc = testobj:ComplexFunc({x=3, y = 'john'}, 100, function()
               print('i am lua callback')
            end)
            print('ComplexFunc ret:', ret, p2, p3, csfunc)
            csfunc()

            --重载方法调用
            testobj:TestFunc(100)
            testobj:TestFunc('hello')

            --操作符
            local testobj2 = DerivedClass()
            testobj2.DMF = 2048
            print('(testobj + testobj2).DMF = ', (testobj + testobj2).DMF)

            --默认值
            testobj:DefaultValueFunc(1)
            testobj:DefaultValueFunc(3, 'hello', 'john')

            --可变参数
            testobj:VariableParamsFunc(5, 'hello', 'john')

            --Extension methods
            print(testobj:GetSomeData()) 
            print(testobj:GetSomeBaseData()) --访问基类的Extension methods
            testobj:GenericMethodOfString()  --通过Extension methods实现访问泛化方法

            --枚举类型
            local e = testobj:EnumTestFunc(CS.Tutorial.TestEnum.E1)
            print(e, e == CS.Tutorial.TestEnum.E2)
            print(CS.Tutorial.TestEnum.__CastFrom(1), CS.Tutorial.TestEnum.__CastFrom('E1'))
            print(CS.Tutorial.DerivedClass.TestEnumInner.E3)
            assert(CS.Tutorial.BaseClass.TestEnumInner == nil)

            --Delegate
            testobj.TestDelegate('hello') --直接调用
            local function lua_delegate(str)
                print('TestDelegate in lua:', str)
            end
            testobj.TestDelegate = lua_delegate + testobj.TestDelegate --combine，这里演示的是C#delegate作为右值，左值也支持
            testobj.TestDelegate('hello')
            testobj.TestDelegate = testobj.TestDelegate - lua_delegate --remove
            testobj.TestDelegate('hello')

            --事件
            local function lua_event_callback1() print('lua_event_callback1') end
            local function lua_event_callback2() print('lua_event_callback2') end
            testobj:TestEvent('+', lua_event_callback1)
            testobj:CallEvent()
            testobj:TestEvent('+', lua_event_callback2)
            testobj:CallEvent()
            testobj:TestEvent('-', lua_event_callback1)
            testobj:CallEvent()
            testobj:TestEvent('-', lua_event_callback2)

            --64位支持
            local l = testobj:TestLong(11)
            print(type(l), l, l + 100, 10000 + l)

            --typeof
            newGameObj:AddComponent(typeof(CS.UnityEngine.ParticleSystem))

            --cast
            local calc = testobj:GetCalc()
            print('assess instance of InnerCalc via reflection', calc:add(1, 2))
            assert(calc.id == 100)
            cast(calc, typeof(CS.Tutorial.ICalc))
            print('cast to interface ICalc', calc:add(1, 2))
            assert(calc.id == nil)
       end

       demo()

       --协程下使用
       local co = coroutine.create(function()
           print('------------------------------------------------------')
           demo()
       end)
       assert(coroutine.resume(co))
    ";

	// Use this for initialization
	void Start()
	{
		luaenv = new LuaEnv();
		luaenv.DoString(script);
	}

	// Update is called once per frame
	void Update()
	{
		if (luaenv != null)
		{
			luaenv.Tick();
		}
	}

	void OnDestroy()
	{
		luaenv.Dispose();
	}
}
