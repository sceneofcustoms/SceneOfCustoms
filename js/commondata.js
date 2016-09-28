//公共静态数据

//1 查询条件 --日期
//var search_condition_date = [
//    { code: 'guanwujieshou', name: '关务接受日期', selected: true },
//    { code: 'baoruhaiguan', name: '报入海关日期' },
//    { code: 'shandankaishi', name: '删单开始日期' },
//    { code: 'shandanwancheng', name: '删单完成日期' },
//    { code: 'gaidankaishi', name: '改单开始日期' },
//    { code: 'gaidanwancheng', name: '改单完成日期' },
//    { code: 'chayanzhilingxiafa', name: '查验指令下发日期' },
//    { code: 'chayankaishi', name: '查验开始日期' },
//    { code: 'chayanwancheng', name: '查验完成日期' },
//    { code: 'danzhengfangxing', name: '单证放行日期' },
//    { code: 'shiwufangxing', name: '实物放行日期' },
//];

//2 查询条件  --单据号
//var search_code = [ 
//    { 
//        code: 'jiedan', name: '分单号', selected: true
//    },
//    { code: 'lihuo', name: '总单号' },
//    { code: 'jihuabaoguan', name: '万达号' },
//    { code: 'lihuoziliaoqiquan', name: '业务编号' },
//    { code: 'baoguan', name: '集装箱号' },
//    { code: 'danzhengfangxing', name: '转关预录入号' },
//    { code: 'shiwufangxing', name: '一程提运单号' },
//    { code: 'chuguanfeng', name: '二程提运单号' },
//    { code: 'koufangxing', name: '载货清单号' },
//    { code: 'koufangxing', name: '报关单号' },
//    { code: 'koufangxing', name: 'FWO订单号' },
//    { code: 'koufangxing', name: 'FO号' }
//];

//3 查询条件  --操作人
var search_operator = [
    { code: 'jiedan', name: '过机人', selected: true },
    { code: 'lihuo', name: '理单人' },
    { code: 'jihuabaoguan', name: '理货资料齐全人' },
    { code: 'lihuoziliaoqiquan', name: '报关人' },
    { code: 'baoguan', name: '单证放行人' },
    { code: 'danzhengfangxing', name: '实物放行人' }
];

//4 查询条件  --海关通关状态
var search_customs_state = [
    { code: 'jiedan', name: '已申报' },
    { code: 'jiedan', name: '简化手续方式' },
    { code: 'jiedan', name: '同意担保放行' },
    { code: 'jiedan', name: '已放行' },
    { code: 'jiedan', name: '删单成功' },
    { code: 'jiedan', name: '已接单' },
    { code: 'jiedan', name: '已结关' },
    { code: 'jiedan', name: '人工审核通过' },
    { code: 'jiedan', name: '人工退单' }
];

//5 查询选择值  --关代码
var is_check = [
    { code: '1', name: '是' },
    { code: '0', name: '否' },
];

//5 查询选择值  --业务类型
var businessin_object = [
    { code: '0', name: '空进' },
    { code: '1', name: '海进' },
    { code: '2', name: '陆进' },
];

//5 查询选择值  --业务类型
var businessout_object = [
    { code: '0', name: '空出' },
    { code: '1', name: '海出' },
    { code: '2', name: '陆出' },
];

//6 查询搜索值  --业务方式
var service_model = [
    { code: '1', name: '进口' },
    { code: '0', name: '出口' },
];

//7 查询文档类型
var doc_type = [
    { code: '1', name: '合同' },
    { code: '1', name: '货物清单' },
    { code: '1', name: '发票' },
    { code: '1', name: '报关文档' },
    { code: '1', name: '报关底单' },
    { code: '1', name: '提运单' },
    { code: '1', name: '委托协议' },
    { code: '1', name: '箱单' },
    { code: '1', name: '报检文档' },
    { code: '1', name: '成本票据' },
    { code: '1', name: '理货文档' },
    { code: '1', name: '文档类型' },
    { code: '1', name: '查检文档' }
]

//8 查询申报方式
var declare_type = [
       { code: '1', name: '一般出口' },
       { code: '1', name: '提前出口' },
       { code: '1', name: '一般进口' },
       { code: '1', name: '提前进口' }
];

//9 查询 报关方式
var declaration_type = [
       { code: '1', name: '逐笔' },
       { code: '2', name: '转厂' },
       { code: '3', name: '集中' },
       { code: '4', name: '作业单' }
];

//10 转入 转出
var out_in = [
       { code: '1', name: '转出' },
       { code: '1', name: '转入' }
];



$(function () {

     $('#searchform #search_operator').combobox({
        data: search_operator,
        valueField: 'code',
        textField: 'name'
    });

     $('#searchform #CUSTOMDISTRICTCODE').combobox({
        url: '/Order/Get_SBGQ',
        method: 'get',
        valueField: 'CODE',
        textField: 'NAME',
        panelWidth: 200,
        panelHeight: 'auto',
        formatter: formatItem,
        label: 'CUSTOMDISTRICTCODE:',
        panelHeight:'200',
        labelPosition: 'top'
    });


    //$('#searchform #search_condition_date').combobox({
    //    data: search_condition_date,
    //    valueField: 'code',
    //    textField: 'name'
    //});
    //$('#searchform #search_code').combobox({
    //    data: search_code,
    //    valueField: 'code',
    //    textField: 'name'
    //});
    $('#searchform #search_operator').combobox({
        data: search_operator,
        valueField: 'code',
        textField: 'name'
    });
    $('#searchform #customs_state').combobox({
        data: search_customs_state,
        valueField: 'code',
        textField: 'name'
    });
    $('#searchform .is_check').combobox({
        data: is_check,
        valueField: 'code',
        textField: 'name'
    });
    $('#searchform #businessin_object').combobox({
        data: businessin_object,
        valueField: 'code',
        textField: 'name'
    });
    $('#searchform #businessout_object').combobox({
        data: businessout_object,
        valueField: 'code',
        textField: 'name'
    });
    $('#searchform #service_model').combobox({
        data: service_model,
        valueField: 'code',
        textField: 'name'
    });
    $('#searchform #doc_type').combobox({
        data: doc_type,
        valueField: 'code',
        textField: 'name'
    });
    $('#searchform #declare_type').combobox({
        data: declare_type,
        valueField: 'code',
        textField: 'name'
    });
    $('#searchform #declaration_type').combobox({
        data: declaration_type,
        valueField: 'code',
        textField: 'name'
    });

    $('#searchform #out_in').combobox({
        data: out_in,
        valueField: 'code',
        textField: 'name'
    });



    function loadsucc(data) {
        if (data['PASSMODE'] != "" && data['PASSMODE'] != null) {
            var PASSMODE = data['PASSMODE'];
            var arr = PASSMODE.split(',');
            for (i = 0; i < arr.length; i++) {
                $('input[name=PASSMODE][value=' + arr[i] + ']')[0].checked = true;
            }
        }
    }

    $('#OrderFrom').form({ onLoadSuccess: loadsucc });


    $('#OrderFrom .fillingData').click(function () {
        debugger;
        var type = this.getAttribute("datatype");
        var ID = getQueryString('ID');
        $.ajax({
            url: '/Order/Edit_Ajax_Scene',// 跳转到 action
            data: {
                'ID': ID,
                'type': type
            },
            type: 'post',
            dataType: 'json',
            success: function (data) {
                if (data.Success == true) {
                    var time = "#" + type + 'TIME';
                    var name = "#" + type + 'USERNAME';
                    debugger;
                    $(time).textbox('setValue', data.datetime);
                    $(name).textbox('setValue', data.name);
                } else {
                    $.messager.alert('异常', '联系管理员');
                }
            },

        });

    });


    $('#many_form .fillingData').click(function () {
        debugger;
        var type = this.getAttribute("datatype");
        var form = $(this).parents('.OrderFrom').attr('id');//查找哪个form
        var findname = "#" + form + " input[name=ID]";
        var ID = $(findname).val();
        $.ajax({
            url: '/Order/Edit_Ajax_Scene',// 跳转到 action
            data: {
                'ID': ID,
                'type': type
            },
            type: 'post',
            dataType: 'json',
            success: function (data) {
                debugger;
                if (data.Success == true) {
                    var time = "#" + form + " #" + type + 'TIME';
                    var name = "#" + form + " #" + type + 'USERNAME';
                    $(time).textbox('setValue', data.datetime);
                    $(name).textbox('setValue', data.name);
                } else {
                    $.messager.alert('异常', '联系管理员');
                }
            },

        });

    });

})



//搜索列表
function search_form() {
    debugger;
}


//重置搜索表表单
function reset_form() {
    $('.value').each(function (i) {
        //if ($(this).hasClass('easyui-combobox')) {
        //} else if ($(this).hasClass('easyui-textbox')) {
        //} else if ($(this).hasClass('easyui-datebox')) {
        //}
        $(this).textbox('setValue', '');
    });
}


function formatItem(row) {
    var s = '<span style="font-weight:bold">' + row.CODE + '</span> &nbsp;&nbsp;' +
            '<span style="color:#888">' + row.NAME + '</span>';
    return s;
}

function getQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}

function submitForm() {
    debugger;
    $('#OrderFrom').form('submit', {
        url: "/Order/SaveData",
        onSubmit: function () {
            if ($(this).form('enableValidation').form('validate')) {
                return true;
            } else {
                return false;
            }
        },
        success: function (data) {
            debugger;
            var data = eval('(' + data + ')');  // change the JSON string to javascript object
            if (data.Success) {
                $.messager.alert('成功', '保存成功');
            } else {
                $.messager.alert('失败', '保存失败');
            }
        }
    });
}

//加载单个form
function Edit() {
    var ID = getQueryString('ID');
    $('#OrderFrom').form('load', '/Order/Edit_Order?ID=' + ID);
}



//多个form
function manySubmitForm() {
    var is_success = true;
    $('#many_form .OrderFrom').form('submit', {
        url: "/Order/SaveData",
        onSubmit: function () {
            if ($(this).form('enableValidation').form('validate')) {
                return true;
            } else {
                return false;
            }
        },
        success: function (data) {
            var data = eval('(' + data + ')');  // change the JSON string to javascript object
            if (!data.Success) {
                is_success = false;
            }
        }
    });

    if (is_success) {
        $.messager.alert('成功', '保存成功');
    }
}





function manyEditForm() {
    $('#many_form form').each(function (i) {
        var formid = "#" + this.id;
        var findid = formid + " input[name=ID]";
        var id = $(findid).val();
        $(formid).form({
            onLoadSuccess: function (data) {
                if (data['PASSMODE'] != "" && data['PASSMODE'] != null) {
                    var PASSMODE = data['PASSMODE'];
                    var arr = PASSMODE.split(',');
                    for (i = 0; i < arr.length; i++) {
                        var str = formid + ' input[name=PASSMODE][value=' + arr[i] + ']';
                        $(str)[0].checked = true;
                    }
                }
            }
        });
        $(formid).form('load', '/Order/Edit_Order?ID=' + id);
    });
}

//加载grid
function loadListGrid(page) {
    $('#datagrid').datagrid({
        url: '/Order/GetData',
        rownumbers: true,
        dataType: 'json',
        method: 'get',
        toolbar: '#tb',
        pageSize: 20,
        pagination: true,
        onDblClickCell: function (index, field, value) {
            var row = $('#datagrid').datagrid('getData').rows[index];
            if (row.ID != "") {
                window.location.href = "/" + page + "?ID=" + row.ID;
            }
        }
    });
}
//弹窗
function opencenterwin(url, width, height) {
    var iWidth = width ? width : "1000", iHeight = height ? height : "600";
    var iTop = (window.screen.availHeight - 30 - iHeight) / 2; //获得窗口的垂直位置;
    var iLeft = (window.screen.availWidth - 10 - iWidth) / 2; //获得窗口的水平位置; 
    window.open(url, '', 'height=' + iHeight + ',,innerHeight=' + iHeight + ',width=' + iWidth + ',innerWidth=' + iWidth + ',top=' + iTop + ',left=' + iLeft + ',toolbar=yes,menubar=yes, location=yes,scrollbars=yes,resizable=yes');
}