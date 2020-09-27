RegistUIView = {};
local this = RegistUIView;

local transform;
local gameObject;

-- awake
function RegistUIView.awake(obj)
    gameObject = obj ;
    transform = obj.transform;

    this.InitView();
end


-- 获取UI组件
function RegistUIView.InitView()

    

end

-- start
function RegistUIView.start()

end


-- update
function RegistUIView.update()

end


-- ondestroy
function RegistUIView.ondestroy()


end