UIRootView = {};
local this = UIRootView;

local transform;
local gameObject;

-- awake
function UIRootView.awake(obj)
    gameObject = obj ;
    transform = obj.transform;

    this.InitView();
end


-- 获取UI组件
function UIRootView.InitView()

    print(string.format("获取 %s 组件","UIRootView"));

    local uiroot = transform:Find("Canvas/UIRoot").transform;
    this.btn_Login = uiroot:Find("btn_Login"):GetComponent("UnityEngine.UI.Button");
    this.btn_Regist = uiroot:Find("btn_Regist"):GetComponent("UnityEngine.UI.Button");
    this.input_Account = uiroot:Find("input_Account"):GetComponent("UnityEngine.UI.InputField");
    this.input_Password = uiroot:Find("input_Password"):GetComponent("UnityEngine.UI.InputField");
   
end

-- start
function UIRootView.start()

end


-- update
function UIRootView.update()

end


-- ondestroy
function UIRootView.ondestroy()


end

