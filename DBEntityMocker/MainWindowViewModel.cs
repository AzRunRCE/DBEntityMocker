using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DBEntityMocker
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName] string str = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(str));
            }

        }
        private Assembly loadedAssembly;

        public Assembly LoadedAssembly
        {
            get
            {
                return loadedAssembly;
            }

            set
            {
                if (value != loadedAssembly)
                {
                    loadedAssembly = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string assemblyPath;

        public string AssemblyPath
        {
            get
            {
                return assemblyPath;
            }

            set
            {
                if (value != assemblyPath)
                {
                    assemblyPath = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private Type dbEntitiesType;

        public Type DBEntitiesType
        {
            get
            {
                return dbEntitiesType;
            }

            set
            {
                if (value != dbEntitiesType)
                {
                    dbEntitiesType = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private ObservableCollection<Type> efTypes;

        public ObservableCollection<Type> EFTypes
        {
            get
            {
                return efTypes;
            }

            set
            {
                if (value != efTypes)
                {
                    efTypes = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string outPutSrc;
        public string OutPutSrc
        {
            get
            {
                return outPutSrc;
            }

            set
            {
                if (value != outPutSrc)
                {
                    outPutSrc = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string selectedEntityName;
        public string SelectedEntityName
        {
            get
            {
                return selectedEntityName;
            }

            set
            {
                if (value != selectedEntityName)
                {
                    selectedEntityName = value;
                    RequestSQL = $"select * from {selectedEntityName} where id = 1";
                    NotifyPropertyChanged();
                }
            }
        }


        private string requestSQL;
        public string RequestSQL
        {
            get
            {
                return requestSQL;
            }

            set
            {
                if (value != requestSQL)
                {
                    requestSQL = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private ICommand loadAssembly;
        public ICommand LoadAssembly
        {
            get
            {
                if (loadAssembly == null)
                {
                    loadAssembly = new RelayCommand<object>((obj) => _LoadAssembly());
                }
                return loadAssembly;
            }
        }

        private ICommand genMock;
        public ICommand GenMock
        {
            get
            {
                if (genMock == null)
                {
                    genMock = new RelayCommand<object>((obj) => _GenMock());
                }
                return genMock;
            }
        }



        public void _LoadAssembly()
        {

            var odf = new OpenFileDialog();
            odf.ShowDialog();
            AssemblyPath = odf.FileName;
            if (string.IsNullOrEmpty(odf.FileName))
                return;
            LoadedAssembly = Assembly.LoadFile(AssemblyPath);
            foreach (Type t in LoadedAssembly.GetTypes())
            {
                if (t.BaseType == (typeof(DbContext)))
                {
                    DBEntitiesType = t;
                    break;
                }
            }
            EFTypes = new ObservableCollection<Type>();
            foreach (var prop in DBEntitiesType.GetProperties())
            {
                Type type = prop.PropertyType;
                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(DbSet<>))
                {
                    EFTypes.Add(type.GetGenericArguments()[0]);
                }

            }
            // EFTypes = new ObservableCollection<Type>(LoadedAssembly.GetTypes().Where(x => x.Name != dbEntitiesType .Name).ToList());

        }

        public void _GenMock()
        {
            using (DbContext myContext = Activator.CreateInstance(DBEntitiesType) as DbContext)
            {
                try
                {
                    var tableType = LoadedAssembly
                     .GetTypes().FirstOrDefault(t => t.Name == SelectedEntityName);

                    var method = myContext.GetType().GetMethods()
                       .First(x => x.IsGenericMethod && x.Name == "Set");

                    MethodInfo generic = method.MakeGenericMethod(tableType);
                    var set = generic.Invoke(myContext, null);

                    DbSet a = myContext.Set(tableType);
                    var rr = a.SqlQuery(RequestSQL).ToListAsync().Result;

                    var props = tableType.GetProperties();

                    string outPut = "";
                    foreach (var elm in rr)
                    {
                    
                        outPut += "new " + tableType.Name + "(){\n";
                        foreach (var prop in props)
                        {

                            object value = prop.GetValue(elm, null); // against prop.Name
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





                        outPut += "};\n";
                        Console.WriteLine(outPut);
                      
                    }
                    OutPutSrc = outPut;
                }
                catch (Exception ex)
                {
                    OutPutSrc = ex.Message;
                }
            }


        }

    }
}

