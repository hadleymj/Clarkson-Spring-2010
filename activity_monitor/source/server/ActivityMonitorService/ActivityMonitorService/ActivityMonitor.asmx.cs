using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using Server;
using System.Reflection;
using System.Xml.Serialization;

namespace ActivityMonitorService
{
    [Serializable]
    public class User
    {
        public string Username, Password, UserClass;
        public int idNumber = -1;
    }

    [Serializable]
    public abstract class DailyTask
    {
        public int idDailyTask;
        public string DailyTaskClass;
        public bool active;
        public DateTime Date;

        public abstract string display();

        public abstract int getTask();
    }

    [Serializable]
    public abstract class Stats
    {

        public string StatsClass;
        public DateTime StartDateTime, EndDateTime;
        public int idStatistics, idDailyTask;

        public abstract string display();

        public abstract int getStats();

    }

    [Serializable]
    public class WalkingStats : Stats
    {
        public int steps;

        public WalkingStats()
        {
        }

        public WalkingStats(int inSteps, long S_time, long E_time)
        {
            steps = inSteps;
            StartDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime().AddSeconds(S_time);
            EndDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime().AddSeconds(S_time);
        }
        public override string display()
        {
            TimeSpan span = (EndDateTime - StartDateTime);
            return "Steps in " + span.Seconds + " seconds is " + steps;
        }
        public override int getStats()//returns steps i guess
        {
            return steps;
        }
    }

    [Serializable]
    public class WalkingTask : DailyTask
    {
        public int steps;

        public override string display()
        {

            return "Walk " + steps.ToString() + " steps";

        }

        public override int getTask()
        {
            return steps;
        }

        public WalkingTask()
        { }


        public WalkingTask(int inSteps)
        {
            steps = inSteps;
        }

        
    }

    public class ObjectConverter
    {

        public static StatisticsDAO convertStats(Stats s)
        {
            //Type daoType = Type.GetType("Server." + s.StatsClass + "DAO", true);
            StatisticsDAO dao = (StatisticsDAO)Activator.CreateInstance("ActivityMonitorDatabase", "Server." + s.StatsClass + "DAO").Unwrap();

            dao.EndDateTime = s.EndDateTime;
            dao.StartDateTime = s.EndDateTime;
            dao.idStatistics = s.idStatistics;
            dao.idDailyTask = s.idDailyTask;
            dao.StatsClass = s.StatsClass;

            if (dao is WalkingStatsDAO)
            {
                ((WalkingStatsDAO)dao).Steps = ((WalkingStats)s).steps;
            }

            return dao;
        }

        /*
        public static DailyTaskDAO convertDailyTask(DailyTask dt)
        {
            DailyTaskDAO dao = (DailyTaskDAO)Activator.CreateInstance(Type.GetType(dt.DailyTaskClass + "DAO"));

            dao.Active = dt.active;
            dao.DailyTaskClass = dt.DailyTaskClass;
            dao.Date = dt.Date;
            dao.idDailyTask = dt.idDailyTask;

            if (dao is WalkingTaskDAO)
            {
                ((WalkingTaskDAO)dao).Steps = ((WalkingTask)dt).steps;
            }

            return dao;
        }
        */

        public static DailyTask convertDailyTask(DailyTaskDAO dao)
        {
            dao.Find();
            Type dtType = Type.GetType("ActivityMonitorService." + dao.DailyTaskClass);
            DailyTask dt = (DailyTask)Activator.CreateInstance(dtType);

            dt.active = dao.Active;
            dt.DailyTaskClass = dao.DailyTaskClass;
            dt.Date = dao.Date;
            dt.idDailyTask = dao.idDailyTask;

            if (dt is WalkingTask)
            {
                ((WalkingTask)dt).steps = ((WalkingTaskDAO)dao).Steps;
            }

            return dt;
        }

        public static Stats convertStats(StatisticsDAO dao)
        {
            Stats s = (Stats)Activator.CreateInstance(Type.GetType(dao.StatsClass));

            s.EndDateTime = dao.EndDateTime;
            s.StartDateTime = dao.EndDateTime;
            s.idStatistics = dao.idStatistics;
            s.idDailyTask = dao.idDailyTask;
            s.StatsClass = dao.StatsClass;

            if (s is WalkingStats)
            {
                ((WalkingStats)s).steps = ((WalkingStatsDAO)dao).Steps;
            }

            return s;
        }
    }
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://localhost:2345")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    [XmlInclude(typeof(WalkingTask)), XmlInclude(typeof(WalkingStats)), XmlInclude(typeof(DailyTask)), XmlInclude(typeof(Stats)), XmlInclude(typeof(User))]
    //, XmlInclude(typeof(WalkingTaskDAO)), XmlInclude(typeof(DailyTaskDAO)), XmlInclude(typeof(WalkingStatsDAO)), XmlInclude(typeof(StatisticsDAO))
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ActivityMonitor : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            UserDAO dao = new UserDAO();
            dao.Username = "ryan";

            dao.Find();

            return dao.Password;

        }

        /// <summary>
        /// Upload a stats object from a remote host to the server.
        /// The user
        /// </summary>
        /// <param name="me">The object representing the user attempting to upload a Statistics object. The Username and Password fields must exist.</param>
        /// <param name="stats">The statistics object to upload and save.</param>
        [WebMethod]
        public void UploadStats(User me, Stats stats)
        {
            UserDAO myself = new UserDAO();
            myself.Username = me.Username;

            //The user does not exist.
            if (!myself.Find() || !myself.Password.Equals(me.Password) )
                return;


            StatisticsDAO nStats = ObjectConverter.convertStats(stats);
            nStats.Insert();
        }

        /// <summary>
        /// Get the list of active tasks for the provided user.
        /// </summary>
        /// <param name="me">The object representing the user to get the tasks for. The Username and Password fields must exist.</param>
        /// <returns>A list of DailyTask objects.</returns>
        [WebMethod]
        public DailyTask [] GetTasks(User me)
        {

            UserDAO myself = new UserDAO();
            myself.Username = me.Username;

            //The user does not exist.
            if (!myself.Find() || !myself.Password.Equals(me.Password))
                return null;

            DailyTaskDAO dtDao = new DailyTaskDAO();
            List<DailyTaskDAO> taskList = myself.GetActiveTasks();
            
            DailyTask[] tasks = new DailyTask[taskList.Count];

            List<DailyTaskDAO>.Enumerator i = taskList.GetEnumerator();
            int ind = 0;
            while (i.MoveNext())
            {
                tasks[ind] = ObjectConverter.convertDailyTask(i.Current);
                ind++;
            }

            return tasks;
        }
    }
}
