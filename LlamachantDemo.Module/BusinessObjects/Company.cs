using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace LlamachantDemo.Module.BusinessObjects
{
    [DefaultClassOptions]
     public class Company : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public Company(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        string taxId;
        string phoneNumber;
        string address;
        string name;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Name
        {
            get => name;
            set => SetPropertyValue(nameof(Name), ref name, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Address
        {
            get => address;
            set => SetPropertyValue(nameof(Address), ref address, value);
        }
        [Size(-1)]
        [ImageEditor(ListViewImageEditorCustomHeight = 40)]
        public byte[] Photo
        {
            get { return GetPropertyValue<byte[]>(nameof(Photo)); }
            set { SetPropertyValue<byte[]>(nameof(Photo), value); }
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string PhoneNumber
        {
            get => phoneNumber;
            set => SetPropertyValue(nameof(PhoneNumber), ref phoneNumber, value);
        }
        
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string TaxId
        {
            get => taxId;
            set => SetPropertyValue(nameof(TaxId), ref taxId, value);
        }
        [Association("Company-Departments")]
        public XPCollection<Department> Departments
        {
            get
            {
                return GetCollection<Department>(nameof(Departments));
            }
        }
        [Association("Company-Contacts")]
        public XPCollection<Contacts> Contacts
        {
            get
            {
                return GetCollection<Contacts>(nameof(Contacts));
            }
        }
    }
}