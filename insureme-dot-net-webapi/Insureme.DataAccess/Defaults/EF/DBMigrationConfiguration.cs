using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Resources;
using Insureme.Core.v1.Entities;
using Insureme.Core.v1.Helpers;
using Insureme.Core.v1.Entities.Dipn;

namespace Insureme.DataAccess.Defaults.EF
{
    public class DBMigrationConfiguration : DbMigrationsConfiguration<DataContext>
    {
        private const string ConfigDbMigrationDirectory = @"Migrations\DBContextMigrations";

        public DBMigrationConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;

            MigrationsDirectory = ConfigDbMigrationDirectory;
        }

        protected override void Seed(DataContext context)
        {
            var dateTime = DateTime.UtcNow;

            //context.Database.ExecuteSqlCommand("ALTER DATABASE InsureMe SET ALLOW_SNAPSHOT_ISOLATION ON");
            //context.Database.ExecuteSqlCommand("ALTER DATABASE InsureMe SET READ_COMMITTED_SNAPSHOT ON");

            if (!context.DipnAppTypes.Any())
            {
                context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('DipnAppTypes', RESEED, 0)");

                context.DipnAppTypes.Add(new AppTypeEntity { Name = "Unknown", Enabled = true, DateTimeCreated = dateTime });
                context.DipnAppTypes.Add(new AppTypeEntity { Name = "Windows", Enabled = true, DateTimeCreated = dateTime });
                context.DipnAppTypes.Add(new AppTypeEntity { Name = "Mac", Enabled = true, DateTimeCreated = dateTime });
                context.DipnAppTypes.Add(new AppTypeEntity { Name = "Linux", Enabled = true, DateTimeCreated = dateTime });

                context.SaveChanges();
            }

            if (!context.DipnApps.Any())
            {
                //context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('DipnApps', RESEED, 1)");
            }

            if (!context.DipnDeviceTypes.Any())
            {
                context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('DipnDeviceTypes', RESEED, 0)");

                context.DipnDeviceTypes.Add(new DeviceTypeEntity { Name = "Unknown", Enabled = true, DateTimeCreated = dateTime });
                context.DipnDeviceTypes.Add(new DeviceTypeEntity { Name = "Android", Enabled = true, DateTimeCreated = dateTime });
                context.DipnDeviceTypes.Add(new DeviceTypeEntity { Name = "iOS", Enabled = true, DateTimeCreated = dateTime });
                context.DipnDeviceTypes.Add(new DeviceTypeEntity { Name = "Windows", Enabled = true, DateTimeCreated = dateTime });

                context.SaveChanges();
            }

            if (!context.DipnDevices.Any())
            {
                //context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('DipnDevices', RESEED, 1)");
            }

            if (!context.Configurations.Any())
            {
                context.Configurations.Add(new ConfigurationEntity { DateTimeCreated = dateTime, BackEndKey = "5FCCBAC26EDE47A4BB5470706EFA50B0", TokenLifeSpanMinutes = 5.00f, GstAustraliaPercent = 10.00f });
                context.SaveChanges();
            }

            if (!context.Keys.Any())
            {
                context.Keys.Add(new KeyEntity { Id = Guid.Parse("ac30c82d-25a0-48f4-af24-31dec9688956"), Name = "WebApp", HostRootUrl = "*", Enabled = true, DateTimeCreated = dateTime });
                context.SaveChanges();
            }

            if (!context.Roles.Any())
            {
                context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Roles', RESEED, 0)");

                context.Roles.Add(new RoleEntity { Name = "Not Selected", Code = "N", Enabled = false, DateTimeCreated = dateTime });
                context.Roles.Add(new RoleEntity { Name = "User", Code = "U", Enabled = true, DateTimeCreated = dateTime });
                context.Roles.Add(new RoleEntity { Name = "Client", Code = "C", Enabled = true, DateTimeCreated = dateTime });
                context.Roles.Add(new RoleEntity { Name = "Admin", Code = "A", Enabled = true, DateTimeCreated = dateTime });

                context.SaveChanges();
            }

            if (!context.FamilyTypes.Any())
            {
                context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('FamilyTypes', RESEED, 0)");

                context.FamilyTypes.Add(new FamilyTypeEntity { Name = "Not selected", Code = "N", Enabled = false, DateTimeCreated = dateTime });
                context.FamilyTypes.Add(new FamilyTypeEntity { Name = "Single", Code = "S", Enabled = true, DateTimeCreated = dateTime });
                context.FamilyTypes.Add(new FamilyTypeEntity { Name = "Single parent", Code = "SP", Enabled = true, DateTimeCreated = dateTime });
                context.FamilyTypes.Add(new FamilyTypeEntity { Name = "Single parent plus", Code = "SPP", Enabled = true, DateTimeCreated = dateTime });
                context.FamilyTypes.Add(new FamilyTypeEntity { Name = "Couple", Code = "C", Enabled = true, DateTimeCreated = dateTime });
                context.FamilyTypes.Add(new FamilyTypeEntity { Name = "Family", Code = "CF", Enabled = true, DateTimeCreated = dateTime });
                context.FamilyTypes.Add(new FamilyTypeEntity { Name = "Family plus", Code = "CFP", Enabled = true, DateTimeCreated = dateTime });
                context.FamilyTypes.Add(new FamilyTypeEntity { Name = "Dependants only", Code = "D", Enabled = true, DateTimeCreated = dateTime });

                context.SaveChanges();
            }

            if (!context.IncomeRanges.Any())
            {
                context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('IncomeRanges', RESEED, 0)");

                context.IncomeRanges.Add(new IncomeRangeEntity { Name = "Not Selected", Description = "User has not selected the income range", Enabled = false, DateTimeCreated = dateTime });
                context.IncomeRanges.Add(new IncomeRangeEntity { Name = "$18,201 – $37,000", Description = "Low income", Enabled = true, DateTimeCreated = dateTime });
                context.IncomeRanges.Add(new IncomeRangeEntity { Name = "$37,001 – $80,000", Description = "Moderate income", Enabled = true, DateTimeCreated = dateTime });
                context.IncomeRanges.Add(new IncomeRangeEntity { Name = "$80,001 – $180,000", Description = "Good income", Enabled = true, DateTimeCreated = dateTime });
                context.IncomeRanges.Add(new IncomeRangeEntity { Name = "$180,001 – $350,000", Description = "High income", Enabled = true, DateTimeCreated = dateTime });
                context.IncomeRanges.Add(new IncomeRangeEntity { Name = "$350,001 – $500,000", Description = "Ultra high income", Enabled = true, DateTimeCreated = dateTime });
                context.IncomeRanges.Add(new IncomeRangeEntity { Name = "Over $500,000", Description = "Executive income", Enabled = true, DateTimeCreated = dateTime });

                context.SaveChanges();
            }

            var stateNon = default(StateEntity);
            var stateAct = default(StateEntity);
            var stateNsw = default(StateEntity);
            var stateVic = default(StateEntity);
            var stateQld = default(StateEntity);
            var stateWau = default(StateEntity);
            var stateSau = default(StateEntity);
            var stateNte = default(StateEntity);
            var stateTas = default(StateEntity);

            if (!context.States.Any())
            {
                context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('States', RESEED, 0)");

                stateNon = context.States.Add(new StateEntity { Name = "Not Selected", Code = "NON", Enabled = false, DateTimeCreated = dateTime });
                stateAct = context.States.Add(new StateEntity { Name = "Australian Capital Territory", Code = "ACT", Enabled = true, DateTimeCreated = dateTime });
                stateNsw = context.States.Add(new StateEntity { Name = "New South Wales", Code = "NSW", Enabled = true, DateTimeCreated = dateTime });
                stateVic = context.States.Add(new StateEntity { Name = "Victoria", Code = "VIC", Enabled = true, DateTimeCreated = dateTime });
                stateQld = context.States.Add(new StateEntity { Name = "Queensland", Code = "QLD", Enabled = true, DateTimeCreated = dateTime });
                stateWau = context.States.Add(new StateEntity { Name = "Western Australia", Code = "WA", Enabled = true, DateTimeCreated = dateTime });
                stateSau = context.States.Add(new StateEntity { Name = "South Australia", Code = "SA", Enabled = true, DateTimeCreated = dateTime });
                stateNte = context.States.Add(new StateEntity { Name = "Northern Territory", Code = "NT", Enabled = true, DateTimeCreated = dateTime });
                stateTas = context.States.Add(new StateEntity { Name = "Tasmania", Code = "TAS", Enabled = true, DateTimeCreated = dateTime });

                context.SaveChanges();
            }

            if (!context.ValueTypes.Any() && !context.InputTypes.Any() && !context.Lists.Any() && !context.ListItems.Any() && !context.Properties.Any() && !context.Groups.Any()&& !context.Users.Any() && !context.Clients.Any() && !context.ProductTypes.Any() && !context.Zones.Any())
            {
                context.InputTypes.Add(new InputTypeEntity { Id = 1, Name = "Input", Enabled = true, DateTimeCreated = dateTime }); // 1
                context.InputTypes.Add(new InputTypeEntity { Id = 2, Name = "TextArea", Enabled = true, DateTimeCreated = dateTime }); // 2
                context.InputTypes.Add(new InputTypeEntity { Id = 3, Name = "Select", Enabled = true, DateTimeCreated = dateTime }); // 3
                context.InputTypes.Add(new InputTypeEntity { Id = 4, Name = "MultiSelect", Enabled = true, DateTimeCreated = dateTime }); // 4
                context.InputTypes.Add(new InputTypeEntity { Id = 5, Name = "RadioGroup", Enabled = true, DateTimeCreated = dateTime }); // 5
                context.InputTypes.Add(new InputTypeEntity { Id = 6, Name = "Switch", Enabled = true, DateTimeCreated = dateTime }); // 6
                context.SaveChanges();

                context.ValueTypes.Add(new ValueTypeEntity { Id = 1, Name = "Boolean", Enabled = true, DateTimeCreated = dateTime }); // 1
                context.ValueTypes.Add(new ValueTypeEntity { Id = 2, Name = "Currency", Enabled = true, DateTimeCreated = dateTime }); // 2
                context.ValueTypes.Add(new ValueTypeEntity { Id = 3, Name = "DateTime", Enabled = true, DateTimeCreated = dateTime }); // 3
                context.ValueTypes.Add(new ValueTypeEntity { Id = 4, Name = "Decimal", Enabled = true, DateTimeCreated = dateTime }); // 4
                context.ValueTypes.Add(new ValueTypeEntity { Id = 5, Name = "Number", Enabled = true, DateTimeCreated = dateTime }); // 5
                context.ValueTypes.Add(new ValueTypeEntity { Id = 6, Name = "NumberWithSeparator", Enabled = true, DateTimeCreated = dateTime }); // 6
                context.ValueTypes.Add(new ValueTypeEntity { Id = 7, Name = "Percentage", Enabled = true, DateTimeCreated = dateTime }); // 7
                context.ValueTypes.Add(new ValueTypeEntity { Id = 8, Name = "Text", Enabled = true, DateTimeCreated = dateTime }); // 8
                context.ValueTypes.Add(new ValueTypeEntity { Id = 9, Name = "DecimalWithSeparator", Enabled = true, DateTimeCreated = dateTime }); // 4
                context.ValueTypes.Add(new ValueTypeEntity { Id = 200700, Name = "Currency|Percentage", Enabled = true, DateTimeCreated = dateTime }); // 9
                context.ValueTypes.Add(new ValueTypeEntity { Id = 200800, Name = "Currency|Text", Enabled = true, DateTimeCreated = dateTime }); // 10
                context.ValueTypes.Add(new ValueTypeEntity { Id = 400700, Name = "Decimal|Percentage", Enabled = true, DateTimeCreated = dateTime }); // 11
                context.ValueTypes.Add(new ValueTypeEntity { Id = 400800, Name = "Decimal|Text", Enabled = true, DateTimeCreated = dateTime }); // 12
                context.ValueTypes.Add(new ValueTypeEntity { Id = 500700, Name = "Number|Percentage", Enabled = true, DateTimeCreated = dateTime }); // 13
                context.ValueTypes.Add(new ValueTypeEntity { Id = 500800, Name = "Number|Text", Enabled = true, DateTimeCreated = dateTime }); // 14
                context.SaveChanges();

                context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Lists', RESEED, 0)");

                context.Lists.Add(new ListEntity { Name = "(Empty)", Enabled = false, DateTimeCreated = dateTime }); // 0
                context.Lists.Add(new ListEntity { Name = "PHI Product Australia State Selections", Enabled = true, DateTimeCreated = dateTime }); // 1
                context.Lists.Add(new ListEntity { Name = "PHI Product Cover For Selections", Enabled = true, DateTimeCreated = dateTime }); // 2
                context.Lists.Add(new ListEntity { Name = "PHI Product Features Waiting Perios Units", Enabled = true, DateTimeCreated = dateTime }); // 3
                context.Lists.Add(new ListEntity { Name = "PHI Hospital Accommodation Cover Descriptions", Enabled = true, DateTimeCreated = dateTime }); // 4
                context.Lists.Add(new ListEntity { Name = "PHI Hospital Features Cover Descriptions", Enabled = true, DateTimeCreated = dateTime }); // 5
                context.Lists.Add(new ListEntity { Name = "PHI Hospital Ambulance Cover Descriptions", Enabled = true, DateTimeCreated = dateTime }); // 6
                context.Lists.Add(new ListEntity { Name = "PHI Hospital Joint Replacement Cover Descriptions", Enabled = true, DateTimeCreated = dateTime }); // 7
                context.Lists.Add(new ListEntity { Name = "PHI General Benefit Limits Units", Enabled = true, DateTimeCreated = dateTime }); // 8
                context.Lists.Add(new ListEntity { Name = "PHI General Ambulance Cover Descriptions", Enabled = true, DateTimeCreated = dateTime }); // 9
                context.SaveChanges();

                context.ListItems.Add(new ListItemEntity { ItemId = 0,   Name = "NONE", ListId = 1, Enabled = false, DateTimeCreated = dateTime });
                context.ListItems.Add(new ListItemEntity { ItemId = 1,   Name = "ACT", ListId = 1, Enabled = true, DateTimeCreated = dateTime });
                context.ListItems.Add(new ListItemEntity { ItemId = 2,   Name = "NSW", ListId = 1, Enabled = true, DateTimeCreated = dateTime });
                context.ListItems.Add(new ListItemEntity { ItemId = 3,   Name = "VIC", ListId = 1, Enabled = true, DateTimeCreated = dateTime });
                context.ListItems.Add(new ListItemEntity { ItemId = 4,   Name = "QLD", ListId = 1, Enabled = true, DateTimeCreated = dateTime });
                context.ListItems.Add(new ListItemEntity { ItemId = 5,   Name = "WA", ListId = 1, Enabled = true, DateTimeCreated = dateTime });
                context.ListItems.Add(new ListItemEntity { ItemId = 6,   Name = "SA", ListId = 1, Enabled = true, DateTimeCreated = dateTime });
                context.ListItems.Add(new ListItemEntity { ItemId = 7,   Name = "NT", ListId = 1, Enabled = true, DateTimeCreated = dateTime });
                context.ListItems.Add(new ListItemEntity { ItemId = 8,   Name = "TAS", ListId = 1, Enabled = true, DateTimeCreated = dateTime });
                
                context.ListItems.Add(new ListItemEntity { ItemId = 0,   Name = "Not selected", ListId = 2, Enabled = false, DateTimeCreated = dateTime });
                context.ListItems.Add(new ListItemEntity { ItemId = 1,   Name = "Single", ListId = 2, Enabled = true, DateTimeCreated = dateTime });
                context.ListItems.Add(new ListItemEntity { ItemId = 2,   Name = "Single parent", ListId = 2, Enabled = true, DateTimeCreated = dateTime });
                context.ListItems.Add(new ListItemEntity { ItemId = 3,   Name = "Single parent plus", ListId = 2, Enabled = true, DateTimeCreated = dateTime });
                context.ListItems.Add(new ListItemEntity { ItemId = 4,   Name = "Couple", ListId = 2, Enabled = true, DateTimeCreated = dateTime });
                context.ListItems.Add(new ListItemEntity { ItemId = 5,   Name = "Family", ListId = 2, Enabled = true, DateTimeCreated = dateTime });
                context.ListItems.Add(new ListItemEntity { ItemId = 6,   Name = "Family plus", ListId = 2, Enabled = true, DateTimeCreated = dateTime });
                context.ListItems.Add(new ListItemEntity { ItemId = 7,   Name = "Dependants only", ListId = 2, Enabled = true, DateTimeCreated = dateTime });

                context.ListItems.Add(new ListItemEntity { ItemId = 10,  Name = "Day/s", ListId = 3, Enabled = true, DateTimeCreated = dateTime });
                context.ListItems.Add(new ListItemEntity { ItemId = 20,  Name = "Month/s", ListId = 3, Enabled = true, DateTimeCreated = dateTime });
                context.ListItems.Add(new ListItemEntity { ItemId = 30,  Name = "Year/s", ListId = 3, Enabled = true, DateTimeCreated = dateTime });

                context.ListItems.Add(new ListItemEntity { ItemId = 10,  Name = "Shared room – Public only", ListId = 4, Enabled = true, DateTimeCreated = dateTime });
                context.ListItems.Add(new ListItemEntity { ItemId = 20,  Name = "Private patient – Public Only", ListId = 4, Enabled = true, DateTimeCreated = dateTime });
                context.ListItems.Add(new ListItemEntity { ItemId = 30,  Name = "Shared room – Private or Public", ListId = 4, Enabled = true, DateTimeCreated = dateTime });
                context.ListItems.Add(new ListItemEntity { ItemId = 40,  Name = "Shared – Private & Private – Public", ListId = 4, Enabled = true, DateTimeCreated = dateTime });
                context.ListItems.Add(new ListItemEntity { ItemId = 50,  Name = "Private patient – Private or Public", ListId = 4, Enabled = true, DateTimeCreated = dateTime });

                context.ListItems.Add(new ListItemEntity { ItemId = 10,  Name = "N/A", ListId = 5, Enabled = true, DateTimeCreated = dateTime });
                context.ListItems.Add(new ListItemEntity { ItemId = 20,  Name = "Restricted", ListId = 5, Enabled = true, DateTimeCreated = dateTime });
                context.ListItems.Add(new ListItemEntity { ItemId = 30,  Name = "BLP-02 (benefit limitation period 02 months)", ListId = 5, Enabled = true, DateTimeCreated = dateTime });
                context.ListItems.Add(new ListItemEntity { ItemId = 40,  Name = "BLP-06 (benefit limitation period 06 months)", ListId = 5, Enabled = true, DateTimeCreated = dateTime });
                context.ListItems.Add(new ListItemEntity { ItemId = 50,  Name = "BLP-12 (benefit limitation period 12 months)", ListId = 5, Enabled = true, DateTimeCreated = dateTime });
                context.ListItems.Add(new ListItemEntity { ItemId = 60,  Name = "BLP-24 (benefit limitation period 24 months)", ListId = 5, Enabled = true, DateTimeCreated = dateTime });
                context.ListItems.Add(new ListItemEntity { ItemId = 70,  Name = "Partially covered", ListId = 5, Enabled = true, DateTimeCreated = dateTime });

                context.ListItems.Add(new ListItemEntity { ItemId = 10,  Name = "N/A", ListId = 6, Enabled = true, DateTimeCreated = dateTime });
                context.ListItems.Add(new ListItemEntity { ItemId = 20,  Name = "Partially covered", ListId = 6, Enabled = true, DateTimeCreated = dateTime });

                context.ListItems.Add(new ListItemEntity { ItemId = 10,  Name = "Hip", ListId = 7, Enabled = true, DateTimeCreated = dateTime });
                context.ListItems.Add(new ListItemEntity { ItemId = 20,  Name = "Hip & knee", ListId = 7, Enabled = true, DateTimeCreated = dateTime });
                context.ListItems.Add(new ListItemEntity { ItemId = 30,  Name = "Hip, knee, shoulder & elbow", ListId = 7, Enabled = true, DateTimeCreated = dateTime });

                context.ListItems.Add(new ListItemEntity { ItemId = 10, Name = "Per policy", ListId = 8, Enabled = true, DateTimeCreated = dateTime });
                context.ListItems.Add(new ListItemEntity { ItemId = 20, Name = "Per person", ListId = 8, Enabled = true, DateTimeCreated = dateTime });
                context.ListItems.Add(new ListItemEntity { ItemId = 30, Name = "Per service", ListId = 8, Enabled = true, DateTimeCreated = dateTime });
                context.ListItems.Add(new ListItemEntity { ItemId = 40, Name = "Per lifetime", ListId = 8, Enabled = true, DateTimeCreated = dateTime });
                context.ListItems.Add(new ListItemEntity { ItemId = 50, Name = "Per eligible prescription", ListId = 8, Enabled = true, DateTimeCreated = dateTime });
                context.ListItems.Add(new ListItemEntity { ItemId = 60, Name = "Per hearing aid", ListId = 8, Enabled = true, DateTimeCreated = dateTime });
                context.ListItems.Add(new ListItemEntity { ItemId = 70, Name = "Per monitor", ListId = 8, Enabled = true, DateTimeCreated = dateTime });
                context.ListItems.Add(new ListItemEntity { ItemId = 80, Name = "Per initial visit", ListId = 8, Enabled = true, DateTimeCreated = dateTime });
                context.ListItems.Add(new ListItemEntity { ItemId = 90, Name = "Per subsequent visit", ListId = 8, Enabled = true, DateTimeCreated = dateTime });

                context.ListItems.Add(new ListItemEntity { ItemId = 10, Name = "N/A", ListId = 9, Enabled = true, DateTimeCreated = dateTime });
                context.ListItems.Add(new ListItemEntity { ItemId = 20, Name = "Partially covered", ListId = 9, Enabled = true, DateTimeCreated = dateTime });
                context.ListItems.Add(new ListItemEntity { ItemId = 30, Name = "As per hospital policy", ListId = 9, Enabled = true, DateTimeCreated = dateTime });

                context.SaveChanges();

                var group1 = context.Groups.Add(new GroupEntity { Name = "PHI Product Header Information Group", Title = "Product information", Enabled = true, DateTimeCreated = dateTime }); // 1
                var group2 = context.Groups.Add(new GroupEntity { Name = "PHI Hospital Features Group", Title = "Features", Enabled = true, DateTimeCreated = dateTime }); // 2
                var group3 = context.Groups.Add(new GroupEntity { Name = "PHI Hospital Additional Charges Group", Title = "Additional charges", Enabled = true, DateTimeCreated = dateTime }); // 3
                var group4 = context.Groups.Add(new GroupEntity { Name = "PHI Hospital Misc Information Group", Title = "Information", Enabled = true, DateTimeCreated = dateTime }); // 4
                var group5 = context.Groups.Add(new GroupEntity { Name = "PHI General Features Group", Title = "Features", Enabled = true, DateTimeCreated = dateTime }); // 5
                var group6 = context.Groups.Add(new GroupEntity { Name = "PHI General Misc Information Group", Title = "Information", Enabled = true, DateTimeCreated = dateTime }); // 6
                context.SaveChanges();

                context.Properties.Add(new PropertyEntity { Name = "PHI Product | Medicare Levy Surcharge", Title = "Medicare levy surcharge exempted", Mandatory = true, InputTypeId = 6, TypeId = 1, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, Enabled = true, DateTimeCreated = dateTime, Hint = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit",  Groups = new List<GroupEntity> { group1 } }); // 1
                context.Properties.Add(new PropertyEntity { Name = "PHI Product | Comments", Title = "Comments", Mandatory = false, InputTypeId = 2, TypeId = 8, ValuesListId = 0, UnitsListId = 0, Enabled = true, PossibleNullNumber = false, PossibleComments = false, DateTimeCreated = dateTime, Hint = "Aliquam tincidunt mauris eu risus",  Groups = new List<GroupEntity> { group1 } }); // 2

                context.Properties.Add(new PropertyEntity { Name = "PHI Hospital | Accommodation", Title = "Accommodation covered", Mandatory = true, InputTypeId = 6, TypeId = 1, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, Enabled = true, DateTimeCreated = dateTime, Hint = "Vestibulum auctor dapibus neque",  Groups = new List<GroupEntity> { group2 } }); // 3
                context.Properties.Add(new PropertyEntity { Name = "PHI Hospital | Cardiac", Title = "Cardiac and cardiac related services covered", Mandatory = true, InputTypeId = 6, TypeId = 1, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, Enabled = true, DateTimeCreated = dateTime, Hint = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit",  Groups = new List<GroupEntity> { group2 } }); // 4
                context.Properties.Add(new PropertyEntity { Name = "PHI Hospital | Cataract", Title = "Cataract & eye lens procedures covered", Mandatory = true, InputTypeId = 6, TypeId = 1, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, Enabled = true, DateTimeCreated = dateTime, Hint = "Aliquam tincidunt mauris eu risus",  Groups = new List<GroupEntity> { group2 } }); // 5
                context.Properties.Add(new PropertyEntity { Name = "PHI Hospital | Pregnancy", Title = "Pregnancy and birth related services covered", Mandatory = true, InputTypeId = 6, TypeId = 1, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, Enabled = true, DateTimeCreated = dateTime, Hint = "Vestibulum auctor dapibus neque",  Groups = new List<GroupEntity> { group2 } }); // 6
                context.Properties.Add(new PropertyEntity { Name = "PHI Hospital | Reproductive", Title = "Assisted reproductive services covered", Mandatory = true, InputTypeId = 6, TypeId = 1, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, Enabled = true, DateTimeCreated = dateTime, Hint = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit",  Groups = new List<GroupEntity> { group2 } }); // 7
                context.Properties.Add(new PropertyEntity { Name = "PHI Hospital | Joints", Title = "Joint replacement (including revisions) covered", Mandatory = true, InputTypeId = 6, TypeId = 1, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, Enabled = true, DateTimeCreated = dateTime, Hint = "Aliquam tincidunt mauris eu risus",  Groups = new List<GroupEntity> { group2 } }); // 8
                context.Properties.Add(new PropertyEntity { Name = "PHI Hospital | Dialysis", Title = "Dialysis for chronic renal failure covered", Mandatory = true, InputTypeId = 6, TypeId = 1, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, Enabled = true, DateTimeCreated = dateTime, Hint = "Vestibulum auctor dapibus neque",  Groups = new List<GroupEntity> { group2 } }); // 9
                context.Properties.Add(new PropertyEntity { Name = "PHI Hospital | Gastric", Title = "Gastric banding and related services covered", Mandatory = true, InputTypeId = 6, TypeId = 1, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, Enabled = true, DateTimeCreated = dateTime, Hint = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit",  Groups = new List<GroupEntity> { group2 } }); // 10
                context.Properties.Add(new PropertyEntity { Name = "PHI Hospital | Sterilisation", Title = "Sterilisation covered", Mandatory = true, InputTypeId = 6, TypeId = 1, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, Enabled = true, DateTimeCreated = dateTime, Hint = "Aliquam tincidunt mauris eu risus",  Groups = new List<GroupEntity> { group2 } }); // 11
                context.Properties.Add(new PropertyEntity { Name = "PHI Hospital | Plastic", Title = "Non-cosmentic plastic surgery covered", Mandatory = true, InputTypeId = 6, TypeId = 1, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, Enabled = true, DateTimeCreated = dateTime, Hint = "Vestibulum auctor dapibus neque",  Groups = new List<GroupEntity> { group2 } }); // 12
                context.Properties.Add(new PropertyEntity { Name = "PHI Hospital | Rehabilitation", Title = "Rehabilitation covered", Mandatory = true, InputTypeId = 6, TypeId = 1, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, Enabled = true, DateTimeCreated = dateTime, Hint = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit",  Groups = new List<GroupEntity> { group2 } }); // 13
                context.Properties.Add(new PropertyEntity { Name = "PHI Hospital | Psychiatric", Title = "Psychiatric services covered", Mandatory = true, InputTypeId = 6, TypeId = 1, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, Enabled = true, DateTimeCreated = dateTime, Hint = "Aliquam tincidunt mauris eu risus",  Groups = new List<GroupEntity> { group2 } }); // 14
                context.Properties.Add(new PropertyEntity { Name = "PHI Hospital | Palliative", Title = "Palliative care covered", Mandatory = true, InputTypeId = 6, TypeId = 1, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, Enabled = true, DateTimeCreated = dateTime, Hint = "Vestibulum auctor dapibus neque",  Groups = new List<GroupEntity> { group2 } }); // 15
                context.Properties.Add(new PropertyEntity { Name = "PHI Hospital | Non-Medicare", Title = "Other non-medicare covered", Mandatory = true, InputTypeId = 6, TypeId = 1, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, Enabled = true, DateTimeCreated = dateTime, Hint = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit",  Groups = new List<GroupEntity> { group2 } }); // 16
                context.Properties.Add(new PropertyEntity { Name = "PHI Hospital | Ambulance", Title = "Ambulance covered", Mandatory = true, InputTypeId = 6, TypeId = 1, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, Enabled = true, DateTimeCreated = dateTime, Hint = "Aliquam tincidunt mauris eu risus",  Groups = new List<GroupEntity> { group2 } }); // 17

                context.Properties.Add(new PropertyEntity { Name = "PHI Hospital | Excess", Title = "Excess applicable", Mandatory = true, InputTypeId = 6, TypeId = 1, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, Enabled = true, DateTimeCreated = dateTime, Hint = "Vestibulum auctor dapibus neque",  Groups = new List<GroupEntity> { group3 } }); // 18
                context.Properties.Add(new PropertyEntity { Name = "PHI Hospital | CoPayments", Title = "Co-payments applicable", Mandatory = true, InputTypeId = 6, TypeId = 1, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, Enabled = true, DateTimeCreated = dateTime, Hint = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit",  Groups = new List<GroupEntity> { group3 } }); // 19

                context.Properties.Add(new PropertyEntity { Name = "PHI Hospital | Others", Title = "Other Features", Mandatory = false, InputTypeId = 2, TypeId = 8, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, Enabled = true, DateTimeCreated = dateTime, Hint = "Aliquam tincidunt mauris eu risus",  Groups = new List<GroupEntity> { group4 } }); // 20
                context.Properties.Add(new PropertyEntity { Name = "PHI Hospital | Comments", Title = "Comments", Mandatory = false, InputTypeId = 2, TypeId = 8, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, Enabled = true, DateTimeCreated = dateTime, Hint = "Vestibulum auctor dapibus neque",  Groups = new List<GroupEntity> { group4 } }); // 21

                context.Properties.Add(new PropertyEntity { Name = "PHI General | Dental General Dental", Title = "General dental covered", Mandatory = true, InputTypeId = 6, TypeId = 1, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, Enabled = true, DateTimeCreated = dateTime, Hint = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit",  Groups = new List<GroupEntity> { group5 } }); // 22
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Dental Major Dental", Title = "Major dental covered", Mandatory = true, InputTypeId = 6, TypeId = 1, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, Enabled = true, DateTimeCreated = dateTime, Hint = "Aliquam tincidunt mauris eu risus",  Groups = new List<GroupEntity> { group5 } }); // 23
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Dental Endodontic", Title = "Endodontic covered", Mandatory = true, InputTypeId = 6, TypeId = 1, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, Enabled = true, DateTimeCreated = dateTime, Hint = "Vestibulum auctor dapibus neque",  Groups = new List<GroupEntity> { group5 } }); // 24
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Dental Orthodontic", Title = "Orthodontic covered", Mandatory = true, InputTypeId = 6, TypeId = 1, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, Enabled = true, DateTimeCreated = dateTime, Hint = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit",  Groups = new List<GroupEntity> { group5 } }); // 25
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Optical", Title = "Optical (eg prescribed spectacles / contact lenses) covered", Mandatory = true, InputTypeId = 6, TypeId = 1, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, Enabled = true, DateTimeCreated = dateTime, Hint = "Aliquam tincidunt mauris eu risus",  Groups = new List<GroupEntity> { group5 } }); // 26
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Non Prescribed Pharmaceuticals", Title = "Non prescribed pharmaceuticals covered", Mandatory = true, InputTypeId = 6, TypeId = 1, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, Enabled = true, DateTimeCreated = dateTime, Hint = "Vestibulum auctor dapibus neque",  Groups = new List<GroupEntity> { group5 } }); // 27
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Physiotherapy", Title = "Physiotherapy covered", Mandatory = true, InputTypeId = 6, TypeId = 1, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, Enabled = true, DateTimeCreated = dateTime, Hint = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit",  Groups = new List<GroupEntity> { group5 } }); // 28
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Chiropractic", Title = "Chiropractic covered", Mandatory = true, InputTypeId = 6, TypeId = 1, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, Enabled = true, DateTimeCreated = dateTime, Hint = "Aliquam tincidunt mauris eu risus",  Groups = new List<GroupEntity> { group5 } }); // 29
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Podiatry", Title = "Podiatry covered", Mandatory = true, InputTypeId = 6, TypeId = 1, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, Enabled = true, DateTimeCreated = dateTime, Hint = "Vestibulum auctor dapibus neque",  Groups = new List<GroupEntity> { group5 } }); // 30
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Psychology", Title = "Psychology covered", Mandatory = true, InputTypeId = 6, TypeId = 1, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, Enabled = true, DateTimeCreated = dateTime, Hint = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit",  Groups = new List<GroupEntity> { group5 } }); // 31
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Acupuncture", Title = "Acupuncture covered", Mandatory = true, InputTypeId = 6, TypeId = 1, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, Enabled = true, DateTimeCreated = dateTime, Hint = "Aliquam tincidunt mauris eu risus",  Groups = new List<GroupEntity> { group5 } }); // 32
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Naturopathy", Title = "Naturopathy covered", Mandatory = true, InputTypeId = 6, TypeId = 1, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, Enabled = true, DateTimeCreated = dateTime, Hint = "Vestibulum auctor dapibus neque",  Groups = new List<GroupEntity> { group5 } }); // 33
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Remedial Massage", Title = "Remedial massage covered", Mandatory = true, InputTypeId = 6, TypeId = 1, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, Enabled = true, DateTimeCreated = dateTime, Hint = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit",  Groups = new List<GroupEntity> { group5 } }); // 34
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Hearing Aids", Title = "Hearing aids covered", Mandatory = true, InputTypeId = 6, TypeId = 1, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, Enabled = true, DateTimeCreated = dateTime, Hint = "Aliquam tincidunt mauris eu risus",  Groups = new List<GroupEntity> { group5 } }); // 35
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Blood Glucose Monitors", Title = "Blood glucose monitors covered", Mandatory = true, InputTypeId = 6, TypeId = 1, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, Enabled = true, DateTimeCreated = dateTime, Hint = "Vestibulum auctor dapibus neque",  Groups = new List<GroupEntity> { group5 } }); // 36
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Gap Saver", Title = "Gap saver included", Mandatory = true, InputTypeId = 6, TypeId = 1, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, Enabled = true, DateTimeCreated = dateTime, Hint = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit",  Groups = new List<GroupEntity> { group5 } }); // 37
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Ambulance", Title = "Ambulance covered", Mandatory = true, InputTypeId = 6, TypeId = 1, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, Enabled = true, DateTimeCreated = dateTime, Hint = "Aliquam tincidunt mauris eu risus",  Groups = new List<GroupEntity> { group5 } }); // 38

                context.Properties.Add(new PropertyEntity { Name = "PHI General | Preferred Provider Arrangements", Title = "Preferred provider arrangements", Mandatory = false, InputTypeId = 2, TypeId = 8, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, Enabled = true, DateTimeCreated = dateTime, Hint = "Vestibulum auctor dapibus neque",  Groups = new List<GroupEntity> { group6 } }); // 39
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Others", Title = "Other features", Mandatory = false, InputTypeId = 2, TypeId = 8, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, Enabled = true, DateTimeCreated = dateTime, Hint = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit",  Groups = new List<GroupEntity> { group6 } }); // 40
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Comments", Title = "Comments", Mandatory = false, InputTypeId = 2, TypeId = 8, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, Enabled = true, DateTimeCreated = dateTime, Hint = "Aliquam tincidunt mauris eu risus",  Groups = new List<GroupEntity> { group6 } }); // 41
                context.SaveChanges();

                context.Properties.Add(new PropertyEntity { Name = "PHI Hospital | Accommodation | Cover Description", Title = "Cover description", Mandatory = true, InputTypeId = 3, TypeId = 8, ValuesListId = 4, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 3, Hint = "Nunc dignissim risus id metus", Enabled = true, DateTimeCreated = dateTime }); // 42
                context.Properties.Add(new PropertyEntity { Name = "PHI Hospital | Cardiac | Cover Description", Title = "Cover description", Mandatory = true, InputTypeId = 3, TypeId = 8, ValuesListId = 5, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 4, Hint = "Nunc dignissim risus id metus", Enabled = true, DateTimeCreated = dateTime }); // 43
                context.Properties.Add(new PropertyEntity { Name = "PHI Hospital | Cardiac | Waiting", Title = "Waiting", Mandatory = true, InputTypeId = 1, TypeId = 5, ValuesListId = 0, UnitsListId = 3, PossibleNullNumber = false, PossibleComments = false, ParentId = 4, Hint = "Nunc dignissim risus id metus", Enabled = true, DateTimeCreated = dateTime }); // 44
                context.Properties.Add(new PropertyEntity { Name = "PHI Hospital | Cataract | Cover Description", Title = "Cover description", Mandatory = true, InputTypeId = 3, TypeId = 8, ValuesListId = 5, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 5, Hint = "Nunc dignissim risus id metus", Enabled = true, DateTimeCreated = dateTime }); // 45
                context.Properties.Add(new PropertyEntity { Name = "PHI Hospital | Cataract | Waiting", Title = "Waiting", Mandatory = true, InputTypeId = 1, TypeId = 5, ValuesListId = 0, UnitsListId = 3, PossibleNullNumber = false, PossibleComments = false, ParentId = 5, Hint = "Cras ornare tristique elit", Enabled = true, DateTimeCreated = dateTime }); // 46
                context.Properties.Add(new PropertyEntity { Name = "PHI Hospital | Pregnancy | Cover Description", Title = "Cover description", Mandatory = true, InputTypeId = 3, TypeId = 8, ValuesListId = 5, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 6, Hint = "Nunc dignissim risus id metus", Enabled = true, DateTimeCreated = dateTime }); // 47
                context.Properties.Add(new PropertyEntity { Name = "PHI Hospital | Pregnancy | Waiting", Title = "Waiting", Mandatory = true, InputTypeId = 1, TypeId = 5, ValuesListId = 0, UnitsListId = 3, PossibleNullNumber = false, PossibleComments = false, ParentId = 6, Hint = "Integer vitae libero ac risus egestas placerat", Enabled = true, DateTimeCreated = dateTime }); // 48
                context.Properties.Add(new PropertyEntity { Name = "PHI Hospital | Reproductive | Cover Description", Title = "Cover description", Mandatory = true, InputTypeId = 3, TypeId = 8, ValuesListId = 5, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 7, Hint = "Nunc dignissim risus id metus", Enabled = true, DateTimeCreated = dateTime }); // 49
                context.Properties.Add(new PropertyEntity { Name = "PHI Hospital | Reproductive | Waiting", Title = "Waiting", Mandatory = true, InputTypeId = 1, TypeId = 5, ValuesListId = 0, UnitsListId = 3, PossibleNullNumber = false, PossibleComments = false, ParentId = 7, Hint = "Nunc dignissim risus id metus", Enabled = true, DateTimeCreated = dateTime }); // 50
                context.Properties.Add(new PropertyEntity { Name = "PHI Hospital | Joints | Cover Description", Title = "Cover description", Mandatory = true, InputTypeId = 3, TypeId = 8, ValuesListId = 7, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 8, Hint = "Integer vitae libero ac risus egestas placerat", Enabled = true, DateTimeCreated = dateTime }); // 51
                context.Properties.Add(new PropertyEntity { Name = "PHI Hospital | Joints | Waiting", Title = "Waiting", Mandatory = true, InputTypeId = 1, TypeId = 5, ValuesListId = 0, UnitsListId = 3, PossibleNullNumber = false, PossibleComments = false, ParentId = 8, Hint = "Cras ornare tristique elit", Enabled = true, DateTimeCreated = dateTime }); // 52
                context.Properties.Add(new PropertyEntity { Name = "PHI Hospital | Dialysis | Cover Description", Title = "Cover description", Mandatory = true, InputTypeId = 3, TypeId = 8, ValuesListId = 5, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 9, Hint = "Nunc dignissim risus id metus", Enabled = true, DateTimeCreated = dateTime }); // 53
                context.Properties.Add(new PropertyEntity { Name = "PHI Hospital | Dialysis | Waiting", Title = "Waiting", Mandatory = true, InputTypeId = 1, TypeId = 5, ValuesListId = 0, UnitsListId = 3, PossibleNullNumber = false, PossibleComments = false, ParentId = 9, Hint = "Nunc dignissim risus id metus", Enabled = true, DateTimeCreated = dateTime }); // 54
                context.Properties.Add(new PropertyEntity { Name = "PHI Hospital | Gastric | Cover Description", Title = "Cover description", Mandatory = true, InputTypeId = 3, TypeId = 8, ValuesListId = 5, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 10, Hint = "Nunc dignissim risus id metus", Enabled = true, DateTimeCreated = dateTime }); // 55
                context.Properties.Add(new PropertyEntity { Name = "PHI Hospital | Gastric | Waiting", Title = "Waiting", Mandatory = true, InputTypeId = 1, TypeId = 5, ValuesListId = 0, UnitsListId = 3, PossibleNullNumber = false, PossibleComments = false, ParentId = 10, Hint = "Cras ornare tristique elit", Enabled = true, DateTimeCreated = dateTime }); // 56
                context.Properties.Add(new PropertyEntity { Name = "PHI Hospital | Sterilisation | Cover Description", Title = "Cover description", Mandatory = true, InputTypeId = 3, TypeId = 8, ValuesListId = 5, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 11, Hint = "Nunc dignissim risus id metus", Enabled = true, DateTimeCreated = dateTime }); // 57
                context.Properties.Add(new PropertyEntity { Name = "PHI Hospital | Sterilisation | Waiting", Title = "Waiting", Mandatory = true, InputTypeId = 1, TypeId = 5, ValuesListId = 0, UnitsListId = 3, PossibleNullNumber = false, PossibleComments = false, ParentId = 11, Hint = "Integer vitae libero ac risus egestas placerat", Enabled = true, DateTimeCreated = dateTime }); // 58
                context.Properties.Add(new PropertyEntity { Name = "PHI Hospital | Plastic | Cover Description", Title = "Cover description", Mandatory = true, InputTypeId = 3, TypeId = 8, ValuesListId = 5, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 12, Hint = "Nunc dignissim risus id metus", Enabled = true, DateTimeCreated = dateTime }); // 59
                context.Properties.Add(new PropertyEntity { Name = "PHI Hospital | Plastic | Waiting", Title = "Waiting", Mandatory = true, InputTypeId = 1, TypeId = 5, ValuesListId = 0, UnitsListId = 3, PossibleNullNumber = false, PossibleComments = false, ParentId = 12, Hint = "Nunc dignissim risus id metus", Enabled = true, DateTimeCreated = dateTime }); // 60
                context.Properties.Add(new PropertyEntity { Name = "PHI Hospital | Rehabilitation | Cover Description", Title = "Cover description", Mandatory = true, InputTypeId = 3, TypeId = 8, ValuesListId = 5, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 13, Hint = "Nunc dignissim risus id metus", Enabled = true, DateTimeCreated = dateTime }); // 61
                context.Properties.Add(new PropertyEntity { Name = "PHI Hospital | Rehabilitation | Waiting", Title = "Waiting", Mandatory = true, InputTypeId = 1, TypeId = 5, ValuesListId = 0, UnitsListId = 3, PossibleNullNumber = false, PossibleComments = false, ParentId = 13, Hint = "Cras ornare tristique elit", Enabled = true, DateTimeCreated = dateTime }); // 62
                context.Properties.Add(new PropertyEntity { Name = "PHI Hospital | Psychiatric | Cover Description", Title = "Cover description", Mandatory = true, InputTypeId = 3, TypeId = 8, ValuesListId = 5, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 14, Hint = "Nunc dignissim risus id metus", Enabled = true, DateTimeCreated = dateTime }); // 63
                context.Properties.Add(new PropertyEntity { Name = "PHI Hospital | Psychiatric | Waiting", Title = "Waiting", Mandatory = true, InputTypeId = 1, TypeId = 5, ValuesListId = 0, UnitsListId = 3, PossibleNullNumber = false, PossibleComments = false, ParentId = 14, Hint = "Integer vitae libero ac risus egestas placerat", Enabled = true, DateTimeCreated = dateTime }); // 64
                context.Properties.Add(new PropertyEntity { Name = "PHI Hospital | Palliative | Cover Description", Title = "Cover description", Mandatory = true, InputTypeId = 3, TypeId = 8, ValuesListId = 5, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 15, Hint = "Nunc dignissim risus id metus", Enabled = true, DateTimeCreated = dateTime }); // 65
                context.Properties.Add(new PropertyEntity { Name = "PHI Hospital | Palliative | Waiting", Title = "Waiting", Mandatory = true, InputTypeId = 1, TypeId = 5, ValuesListId = 0, UnitsListId = 3, PossibleNullNumber = false, PossibleComments = false, ParentId = 15, Hint = "Nunc dignissim risus id metus", Enabled = true, DateTimeCreated = dateTime }); // 66
                context.Properties.Add(new PropertyEntity { Name = "PHI Hospital | Non-Medicare | Cover Description", Title = "Cover description", Mandatory = true, InputTypeId = 3, TypeId = 8, ValuesListId = 5, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 16, Hint = "Nunc dignissim risus id metus", Enabled = true, DateTimeCreated = dateTime }); // 67
                context.Properties.Add(new PropertyEntity { Name = "PHI Hospital | Non-Medicare | Waiting", Title = "Waiting", Mandatory = true, InputTypeId = 1, TypeId = 5, ValuesListId = 0, UnitsListId = 3, PossibleNullNumber = false, PossibleComments = false, ParentId = 16, Hint = "Cras ornare tristique elit", Enabled = true, DateTimeCreated = dateTime }); // 68
                context.Properties.Add(new PropertyEntity { Name = "PHI Hospital | Ambulance | Cover Description", Title = "Cover description", Mandatory = true, InputTypeId = 3, TypeId = 8, ValuesListId = 6, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 17, Hint = "Integer vitae libero ac risus egestas placerat", Enabled = true, DateTimeCreated = dateTime }); // 69
                context.Properties.Add(new PropertyEntity { Name = "PHI Hospital | Ambulance | Waiting", Title = "Waiting", Mandatory = true, InputTypeId = 1, TypeId = 5, ValuesListId = 0, UnitsListId = 3, PossibleNullNumber = false, PossibleComments = false, ParentId = 17, Hint = "Integer vitae libero ac risus egestas placerat", Enabled = true, DateTimeCreated = dateTime }); // 70
                
                context.Properties.Add(new PropertyEntity { Name = "PHI Hospital | Excess | Per Hospital Visit", Title = "Per hospital visit", Mandatory = true, InputTypeId = 1, TypeId = 2, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 18, Hint = "Nunc dignissim risus id metus", Enabled = true, DateTimeCreated = dateTime }); // 71
                context.Properties.Add(new PropertyEntity { Name = "PHI Hospital | Excess | Maximum Per Person", Title = "Maximum per person", Mandatory = true, InputTypeId = 1, TypeId = 2, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 18, Hint = "Cras ornare tristique elit", Enabled = true, DateTimeCreated = dateTime }); // 72
                context.Properties.Add(new PropertyEntity { Name = "PHI Hospital | Excess | Maximum Per Annum", Title = "Maximum per annum", Mandatory = true, InputTypeId = 1, TypeId = 2, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 18, Hint = "Integer vitae libero ac risus egestas placerat", Enabled = true, DateTimeCreated = dateTime }); // 73
                context.Properties.Add(new PropertyEntity { Name = "PHI Hospital | CoPayments | Shared Room", Title = "Shared room", Mandatory = true, InputTypeId = 1, TypeId = 2, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 19, Hint = "Nunc dignissim risus id metus", Enabled = true, DateTimeCreated = dateTime }); // 74
                context.Properties.Add(new PropertyEntity { Name = "PHI Hospital | CoPayments | Private Room", Title = "Private room", Mandatory = true, InputTypeId = 1, TypeId = 2, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 19, Hint = "Cras ornare tristique elit", Enabled = true, DateTimeCreated = dateTime }); // 75
                context.Properties.Add(new PropertyEntity { Name = "PHI Hospital | CoPayments | Day Surgery", Title = "Day surgery", Mandatory = true, InputTypeId = 1, TypeId = 2, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 19, Hint = "Integer vitae libero ac risus egestas placerat", Enabled = true, DateTimeCreated = dateTime }); // 76

                context.Properties.Add(new PropertyEntity { Name = "PHI General | Dental General Dental | Waiting", Title = "Waiting", Mandatory = true, InputTypeId = 1, TypeId = 5, ValuesListId = 0, UnitsListId = 3, PossibleNullNumber = true, PossibleComments = true, ParentId = 22, Hint = "Nunc dignissim risus id metus", Enabled = true, DateTimeCreated = dateTime }); // 77
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Dental General Dental | Benefit Limits", Title = "Benefit limits", Mandatory = true, InputTypeId = 1, TypeId = 200800, ValuesListId = 0, UnitsListId = 8, PossibleNullNumber = true, PossibleComments = true, ParentId = 22, Hint = "Cras ornare tristique elit", Enabled = true, DateTimeCreated = dateTime }); // 78
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Dental General Dental | Maximum Benefits Examples", Title = "Maximum benefits examples", Mandatory = true, InputTypeId = 6, TypeId = 1, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 22, Hint = "Integer vitae libero ac risus egestas placerat", Enabled = true, DateTimeCreated = dateTime }); // 79
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Dental Major Dental | Waiting", Title = "Waiting", Mandatory = true, InputTypeId = 1, TypeId = 5, ValuesListId = 0, UnitsListId = 3, PossibleNullNumber = true, PossibleComments = true, ParentId = 23, Hint = "Nunc dignissim risus id metus", Enabled = true, DateTimeCreated = dateTime }); // 80
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Dental Major Dental | Benefit Limits", Title = "Benefit limits", Mandatory = true, InputTypeId = 1, TypeId = 200800, ValuesListId = 0, UnitsListId = 8, PossibleNullNumber = true, PossibleComments = true, ParentId = 23, Hint = "Cras ornare tristique elit", Enabled = true, DateTimeCreated = dateTime }); // 81
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Dental Major Dental | Maximum Benefits Examples", Title = "Maximum benefits examples", Mandatory = true, InputTypeId = 6, TypeId = 1, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 23, Hint = "Integer vitae libero ac risus egestas placerat", Enabled = true, DateTimeCreated = dateTime }); // 82
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Dental Endodontic | Waiting", Title = "Waiting", Mandatory = true, InputTypeId = 1, TypeId = 5, ValuesListId = 0, UnitsListId = 3, PossibleNullNumber = true, PossibleComments = true, ParentId = 24, Hint = "Nunc dignissim risus id metus", Enabled = true, DateTimeCreated = dateTime }); // 83
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Dental Endodontic | Benefit Limits", Title = "Benefit limits", Mandatory = true, InputTypeId = 1, TypeId = 200800, ValuesListId = 0, UnitsListId = 8, PossibleNullNumber = true, PossibleComments = true, ParentId = 24, Hint = "Cras ornare tristique elit", Enabled = true, DateTimeCreated = dateTime }); // 84
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Dental Endodontic | Maximum Benefits Examples", Title = "Maximum benefits examples", Mandatory = true, InputTypeId = 6, TypeId = 1, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 24, Hint = "Integer vitae libero ac risus egestas placerat", Enabled = true, DateTimeCreated = dateTime }); // 85
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Dental Orthodontic | Waiting", Title = "Waiting", Mandatory = true, InputTypeId = 1, TypeId = 5, ValuesListId = 0, UnitsListId = 3, PossibleNullNumber = true, PossibleComments = true, ParentId = 25, Hint = "Nunc dignissim risus id metus", Enabled = true, DateTimeCreated = dateTime }); // 86
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Dental Orthodontic | Benefit Limits", Title = "Benefit limits", Mandatory = true, InputTypeId = 1, TypeId = 200800, ValuesListId = 0, UnitsListId = 8, PossibleNullNumber = true, PossibleComments = true, ParentId = 25, Hint = "Cras ornare tristique elit", Enabled = true, DateTimeCreated = dateTime }); // 87
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Dental Orthodontic | Benefit Lifetime Limits", Title = "Benefit lifetime limits", Mandatory = true, InputTypeId = 1, TypeId = 200800, ValuesListId = 0, UnitsListId = 8, PossibleNullNumber = true, PossibleComments = true, ParentId = 25, Hint = "Integer vitae libero ac risus egestas placerat", Enabled = true, DateTimeCreated = dateTime }); // 88
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Dental Orthodontic | Maximum Benefits Examples", Title = "Maximum benefits examples", Mandatory = true, InputTypeId = 6, TypeId = 1, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 25, Hint = "Nunc dignissim risus id metus", Enabled = true, DateTimeCreated = dateTime }); // 89
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Optical | Waiting", Title = "Waiting", Mandatory = true, InputTypeId = 1, TypeId = 5, ValuesListId = 0, UnitsListId = 3, PossibleNullNumber = false, PossibleComments = false, ParentId = 26, Hint = "Cras ornare tristique elit", Enabled = true, DateTimeCreated = dateTime }); // 90
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Optical | Benefit Limits", Title = "Benefit limits", Mandatory = true, InputTypeId = 1, TypeId = 200800, ValuesListId = 0, UnitsListId = 8, PossibleNullNumber = true, PossibleComments = true, ParentId = 26, Hint = "Integer vitae libero ac risus egestas placerat", Enabled = true, DateTimeCreated = dateTime }); // 91
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Optical | Maximum Benefits Examples", Title = "Maximum benefits examples", Mandatory = true, InputTypeId = 6, TypeId = 1, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 26, Hint = "Nunc dignissim risus id metus", Enabled = true, DateTimeCreated = dateTime }); // 92
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Non Prescribed Pharmaceuticals | Waiting", Title = "Waiting", Mandatory = true, InputTypeId = 1, TypeId = 5, ValuesListId = 0, UnitsListId = 3, PossibleNullNumber = false, PossibleComments = false, ParentId = 27, Hint = "Cras ornare tristique elit", Enabled = true, DateTimeCreated = dateTime }); // 93
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Non Prescribed Pharmaceuticals | Benefit Limits", Title = "Benefit limits", Mandatory = true, InputTypeId = 1, TypeId = 200800, ValuesListId = 0, UnitsListId = 8, PossibleNullNumber = true, PossibleComments = true, ParentId = 27, Hint = "Integer vitae libero ac risus egestas placerat", Enabled = true, DateTimeCreated = dateTime }); // 94
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Non Prescribed Pharmaceuticals | Maximum Benefits Examples", Title = "Maximum benefits examples", Mandatory = true, InputTypeId = 6, TypeId = 1, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 27, Hint = "Nunc dignissim risus id metus", Enabled = true, DateTimeCreated = dateTime }); // 95
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Physiotherapy | Waiting", Title = "Waiting", Mandatory = true, InputTypeId = 1, TypeId = 5, ValuesListId = 0, UnitsListId = 3, PossibleNullNumber = false, PossibleComments = false, ParentId = 28, Hint = "Cras ornare tristique elit", Enabled = true, DateTimeCreated = dateTime }); // 96
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Physiotherapy | Benefit Limits", Title = "Benefit limits", Mandatory = true, InputTypeId = 1, TypeId = 200800, ValuesListId = 0, UnitsListId = 8, PossibleNullNumber = true, PossibleComments = true, ParentId = 28, Hint = "Integer vitae libero ac risus egestas placerat", Enabled = true, DateTimeCreated = dateTime }); // 97
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Physiotherapy | Maximum Benefits Examples", Title = "Maximum benefits examples", Mandatory = true, InputTypeId = 6, TypeId = 1, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 28, Hint = "Nunc dignissim risus id metus", Enabled = true, DateTimeCreated = dateTime }); // 98
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Chiropractic | Waiting", Title = "Waiting", Mandatory = true, InputTypeId = 1, TypeId = 5, ValuesListId = 0, UnitsListId = 3, PossibleNullNumber = false, PossibleComments = false, ParentId = 29, Hint = "Cras ornare tristique elit", Enabled = true, DateTimeCreated = dateTime }); // 99
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Chiropractic | Benefit Limits", Title = "Benefit limits", Mandatory = true, InputTypeId = 1, TypeId = 200800, ValuesListId = 0, UnitsListId = 8, PossibleNullNumber = true, PossibleComments = true, ParentId = 29, Hint = "Integer vitae libero ac risus egestas placerat", Enabled = true, DateTimeCreated = dateTime }); // 100
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Chiropractic | Maximum Benefits Examples", Title = "Maximum benefits examples", Mandatory = true, InputTypeId = 6, TypeId = 1, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 29, Hint = "Nunc dignissim risus id metus", Enabled = true, DateTimeCreated = dateTime }); // 101
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Podiatry | Waiting", Title = "Waiting", Mandatory = true, InputTypeId = 1, TypeId = 5, ValuesListId = 0, UnitsListId = 3, PossibleNullNumber = false, PossibleComments = false, ParentId = 30, Hint = "Cras ornare tristique elit", Enabled = true, DateTimeCreated = dateTime }); // 102
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Podiatry | Benefit Limits", Title = "Benefit limits", Mandatory = true, InputTypeId = 1, TypeId = 200800, ValuesListId = 0, UnitsListId = 8, PossibleNullNumber = true, PossibleComments = true, ParentId = 30, Hint = "Integer vitae libero ac risus egestas placerat", Enabled = true, DateTimeCreated = dateTime }); // 103
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Podiatry | Maximum Benefits Examples", Title = "Maximum benefits examples", Mandatory = true, InputTypeId = 6, TypeId = 1, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 30, Hint = "Nunc dignissim risus id metus", Enabled = true, DateTimeCreated = dateTime }); // 104
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Psychology | Waiting", Title = "Waiting", Mandatory = true, InputTypeId = 1, TypeId = 5, ValuesListId = 0, UnitsListId = 3, PossibleNullNumber = false, PossibleComments = false, ParentId = 31, Hint = "Cras ornare tristique elit", Enabled = true, DateTimeCreated = dateTime }); // 105
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Psychology | Benefit Limits", Title = "Benefit limits", Mandatory = true, InputTypeId = 1, TypeId = 200800, ValuesListId = 0, UnitsListId = 8, PossibleNullNumber = true, PossibleComments = true, ParentId = 31, Hint = "Integer vitae libero ac risus egestas placerat", Enabled = true, DateTimeCreated = dateTime }); // 106
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Psychology | Maximum Benefits Examples", Title = "Maximum benefits examples", Mandatory = true, InputTypeId = 6, TypeId = 1, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 31, Hint = "Nunc dignissim risus id metus", Enabled = true, DateTimeCreated = dateTime }); // 107
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Acupuncture | Waiting", Title = "Waiting", Mandatory = true, InputTypeId = 1, TypeId = 5, ValuesListId = 0, UnitsListId = 3, PossibleNullNumber = false, PossibleComments = false, ParentId = 32, Hint = "Cras ornare tristique elit", Enabled = true, DateTimeCreated = dateTime }); // 108
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Acupuncture | Benefit Limits", Title = "Benefit limits", Mandatory = true, InputTypeId = 1, TypeId = 200800, ValuesListId = 0, UnitsListId = 8, PossibleNullNumber = true, PossibleComments = true, ParentId = 32, Hint = "Integer vitae libero ac risus egestas placerat", Enabled = true, DateTimeCreated = dateTime }); // 109
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Acupuncture | Maximum Benefits Examples", Title = "Maximum benefits examples", Mandatory = true, InputTypeId = 6, TypeId = 1, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 32, Hint = "Nunc dignissim risus id metus", Enabled = true, DateTimeCreated = dateTime }); // 110
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Naturopathy | Waiting", Title = "Waiting", Mandatory = true, InputTypeId = 1, TypeId = 5, ValuesListId = 0, UnitsListId = 3, PossibleNullNumber = false, PossibleComments = false, ParentId = 33, Hint = "Cras ornare tristique elit", Enabled = true, DateTimeCreated = dateTime }); // 111
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Naturopathy | Benefit Limits", Title = "Benefit limits", Mandatory = true, InputTypeId = 1, TypeId = 200800, ValuesListId = 0, UnitsListId = 8, PossibleNullNumber = true, PossibleComments = true, ParentId = 33, Hint = "Integer vitae libero ac risus egestas placerat", Enabled = true, DateTimeCreated = dateTime }); // 112
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Naturopathy | Maximum Benefits Examples", Title = "Maximum benefits examples", Mandatory = true, InputTypeId = 6, TypeId = 1, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 33, Hint = "Nunc dignissim risus id metus", Enabled = true, DateTimeCreated = dateTime }); // 113
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Remedial Massage | Waiting", Title = "Waiting", Mandatory = true, InputTypeId = 1, TypeId = 5, ValuesListId = 0, UnitsListId = 3, PossibleNullNumber = false, PossibleComments = false, ParentId = 34, Hint = "Cras ornare tristique elit", Enabled = true, DateTimeCreated = dateTime }); // 114
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Remedial Massage | Benefit Limits", Title = "Benefit limits", Mandatory = true, InputTypeId = 1, TypeId = 200800, ValuesListId = 0, UnitsListId = 8, PossibleNullNumber = true, PossibleComments = true, ParentId = 34, Hint = "Integer vitae libero ac risus egestas placerat", Enabled = true, DateTimeCreated = dateTime }); // 115
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Remedial Massage | Maximum Benefits Examples", Title = "Maximum benefits examples", Mandatory = true, InputTypeId = 6, TypeId = 1, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 34, Hint = "Nunc dignissim risus id metus", Enabled = true, DateTimeCreated = dateTime }); // 116
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Hearing Aids | Waiting", Title = "Waiting", Mandatory = true, InputTypeId = 1, TypeId = 5, ValuesListId = 0, UnitsListId = 3, PossibleNullNumber = false, PossibleComments = false, ParentId = 35, Hint = "Cras ornare tristique elit", Enabled = true, DateTimeCreated = dateTime }); // 117
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Hearing Aids | Benefit Limits", Title = "Benefit limits", Mandatory = true, InputTypeId = 1, TypeId = 200800, ValuesListId = 0, UnitsListId = 8, PossibleNullNumber = true, PossibleComments = true, ParentId = 35, Hint = "Integer vitae libero ac risus egestas placerat", Enabled = true, DateTimeCreated = dateTime }); // 118
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Hearing Aids | Maximum Benefits Examples", Title = "Maximum benefits examples", Mandatory = true, InputTypeId = 6, TypeId = 1, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 35, Hint = "Nunc dignissim risus id metus", Enabled = true, DateTimeCreated = dateTime }); // 119
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Blood Glucose Monitors | Waiting", Title = "Waiting", Mandatory = true, InputTypeId = 1, TypeId = 5, ValuesListId = 0, UnitsListId = 3, PossibleNullNumber = false, PossibleComments = false, ParentId = 36, Hint = "Cras ornare tristique elit", Enabled = true, DateTimeCreated = dateTime }); // 120
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Blood Glucose Monitors | Benefit Limits", Title = "Benefit limits", Mandatory = true, InputTypeId = 1, TypeId = 200800, ValuesListId = 0, UnitsListId = 8, PossibleNullNumber = true, PossibleComments = true, ParentId = 36, Hint = "Integer vitae libero ac risus egestas placerat", Enabled = true, DateTimeCreated = dateTime }); // 121
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Blood Glucose Monitors | Maximum Benefits Examples", Title = "Maximum benefits examples", Mandatory = true, InputTypeId = 6, TypeId = 1, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 36, Hint = "Nunc dignissim risus id metus", Enabled = true, DateTimeCreated = dateTime }); // 122
                context.Properties.Add(new PropertyEntity { Name = "PHI General | GapSaver | Gap Benefits", Title = "Gap benefits", Mandatory = true, InputTypeId = 1, TypeId = 2, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 37, Hint = "Cras ornare tristique elit", Enabled = true, DateTimeCreated = dateTime }); // 123
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Ambulance | Waiting", Title = "Waiting", Mandatory = true, InputTypeId = 1, TypeId = 500800, ValuesListId = 0, UnitsListId = 3, PossibleNullNumber = false, PossibleComments = false, ParentId = 38, Hint = "Integer vitae libero ac risus egestas placerat", Enabled = true, DateTimeCreated = dateTime }); // 124
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Ambulance | Cover Description", Title = "Cover description", Mandatory = true, InputTypeId = 3, TypeId = 8, ValuesListId = 9, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 38, Hint = "Integer vitae libero ac risus egestas placerat", Enabled = true, DateTimeCreated = dateTime }); // 125
                context.SaveChanges();

                context.Properties.Add(new PropertyEntity { Name = "PHI General | Dental General Dental | Maximum Benefits Examples | Periodic Oral Examination", Title = "Periodic oral examination", Mandatory = true, InputTypeId = 1, TypeId = 200700, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 79, Hint = "Cras ornare tristique elit", Enabled = true, DateTimeCreated = dateTime });
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Dental General Dental | Maximum Benefits Examples | Scale & Clean", Title = "Scale & clean", Mandatory = true, InputTypeId = 1, TypeId = 200700, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 79, Hint = "Integer vitae libero ac risus egestas placerat", Enabled = true, DateTimeCreated = dateTime });
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Dental General Dental | Maximum Benefits Examples | Fluoride Treatment", Title = "Fluoride treatment", Mandatory = true, InputTypeId = 1, TypeId = 200700, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 79, Hint = "Nunc dignissim risus id metus", Enabled = true, DateTimeCreated = dateTime });
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Dental General Dental | Maximum Benefits Examples | Surgical Tooth Extraction", Title = "Surgical tooth extraction", Mandatory = true, InputTypeId = 1, TypeId = 200700, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 79, Hint = "Cras ornare tristique elit", Enabled = true, DateTimeCreated = dateTime });
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Dental Major Dental | Maximum Benefits Examples | Full Crown Veneered", Title = "Full crown veneered", Mandatory = true, InputTypeId = 1, TypeId = 200700, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 82, Hint = "Integer vitae libero ac risus egestas placerat", Enabled = true, DateTimeCreated = dateTime });
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Dental Major Dental | Maximum Benefits Examples | Surgical Tooth Extraction", Title = "Surgical tooth extraction", Mandatory = true, InputTypeId = 1, TypeId = 200700, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 82, Hint = "Nunc dignissim risus id metus", Enabled = true, DateTimeCreated = dateTime });
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Dental Endodontic | Maximum Benefits Examples | One Root Canal Filling", Title = "Filling of one root canal", Mandatory = true, InputTypeId = 1, TypeId = 200700, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 85, Hint = "Cras ornare tristique elit", Enabled = true, DateTimeCreated = dateTime });
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Dental Orthodontic | Maximum Benefits Examples | Upper - Lower Teeth Braces Removal & Fitting Retainer", Title = "Braces for upper & lower teeth, including removal plus fitting of retainer", Mandatory = true, InputTypeId = 1, TypeId = 200700, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 89, Hint = "Integer vitae libero ac risus egestas placerat", Enabled = true, DateTimeCreated = dateTime });
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Optical | Maximum Benefits Examples | Single Vision Lenses & Frames", Title = "Single vision lenses & frames", Mandatory = true, InputTypeId = 1, TypeId = 200700, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 92, Hint = "Nunc dignissim risus id metus", Enabled = true, DateTimeCreated = dateTime });
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Optical | Maximum Benefits Examples | Multi-focal Lenses & Frames", Title = "Multi-focal lenses & frames", Mandatory = true, InputTypeId = 1, TypeId = 200700, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 92, Hint = "Cras ornare tristique elit", Enabled = true, DateTimeCreated = dateTime });
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Non Prescribed Pharmaceuticals | Maximum Benefits Examples | Per Eligible Prescription", Title = "Per eligible prescription", Mandatory = true, InputTypeId = 1, TypeId = 200700, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 95, Hint = "Integer vitae libero ac risus egestas placerat", Enabled = true, DateTimeCreated = dateTime });
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Physiotherapy | Maximum Benefits Examples | Initial Visit", Title = "Initial visit", Mandatory = true, InputTypeId = 1, TypeId = 200700, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 98, Hint = "Nunc dignissim risus id metus", Enabled = true, DateTimeCreated = dateTime });
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Physiotherapy | Maximum Benefits Examples | Subsequent Visit", Title = "Subsequent visit", Mandatory = true, InputTypeId = 1, TypeId = 200700, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 98, Hint = "Cras ornare tristique elit", Enabled = true, DateTimeCreated = dateTime });
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Chiropractic | Maximum Benefits Examples | Initial Visit", Title = "Initial visit", Mandatory = true, InputTypeId = 1, TypeId = 200700, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 101, Hint = "Integer vitae libero ac risus egestas placerat", Enabled = true, DateTimeCreated = dateTime });
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Chiropractic | Maximum Benefits Examples | Subsequent Visit", Title = "Subsequent visit", Mandatory = true, InputTypeId = 1, TypeId = 200700, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 101, Hint = "Nunc dignissim risus id metus", Enabled = true, DateTimeCreated = dateTime });
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Podiatry | Maximum Benefits Examples | Initial Visit", Title = "Initial visit", Mandatory = true, InputTypeId = 1, TypeId = 200700, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 104, Hint = "Cras ornare tristique elit", Enabled = true, DateTimeCreated = dateTime });
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Podiatry | Maximum Benefits Examples | Subsequent Visit", Title = "Subsequent visit", Mandatory = true, InputTypeId = 1, TypeId = 200700, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 104, Hint = "Integer vitae libero ac risus egestas placerat", Enabled = true, DateTimeCreated = dateTime });
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Psychology | Maximum Benefits Examples | Initial Visit", Title = "Initial visit", Mandatory = true, InputTypeId = 1, TypeId = 200700, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 107, Hint = "Nunc dignissim risus id metus", Enabled = true, DateTimeCreated = dateTime });
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Psychology | Maximum Benefits Examples | Subsequent Visit", Title = "Subsequent visit", Mandatory = true, InputTypeId = 1, TypeId = 200700, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 107, Hint = "Cras ornare tristique elit", Enabled = true, DateTimeCreated = dateTime });
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Acupuncture | Maximum Benefits Examples | Initial Visit", Title = "Initial visit", Mandatory = true, InputTypeId = 1, TypeId = 200700, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 110, Hint = "Integer vitae libero ac risus egestas placerat", Enabled = true, DateTimeCreated = dateTime });
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Acupuncture | Maximum Benefits Examples | Subsequent Visit", Title = "Subsequent visit", Mandatory = true, InputTypeId = 1, TypeId = 200700, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 110, Hint = "Nunc dignissim risus id metus", Enabled = true, DateTimeCreated = dateTime });
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Naturopathy | Maximum Benefits Examples | Initial Visit", Title = "Initial visit", Mandatory = true, InputTypeId = 1, TypeId = 200700, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 113, Hint = "Cras ornare tristique elit", Enabled = true, DateTimeCreated = dateTime });
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Naturopathy | Maximum Benefits Examples | Subsequent Visit", Title = "Subsequent visit", Mandatory = true, InputTypeId = 1, TypeId = 200700, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 113, Hint = "Integer vitae libero ac risus egestas placerat", Enabled = true, DateTimeCreated = dateTime });
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Remedial Massage | Maximum Benefits Examples | Initial Visit", Title = "Initial visit", Mandatory = true, InputTypeId = 1, TypeId = 200700, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 116, Hint = "Nunc dignissim risus id metus", Enabled = true, DateTimeCreated = dateTime });
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Remedial Massage | Maximum Benefits Examples | Subsequent Visit", Title = "Subsequent visit", Mandatory = true, InputTypeId = 1, TypeId = 200700, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 116, Hint = "Cras ornare tristique elit", Enabled = true, DateTimeCreated = dateTime });
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Hearing Aids | Maximum Benefits Examples | Per Hearing Aid", Title = "Per hearing aid", Mandatory = true, InputTypeId = 1, TypeId = 200700, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 119, Hint = "Integer vitae libero ac risus egestas placerat", Enabled = true, DateTimeCreated = dateTime });
                context.Properties.Add(new PropertyEntity { Name = "PHI General | Blood Glucose Monitors | Maximum Benefits Examples | Per Monitor", Title = "Per monitor", Mandatory = true, InputTypeId = 1, TypeId = 200700, ValuesListId = 0, UnitsListId = 0, PossibleNullNumber = false, PossibleComments = false, ParentId = 122, Hint = "Nunc dignissim risus id metus", Enabled = true, DateTimeCreated = dateTime });
                context.SaveChanges();

                var clients = new List<ClientEntity>
                {
                    new ClientEntity { Name = "ACA Health Benefits Fund", Code = "ACA", Url = "http://acahealth.com.au", Enabled = true, DateTimeCreated = dateTime },
                    new ClientEntity { Name = "ahm Health Insurance", Code = "AHM", Url = "https://ahm.com.au", Enabled = true, DateTimeCreated = dateTime },
                    new ClientEntity { Name = "Australian Unity Health Limited", Code = "AUF", Url = "http://www.australianunity.com.au", Enabled = true, DateTimeCreated = dateTime },
                    new ClientEntity { Name = "Bupa HI Pty Ltd", Code = "BUP", Url = "http://www.bupa.com.au", Enabled = true, DateTimeCreated = dateTime },
                    new ClientEntity { Name = "CBHS Corporate Health Pty Ltd", Url = "https://www.cbhscorporatehealth.com.au", Enabled = true, Code = "CBC", DateTimeCreated = dateTime },
                    new ClientEntity { Name = "CBHS Health Fund Limited", Code = "CBH", Url = "https://www.cbhs.com.au", Enabled = true, DateTimeCreated = dateTime },
                    new ClientEntity { Name = "CDH Benefits Fund", Code = "CDH", Url = "http://www.cdhbf.com.au", Enabled = true, DateTimeCreated = dateTime },
                    new ClientEntity { Name = "CUA Health Limited", Code = "CPS", Url = "https://www.cua.com.au", Enabled = true, DateTimeCreated = dateTime },
                    new ClientEntity { Name = "Defence Health Limited", Code = "AHB", Url = "http://www.defencehealth.com.au", Enabled = true, DateTimeCreated = dateTime },
                    new ClientEntity { Name = "Doctors' Health Fund", Code = "AMA", Url = "https://www.doctorshealthfund.com.au", Enabled = true, DateTimeCreated = dateTime },
                    new ClientEntity { Name = "Emergency Services Health", Code = "ESH", Url = "https://eshealth.com.au", Enabled = true, DateTimeCreated = dateTime },
                    new ClientEntity { Name = "GMHBA Limited", Code = "GMH", Url = "https://www.gmhba.com.au", DateTimeCreated = dateTime },
                    new ClientEntity { Name = "Grand United Corporate Health", Code = "FAI", Url = "https://www.guhealth.com.au", Enabled = true, DateTimeCreated = dateTime },
                    new ClientEntity { Name = "HBF Health Limited", Code = "HBF", Url = "https://www.hbf.com.au/", Enabled = true, DateTimeCreated = dateTime },
                    new ClientEntity { Name = "HCF", Code = "HCF", Url = "https://www.hcf.com.au", Enabled = true, DateTimeCreated = dateTime },
                    new ClientEntity { Name = "Health Care Insurance Limited", Code = "HCI", Url = "http://www.hciltd.com.au", Enabled = true, DateTimeCreated = dateTime },
                    new ClientEntity { Name = "Health Insurance Fund of Australia Limited", Code = "HIF", Url = "https://www.hif.com.au", Enabled = true, DateTimeCreated = dateTime },
                    new ClientEntity { Name = "Health Partners", Code = "SPS", Url = "https://www.healthpartners.com.au", Enabled = true, DateTimeCreated = dateTime },
                    new ClientEntity { Name = "health.com.au", Code = "HEA", Url = "https://health.com.au", Enabled = true, DateTimeCreated = dateTime },
                    new ClientEntity { Name = "Latrobe Health Services", Code = "LHS", Url = "http://lchs.com.au", Enabled = true, DateTimeCreated = dateTime },
                    new ClientEntity { Name = "Medibank Private Limited", Code = "MBP", Url = "https://www.medibank.com.au", Enabled = true, DateTimeCreated = dateTime },
                    new ClientEntity { Name = "Mildura Health Fund", Code = "MDH", Url = "http://www.mildurahealthfund.com.au", Enabled = true, DateTimeCreated = dateTime },
                    new ClientEntity { Name = "National Health Benefits Australia Pty Ltd (onemedifund)", Code = "OMF", Url = "https://www.onemedifund.com.au", Enabled = true, DateTimeCreated = dateTime },
                    new ClientEntity { Name = "Navy Health Ltd", Code = "NHB", Url = "http://navyhealth.com.au", Enabled = true, DateTimeCreated = dateTime },
                    new ClientEntity { Name = "NIB Health Funds Ltd.", Code = "NIB", Url = "https://www.nib.com.au", Enabled = true, DateTimeCreated = dateTime },
                    new ClientEntity { Name = "Nurses & Midwives Health", Code = "NMW", Url = "https://www.nmhealth.com.au", Enabled = true, DateTimeCreated = dateTime },
                    new ClientEntity { Name = "Peoplecare Health Insurance", Code = "LHM", Url = "https://www.peoplecare.com.au", Enabled = true, DateTimeCreated = dateTime },
                    new ClientEntity { Name = "Phoenix Health Fund Limited", Code = "PWA", Url = "http://www.phoenixhealthfund.com.au", Enabled = true, DateTimeCreated = dateTime },
                    new ClientEntity { Name = "Police Health", Code = "SPE", Url = "http://www.policehealth.com.au", Enabled = true, DateTimeCreated = dateTime },
                    new ClientEntity { Name = "Queensland Country Health Fund Ltd", Code = "QCH", Url = "https://www.qldcountryhealth.com.au", Enabled = true, DateTimeCreated = dateTime },
                    new ClientEntity { Name = "Railway and Transport Health Fund Limited", Code = "RTE", Url = "https://www.rthealthfund.com.au", Enabled = true, DateTimeCreated = dateTime },
                    new ClientEntity { Name = "Reserve Bank Health Society Ltd", Code = "RBH", Url = "https://www.myrbhs.com.au", Enabled = true, DateTimeCreated = dateTime },
                    new ClientEntity { Name = "St.Lukes Health", Code = "SLM", Url = "http://www.stlukes.com.au", Enabled = true, DateTimeCreated = dateTime },
                    new ClientEntity { Name = "Teachers Health Fund", Code = "NTF", Url = "https://www.teachershealth.com.au", Enabled = true, DateTimeCreated = dateTime },
                    new ClientEntity { Name = "Transport Health Pty Ltd", Code = "TFS", Url = "https://transporthealth.com.au", Enabled = true, DateTimeCreated = dateTime },
                    new ClientEntity { Name = "Teachers Union Health", Code = "QTU", Url = "http://tuh.com.au", Enabled = true, DateTimeCreated = dateTime },
                    new ClientEntity { Name = "Westfund Limited", Code = "WFD", Url = "http://www.westfund.com.au", Enabled = true, DateTimeCreated = dateTime },
                };

                var zones = new List<ZoneEntity>
                {
                    new ZoneEntity { Name = "PHI Product Header Zone", Title = "General section", Enabled = true, DateTimeCreated = dateTime, Groups = new List<GroupEntity> { group1 } },
                    new ZoneEntity { Name = "PHI Hospital Component Zone", Title = "Hospital section", Enabled = true, DateTimeCreated = dateTime, Groups = new List<GroupEntity> { group2, group3, group4 } },
                    new ZoneEntity { Name = "PHI General Component Zone", Title = "Extras section", Enabled = true, DateTimeCreated = dateTime, Groups = new List<GroupEntity> { group5, group6 } }
                };

                var users =  new List<UserEntity>
                {
                    new UserEntity { FirstName = "IMA", LastName = "CP", BirthDate = DateTime.Parse("1978-12-09 00:00:00"), FamilyTypeId = 2, StateId = 3, PostCode = "BERWICK 3806", Mobile = "0455077026", Email = "chirag@insureme.com.au", Password = "Cm80Gm78".HashMD5(), Key = "chirag@insureme.com.au".HashMD5(), Code = string.Empty.GetUniqueString(), RoleId = 3, Enabled = true, Verified = true, IncomeRangeId = 3, DateTimeCreated = dateTime, Clients = clients },
                    new UserEntity { FirstName = "IMA", LastName = "GP", BirthDate = DateTime.Parse("1980-10-21 00:00:00"), FamilyTypeId = 3, StateId = 2, PostCode = "KELLYVILLE 2155", Mobile = "0414854093", Email = "gunjan@insureme.com.au", Password = "Cm80Gm78".HashMD5(), Key = "gunjan@insureme.com.au".HashMD5(), Code = string.Empty.GetUniqueString(), RoleId = 3, Enabled = true, Verified = true, IncomeRangeId = 3, DateTimeCreated = dateTime, Clients = clients },
                    new UserEntity { FirstName = "Gunjan", LastName = "Patel", BirthDate = DateTime.Parse("1980-10-21 00:00:00"), FamilyTypeId = 3, StateId = 2, PostCode = "KELLYVILLE 2155", Mobile = "0414854093", Email = "gunjan.patel@cbhs.com.au", Password = "Cm80Gm78".HashMD5(), Key = "gunjan.patel@cbhs.com.au".HashMD5(), Code = string.Empty.GetUniqueString(), RoleId = 2, Enabled = true, Verified = true, IncomeRangeId = 3, DateTimeCreated = dateTime, Clients = new List<ClientEntity> {clients[4], clients[5]} },
                    new UserEntity { FirstName = "Chirag", LastName = "Patel", BirthDate = DateTime.Parse("1978-12-09 00:00:00"), FamilyTypeId = 2, StateId = 3, PostCode = "BERWICK 3806", Mobile = "0455077026", Email = "kanchi7880@gmail.com", Password = "Cm80Gm78".HashMD5(), Key = "kanchi7880@gmail.com".HashMD5(), Code = string.Empty.GetUniqueString(), RoleId = 1, Enabled = true, Verified = true, IncomeRangeId = 3, DateTimeCreated = dateTime },
                    new UserEntity { FirstName = "Gunjan", LastName = "Patel", BirthDate = DateTime.Parse("1980-10-21 00:00:00"), FamilyTypeId = 3, StateId = 2, PostCode = "KELLYVILLE 2155", Mobile = "0414854093", Email = "gmpat4u@gmail.com", Password = "Cm80Gm78".HashMD5(), Key = "gmpat4u@gmail.com".HashMD5(), Code = string.Empty.GetUniqueString(), RoleId = 1, Enabled = true, Verified = true, IncomeRangeId = 3, DateTimeCreated = dateTime }
                };

                var productTypes = new List<ProductTypeEntity>
                {
                    new ProductTypeEntity { Name = "PHI Hospital Product", Title = "Hospital", Enabled = true, DateTimeCreated = dateTime, Zones = new List<ZoneEntity> { zones[0], zones[1] }, Clients = clients },
                    new ProductTypeEntity { Name = "PHI General Product", Title = "Extras", Enabled = true, DateTimeCreated = dateTime, Zones = new List<ZoneEntity> { zones[0], zones[2] }, Clients = clients },
                    new ProductTypeEntity { Name = "PHI Combined Product", Title = "Combined", Enabled = true, DateTimeCreated = dateTime, Zones = zones, Clients = clients }
                };

                foreach (var productType in productTypes)
                {
                    context.ProductTypes.Add(productType);
                }

                foreach (var user in users)
                {
                    context.Users.Add(user);
                }

                context.SaveChanges();
            }

                        
            if (!context.LinkTypes.Any())
            {
                context.LinkTypes.Add(new LinkTypeEntity { Name = "Web", Enabled = true, DateTimeCreated = dateTime });
                context.LinkTypes.Add(new LinkTypeEntity { Name = "Brochure", Enabled = true, DateTimeCreated = dateTime });

                context.SaveChanges();
            }

            if (!context.Products.Any())
            {
                context.Products.Add(new ProductEntity { Title = "FlexiSaver", Name = "FlexiSaver", Variation = "500 EX | 0 CP", ClientId = 6, TypeId = 3, FamilyTypeId = 4, Price = 214.85f, PriceDescription = "Monthly premium (without any rebates)", IsGstApplicableOnPrice = false, Shipping = 0.00f, ShippingDescription = null, IsGstApplicableOnShipping = false, States = new List<StateEntity> { stateAct, stateNsw }, AvailableToPurchaseInStates = string.Join(", ", new List<StateEntity> { stateAct, stateNsw }.Select(x => x.Code)), Enabled = true, DateTimeCreated = dateTime });
                context.SaveChanges();

                context.Links.Add(new LinkEntity { TypeId = 1, ProductId = 1, Url = "https://www.cbhs.com.au/members/member-information/product-sheets", Hint = "Product listing web page", Enabled = true, DateTimeCreated = dateTime });
                context.Links.Add(new LinkEntity { TypeId = 2, ProductId = 1, Url = "https://www.cbhs.com.au/docs/default-source/product-sheets/CBHS-FlexiSaver.pdf", Hint = "Product document", Enabled = true, DateTimeCreated = dateTime });
                context.Links.Add(new LinkEntity { TypeId = 2, ProductId = 1, Url = "http://privatehealth.gov.au/dynamic/download.ashx?id=CBH/J12/NAJS20", Hint = "Product document (SIS)", Enabled = true, DateTimeCreated = dateTime });
                context.SaveChanges();

                context.Products.Add(new ProductEntity { Title = "KickStart", Name = "KickStart", Variation = "0 EX | 70 CP", ClientId = 6, TypeId = 3, FamilyTypeId = 4, Price = 241.19f, PriceDescription = "Monthly premium (without any rebates)", IsGstApplicableOnPrice = false, Shipping = 0.00f, ShippingDescription = null, IsGstApplicableOnShipping = false, States = new List<StateEntity> { stateAct, stateNsw }, AvailableToPurchaseInStates = string.Join(", ", new List<StateEntity> { stateAct, stateNsw }.Select(x => x.Code)), Enabled = true, DateTimeCreated = dateTime });
                context.SaveChanges();

                context.Links.Add(new LinkEntity { TypeId = 1, ProductId = 2, Url = "https://www.cbhs.com.au/members/member-information/product-sheets", Hint = "Product listing web page", Enabled = true, DateTimeCreated = dateTime });
                context.Links.Add(new LinkEntity { TypeId = 2, ProductId = 2, Url = "https://www.cbhs.com.au/docs/default-source/product-sheets/cbhs-kickstart.pdf", Hint = "Product document", Enabled = true, DateTimeCreated = dateTime });
                context.Links.Add(new LinkEntity { TypeId = 2, ProductId = 2, Url = "http://privatehealth.gov.au/dynamic/download.ashx?id=CBH/J1/NADW20", Hint = "Product document (SIS)", Enabled = true, DateTimeCreated = dateTime });
                context.SaveChanges();

                context.Products.Add(new ProductEntity { Title = "StepUp", Name = "StepUp", Variation = "0 EX | 70 CP", ClientId = 6, TypeId = 3, FamilyTypeId = 4, Price = 438.97f, PriceDescription = "Monthly premium (without any rebates)", IsGstApplicableOnPrice = false, Shipping = 0.00f, ShippingDescription = null, IsGstApplicableOnShipping = false, States = new List<StateEntity> { stateAct, stateNsw }, AvailableToPurchaseInStates = string.Join(", ", new List<StateEntity> { stateAct, stateNsw }.Select(x => x.Code)), Enabled = true, DateTimeCreated = dateTime });
                context.SaveChanges();

                context.Links.Add(new LinkEntity { TypeId = 1, ProductId = 3, Url = "https://www.cbhs.com.au/members/member-information/product-sheets", Hint = "Product listing web page", Enabled = true, DateTimeCreated = dateTime });
                context.Links.Add(new LinkEntity { TypeId = 2, ProductId = 3, Url = "https://www.cbhs.com.au/docs/default-source/product-sheets/cbhs-stepup.pdf", Hint = "Product document", Enabled = true, DateTimeCreated = dateTime });
                context.Links.Add(new LinkEntity { TypeId = 2, ProductId = 3, Url = "http://privatehealth.gov.au/dynamic/download.ashx?id=CBH/J6/NABR20", Hint = "Product document (SIS)", Enabled = true, DateTimeCreated = dateTime });
                context.SaveChanges();

                context.Products.Add(new ProductEntity { Title = "Prestige", Name = "Prestige", Variation = "0 EX | 0 CP", ClientId = 6, TypeId = 3, FamilyTypeId = 4, Price = 657.02f, PriceDescription = "Monthly premium (without any rebates)", IsGstApplicableOnPrice = false, Shipping = 0.00f, ShippingDescription = null, IsGstApplicableOnShipping = false, States = new List<StateEntity> { stateAct, stateNsw }, AvailableToPurchaseInStates = string.Join(", ", new List<StateEntity> { stateAct, stateNsw }.Select(x => x.Code)), Enabled = true, DateTimeCreated = dateTime });
                context.SaveChanges();

                context.Links.Add(new LinkEntity { TypeId = 1, ProductId = 4, Url = "https://www.cbhs.com.au/members/member-information/product-sheets", Hint = "Product listing web page", Enabled = true, DateTimeCreated = dateTime });
                context.Links.Add(new LinkEntity { TypeId = 2, ProductId = 4, Url = "https://www.cbhs.com.au/docs/default-source/product-sheets/cbhs-prestige.pdf", Hint = "Product document", Enabled = true, DateTimeCreated = dateTime });
                context.Links.Add(new LinkEntity { TypeId = 2, ProductId = 4, Url = "http://privatehealth.gov.au/dynamic/download.ashx?id=CBH/J11/NAET20", Hint = "Product document (SIS)", Enabled = true, DateTimeCreated = dateTime });
                context.SaveChanges();

                context.Products.Add(new ProductEntity { Title = "Active Hospital 100", Name = "Active Hospital", Variation = "0 EX | 100 CP", ClientId = 6, TypeId = 1, FamilyTypeId = 1, Price = 158.34f, PriceDescription = "Monthly premium (without any rebates)", IsGstApplicableOnPrice = false, Shipping = 0.00f, ShippingDescription = null, IsGstApplicableOnShipping = false, States = new List<StateEntity> { stateAct, stateNsw }, AvailableToPurchaseInStates = string.Join(", ", new List<StateEntity> { stateAct, stateNsw }.Select(x => x.Code)), Enabled = true, DateTimeCreated = dateTime });
                context.SaveChanges();

                context.Links.Add(new LinkEntity { TypeId = 1, ProductId = 5, Url = "https://www.cbhs.com.au/members/member-information/product-sheets", Hint = "Product listing web page", Enabled = true, DateTimeCreated = dateTime });
                context.Links.Add(new LinkEntity { TypeId = 2, ProductId = 5, Url = "https://www.cbhs.com.au/docs/default-source/product-sheets/active-hospital.pdf", Hint = "Product document", Enabled = true, DateTimeCreated = dateTime });
                context.Links.Add(new LinkEntity { TypeId = 2, ProductId = 5, Url = "http://privatehealth.gov.au/dynamic/download.ashx?id=CBH/H4B/NAVA10", Hint = "Product document (SIS)", Enabled = true, DateTimeCreated = dateTime });
                context.SaveChanges();

                context.Products.Add(new ProductEntity { Title = "Basic Hospital Excess 500", Name = "Basic Hospital", Variation = "500 EX | 0 CP", ClientId = 6, TypeId = 1, FamilyTypeId = 1, Price = 87.32f, PriceDescription = "Monthly premium (without any rebates)", IsGstApplicableOnPrice = false, Shipping = 0.00f, ShippingDescription = null, IsGstApplicableOnShipping = false, States = new List<StateEntity> { stateAct, stateNsw }, AvailableToPurchaseInStates = string.Join(", ", new List<StateEntity> { stateAct, stateNsw }.Select(x => x.Code)), Enabled = true, DateTimeCreated = dateTime });
                context.SaveChanges();

                context.Links.Add(new LinkEntity { TypeId = 1, ProductId = 6, Url = "https://www.cbhs.com.au/members/member-information/product-sheets", Hint = "Product listing web page", Enabled = true, DateTimeCreated = dateTime });
                context.Links.Add(new LinkEntity { TypeId = 2, ProductId = 6, Url = "https://www.cbhs.com.au/docs/default-source/product-sheets/basic-hospital.pdf", Hint = "Product document", Enabled = true, DateTimeCreated = dateTime });
                context.Links.Add(new LinkEntity { TypeId = 2, ProductId = 6, Url = "http://privatehealth.gov.au/dynamic/download.ashx?id=CBH/H3A/NAGK10", Hint = "Product document (SIS)", Enabled = true, DateTimeCreated = dateTime });
                context.SaveChanges();

                context.Products.Add(new ProductEntity { Title = "Basic Hospital", Name = "Basic Hospital", Variation = "0 EX | 0 CP", ClientId = 6, TypeId = 1, FamilyTypeId = 1, Price = 114.31f, PriceDescription = "Monthly premium (without any rebates)", IsGstApplicableOnPrice = false, Shipping = 0.00f, ShippingDescription = null, IsGstApplicableOnShipping = false, States = new List<StateEntity> { stateAct, stateNsw }, AvailableToPurchaseInStates = string.Join(", ", new List<StateEntity> { stateAct, stateNsw }.Select(x => x.Code)), Enabled = true, DateTimeCreated = dateTime });
                context.SaveChanges();

                context.Links.Add(new LinkEntity { TypeId = 1, ProductId = 7, Url = "https://www.cbhs.com.au/members/member-information/product-sheets", Hint = "Product listing web page", Enabled = true, DateTimeCreated = dateTime });
                context.Links.Add(new LinkEntity { TypeId = 2, ProductId = 7, Url = "https://www.cbhs.com.au/docs/default-source/product-sheets/basic-hospital.pdf", Hint = "Product document", Enabled = true, DateTimeCreated = dateTime });
                context.Links.Add(new LinkEntity { TypeId = 2, ProductId = 7, Url = "http://privatehealth.gov.au/dynamic/download.ashx?id=CBH/H3/NAQZ10", Hint = "Product document (SIS)", Enabled = true, DateTimeCreated = dateTime });
                context.SaveChanges();

                context.Products.Add(new ProductEntity { Title = "Comprehensive Hospital 100", Name = "Comprehensive Hospital", Variation = "0 EX | 100 CP", ClientId = 6, TypeId = 1, FamilyTypeId = 1, Price = 171.04f, PriceDescription = "Monthly premium (without any rebates)", IsGstApplicableOnPrice = false, Shipping = 0.00f, ShippingDescription = null, IsGstApplicableOnShipping = false, States = new List<StateEntity> { stateAct, stateNsw }, AvailableToPurchaseInStates = string.Join(", ", new List<StateEntity> { stateAct, stateNsw }.Select(x => x.Code)), Enabled = true, DateTimeCreated = dateTime });
                context.SaveChanges();

                context.Links.Add(new LinkEntity { TypeId = 1, ProductId = 8, Url = "https://www.cbhs.com.au/members/member-information/product-sheets", Hint = "Product listing web page", Enabled = true, DateTimeCreated = dateTime });
                context.Links.Add(new LinkEntity { TypeId = 2, ProductId = 8, Url = "https://www.cbhs.com.au/docs/default-source/product-sheets/comprehensive-hospital.pdf", Hint = "Product document", Enabled = true, DateTimeCreated = dateTime });
                context.Links.Add(new LinkEntity { TypeId = 2, ProductId = 8, Url = "http://privatehealth.gov.au/dynamic/download.ashx?id=CBH/H1B/NAMD10", Hint = "Product document (SIS)", Enabled = true, DateTimeCreated = dateTime });
                context.SaveChanges();

                context.Products.Add(new ProductEntity { Title = "Comprehensive Hospital 70", Name = "Comprehensive Hospital", Variation = "0 EX | 70 CP", ClientId = 6, TypeId = 1, FamilyTypeId = 1, Price = 177.80f, PriceDescription = "Monthly premium (without any rebates)", IsGstApplicableOnPrice = false, Shipping = 0.00f, ShippingDescription = null, IsGstApplicableOnShipping = false, States = new List<StateEntity> { stateAct, stateNsw }, AvailableToPurchaseInStates = string.Join(", ", new List<StateEntity> { stateAct, stateNsw }.Select(x => x.Code)), Enabled = true, DateTimeCreated = dateTime });
                context.SaveChanges();

                context.Links.Add(new LinkEntity { TypeId = 1, ProductId = 9, Url = "https://www.cbhs.com.au/members/member-information/product-sheets", Hint = "Product listing web page", Enabled = true, DateTimeCreated = dateTime });
                context.Links.Add(new LinkEntity { TypeId = 2, ProductId = 9, Url = "https://www.cbhs.com.au/docs/default-source/product-sheets/comprehensive-hospital.pdf", Hint = "Product document", Enabled = true, DateTimeCreated = dateTime });
                context.Links.Add(new LinkEntity { TypeId = 2, ProductId = 9, Url = "http://privatehealth.gov.au/dynamic/download.ashx?id=CBH/H1A/NALD10", Hint = "Product document (SIS)", Enabled = true, DateTimeCreated = dateTime });
                context.SaveChanges();

                context.Products.Add(new ProductEntity { Title = "Comprehensive Hospital", Name = "Comprehensive Hospital", Variation = "0 EX | 0 CP", ClientId = 6, TypeId = 1, FamilyTypeId = 1, Price = 196.30f, PriceDescription = "Monthly premium (without any rebates)", IsGstApplicableOnPrice = false, Shipping = 0.00f, ShippingDescription = null, IsGstApplicableOnShipping = false, States = new List<StateEntity> { stateAct, stateNsw }, AvailableToPurchaseInStates = string.Join(", ", new List<StateEntity> { stateAct, stateNsw }.Select(x => x.Code)), Enabled = true, DateTimeCreated = dateTime });
                context.SaveChanges();

                context.Links.Add(new LinkEntity { TypeId = 1, ProductId = 10, Url = "https://www.cbhs.com.au/members/member-information/product-sheets", Hint = "Product listing web page", Enabled = true, DateTimeCreated = dateTime });
                context.Links.Add(new LinkEntity { TypeId = 2, ProductId = 10, Url = "https://www.cbhs.com.au/docs/default-source/product-sheets/comprehensive-hospital.pdf", Hint = "Product document", Enabled = true, DateTimeCreated = dateTime });
                context.Links.Add(new LinkEntity { TypeId = 2, ProductId = 10, Url = "http://privatehealth.gov.au/dynamic/download.ashx?id=CBH/H1/NAKD10", Hint = "Product document (SIS)", Enabled = true, DateTimeCreated = dateTime });
                context.SaveChanges();

                context.Products.Add(new ProductEntity { Title = "Limited Hospital 100", Name = "Limited Hospital", Variation = "0 EX | 100 CP", ClientId = 6, TypeId = 1, FamilyTypeId = 1, Price = 121.90f, PriceDescription = "Monthly premium (without any rebates)", IsGstApplicableOnPrice = false, Shipping = 0.00f, ShippingDescription = null, IsGstApplicableOnShipping = false, States = new List<StateEntity> { stateAct, stateNsw }, AvailableToPurchaseInStates = string.Join(", ", new List<StateEntity> { stateAct, stateNsw }.Select(x => x.Code)), Enabled = true, DateTimeCreated = dateTime });
                context.SaveChanges();

                context.Links.Add(new LinkEntity { TypeId = 1, ProductId = 11, Url = "https://www.cbhs.com.au/members/member-information/product-sheets", Hint = "Product listing web page", Enabled = true, DateTimeCreated = dateTime });
                context.Links.Add(new LinkEntity { TypeId = 2, ProductId = 11, Url = "https://www.cbhs.com.au/docs/default-source/product-sheets/limited-hospital.pdf", Hint = "Product document", Enabled = true, DateTimeCreated = dateTime });
                context.Links.Add(new LinkEntity { TypeId = 2, ProductId = 11, Url = "http://privatehealth.gov.au/dynamic/download.ashx?id=CBH/H2B/NAPQ10", Hint = "Product document (SIS)", Enabled = true, DateTimeCreated = dateTime });
                context.SaveChanges();

                context.Products.Add(new ProductEntity { Title = "Limited Hospital 70", Name = "Limited Hospital", Variation = "0 EX | 70 CP", ClientId = 6, TypeId = 1, FamilyTypeId = 1, Price = 126.45f, PriceDescription = "Monthly premium (without any rebates)", IsGstApplicableOnPrice = false, Shipping = 0.00f, ShippingDescription = null, IsGstApplicableOnShipping = false, States = new List<StateEntity> { stateAct, stateNsw }, AvailableToPurchaseInStates = string.Join(", ", new List<StateEntity> { stateAct, stateNsw }.Select(x => x.Code)), Enabled = true, DateTimeCreated = dateTime });
                context.SaveChanges();

                context.Links.Add(new LinkEntity { TypeId = 1, ProductId = 12, Url = "https://www.cbhs.com.au/members/member-information/product-sheets", Hint = "Product listing web page", Enabled = true, DateTimeCreated = dateTime });
                context.Links.Add(new LinkEntity { TypeId = 2, ProductId = 12, Url = "https://www.cbhs.com.au/docs/default-source/product-sheets/limited-hospital.pdf", Hint = "Product document", Enabled = true, DateTimeCreated = dateTime });
                context.Links.Add(new LinkEntity { TypeId = 2, ProductId = 12, Url = "http://privatehealth.gov.au/dynamic/download.ashx?id=CBH/H2A/NAOO10", Hint = "Product document (SIS)", Enabled = true, DateTimeCreated = dateTime });
                context.SaveChanges();

                context.Products.Add(new ProductEntity { Title = "Limited Hospital", Name = "Limited Hospital", Variation = "0 EX | 0 CP", ClientId = 6, TypeId = 1, FamilyTypeId = 1, Price = 142.65f, PriceDescription = "Monthly premium (without any rebates)", IsGstApplicableOnPrice = false, Shipping = 0.00f, ShippingDescription = null, IsGstApplicableOnShipping = false, States = new List<StateEntity> { stateAct, stateNsw }, AvailableToPurchaseInStates = string.Join(", ", new List<StateEntity> { stateAct, stateNsw }.Select(x => x.Code)), Enabled = true, DateTimeCreated = dateTime });
                context.SaveChanges();

                context.Links.Add(new LinkEntity { TypeId = 1, ProductId = 13, Url = "https://www.cbhs.com.au/members/member-information/product-sheets", Hint = "Product listing web page", Enabled = true, DateTimeCreated = dateTime });
                context.Links.Add(new LinkEntity { TypeId = 2, ProductId = 13, Url = "https://www.cbhs.com.au/docs/default-source/product-sheets/limited-hospital.pdf", Hint = "Product document", Enabled = true, DateTimeCreated = dateTime });
                context.Links.Add(new LinkEntity { TypeId = 2, ProductId = 13, Url = "http://privatehealth.gov.au/dynamic/download.ashx?id=CBH/H2/NANM10", Hint = "Product document (SIS)", Enabled = true, DateTimeCreated = dateTime });
                context.SaveChanges();

                context.Products.Add(new ProductEntity { Title = "Essential Extras", Name = "Essential Extras", Variation = "N/A", ClientId = 6, TypeId = 2, FamilyTypeId = 5, Price = 60.06f, PriceDescription = "Monthly premium (without any rebates)", IsGstApplicableOnPrice = false, Shipping = 0.00f, ShippingDescription = null, IsGstApplicableOnShipping = false, States = new List<StateEntity> { stateAct, stateNsw }, AvailableToPurchaseInStates = string.Join(", ", new List<StateEntity> { stateAct, stateNsw }.Select(x => x.Code)), Enabled = true, DateTimeCreated = dateTime });
                context.SaveChanges();

                context.Links.Add(new LinkEntity { TypeId = 1, ProductId = 14, Url = "https://www.cbhs.com.au/members/member-information/product-sheets", Hint = "Product listing web page", Enabled = true, DateTimeCreated = dateTime });
                context.Links.Add(new LinkEntity { TypeId = 2, ProductId = 14, Url = "https://www.cbhs.com.au/docs/default-source/product-sheets/essential-extras.pdf", Hint = "Product document", Enabled = true, DateTimeCreated = dateTime });
                context.Links.Add(new LinkEntity { TypeId = 2, ProductId = 14, Url = "http://privatehealth.gov.au/dynamic/download.ashx?id=CBH/I3/NAUD2D", Hint = "Product document (SIS)", Enabled = true, DateTimeCreated = dateTime });
                context.SaveChanges();

                context.Products.Add(new ProductEntity { Title = "Intermediate Extras", Name = "Intermediate Extras", Variation = "N/A", ClientId = 6, TypeId = 2, FamilyTypeId = 5, Price = 93.69f, PriceDescription = "Monthly premium (without any rebates)", IsGstApplicableOnPrice = false, Shipping = 0.00f, ShippingDescription = null, IsGstApplicableOnShipping = false, States = new List<StateEntity> { stateAct, stateNsw }, AvailableToPurchaseInStates = string.Join(", ", new List<StateEntity> { stateAct, stateNsw }.Select(x => x.Code)), Enabled = true, DateTimeCreated = dateTime });
                context.SaveChanges();

                context.Links.Add(new LinkEntity { TypeId = 1, ProductId = 15, Url = "https://www.cbhs.com.au/members/member-information/product-sheets", Hint = "Product listing web page", Enabled = true, DateTimeCreated = dateTime });
                context.Links.Add(new LinkEntity { TypeId = 2, ProductId = 15, Url = "https://www.cbhs.com.au/docs/default-source/product-sheets/intermediate-extras.pdf", Hint = "Product document", Enabled = true, DateTimeCreated = dateTime });
                context.Links.Add(new LinkEntity { TypeId = 2, ProductId = 15, Url = "http://privatehealth.gov.au/dynamic/download.ashx?id=CBH/I2/NATB2D", Hint = "Product document (SIS)", Enabled = true, DateTimeCreated = dateTime });
                context.SaveChanges();

                context.Products.Add(new ProductEntity { Title = "Top Extras", Name = "Top Extras", Variation = "N/A", ClientId = 6, TypeId = 2, FamilyTypeId = 5, Price = 161.37f, PriceDescription = "Monthly premium (without any rebates)", IsGstApplicableOnPrice = false, Shipping = 0.00f, ShippingDescription = null, IsGstApplicableOnShipping = false, States = new List<StateEntity> { stateAct, stateNsw }, AvailableToPurchaseInStates = string.Join(", ", new List<StateEntity> { stateAct, stateNsw }.Select(x => x.Code)), Enabled = true, DateTimeCreated = dateTime });
                context.SaveChanges();

                context.Links.Add(new LinkEntity { TypeId = 1, ProductId = 16, Url = "https://www.cbhs.com.au/members/member-information/product-sheets", Hint = "Product listing web page", Enabled = true, DateTimeCreated = dateTime });
                context.Links.Add(new LinkEntity { TypeId = 2, ProductId = 16, Url = "https://www.cbhs.com.au/docs/default-source/product-sheets/top-extras.pdf", Hint = "Product document", Enabled = true, DateTimeCreated = dateTime });
                context.Links.Add(new LinkEntity { TypeId = 2, ProductId = 16, Url = "http://privatehealth.gov.au/dynamic/download.ashx?id=CBH/I1/NARZ2D", Hint = "Product document (SIS)", Enabled = true, DateTimeCreated = dateTime });
                context.SaveChanges();

                context.Database.ExecuteSqlCommand(Properties.Resources.SeedDataSheetForCBHSPrestige);
            }
        }
    }
}