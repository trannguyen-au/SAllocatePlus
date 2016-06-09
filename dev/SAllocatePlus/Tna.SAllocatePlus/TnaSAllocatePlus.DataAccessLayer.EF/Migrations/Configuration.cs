namespace TnaSAllocatePlus.DataAccessLayer.EF.Migrations
{
    using AutoPoco;
    using AutoPoco.Engine;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Diagnostics;
    using System.Linq;
    using Tna.SAllocatePlus.CommonShared;
    using Tna.SAllocatePlus.DataAccessLayer.EF;
    using Tna.SAllocatePlus.DataAccessLayer.Entities;

    internal sealed class Configuration : DbMigrationsConfiguration<Tna.SAllocatePlus.DataAccessLayer.EF.TnaContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Tna.SAllocatePlus.DataAccessLayer.EF.TnaContext context)
        {
            // add region
            AddRegion(context);
            // add job
            AddJob(context);
            // add staff
            AddStaff(context);
            // add users
            AddUser(context);
            // add role
            AddRole(context);
            // add user to role
            AddUserToRole(context);
            // add access rights for staff user 
            AssignAccessRights(context);
        }

        private void AssignAccessRights(TnaContext context)
        {
            var costCentreList = context.CostCentreSet.ToList();
            var wery = context.StaffSet.FirstOrDefault(u => u.Username == "werynguyen");
            wery.AccessList = new List<StaffAccessRight>();
            wery.AccessList.Add(new StaffAccessRight()
            {
                AccessRights = AccessRightsEnum.Write,
                CostCentreCode = "AU-VIC",
                CostCentre = costCentreList.FirstOrDefault(c=>c.CostCentreCode == "AU-VIC")
            });
            wery.AccessList.Add(new StaffAccessRight()
            {
                AccessRights = AccessRightsEnum.Write,
                CostCentreCode = "AU-NSW",
                StaffUserID = wery.StaffID,
                CostCentre = costCentreList.FirstOrDefault(c => c.CostCentreCode == "AU-NSW")
            });
            wery.AccessList.Add(new StaffAccessRight()
            {
                AccessRights = AccessRightsEnum.Write,
                CostCentreCode = "AU-QLD",
                StaffUserID = wery.StaffID,
                CostCentre = costCentreList.FirstOrDefault(c => c.CostCentreCode == "AU-QLD")
            });
            wery.AccessList.Add(new StaffAccessRight()
            {
                AccessRights = AccessRightsEnum.Write,
                CostCentreCode = "AU-WA",
                StaffUserID = wery.StaffID,
                CostCentre = costCentreList.FirstOrDefault(c => c.CostCentreCode == "AU-WA")
            });
            wery.AccessList.Add(new StaffAccessRight()
            {
                AccessRights = AccessRightsEnum.Write,
                CostCentreCode = "AU-SAT",
                StaffUserID = wery.StaffID,
                CostCentre = costCentreList.FirstOrDefault(c => c.CostCentreCode == "AU-SAT")
            });

            var nguyen = context.StaffSet.FirstOrDefault(u => u.Username == "nguyennt");
            nguyen.AccessList = new List<StaffAccessRight>();
            nguyen.AccessList.Add(new StaffAccessRight()
            {
                AccessRights = AccessRightsEnum.Write,
                CostCentreCode = "AU-VIC",
                CostCentre = costCentreList.FirstOrDefault(c => c.CostCentreCode == "AU-VIC")
            });
            nguyen.AccessList.Add(new StaffAccessRight()
            {
                AccessRights = AccessRightsEnum.Write,
                CostCentreCode = "AU-NSW",
                StaffUserID = wery.StaffID,
                CostCentre = costCentreList.FirstOrDefault(c => c.CostCentreCode == "AU-NSW")
            });

            context.SaveChanges();

        }

        private void AddUserToRole(TnaContext context)
        {
            var allStaff = context.StaffSet.ToList();
            var allRoles = context.RoleSet.ToList();
            var adminRole = allRoles.FirstOrDefault(r => r.RoleName == Constants.RoleValue.Administrator);

            adminRole.StaffList = new List<Staff>();
            adminRole.StaffList.Add(context.StaffSet.FirstOrDefault(u => u.Username == "werynguyen"));
            adminRole.StaffList.Add(context.StaffSet.FirstOrDefault(u => u.Username == "nguyennt"));

            var employeeRole = allRoles.FirstOrDefault(r => r.RoleName == Constants.RoleValue.Employee);
            employeeRole.StaffList = new List<Staff>();
            employeeRole.StaffList.AddRange(allStaff);

            context.SaveChanges();

        }

        private void AddRole(TnaContext context)
        {
            context.RoleSet.AddOrUpdate(r => r.RoleName, new Role
            {
                RoleName = Constants.RoleValue.Administrator
            });

            context.RoleSet.AddOrUpdate(r => r.RoleName, new Role
            {
                RoleName = Constants.RoleValue.Employee
            });

            context.SaveChanges();
        }

        private void AddRegion(TnaContext context)
        {
            var regionList = new CostCentre[] {
                new CostCentre() {CostCentreCode="AU",Name= "Australia"},
                new CostCentre() {CostCentreCode="AU-VIC",Name= "Victoria"},
                new CostCentre() {CostCentreCode="AU-NSW",Name= "New South Wales"},
                new CostCentre() {CostCentreCode="AU-QLD",Name= "Queensland"},
                new CostCentre() {CostCentreCode="AU-WA",Name= "Western Australia"},
                new CostCentre() {CostCentreCode="AU-SAT",Name= "Satelite regions"}
            };

            foreach (var region in regionList)
            {
                
                context.CostCentreSet.AddOrUpdate(r=>r.CostCentreCode, region);
                context.SaveChanges();
            }
        }

        private void AddUser(Tna.SAllocatePlus.DataAccessLayer.EF.TnaContext context)
        {
            var costCentreList = context.CostCentreSet.ToList();
            Staff user = new Staff()
            {
                Email = "nguyennt86@gmail.com",
                Password = "123456789",
                FirstName = "Nguyen",
                SurName="Tran",
                Username = "nguyennt",
                Active = true,
                Mobile = "0432431067",
                StaffCostCentre = "AU-VIC",
                CostCentre = costCentreList.FirstOrDefault(cc=>cc.CostCentreCode == "AU-VIC")
            };

            context.StaffSet.AddOrUpdate(u => u.Username, user);

            user = new Staff()
            {
                Email = "6586880@student.swin.edu.au",
                Password = "123456789",
                FirstName = "Wery",
                SurName = "Nguyen",
                Username = "werynguyen",
                Active = true,
                Mobile = "0432431067",
                StaffCostCentre = "AU-NSW",
                CostCentre = costCentreList.FirstOrDefault(cc => cc.CostCentreCode == "AU-NSW")
            };

            context.StaffSet.AddOrUpdate(u => u.Username, user);
            context.SaveChanges();
        }

        private void AddStaff(Tna.SAllocatePlus.DataAccessLayer.EF.TnaContext context)
        {
            int totalStaffCount = 100;
            if (context.StaffSet.Count() >= totalStaffCount) return;

            var session = GetCodeGenFac().CreateSession();
            var costCentreList = context.CostCentreSet.ToList();
            Random rand = new Random();
            var firstNameList = new List<string>() {
                "REBECCA","ERI","AKI","ELISE","KAI","RUI","DANIELLE","MARIO","JAIME","PHILLIP","TIHANA","ERICA","DIHENG (WENDY)","TAMSIN","TOYAH","CHERILYA","YANG","SLAVKA","LI","CELESTE","PIERCE","SYLVESTER","RHIARN","JESSICA","STACIE","DIANE","ASHLEY","TARA","VIOLET","ANJA","CARL","SHAUN","SUSAN","JAMES","MARIA","REBEKAH","KRISTIN","LEANNE","BARRY","DAVID","SLAVKA","JENNY","SANDRA","JESSICA","CORRINE","DEON","MALCOLM","LORRAINE","KEITH","EMMA","EMMA","ELIZABETH"
            };
            var lastNameList = new List<string>() {
                "BAIR","GREEN","MILINKOVIC","MARSDEN","WATTS","LEGG","LAMBLE","BARNES","FITZHERBERT","ANDERSON","CRAIG","LANGTON","BYRNE","KIPPEN","SIMPSON","ROBINSON","GALLAGHER","COPPER","LANGLEY","MILINKOVIC","MUGATROYD","GENGE","WILLIAMS","CLIFFORD","MERCIECA","HUNT","HODGSON","HORN","GRAY","BEATIE","CLAY","HARK","HEARNE","SURIDGE","GRIFFIN","SIELAFF","DAVY","BRAZIER","PAULDEN","SHORTER","CHAND","BARRERA","CHATWIN","FULTON"
            };

            var staffList = session.List<Staff>(totalStaffCount)
                .Random(25)
                    .Impose(x => x.CostCentre, costCentreList.FirstOrDefault(c=>c.CostCentreCode == "AU-VIC"))
                .Next(25)
                    .Impose(x => x.CostCentre, costCentreList.FirstOrDefault(c=>c.CostCentreCode =="AU-NSW"))
                .Next(25)
                    .Impose(x => x.CostCentre, costCentreList.FirstOrDefault(c=>c.CostCentreCode =="AU-QLD"))
                .Next(10)
                    .Impose(x => x.CostCentre, costCentreList.FirstOrDefault(c=>c.CostCentreCode =="AU-WA"))
                .Next(15)
                    .Impose(x => x.CostCentre, costCentreList.FirstOrDefault(c=>c.CostCentreCode =="AU-SAT"))
                .All()
                    .Impose(x => x.StaffID, 0)
                    .Impose(x => x.Active, true)
                    .Impose(x => x.Email, "6586880@student.swin.edu.au")
                    .Impose(x => x.Password, "123456")
                .Get();
            foreach (var staff in staffList)
            {
                staff.FirstName = firstNameList[rand.Next(firstNameList.Count)];
                staff.SurName = lastNameList[rand.Next(lastNameList.Count)];
                staff.Mobile = "0456 " + rand.Next(999).ToString().PadLeft(3, '0') + " " + rand.Next(999).ToString().PadLeft(3, '0');
                staff.Password = "123456";
                staff.Username = ""+staff.FirstName[0]+ staff.SurName[0] + rand.Next(999999).ToString().PadLeft(6, '0');

                context.StaffSet.AddOrUpdate(s=>s.Username, staff);
            }

            context.SaveChanges();
        }

        private void AddJob(Tna.SAllocatePlus.DataAccessLayer.EF.TnaContext context)
        {
            int totalJobCount = 100;
            if (context.JobSet.Count()>= totalJobCount) return;

            var session = GetCodeGenFac().CreateSession();

            var nextMonday = GetNextMonday();
            var costCentreList = context.CostCentreSet.ToList();
            Random rand = new Random();

            var siteName = new List<string>(){
                "ABC CANBERRA","FRANKLINS WAVERLEY GARDENS","MILEARA INT FOODS","FRANKLINS BELCONNEN","FRANKLINS TUGGERANONG","ACCUWEIGH QLD","ACCUWEIGH VIC","ACCUWEIGH SA","LAVERTON SECONDARY COLLEGE","SOLECTRON","ST AUGUSTINES PRIMARY SCHOOL","BROADFORD PRIMARY SCHOOL","HAILEYBURY COLLEGE","SACRED HEART SENIOR COLLEGE","PASCOE VALE PRIMARY SCHOOL","INTELLINK","BRIGHTON BEACH PRIMARY SCHOOL","HORSHAM COLLEGE","BRIBIE ISLAND RETIREMENT VILLAGE","ARARAT NORTH PRIMARY SCHOOL","ARARAT PRIMARY SCHOOL","WHITTLESEA SECONDARY COLLEGE","ALEXANDRA PRIMARY SCHOOL","KANIVA COLLEGE","HRL LIMITED","STONE WATER HOMES","SILHOUETTE","LABURNAM PRIMARY SCHOOL","STOCKTON IGA","STATUS PAINT AND PANEL","GLENROY CENTRAL PRIMARY SCHOOL (OLD GLENROY PS)","OAK PARK PRIMARY SCHOOL","PASCOE VALE SOUTH PRIMARY SCHOOL","LILYDALE WEST PRIMARY SCHOOL","MALVERN VALLEY PRIMARY SCHOOL","EURO IMAGE","HAMLYN BANKS PRIMARY SCHOOL","ELSTERNWICK PRIMARY SCHOOL","LAMBDAH GROUP","VERMONT SECONDARY COLLEGE","YEA PRIMARY SCHOOL","BAYSIDE P-12 COLLEGE WILLIAMSTOWN","NORTH MELBOURNE PRIMARY SCHOOL","WHITTLESEA PRIMARY SCHOOL","EAGLEHAWK PRIMARY SCHOOL","SKYE PRIMARY SCHOOL","UPPER PLENTY PRIMARY SCHOOL","AUSTRALIAN  AUTOMOTIVE AIR","GLENROY COLLEGE","FIELD SOLUTIONS INC","BARWON WATER","LAKE BOLAC COLLEGE","AVE MARIA COLLEGE","WILMOT ROAD PRIMARY SCHOOL","TATURA PRIMARY SCHOOL","BOURCHIER STREET PRIMARY SCHOOL","ASSETS - TRAINING","BUNBURY CATHOLIC COLLEGE","RUSHWORTH P-12  COLLEGE","PEL ONEHUNGA","DEVON MEADOWS PRIMARY SCHOOL","ORRVALE PRIMARY SCHOOL","WALLAN PRIMARY SCHOOL","RACV CLUBS","SUPRE AUSTRALIA FAIR CLEARANCE STORE","THORNBURY HIGH SCHOOL","BELLBRIDGE PRIMARY SCHOOL","SEYMOUR SPECIAL SCHOOL","CLUB LEEDS","SHOWGIRLS BAR 20","CRAIGIEBURN SECONDARY COLLEGE","CRANBOURNE SOUTH PRIMARY SCHOOL","LOWTHER HALL ANGLICAN GRAMMAR SCHOOL","WHISKEY BAR","FORD CREDIT","MT BULLER CHALET","ACCENT COMMS","LE MAC","NIB HEALTH FUNDS","KP & DC MACHINERY OVERHAULS","AVONDALE COLLEGE","BARANDUDA PRIMARY SCHOOL","MITCHELL SECONDARY COLLEGE","WODONGA HIGH SCHOOL","WODONGA WEST PRIMARY SCHOOL","KIEWA VALLEY PRIMARY SCHOOL","RUTHERGLEN PRIMARY SCHOOL","RUTHERGLEN HIGH SCHOOL","WM JOHNSON & CO PTY LTD","WODONGA INSTITUTE OF TAFE","WODONGA PRIMARY SCHOOL","WODONGA SOUTH PRIMARY SCHOOL","WODONGA MIDDLE YEARS COLLEGE - FELLTIMBER","MELROSE PRIMARY SCHOOL","TALLANGATTA SECONDARY COLLEGE","CORRYONG COLLEGE","MARCO ENGINEERING","R MCCLINTOCK & CO","MUDGEE MACHINERY CENTRE","SOCOTEL","BARBERIE MACHINERY","KENWAY & CLARK","W (BILL) CORNISH HOLDINGS PTY LTD","TAMTEL","THE UNIVERSITY OF NEW ENGLAND","DUNCAN & DUNCAN WALGETT PTY LTD","PETER THOMAS SALES & SERVICE","R MCCLINTOCK & CO (FORBES) P/L","FARM IMP. TRACTOR & MOTOR CO","CHARLES STURT UNIVERSITY"
            };
            var siteAddress = new List<string>() {
                "SHOP CF12, CANBERRA CENTRE, CANBERRA, ACT, 2600","CNR POLICE & JACKSON RDS, , VIC, ","MILEARA SHOPPING CENTRE, MILEARA ROAD, East Keilor, VIC, ","LEVEL 3 BENJAMIN WAY, BELCONNEN, ACT, 2617","HYPERDOME, SCOLLAY ST, TUGGERANONG, ACT, 2900","1/11 CHROME STREET, SALISBURY, QLD, 4107","20 TERRACOTT DRIVE, BLACKBURN, VIC, 3130","48 WEST THEBURTON ROAD, THEBARTON, SA, 5031","JENNINGS & BLADIN STREETS, LAVERTON, VIC, 3028","17-23 BRYANT STREET, PADSTOW, NSW, 2211","AUGUSTINES WAY, KEILOR, VIC, 3036","17-23 POWLEETT STREET, BROADFORD, VIC, 3658","855-891 SPRINGVALE ROAD, KEYSBOROUGH, VIC, 3173","195-239 BRIGHTON ROAD, SOMERTON PARK, SA, 5044","GAFFNEY STREET, PASCOE VALE, VIC, 3044","15 ALBERT STREET, BRUNSWICK EAST, VIC, 3057","WINDERMERE CRES, BRIGHTON, VIC, 3186","59 DIMBOOLA ROAD, HORSHAM, VIC, 3400","FOLEY STREET, BONGAREE, QLD, 4507","BLAKE STREET, ARARAT, VIC, 3377","MOORE STREET, ARARAT, VIC, 3377","LAUREL STREET, WHITTLESEA, VIC, 3757","WEBSTER STREET, ALEXANDRA, VIC, 3714","FARMERS STREET, KANIVA, VIC, 3419","677 SPRINGVALE ROAD, MULGRAVE, VIC, 3170","SUITE 21/89 HIGH STREET, KEW, VIC, 3101","212 SILVERWATER RD, SILVERWATER, NSW, 2264","JANET STREET, BLACKBURN, VIC, 3130","53 MITCHELL ST, STOCKTON, NSW, 2295","26B DAVIS RD, WETHERILL PARK, NSW, 2164","50 WHEATSHEAF ROAD, GLENROY, VIC, 3046","WILLETT AVENUE, OAK PARK, VIC, 3046","REYNARD STREET, PASCOE VALE SOUTH, VIC, 3044","BOWEN ROAD, LILYDALE, LILYDALE, VIC, 3140","ABBOTSFORD AVE, MALVERN EAST, VIC, 3145","296-298 CHESTERVILLE RD, MOORABBIN, VIC, 3189","VINES ROAD, HAMLYN HEIGHTS, VIC, 3215","MURPHY STREET, BRIGHTON, VIC, 3186","PO BOX 788, SPRING HILL, QLD, 4004","MORACK RD, VERMONT, VIC, 3133","23 STATION ST, YEA, VIC, 3717","KOROROIT CREEK RD, WILLIAMSTOWN, VIC, 3016","210 ERROL STREET, NORTH MELBOURNE, VIC, 3051","PLENTY ROAD, WHITTLESEA, VIC, 3757","CHURCH STREET, EAGLEHAWK, VIC, 3556","395 BALLARTO ROAD, SKYE, VIC, 3977","CLARKES RD, PLENTY UPPER, VIC, 3756","453 DORSET ROAD, CROYDON, VIC, 3136","GLENROY ROAD, GLENROY, VIC, 3046","707 SKOKIE BOULEVARD, NORTHBROOK, NULL, 60062","MONTGOMERY ST, LAKE BOLAC, VIC, 3351","20 VIDA ST, ABERFELDIE, VIC, 3040","WILMOT RD, SHEPPARTON, VIC, 3630","ALBERT STREET, TATURA, VIC, 3616","BOURCHIER STREET, SHEPPARTON, VIC, 3630","30 HIGH STREET, GLEN IRIS, VIC, 3146","RODSTED STREET, BUNBURY, WA, 6230","HEILY STREET, RUSHWORTH, VIC, 3612","WORTHING RD, DEVON MEADOWS, VIC, 3977","300 CHANNEL RD, ORRVALE, VIC, 3631","46-48 QUEEN ST, WALLAN, VIC, 3756","123 QUEEN ST, MELBOURNE, VIC, 3000","42 MARINE PARADE, SOUTHPORT, QLD, 4215","COLLINS STREET, THORNBURY, VIC, 3071","BELLBRIDGE DRIVE, HOPPERS CROSSING, VIC, 3029","TULLAROOK ST, SEYMOUR, VIC, 3660","17 LEEDS ST, FOOTSCRAY, VIC, 3011","40 KING ST, MELBOURNE, VIC, 3000","102 HOTHLYN DRIVE, CRAIGIEBURN, VIC, 3064","CNR PEARCEDALE & BROWNS RD, CRANBOURNE, VIC, 3977","17 LESLIE ROAD, ESSENDON, VIC, 3040","139 BOURKE ST, MELBOURNE, VIC, 3000","GPO BOX 754, NULL, QLD, NULL","344 MANSFIELD STREET, THORNBURY, VIC, 3071","CNR RAILWAY RD & HUDSON PLACE, MULGRAVE, NSW, 2756","LEVEL 5, 384 HUNTER STREET, NEWCASTLE, NSW, 2300","LOT 1 NEW ENGLAND HIGHWAY, RUTHERFORD, NSW, 2320","PO BOX 19, COORANBONG, NSW, 2265","7 VERBENA STREET, BARANDUDA, VIC, 3691","22-24 MITCHELL STREET, WODONGA, VIC, 3689","WOODLAND STREET, WODONGA, VIC, 3690","LAWRENCE ST., WODONGA, VIC, 3690","KIEWA EAST ROAD, TANGAMBALANGA, VIC, 3691","MURRAY STREET, RUTHERGLEN, VIC, 3685","SHERIDANS BRIDGE ROAD, RUTHERGLEN, VIC, 3685","437-447 HONOUR AVENUE, COROWA, NSW, 2646","15 MCKOY STREET, WODONGA, VIC, 3690","BROCKLEY STREET, WODONGA, VIC, 3690","JARRAH STREET, WODONGA, VIC, 3690","HEDGEROW COURT, WODONGA, VIC, 3690","SILVA DRIVE, WODONGA, WODONGA, VIC, 3690","TOWONG STREET, TALLANGATTA, VIC, 3700","TOWONG ROAD, CORRYONG, VIC, 3707","47 MAIN STREET, GRENFELL, NSW, 2810","27 SYDNEY STREET, MUDGEE, NSW, 2850","LOT 4, PACIFIC HIGHWAY, TAREE, NSW, 2340","369-379 FROME STREET, MOREE, NSW, 2400","22 DAMPIER STREET, TAMWORTH, NSW, 2340","THE UNIVERSITY OF NEW ENGLAND, ARMIDALE, NSW, 2351","4 FOX STREET, WALGETT, NSW, 2832","25 ZORA STREET, WARREN, NSW, 2824","1 JUNCTION STREET, FORBES, NSW, 2871","LOT 228 PEAK HILL ROAD, PARKES, NSW, 2870","PO BOX 588, WAGGA WAGGA, NSW, 2678","WAMOON AVENUE, LEETON, NSW, 2705","21 BENCUBBIN AVENUE, COLLEAMBALLY, NSW, 2707","UNGARIE ROAD, WEST WYALONG, NSW, 2671","UNIT 5, 37-41 DOODY STREET, ALEXANDRIA, NSW, 2015","22 RODBOROUGH ROAD, FRENCHS FOREST, NSW, 2086","LEVEL 6, SYDNEY, NSW, 2000"
            };
            var jobTime = new List<TimeSpan>();
            var baseTimeSpan = new TimeSpan(7,0,0);
            for (int i = 0; i < 20; i++)
            {
                jobTime.Add(baseTimeSpan.Add(new TimeSpan(0,i*30,0)));
            }

            var jobList = session.List<Job>(totalJobCount)
                .First(10)
                    .Impose(x => x.JobDate, nextMonday)
                    .Next(10)
                        .Impose(x => x.JobDate, nextMonday.AddDays(1))
                    .Next(10)
                        .Impose(x => x.JobDate, nextMonday.AddDays(2))
                    .Next(10)
                        .Impose(x => x.JobDate, nextMonday.AddDays(3))
                    .Next(10)
                        .Impose(x => x.JobDate, nextMonday.AddDays(4))
                    .Next(10)
                        .Impose(x => x.JobDate, nextMonday.AddDays(5))
                    .Next(10)
                        .Impose(x => x.JobDate, nextMonday.AddDays(6))
                    .Next(10)
                        .Impose(x => x.JobDate, nextMonday.AddDays(7))
                    .Next(10)
                        .Impose(x => x.JobDate, nextMonday.AddDays(8))
                    .Next(10)
                        .Impose(x => x.JobDate, nextMonday.AddDays(9))
                .All().Random(25)
                        .Impose(x => x.CostCentre, costCentreList.FirstOrDefault(r => r.CostCentreCode == "AU-VIC"))
                    .Next(25)
                        .Impose(x => x.CostCentre, costCentreList.FirstOrDefault(r => r.CostCentreCode == "AU-NSW"))
                    .Next(25)
                        .Impose(x => x.CostCentre, costCentreList.FirstOrDefault(r => r.CostCentreCode == "AU-QLD"))
                    .Next(10)
                        .Impose(x => x.CostCentre, costCentreList.FirstOrDefault(r => r.CostCentreCode == "AU-WA"))
                    .Next(15)
                        .Impose(x => x.CostCentre, costCentreList.FirstOrDefault(r => r.CostCentreCode == "AU-SAT"))
                .All()
                    .Impose(x => x.BookID, 0)
                    .Impose(x => x.JobStage, JobStageEnum.Booked)
                    .Impose(x => x.StaffRequired, rand.Next(10))
                    .Impose(x => x.Supervisor, null)
                .Get();
            bool hasError = false;
            foreach (var job in jobList)
            {
                job.SiteName = siteName[rand.Next(siteName.Count)];
                job.SiteAddress = siteAddress[rand.Next(siteAddress.Count)];
                job.StaffRequired = rand.Next(10);
                job.JobTime = jobTime[rand.Next(jobTime.Count - 1)];

                context.JobSet.AddOrUpdate(j=>j.SiteName, job);
            }

            if (!hasError)
                context.SaveChanges();
        }

        private DateTime GetNextMonday()
        {
            var currentDate = DateTime.Now;
            while (currentDate.DayOfWeek != DayOfWeek.Monday)
            {
                currentDate = currentDate.AddDays(1);
            }

            return currentDate.Date;
        }

        private IGenerationSessionFactory GetCodeGenFac()
        {
            return AutoPocoContainer.Configure(x =>
            {
                x.Conventions(c =>
                {
                    c.UseDefaultConventions();
                });

                x.AddFromAssemblyContainingType<Staff>();
            });
        }

        internal void RunSeed()
        {
            Seed(new TnaContext());
        }
    }
}
