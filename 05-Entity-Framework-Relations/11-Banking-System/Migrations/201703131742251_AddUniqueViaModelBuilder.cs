namespace _11_Banking_System.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class AddUniqueViaModelBuilder : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CheckingAccounts", "AccountNumber", c => c.String(nullable: false,
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "IX_SavingAccounts_AccountNumber",
                        new AnnotationValues(oldValue: null, newValue: "IndexAnnotation: { IsUnique: True }")
                    },
                }));
            AlterColumn("dbo.Users", "Username", c => c.String(nullable: false,
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "IX_Users_Username",
                        new AnnotationValues(oldValue: null, newValue: "IndexAnnotation: { IsUnique: True }")
                    },
                }));
            AlterColumn("dbo.Users", "Email", c => c.String(nullable: false,
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "IX_Users_Email",
                        new AnnotationValues(oldValue: null, newValue: "IndexAnnotation: { IsUnique: True }")
                    },
                }));
            AlterColumn("dbo.SavingAccounts", "AccountNumber", c => c.String(nullable: false,
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "IX_SavingAccounts_Username",
                        new AnnotationValues(oldValue: null, newValue: "IndexAnnotation: { IsUnique: True }")
                    },
                }));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SavingAccounts", "AccountNumber", c => c.String(nullable: false,
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "IX_SavingAccounts_Username",
                        new AnnotationValues(oldValue: "IndexAnnotation: { IsUnique: True }", newValue: null)
                    },
                }));
            AlterColumn("dbo.Users", "Email", c => c.String(nullable: false,
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "IX_Users_Email",
                        new AnnotationValues(oldValue: "IndexAnnotation: { IsUnique: True }", newValue: null)
                    },
                }));
            AlterColumn("dbo.Users", "Username", c => c.String(nullable: false,
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "IX_Users_Username",
                        new AnnotationValues(oldValue: "IndexAnnotation: { IsUnique: True }", newValue: null)
                    },
                }));
            AlterColumn("dbo.CheckingAccounts", "AccountNumber", c => c.String(nullable: false,
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "IX_SavingAccounts_AccountNumber",
                        new AnnotationValues(oldValue: "IndexAnnotation: { IsUnique: True }", newValue: null)
                    },
                }));
        }
    }
}
