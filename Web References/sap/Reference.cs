﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18408
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

// 
// 此源代码是由 Microsoft.VSDesigner 4.0.30319.18408 版自动生成。
// 
#pragma warning disable 1591

namespace SceneOfCustoms.sap {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="SI_CUS_CUS1002Binding", Namespace="URN:CUS1002")]
    public partial class SI_CUS_CUS1002Service : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback SI_CUS_CUS1002OperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public SI_CUS_CUS1002Service() {
            this.Url = global::SceneOfCustoms.Properties.Settings.Default.SceneOfCustoms_sap_SI_CUS_CUS1002Service;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event SI_CUS_CUS1002CompletedEventHandler SI_CUS_CUS1002Completed;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://sap.com/xi/WebService/soap1.1", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        [return: System.Xml.Serialization.XmlElementAttribute("MT_CUS_CUS1002_RES", Namespace="URN:CUS1002")]
        public DT_CUS_CUS1002_RES SI_CUS_CUS1002([System.Xml.Serialization.XmlArrayAttribute(Namespace="URN:CUS1002")] [System.Xml.Serialization.XmlArrayItemAttribute("ITEM", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)] DT_CUS_CUS1002_REQITEM[] MT_CUS_CUS1002_REQ) {
            object[] results = this.Invoke("SI_CUS_CUS1002", new object[] {
                        MT_CUS_CUS1002_REQ});
            return ((DT_CUS_CUS1002_RES)(results[0]));
        }
        
        /// <remarks/>
        public void SI_CUS_CUS1002Async(DT_CUS_CUS1002_REQITEM[] MT_CUS_CUS1002_REQ) {
            this.SI_CUS_CUS1002Async(MT_CUS_CUS1002_REQ, null);
        }
        
        /// <remarks/>
        public void SI_CUS_CUS1002Async(DT_CUS_CUS1002_REQITEM[] MT_CUS_CUS1002_REQ, object userState) {
            if ((this.SI_CUS_CUS1002OperationCompleted == null)) {
                this.SI_CUS_CUS1002OperationCompleted = new System.Threading.SendOrPostCallback(this.OnSI_CUS_CUS1002OperationCompleted);
            }
            this.InvokeAsync("SI_CUS_CUS1002", new object[] {
                        MT_CUS_CUS1002_REQ}, this.SI_CUS_CUS1002OperationCompleted, userState);
        }
        
        private void OnSI_CUS_CUS1002OperationCompleted(object arg) {
            if ((this.SI_CUS_CUS1002Completed != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SI_CUS_CUS1002Completed(this, new SI_CUS_CUS1002CompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18408")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="URN:CUS1002")]
    public partial class DT_CUS_CUS1002_REQITEM {
        
        private string fWO_IDField;
        
        private string fOO_IDField;
        
        private string eVENT_CODEField;
        
        private string eVENT_DATField;
        
        private DT_CUS_CUS1002_REQITEMORDER[] oRDERField;
        
        private string zBGDTSField;
        
        private string zCYCSField;
        
        private string zDDCSField;
        
        private string zLHCSField;
        
        private string zBGSDCSField;
        
        private string zBGGDCSField;
        
        private string zBGDGSField;
        
        private string zHGXYBZField;
        
        private string zJYHField;
        
        private string zCYLXField;
        
        private string zJYDDField;
        
        private string zPTSCField;
        
        private string zBGCYBJField;
        
        private string zDDBJField;
        
        private string zLHBJField;
        
        private string zKHBJField;
        
        private string zSFBGSDField;
        
        private string zSFBGGDField;
        
        private string zJSBJField;
        
        private string zFJBJField;
        
        private string zBJCYBJField;
        
        private string zXZBJField;
        
        private string zJYBJField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string FWO_ID {
            get {
                return this.fWO_IDField;
            }
            set {
                this.fWO_IDField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string FOO_ID {
            get {
                return this.fOO_IDField;
            }
            set {
                this.fOO_IDField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string EVENT_CODE {
            get {
                return this.eVENT_CODEField;
            }
            set {
                this.eVENT_CODEField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string EVENT_DAT {
            get {
                return this.eVENT_DATField;
            }
            set {
                this.eVENT_DATField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ORDER", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public DT_CUS_CUS1002_REQITEMORDER[] ORDER {
            get {
                return this.oRDERField;
            }
            set {
                this.oRDERField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ZBGDTS {
            get {
                return this.zBGDTSField;
            }
            set {
                this.zBGDTSField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ZCYCS {
            get {
                return this.zCYCSField;
            }
            set {
                this.zCYCSField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ZDDCS {
            get {
                return this.zDDCSField;
            }
            set {
                this.zDDCSField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ZLHCS {
            get {
                return this.zLHCSField;
            }
            set {
                this.zLHCSField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ZBGSDCS {
            get {
                return this.zBGSDCSField;
            }
            set {
                this.zBGSDCSField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ZBGGDCS {
            get {
                return this.zBGGDCSField;
            }
            set {
                this.zBGGDCSField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ZBGDGS {
            get {
                return this.zBGDGSField;
            }
            set {
                this.zBGDGSField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ZHGXYBZ {
            get {
                return this.zHGXYBZField;
            }
            set {
                this.zHGXYBZField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ZJYH {
            get {
                return this.zJYHField;
            }
            set {
                this.zJYHField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ZCYLX {
            get {
                return this.zCYLXField;
            }
            set {
                this.zCYLXField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ZJYDD {
            get {
                return this.zJYDDField;
            }
            set {
                this.zJYDDField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ZPTSC {
            get {
                return this.zPTSCField;
            }
            set {
                this.zPTSCField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ZBGCYBJ {
            get {
                return this.zBGCYBJField;
            }
            set {
                this.zBGCYBJField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ZDDBJ {
            get {
                return this.zDDBJField;
            }
            set {
                this.zDDBJField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ZLHBJ {
            get {
                return this.zLHBJField;
            }
            set {
                this.zLHBJField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ZKHBJ {
            get {
                return this.zKHBJField;
            }
            set {
                this.zKHBJField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ZSFBGSD {
            get {
                return this.zSFBGSDField;
            }
            set {
                this.zSFBGSDField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ZSFBGGD {
            get {
                return this.zSFBGGDField;
            }
            set {
                this.zSFBGGDField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ZJSBJ {
            get {
                return this.zJSBJField;
            }
            set {
                this.zJSBJField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ZFJBJ {
            get {
                return this.zFJBJField;
            }
            set {
                this.zFJBJField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ZBJCYBJ {
            get {
                return this.zBJCYBJField;
            }
            set {
                this.zBJCYBJField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ZXZBJ {
            get {
                return this.zXZBJField;
            }
            set {
                this.zXZBJField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ZJYBJ {
            get {
                return this.zJYBJField;
            }
            set {
                this.zJYBJField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18408")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="URN:CUS1002")]
    public partial class DT_CUS_CUS1002_REQITEMORDER {
        
        private string zBGDHField;
        
        private string zZGYLHField;
        
        private string zMYFSField;
        
        private string zBGDZSField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ZBGDH {
            get {
                return this.zBGDHField;
            }
            set {
                this.zBGDHField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ZZGYLH {
            get {
                return this.zZGYLHField;
            }
            set {
                this.zZGYLHField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ZMYFS {
            get {
                return this.zMYFSField;
            }
            set {
                this.zMYFSField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ZBGDZS {
            get {
                return this.zBGDZSField;
            }
            set {
                this.zBGDZSField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18408")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="URN:CUS1002")]
    public partial class DT_CUS_CUS1002_RES {
        
        private string eV_ERRORField;
        
        private string eV_MSGField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string EV_ERROR {
            get {
                return this.eV_ERRORField;
            }
            set {
                this.eV_ERRORField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string EV_MSG {
            get {
                return this.eV_MSGField;
            }
            set {
                this.eV_MSGField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    public delegate void SI_CUS_CUS1002CompletedEventHandler(object sender, SI_CUS_CUS1002CompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SI_CUS_CUS1002CompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SI_CUS_CUS1002CompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public DT_CUS_CUS1002_RES Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((DT_CUS_CUS1002_RES)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591