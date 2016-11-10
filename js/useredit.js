function user_win() {
    var field_ID = Ext.create('Ext.form.field.Hidden', { 
        name: 'ID'
    });
    var field_NAME = Ext.create('Ext.form.field.Text', {
        name: 'NAME',
        allowBlank: false,
        fieldLabel: '登录名',
        readOnly: userid,//编辑的时候不允许修改登录名
        blankText: '登录名不能为空!'
    });
    var field_REALNAME = Ext.create('Ext.form.field.Text', {
        name: 'REALNAME',
        fieldLabel: '姓名'
    });
    var field_EMAIL = Ext.create('Ext.form.field.Text', {
        fieldLabel: '邮箱',
        name: 'EMAIL',
        vtype: 'email',
        vtypeText: 'Email格式不正确'
    });
    var field_MOBILE = Ext.create('Ext.form.field.Number', {
        name: 'MOBILE',
        fieldLabel: '手机',
        maxLengthText: '手机号最多位11位',
        maxLength: 11,
        allowBlank: false,
        blankText: '手机号码不能为空!',
        hideTrigger: true
    });
    var store_ENABLED = Ext.create('Ext.data.JsonStore', {
        fields: ['id', 'name'],
        data: [{ id: 1, name: '启用' }, { id: 0, name: '停用' }]
    })
    var combo_ENABLED = Ext.create('Ext.form.field.ComboBox', {
        name: 'STATUS',
        value: 1,
        editable: false,
        store: store_ENABLED,
        fieldLabel: '状态',
        displayField: 'name',
        valueField: 'id',
        queryMode: 'local'
    })
    var field_REMARK = Ext.create('Ext.form.field.TextArea', {
        fieldLabel: '备注',
        name: 'REMARK',
        height: 65,
        anchor: '100%'
    })
    var formp_user = Ext.create('Ext.form.Panel', {
        title: '用户信息',
        region: 'center',
        fieldDefaults: {
            margin: '0 5 10 0',
            labelWidth: 80,
            columnWidth: .5,
            labelAlign: 'right',
            labelSeparator: '',
            msgTarget: 'under'
        },
        items: [
        { layout: 'column', height: 42, margin: '10 0 0 0', border: 0, items: [field_NAME, field_REALNAME] },
        { layout: 'column', height: 42, border: 0, items: [field_EMAIL, field_MOBILE] },
        { layout: 'column', height: 42, border: 0, items: [combo_ENABLED] },
        field_REMARK, field_ID
        ]
    });
    var win = Ext.create("Ext.window.Window", {
        width: 700,
        height: 570,
        modal: true,
        items: [formp_user],
        layout: 'border',
        buttonAlign: 'center',
        buttons: [{
            text: '<i class="fa fa-check-square-o"></i>&nbsp;保存', handler: function () {
                var mask = new Ext.LoadMask(Ext.getBody(), { msg: "数据保存中，请稍等..." });
                if (formp_user.getForm().isValid()) {
                    mask.show();
                    Ext.Ajax.request({
                        url: '/Backstage/saveuser',
                        params: { userid: userid, json: Ext.encode(formp_user.getForm().getValues()) },
                        success: function (response, option) {
                            var data = Ext.decode(response.responseText);
                            if (data.success == true) {
                                Ext.MessageBox.alert('提示', '保存成功！', function () {
                                    store_user.load();
                                    win.close();
                                });
                            }
                            else {
                                Ext.MessageBox.alert('提示', '保存失败，登录名不能重复！', function () {
                                    win.close()
                                });
                            }
                            mask.hide();
                        }
                    });
                }
            }
        }, {
            text: '<i class="fa fa-times"></i>&nbsp;关闭', handler: function () {
                win.close();
            }
        }]
    });
    win.show();
    if (userid) {
        Ext.Ajax.request({
            url: '/Backstage/getuser',
            params: { userid: userid },
            success: function (response, option) {
                var data = Ext.decode(response.responseText);
                formp_user.getForm().setValues(data); 
            }
        });
    }
}