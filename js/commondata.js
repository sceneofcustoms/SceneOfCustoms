//公共静态数据

//1 查询条件 --日期
var search_condition_date = [
    { code: 'jiedan', name: '接单日期', selected: true },
    { code: 'lihuo', name: '理货日期' },
    { code: 'jihuabaoguan', name: '计划报关日期' },
    { code: 'lihuoziliaoqiquan', name: '理货资料齐全日期' },
    { code: 'baoguan', name: '报关日期' },
    { code: 'danzhengfangxing', name: '单证放行日期' },
    { code: 'shiwufangxing', name: '实物放行日期' },
    { code: 'chuguanfeng', name: '出关封日期' },
    { code: 'koufangxing', name: '口岸放行时间' },
    { code: 'koufangxing', name: '口岸安排报关时间' }
];

//2 查询条件  --号
var search_code = [
    {
        code: 'jiedan', name: '分单号', selected: true
    },
    { code: 'lihuo', name: '总单号' },
    { code: 'jihuabaoguan', name: '平台编号' },
    { code: 'lihuoziliaoqiquan', name: '业务编号' },
    { code: 'baoguan', name: '合同/发票号' },
    { code: 'danzhengfangxing', name: '转关预录入号' },
    { code: 'shiwufangxing', name: '转关号' },
    { code: 'chuguanfeng', name: '通关单号' },
    { code: 'koufangxing', name: '手册号' },
    { code: 'koufangxing', name: '报关单号' },
    { code: 'koufangxing', name: '签收号' },
    { code: 'koufangxing', name: '绑定号' }
];

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

//5 查询选择值  --是否
var is_check = [
    { code: '1', name: '是' },
    { code: '0', name: '否' },
];

//5 查询选择值  --是否
var business_object = [
    { code: '1', name: '收货人' },
    { code: '0', name: '结算方' },
];

$(function () {

    $('#searchform #search_condition_date').combobox({
        data: search_condition_date,
        valueField: 'code',
        textField: 'name'
    });
    $('#searchform #search_code').combobox({
        data: search_code,
        valueField: 'code',
        textField: 'name'
    });
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
    $('#searchform #is_clearance').combobox({
        data: is_check,
        valueField: 'code',
        textField: 'name'
    });
    $('#searchform #business_object').combobox({
        data: business_object,
        valueField: 'code',
        textField: 'name'
    });
})



//搜索列表
function search_form() { }


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