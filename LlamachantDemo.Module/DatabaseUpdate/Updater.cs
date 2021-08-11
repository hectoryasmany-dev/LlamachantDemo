using System;
using System.Linq;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Updating;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.BaseImpl;
using LlamachantDemo.Module.BusinessObjects;
using Bogus;
using DevExpress.ExpressApp.Utils;
using System.Drawing;

namespace LlamachantDemo.Module.DatabaseUpdate {
    // For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Updating.ModuleUpdater
    public class Updater : ModuleUpdater {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
            base(objectSpace, currentDBVersion) {
        }
        public override void UpdateDatabaseAfterUpdateSchema() {
            base.UpdateDatabaseAfterUpdateSchema();
            //string name = "MyName";
            
            Company company = ObjectSpace.FirstOrDefault<Company>(u => u.Name == "Llamachant Ltd.");
            if (company == null)
            {
                var imagen= ImageLoader.Instance.GetImageInfo("llamachantLogo.png").Image;
                ImageConverter imageConverter = new ImageConverter();
                var photo = (Byte[])imageConverter.ConvertTo(imagen, typeof(Byte[]));
                company = ObjectSpace.CreateObject<Company>();
                company.Name = "Llamachant Ltd.";
                company.PhoneNumber = "416-477-2560";
                company.Address = "16 Beckett View Drive, Pembroke, ON, K8A 6W2, Canada";
                company.TaxId = "LL2021LTD";
                company.Photo = photo;

                ObjectSpace.CommitChanges();
            }
            
            
            if (ObjectSpace.GetObjects<Contacts>().Count == 0)
            {
                var departments = ObjectSpace.GetObjects<Department>();

                var Faker = new Faker<Contacts>()
               .CustomInstantiator(f => this.ObjectSpace.CreateObject<Contacts>())
               .RuleFor(o => o.Name, f => f.Name.FirstName())
               .RuleFor(o => o.LastName, f => f.Name.LastName())
               .RuleFor(o => o.PhoneNumber, f => f.Phone.PhoneNumber())
               .RuleFor(o => o.Address, f => f.Address.StreetAddress(true))
               .RuleFor(o => o.Email, f => f.Internet.Email())
               .RuleFor(o => o.Company, company)
               .RuleFor(o => o.HiringDate, f => f.Date.Recent(100))
               .RuleFor(o => o.Budget, f => double.Parse(f.Commerce.Price()))
               .RuleFor(o => o.Department, () => new Faker<Department>().CustomInstantiator(f => this.ObjectSpace.CreateObject<Department>()).RuleFor(o => o.Name, f => f.Commerce.Department())
               .RuleFor(o => o.Name, f => f.Commerce.Department())
               .RuleFor(o => o.Company, company));
                Faker.Generate(100);
            }
            if (ObjectSpace.GetObjects<Clients>().Count == 0)
            {
                var departments = ObjectSpace.GetObjects<Department>();
                var Faker = new Faker<Clients>()
               .CustomInstantiator(f => this.ObjectSpace.CreateObject<Clients>())
               .RuleFor(o => o.Name, f => f.Name.FirstName())
               .RuleFor(o => o.LastName, f => f.Name.LastName())
               .RuleFor(o => o.PhoneNumber, f => f.Phone.PhoneNumber())
               .RuleFor(o => o.Address, f => f.Address.StreetAddress(true))
               .RuleFor(o => o.Email, f => f.Internet.Email())
               .RuleFor(o => o.HiringDate, f => f.Date.Recent(100))
               .RuleFor(o => o.Budget, f => double.Parse(f.Commerce.Price()))
               .RuleFor(o => o.Company, company)
               .RuleFor(o => o.Department, () => new Faker<Department>().CustomInstantiator(f => this.ObjectSpace.CreateObject<Department>()).RuleFor(o => o.Name, f => f.Commerce.Department())
               .RuleFor(o => o.Company, company))              
               .RuleFor(o => o.ClientCode, f => f.Random.AlphaNumeric(8));
                Faker.Generate(100);
                
            }
     
            
            ObjectSpace.CommitChanges();
            //ObjectSpace.CommitChanges(); //Uncomment this line to persist created object(s).
        }
        public override void UpdateDatabaseBeforeUpdateSchema() {
            base.UpdateDatabaseBeforeUpdateSchema();

            
            //if(CurrentDBVersion < new Version("1.1.0.0") && CurrentDBVersion > new Version("0.0.0.0")) {
            //    RenameColumn("DomainObject1Table", "OldColumnName", "NewColumnName");
            //}
        }
    }
}
