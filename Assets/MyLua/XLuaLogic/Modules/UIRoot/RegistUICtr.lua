RegistUICtr = {};
local this = RegistUICtr;

 
local transform;
local gameObject;

-- new 一个实例
function RegistUICtr.New()
    return this;
end

-- awake
function RegistUICtr.Awake()
   
        --调用C#脚本去加载资源 实例化UI
        CS.guoShuai.Lua.LuaHelper.Instance:LoadUIScene("UIPrefab/RegistUIView",this.OnCreate);

end



--实例化UI成功的回调
function RegistUICtr.OnCreate(obj)
    print("实例化 " .. obj.name .." 成功"); --实例化成功后,会执行UI预制体 挂载的 LuaViewBehaviour 脚本
end