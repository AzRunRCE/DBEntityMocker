using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

using System.Collections.Generic;
using System.Globalization;
using System.Data.Entity;
using System.Reflection;
using System.Data.SqlClient;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GenData()
        {
            //using (ModelEntity.SellMyBtcEntities db = new ModelEntity.SellMyBtcEntities())
            //{
            //    var a = db.Asset.First();
            //    var props = typeof(ModelEntity.Asset).GetProperties();
            //    string outPut = "";
            //    outPut = "new " + typeof(Asset).Name + "(){\n";
            //    foreach (var prop in props)
            //    {

            //        object value = prop.GetValue(a, null); // against prop.Name
            //        Type type = prop.PropertyType;
            //        string name = prop.Name; // against prop.Name
            //        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ICollection<>))
            //            continue;

            //        if (type == typeof(int) || type == typeof(int?))
            //        {
            //            if (value != null)
            //            {
            //                outPut += '@' + name + "=" + value + ",\n";
            //            }
            //            else
            //                outPut += '@' + name + "=null,\n";
            //        }
            //        else if (type == typeof(string))
            //        {
            //            outPut += '@' + name + "=\"" + value + "\",\n";
            //        }
            //        else if (type == typeof(decimal) || type == typeof(decimal?))
            //        {
            //            if (value != null)
            //            {
            //                decimal val = (decimal)value;
            //                outPut += '@' + name + "=" + val.ToString(CultureInfo.InvariantCulture) + "m,\n";
            //            }
            //            else
            //                outPut += '@' + name + "=null,\n";
            //        }
            //        else if (type == typeof(DateTime) || type == typeof(DateTime?))
            //        {

            //            if (value != null)
            //            {
            //                DateTime aaa = (DateTime)value;
            //                //   var a = new DateTime(aaa.Year, aaa.Month, aaa.Day, aaa.Hour, aaa.Minute, aaa.Second);

            //                var dateString = $"{aaa.Year},{aaa.Month},{aaa.Day},{aaa.Hour},{aaa.Minute},{aaa.Second}";
            //                outPut += '@' + name + "=new DateTime(" + dateString + "),\n";
            //            }
            //            else
            //            {
            //                outPut += '@' + name + "=null,\n";
            //            }
            //        }
            //        else if (type == typeof(bool) || type == typeof(bool?))
            //        {
            //            if (value != null)
            //            {
            //                outPut += '@' + name + "=" + value.ToString().ToLower() + ",\n";
            //            }
            //            else
            //                outPut += '@' + name + "=null,\n";
            //        }

            //    }





            //    outPut += "};";
            //    Console.WriteLine(outPut);
            //}



        }

        [TestMethod]
        public void GenDataDynamic()
        {
            getType();
           
        }


        private void getType()
        {


            var asm = Assembly.LoadFile(@"");
            Type dbcontext = null;
            foreach (Type t in asm.GetTypes())
            {
                if (t.BaseType == (typeof(DbContext)))
                {
                    dbcontext = t;
                    break;
                }
            }
       
            using (DbContext myContext = Activator.CreateInstance(dbcontext) as DbContext)
            {
                string tableName = "Asset";
                var tableType = asm
                   .GetTypes().FirstOrDefault(t => t.Name == tableName);

                var method = myContext.GetType().GetMethods()
                   .First(x => x.IsGenericMethod && x.Name == "Set");

                MethodInfo generic = method.MakeGenericMethod(tableType);
                var set = generic.Invoke(myContext, null);
                string SQL = "select * from Asset where id = @id";

                SqlParameter latParam = new SqlParameter("id", 2);
    
                object[] parameters = new object[] { latParam};
                DbSet a = myContext.Set(tableType);
                var rr = a.SqlQuery(SQL, parameters).ToListAsync().Result.First();
                        
                var props = tableType.GetProperties();

                string outPut = "";
                outPut = "new " + tableType.Name + "(){\n";
                foreach (var prop in props)
                {

                    object value = prop.GetValue(rr, null); // against prop.Name
                    Type type = prop.PropertyType;
                    string name = prop.Name; // against prop.Name
                    if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ICollection<>))
                        continue;

                    if (type == typeof(int) || type == typeof(int?))
                    {
                        if (value != null)
                        {
                            outPut += '@' + name + "=" + value + ",\n";
                        }
                        else
                            outPut += '@' + name + "=null,\n";
                    }
                    else if (type == typeof(string))
                    {
                        outPut += '@' + name + "=\"" + value + "\",\n";
                    }
                    else if (type == typeof(decimal) || type == typeof(decimal?))
                    {
                        if (value != null)
                        {
                            decimal val = (decimal)value;
                            outPut += '@' + name + "=" + val.ToString(CultureInfo.InvariantCulture) + "m,\n";
                        }
                        else
                            outPut += '@' + name + "=null,\n";
                    }
                    else if (type == typeof(DateTime) || type == typeof(DateTime?))
                    {

                        if (value != null)
                        {
                            DateTime aaa = (DateTime)value;
                            //   var a = new DateTime(aaa.Year, aaa.Month, aaa.Day, aaa.Hour, aaa.Minute, aaa.Second);

                            var dateString = $"{aaa.Year},{aaa.Month},{aaa.Day},{aaa.Hour},{aaa.Minute},{aaa.Second}";
                            outPut += '@' + name + "=new DateTime(" + dateString + "),\n";
                        }
                        else
                        {
                            outPut += '@' + name + "=null,\n";
                        }
                    }
                    else if (type == typeof(bool) || type == typeof(bool?))
                    {
                        if (value != null)
                        {
                            outPut += '@' + name + "=" + value.ToString().ToLower() + ",\n";
                        }
                        else
                            outPut += '@' + name + "=null,\n";
                    }

                }





                outPut += "};";
                Console.WriteLine(outPut);
            }
           
            
        }
    }
}
