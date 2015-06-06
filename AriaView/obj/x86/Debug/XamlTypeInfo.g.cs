﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------



namespace AriaView
{
    public partial class App : global::Windows.UI.Xaml.Markup.IXamlMetadataProvider
    {
        private global::AriaView.AriaView_XamlTypeInfo.XamlTypeInfoProvider _provider;

        public global::Windows.UI.Xaml.Markup.IXamlType GetXamlType(global::System.Type type)
        {
            if(_provider == null)
            {
                _provider = new global::AriaView.AriaView_XamlTypeInfo.XamlTypeInfoProvider();
            }
            return _provider.GetXamlTypeByType(type);
        }

        public global::Windows.UI.Xaml.Markup.IXamlType GetXamlType(string fullName)
        {
            if(_provider == null)
            {
                _provider = new global::AriaView.AriaView_XamlTypeInfo.XamlTypeInfoProvider();
            }
            return _provider.GetXamlTypeByName(fullName);
        }

        public global::Windows.UI.Xaml.Markup.XmlnsDefinition[] GetXmlnsDefinitions()
        {
            return new global::Windows.UI.Xaml.Markup.XmlnsDefinition[0];
        }
    }
}

namespace AriaView.AriaView_XamlTypeInfo
{
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks", "4.0.0.0")]    
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    internal partial class XamlTypeInfoProvider
    {
        public global::Windows.UI.Xaml.Markup.IXamlType GetXamlTypeByType(global::System.Type type)
        {
            global::Windows.UI.Xaml.Markup.IXamlType xamlType;
            if (_xamlTypeCacheByType.TryGetValue(type, out xamlType))
            {
                return xamlType;
            }
            int typeIndex = LookupTypeIndexByType(type);
            if(typeIndex != -1)
            {
                xamlType = CreateXamlType(typeIndex);
            }
            if (xamlType != null)
            {
                _xamlTypeCacheByName.Add(xamlType.FullName, xamlType);
                _xamlTypeCacheByType.Add(xamlType.UnderlyingType, xamlType);
            }
            return xamlType;
        }

        public global::Windows.UI.Xaml.Markup.IXamlType GetXamlTypeByName(string typeName)
        {
            if (string.IsNullOrEmpty(typeName))
            {
                return null;
            }
            global::Windows.UI.Xaml.Markup.IXamlType xamlType;
            if (_xamlTypeCacheByName.TryGetValue(typeName, out xamlType))
            {
                return xamlType;
            }
            int typeIndex = LookupTypeIndexByName(typeName);
            if(typeIndex != -1)
            {
                xamlType = CreateXamlType(typeIndex);
            }
            if (xamlType != null)
            {
                _xamlTypeCacheByName.Add(xamlType.FullName, xamlType);
                _xamlTypeCacheByType.Add(xamlType.UnderlyingType, xamlType);
            }
            return xamlType;
        }

        public global::Windows.UI.Xaml.Markup.IXamlMember GetMemberByLongName(string longMemberName)
        {
            if (string.IsNullOrEmpty(longMemberName))
            {
                return null;
            }
            global::Windows.UI.Xaml.Markup.IXamlMember xamlMember;
            if (_xamlMembers.TryGetValue(longMemberName, out xamlMember))
            {
                return xamlMember;
            }
            xamlMember = CreateXamlMember(longMemberName);
            if (xamlMember != null)
            {
                _xamlMembers.Add(longMemberName, xamlMember);
            }
            return xamlMember;
        }

        global::System.Collections.Generic.Dictionary<string, global::Windows.UI.Xaml.Markup.IXamlType>
                _xamlTypeCacheByName = new global::System.Collections.Generic.Dictionary<string, global::Windows.UI.Xaml.Markup.IXamlType>();

        global::System.Collections.Generic.Dictionary<global::System.Type, global::Windows.UI.Xaml.Markup.IXamlType>
                _xamlTypeCacheByType = new global::System.Collections.Generic.Dictionary<global::System.Type, global::Windows.UI.Xaml.Markup.IXamlType>();

        global::System.Collections.Generic.Dictionary<string, global::Windows.UI.Xaml.Markup.IXamlMember>
                _xamlMembers = new global::System.Collections.Generic.Dictionary<string, global::Windows.UI.Xaml.Markup.IXamlMember>();

        string[] _typeNameTable = null;
        global::System.Type[] _typeTable = null;

        private void InitTypeTables()
        {
            _typeNameTable = new string[14];
            _typeNameTable[0] = "AriaView.Model.AuthentificationForm";
            _typeNameTable[1] = "Windows.UI.Xaml.Controls.UserControl";
            _typeNameTable[2] = "Windows.UI.Xaml.Controls.Page";
            _typeNameTable[3] = "AriaView.ViewModel.AuthentificationViewModel";
            _typeNameTable[4] = "AriaView.Common.ObservableDictionary";
            _typeNameTable[5] = "Object";
            _typeNameTable[6] = "String";
            _typeNameTable[7] = "AriaView.MainPage";
            _typeNameTable[8] = "AriaView.Model.MapView";
            _typeNameTable[9] = "AriaView.Model.MapPage";
            _typeNameTable[10] = "AriaView.ViewModel.MapPageViewModel";
            _typeNameTable[11] = "AriaView.Common.NavigationHelper";
            _typeNameTable[12] = "Windows.UI.Xaml.DependencyObject";
            _typeNameTable[13] = "AriaView.SiteSelectionPage";

            _typeTable = new global::System.Type[14];
            _typeTable[0] = typeof(global::AriaView.Model.AuthentificationForm);
            _typeTable[1] = typeof(global::Windows.UI.Xaml.Controls.UserControl);
            _typeTable[2] = typeof(global::Windows.UI.Xaml.Controls.Page);
            _typeTable[3] = typeof(global::AriaView.ViewModel.AuthentificationViewModel);
            _typeTable[4] = typeof(global::AriaView.Common.ObservableDictionary);
            _typeTable[5] = typeof(global::System.Object);
            _typeTable[6] = typeof(global::System.String);
            _typeTable[7] = typeof(global::AriaView.MainPage);
            _typeTable[8] = typeof(global::AriaView.Model.MapView);
            _typeTable[9] = typeof(global::AriaView.Model.MapPage);
            _typeTable[10] = typeof(global::AriaView.ViewModel.MapPageViewModel);
            _typeTable[11] = typeof(global::AriaView.Common.NavigationHelper);
            _typeTable[12] = typeof(global::Windows.UI.Xaml.DependencyObject);
            _typeTable[13] = typeof(global::AriaView.SiteSelectionPage);
        }

        private int LookupTypeIndexByName(string typeName)
        {
            if (_typeNameTable == null)
            {
                InitTypeTables();
            }
            for (int i=0; i<_typeNameTable.Length; i++)
            {
                if(0 == string.CompareOrdinal(_typeNameTable[i], typeName))
                {
                    return i;
                }
            }
            return -1;
        }

        private int LookupTypeIndexByType(global::System.Type type)
        {
            if (_typeTable == null)
            {
                InitTypeTables();
            }
            for(int i=0; i<_typeTable.Length; i++)
            {
                if(type == _typeTable[i])
                {
                    return i;
                }
            }
            return -1;
        }

        private object Activate_0_AuthentificationForm() { return new global::AriaView.Model.AuthentificationForm(); }
        private object Activate_3_AuthentificationViewModel() { return new global::AriaView.ViewModel.AuthentificationViewModel(); }
        private object Activate_4_ObservableDictionary() { return new global::AriaView.Common.ObservableDictionary(); }
        private object Activate_7_MainPage() { return new global::AriaView.MainPage(); }
        private object Activate_8_MapView() { return new global::AriaView.Model.MapView(); }
        private object Activate_9_MapPage() { return new global::AriaView.Model.MapPage(); }
        private object Activate_10_MapPageViewModel() { return new global::AriaView.ViewModel.MapPageViewModel(); }
        private object Activate_13_SiteSelectionPage() { return new global::AriaView.SiteSelectionPage(); }
        private void MapAdd_3_AuthentificationViewModel(object instance, object key, object item)
        {
            var collection = (global::System.Collections.Generic.IDictionary<global::System.String, global::System.Object>)instance;
            var newKey = (global::System.String)key;
            var newItem = (global::System.Object)item;
            collection.Add(newKey, newItem);
        }
        private void MapAdd_4_ObservableDictionary(object instance, object key, object item)
        {
            var collection = (global::System.Collections.Generic.IDictionary<global::System.String, global::System.Object>)instance;
            var newKey = (global::System.String)key;
            var newItem = (global::System.Object)item;
            collection.Add(newKey, newItem);
        }
        private void MapAdd_10_MapPageViewModel(object instance, object key, object item)
        {
            var collection = (global::System.Collections.Generic.IDictionary<global::System.String, global::System.Object>)instance;
            var newKey = (global::System.String)key;
            var newItem = (global::System.Object)item;
            collection.Add(newKey, newItem);
        }

        private global::Windows.UI.Xaml.Markup.IXamlType CreateXamlType(int typeIndex)
        {
            global::AriaView.AriaView_XamlTypeInfo.XamlSystemBaseType xamlType = null;
            global::AriaView.AriaView_XamlTypeInfo.XamlUserType userType;
            string typeName = _typeNameTable[typeIndex];
            global::System.Type type = _typeTable[typeIndex];

            switch (typeIndex)
            {

            case 0:   //  AriaView.Model.AuthentificationForm
                userType = new global::AriaView.AriaView_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Windows.UI.Xaml.Controls.UserControl"));
                userType.Activator = Activate_0_AuthentificationForm;
                userType.AddMemberName("Caller");
                userType.AddMemberName("ViewModel");
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 1:   //  Windows.UI.Xaml.Controls.UserControl
                xamlType = new global::AriaView.AriaView_XamlTypeInfo.XamlSystemBaseType(typeName, type);
                break;

            case 2:   //  Windows.UI.Xaml.Controls.Page
                xamlType = new global::AriaView.AriaView_XamlTypeInfo.XamlSystemBaseType(typeName, type);
                break;

            case 3:   //  AriaView.ViewModel.AuthentificationViewModel
                userType = new global::AriaView.AriaView_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("AriaView.Common.ObservableDictionary"));
                userType.DictionaryAdd = MapAdd_3_AuthentificationViewModel;
                userType.SetIsReturnTypeStub();
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 4:   //  AriaView.Common.ObservableDictionary
                userType = new global::AriaView.AriaView_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Object"));
                userType.Activator = Activate_4_ObservableDictionary;
                userType.DictionaryAdd = MapAdd_4_ObservableDictionary;
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 5:   //  Object
                xamlType = new global::AriaView.AriaView_XamlTypeInfo.XamlSystemBaseType(typeName, type);
                break;

            case 6:   //  String
                xamlType = new global::AriaView.AriaView_XamlTypeInfo.XamlSystemBaseType(typeName, type);
                break;

            case 7:   //  AriaView.MainPage
                userType = new global::AriaView.AriaView_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
                userType.Activator = Activate_7_MainPage;
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 8:   //  AriaView.Model.MapView
                userType = new global::AriaView.AriaView_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Windows.UI.Xaml.Controls.UserControl"));
                userType.Activator = Activate_8_MapView;
                userType.AddMemberName("ViewModel");
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 9:   //  AriaView.Model.MapPage
                userType = new global::AriaView.AriaView_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
                userType.Activator = Activate_9_MapPage;
                userType.AddMemberName("ViewModel");
                userType.AddMemberName("NavigationHelper");
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 10:   //  AriaView.ViewModel.MapPageViewModel
                userType = new global::AriaView.AriaView_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("AriaView.Common.ObservableDictionary"));
                userType.DictionaryAdd = MapAdd_10_MapPageViewModel;
                userType.SetIsReturnTypeStub();
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 11:   //  AriaView.Common.NavigationHelper
                userType = new global::AriaView.AriaView_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Windows.UI.Xaml.DependencyObject"));
                userType.SetIsReturnTypeStub();
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 12:   //  Windows.UI.Xaml.DependencyObject
                xamlType = new global::AriaView.AriaView_XamlTypeInfo.XamlSystemBaseType(typeName, type);
                break;

            case 13:   //  AriaView.SiteSelectionPage
                userType = new global::AriaView.AriaView_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
                userType.Activator = Activate_13_SiteSelectionPage;
                userType.AddMemberName("NavigationHelper");
                userType.SetIsLocalType();
                xamlType = userType;
                break;
            }
            return xamlType;
        }


        private object get_0_AuthentificationForm_Caller(object instance)
        {
            var that = (global::AriaView.Model.AuthentificationForm)instance;
            return that.Caller;
        }
        private void set_0_AuthentificationForm_Caller(object instance, object Value)
        {
            var that = (global::AriaView.Model.AuthentificationForm)instance;
            that.Caller = (global::Windows.UI.Xaml.Controls.Page)Value;
        }
        private object get_1_AuthentificationForm_ViewModel(object instance)
        {
            var that = (global::AriaView.Model.AuthentificationForm)instance;
            return that.ViewModel;
        }
        private void set_1_AuthentificationForm_ViewModel(object instance, object Value)
        {
            var that = (global::AriaView.Model.AuthentificationForm)instance;
            that.ViewModel = (global::AriaView.ViewModel.AuthentificationViewModel)Value;
        }
        private object get_2_MapView_ViewModel(object instance)
        {
            var that = (global::AriaView.Model.MapView)instance;
            return that.ViewModel;
        }
        private object get_3_MapPage_ViewModel(object instance)
        {
            var that = (global::AriaView.Model.MapPage)instance;
            return that.ViewModel;
        }
        private object get_4_MapPage_NavigationHelper(object instance)
        {
            var that = (global::AriaView.Model.MapPage)instance;
            return that.NavigationHelper;
        }
        private object get_5_SiteSelectionPage_NavigationHelper(object instance)
        {
            var that = (global::AriaView.SiteSelectionPage)instance;
            return that.NavigationHelper;
        }

        private global::Windows.UI.Xaml.Markup.IXamlMember CreateXamlMember(string longMemberName)
        {
            global::AriaView.AriaView_XamlTypeInfo.XamlMember xamlMember = null;
            global::AriaView.AriaView_XamlTypeInfo.XamlUserType userType;

            switch (longMemberName)
            {
            case "AriaView.Model.AuthentificationForm.Caller":
                userType = (global::AriaView.AriaView_XamlTypeInfo.XamlUserType)GetXamlTypeByName("AriaView.Model.AuthentificationForm");
                xamlMember = new global::AriaView.AriaView_XamlTypeInfo.XamlMember(this, "Caller", "Windows.UI.Xaml.Controls.Page");
                xamlMember.Getter = get_0_AuthentificationForm_Caller;
                xamlMember.Setter = set_0_AuthentificationForm_Caller;
                break;
            case "AriaView.Model.AuthentificationForm.ViewModel":
                userType = (global::AriaView.AriaView_XamlTypeInfo.XamlUserType)GetXamlTypeByName("AriaView.Model.AuthentificationForm");
                xamlMember = new global::AriaView.AriaView_XamlTypeInfo.XamlMember(this, "ViewModel", "AriaView.ViewModel.AuthentificationViewModel");
                xamlMember.Getter = get_1_AuthentificationForm_ViewModel;
                xamlMember.Setter = set_1_AuthentificationForm_ViewModel;
                break;
            case "AriaView.Model.MapView.ViewModel":
                userType = (global::AriaView.AriaView_XamlTypeInfo.XamlUserType)GetXamlTypeByName("AriaView.Model.MapView");
                xamlMember = new global::AriaView.AriaView_XamlTypeInfo.XamlMember(this, "ViewModel", "AriaView.Common.ObservableDictionary");
                xamlMember.Getter = get_2_MapView_ViewModel;
                xamlMember.SetIsReadOnly();
                break;
            case "AriaView.Model.MapPage.ViewModel":
                userType = (global::AriaView.AriaView_XamlTypeInfo.XamlUserType)GetXamlTypeByName("AriaView.Model.MapPage");
                xamlMember = new global::AriaView.AriaView_XamlTypeInfo.XamlMember(this, "ViewModel", "AriaView.ViewModel.MapPageViewModel");
                xamlMember.Getter = get_3_MapPage_ViewModel;
                xamlMember.SetIsReadOnly();
                break;
            case "AriaView.Model.MapPage.NavigationHelper":
                userType = (global::AriaView.AriaView_XamlTypeInfo.XamlUserType)GetXamlTypeByName("AriaView.Model.MapPage");
                xamlMember = new global::AriaView.AriaView_XamlTypeInfo.XamlMember(this, "NavigationHelper", "AriaView.Common.NavigationHelper");
                xamlMember.Getter = get_4_MapPage_NavigationHelper;
                xamlMember.SetIsReadOnly();
                break;
            case "AriaView.SiteSelectionPage.NavigationHelper":
                userType = (global::AriaView.AriaView_XamlTypeInfo.XamlUserType)GetXamlTypeByName("AriaView.SiteSelectionPage");
                xamlMember = new global::AriaView.AriaView_XamlTypeInfo.XamlMember(this, "NavigationHelper", "AriaView.Common.NavigationHelper");
                xamlMember.Getter = get_5_SiteSelectionPage_NavigationHelper;
                xamlMember.SetIsReadOnly();
                break;
            }
            return xamlMember;
        }
    }

    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks", "4.0.0.0")]    
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    internal class XamlSystemBaseType : global::Windows.UI.Xaml.Markup.IXamlType
    {
        string _fullName;
        global::System.Type _underlyingType;

        public XamlSystemBaseType(string fullName, global::System.Type underlyingType)
        {
            _fullName = fullName;
            _underlyingType = underlyingType;
        }

        public string FullName { get { return _fullName; } }

        public global::System.Type UnderlyingType
        {
            get
            {
                return _underlyingType;
            }
        }

        virtual public global::Windows.UI.Xaml.Markup.IXamlType BaseType { get { throw new global::System.NotImplementedException(); } }
        virtual public global::Windows.UI.Xaml.Markup.IXamlMember ContentProperty { get { throw new global::System.NotImplementedException(); } }
        virtual public global::Windows.UI.Xaml.Markup.IXamlMember GetMember(string name) { throw new global::System.NotImplementedException(); }
        virtual public bool IsArray { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsCollection { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsConstructible { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsDictionary { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsMarkupExtension { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsBindable { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsReturnTypeStub { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsLocalType { get { throw new global::System.NotImplementedException(); } }
        virtual public global::Windows.UI.Xaml.Markup.IXamlType ItemType { get { throw new global::System.NotImplementedException(); } }
        virtual public global::Windows.UI.Xaml.Markup.IXamlType KeyType { get { throw new global::System.NotImplementedException(); } }
        virtual public object ActivateInstance() { throw new global::System.NotImplementedException(); }
        virtual public void AddToMap(object instance, object key, object item)  { throw new global::System.NotImplementedException(); }
        virtual public void AddToVector(object instance, object item)  { throw new global::System.NotImplementedException(); }
        virtual public void RunInitializer()   { throw new global::System.NotImplementedException(); }
        virtual public object CreateFromString(string input)   { throw new global::System.NotImplementedException(); }
    }
    
    internal delegate object Activator();
    internal delegate void AddToCollection(object instance, object item);
    internal delegate void AddToDictionary(object instance, object key, object item);

    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks", "4.0.0.0")]    
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    internal class XamlUserType : global::AriaView.AriaView_XamlTypeInfo.XamlSystemBaseType
    {
        global::AriaView.AriaView_XamlTypeInfo.XamlTypeInfoProvider _provider;
        global::Windows.UI.Xaml.Markup.IXamlType _baseType;
        bool _isArray;
        bool _isMarkupExtension;
        bool _isBindable;
        bool _isReturnTypeStub;
        bool _isLocalType;

        string _contentPropertyName;
        string _itemTypeName;
        string _keyTypeName;
        global::System.Collections.Generic.Dictionary<string, string> _memberNames;
        global::System.Collections.Generic.Dictionary<string, object> _enumValues;

        public XamlUserType(global::AriaView.AriaView_XamlTypeInfo.XamlTypeInfoProvider provider, string fullName, global::System.Type fullType, global::Windows.UI.Xaml.Markup.IXamlType baseType)
            :base(fullName, fullType)
        {
            _provider = provider;
            _baseType = baseType;
        }

        // --- Interface methods ----

        override public global::Windows.UI.Xaml.Markup.IXamlType BaseType { get { return _baseType; } }
        override public bool IsArray { get { return _isArray; } }
        override public bool IsCollection { get { return (CollectionAdd != null); } }
        override public bool IsConstructible { get { return (Activator != null); } }
        override public bool IsDictionary { get { return (DictionaryAdd != null); } }
        override public bool IsMarkupExtension { get { return _isMarkupExtension; } }
        override public bool IsBindable { get { return _isBindable; } }
        override public bool IsReturnTypeStub { get { return _isReturnTypeStub; } }
        override public bool IsLocalType { get { return _isLocalType; } }

        override public global::Windows.UI.Xaml.Markup.IXamlMember ContentProperty
        {
            get { return _provider.GetMemberByLongName(_contentPropertyName); }
        }

        override public global::Windows.UI.Xaml.Markup.IXamlType ItemType
        {
            get { return _provider.GetXamlTypeByName(_itemTypeName); }
        }

        override public global::Windows.UI.Xaml.Markup.IXamlType KeyType
        {
            get { return _provider.GetXamlTypeByName(_keyTypeName); }
        }

        override public global::Windows.UI.Xaml.Markup.IXamlMember GetMember(string name)
        {
            if (_memberNames == null)
            {
                return null;
            }
            string longName;
            if (_memberNames.TryGetValue(name, out longName))
            {
                return _provider.GetMemberByLongName(longName);
            }
            return null;
        }

        override public object ActivateInstance()
        {
            return Activator(); 
        }

        override public void AddToMap(object instance, object key, object item) 
        {
            DictionaryAdd(instance, key, item);
        }

        override public void AddToVector(object instance, object item)
        {
            CollectionAdd(instance, item);
        }

        override public void RunInitializer() 
        {
            System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor(UnderlyingType.TypeHandle);
        }

        override public object CreateFromString(string input)
        {
            if (_enumValues != null)
            {
                int value = 0;

                string[] valueParts = input.Split(',');

                foreach (string valuePart in valueParts) 
                {
                    object partValue;
                    int enumFieldValue = 0;
                    try
                    {
                        if (_enumValues.TryGetValue(valuePart.Trim(), out partValue))
                        {
                            enumFieldValue = global::System.Convert.ToInt32(partValue);
                        }
                        else
                        {
                            try
                            {
                                enumFieldValue = global::System.Convert.ToInt32(valuePart.Trim());
                            }
                            catch( global::System.FormatException )
                            {
                                foreach( string key in _enumValues.Keys )
                                {
                                    if( string.Compare(valuePart.Trim(), key, global::System.StringComparison.OrdinalIgnoreCase) == 0 )
                                    {
                                        if( _enumValues.TryGetValue(key.Trim(), out partValue) )
                                        {
                                            enumFieldValue = global::System.Convert.ToInt32(partValue);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        value |= enumFieldValue; 
                    }
                    catch( global::System.FormatException )
                    {
                        throw new global::System.ArgumentException(input, FullName);
                    }
                }

                return value; 
            }
            throw new global::System.ArgumentException(input, FullName);
        }

        // --- End of Interface methods

        public Activator Activator { get; set; }
        public AddToCollection CollectionAdd { get; set; }
        public AddToDictionary DictionaryAdd { get; set; }

        public void SetContentPropertyName(string contentPropertyName)
        {
            _contentPropertyName = contentPropertyName;
        }

        public void SetIsArray()
        {
            _isArray = true; 
        }

        public void SetIsMarkupExtension()
        {
            _isMarkupExtension = true;
        }

        public void SetIsBindable()
        {
            _isBindable = true;
        }

        public void SetIsReturnTypeStub()
        {
            _isReturnTypeStub = true;
        }

        public void SetIsLocalType()
        {
            _isLocalType = true;
        }

        public void SetItemTypeName(string itemTypeName)
        {
            _itemTypeName = itemTypeName;
        }

        public void SetKeyTypeName(string keyTypeName)
        {
            _keyTypeName = keyTypeName;
        }

        public void AddMemberName(string shortName)
        {
            if(_memberNames == null)
            {
                _memberNames =  new global::System.Collections.Generic.Dictionary<string,string>();
            }
            _memberNames.Add(shortName, FullName + "." + shortName);
        }

        public void AddEnumValue(string name, object value)
        {
            if (_enumValues == null)
            {
                _enumValues = new global::System.Collections.Generic.Dictionary<string, object>();
            }
            _enumValues.Add(name, value);
        }
    }

    internal delegate object Getter(object instance);
    internal delegate void Setter(object instance, object value);

    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks", "4.0.0.0")]    
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    internal class XamlMember : global::Windows.UI.Xaml.Markup.IXamlMember
    {
        global::AriaView.AriaView_XamlTypeInfo.XamlTypeInfoProvider _provider;
        string _name;
        bool _isAttachable;
        bool _isDependencyProperty;
        bool _isReadOnly;

        string _typeName;
        string _targetTypeName;

        public XamlMember(global::AriaView.AriaView_XamlTypeInfo.XamlTypeInfoProvider provider, string name, string typeName)
        {
            _name = name;
            _typeName = typeName;
            _provider = provider;
        }

        public string Name { get { return _name; } }

        public global::Windows.UI.Xaml.Markup.IXamlType Type
        {
            get { return _provider.GetXamlTypeByName(_typeName); }
        }

        public void SetTargetTypeName(string targetTypeName)
        {
            _targetTypeName = targetTypeName;
        }
        public global::Windows.UI.Xaml.Markup.IXamlType TargetType
        {
            get { return _provider.GetXamlTypeByName(_targetTypeName); }
        }

        public void SetIsAttachable() { _isAttachable = true; }
        public bool IsAttachable { get { return _isAttachable; } }

        public void SetIsDependencyProperty() { _isDependencyProperty = true; }
        public bool IsDependencyProperty { get { return _isDependencyProperty; } }

        public void SetIsReadOnly() { _isReadOnly = true; }
        public bool IsReadOnly { get { return _isReadOnly; } }

        public Getter Getter { get; set; }
        public object GetValue(object instance)
        {
            if (Getter != null)
                return Getter(instance);
            else
                throw new global::System.InvalidOperationException("GetValue");
        }

        public Setter Setter { get; set; }
        public void SetValue(object instance, object value)
        {
            if (Setter != null)
                Setter(instance, value);
            else
                throw new global::System.InvalidOperationException("SetValue");
        }
    }
}





