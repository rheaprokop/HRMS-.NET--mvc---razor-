namespace HRMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Candidates",
                c => new
                    {
                        CandidateID = c.Int(nullable: false, identity: true),
                        Firstname = c.String(),
                        Lastname = c.String(),
                        Title = c.String(),
                        Email = c.String(),
                        Telephone = c.String(),
                        ShortDesc = c.String(),
                        Status = c.String(),
                        CVFilename = c.String(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(),
                    })
                .PrimaryKey(t => t.CandidateID);
            
            CreateTable(
                "dbo.Openings",
                c => new
                    {
                        OpeningID = c.Int(nullable: false, identity: true),
                        JobTitle = c.String(),
                        JobDesc = c.String(),
                        Location = c.String(),
                        EmplType = c.String(),
                        CreatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.OpeningID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Openings");
            DropTable("dbo.Candidates");
        }
    }
}
