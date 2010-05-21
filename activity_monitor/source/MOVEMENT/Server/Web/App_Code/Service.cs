/*
Web services for MOVEMENT server side logic
Authors: Andrew Carter
         Andrew Pucci
Version: 5.0.7
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Services;
using Movement.Database;

#region Structures
/// <summary>
/// Defines an object to store patient info in
/// </summary>
/// <remarks>The serializable attribute declaration specifies that this class
/// can be serialized and passed across application domains.  By default, 
/// serialization can be to/from binary or XML; other formatters can be written.</remarks>
[Serializable]
public struct PatientObject
{
    public string name;
    public string address;
    public string ContactInfo;
    public char sex;
    public DateTime dob;
    public char handedness;
    public string ssn;
    public int ID;
}

[Serializable]
public struct UserObject
{
    public string name;
    public string contactInfo;
    public string userName;
    public string password;
    public char role;
    public int userID;
}

[Serializable]
public struct Data
{
    public Int16 x;
    public Int16 y;
    public Int32 time;
    public Int16 pressure;
}

[Serializable]
public struct Test
{
    public UserObject user;
    public PatientObject patient;
    public Script script;
    //arraylist
    public List<Data> data;
    public char hand;
    public char mode;
    //mean X and Y for a test
    public double avg_X;
    public double avg_Y;
    public short rotation;
    public bool isNormal;
    public List<AnalysisData> anal;
    public DateTime timestamp;
    public int ID;
}

[Serializable]
public struct ScriptInfo
{
    public string name;
    public int scriptID;
    public int versionID;
    public string description;
}

[Serializable]
public struct Script
{
    public string scriptData;
    public int scriptID;
    public string type;
    public int version;
}

[Serializable]
public struct Batch
{
    public string name;
    public string description;
    public List<ScriptInfo> scripts;
    public int batchID;
    public int count;
}

[Serializable]
public struct AnalysisData
{
    public Movement.Analysis.AnalysisMetric metric;
    public double mean;
    public double stdDev;
    public double min;
    public double max;
}

[Serializable]
public struct Notes
{
    public string author;
    public string note;
    public DateTime time;
}
#endregion

[WebService(Namespace = "http://softeng-lab.camp.clarkson.edu/movement/server/Service.asmx")]
//[WebService(Namespace = "http://localhost/Movement/Service.asm x")]
public class Service : System.Web.Services.WebService	//inherits from the .NET webservice class
{
    public Service()
    {
        //CODEGEN: This call is required by the ASP.NET Web Services Designer
        InitializeComponent();
    }

    #region Component Designer generated code

    //Required by the Web Services Designer 
    private IContainer components = null;

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #endregion

    #region tester
    /*
    [WebMethod(EnableSession = true)]
    public void tester()
    {
        List<Test> t1 = new List<Test>();

        //throw new Exception();
        Test t = new Test();
        t.avg_X = 0;
        t.avg_Y = 0;
        t.data = new List<Data>();
        t.hand = 'R';
        t.mode = 'T';
        t.patient = new PatientObject();
        t.patient.ID = 41;
        t.rotation = 0;
        t.script = new Script();
        t.script.scriptID = 1;
        t.script.type = "triangle";
        t.user = new UserObject();
        t.user.userID = 2;
        t.anal = new List<AnalysisData>();
        logIn("Duffy", "131432");
        t1 = getTests(0, t.patient);
        logOut();
    }
     * */
    #endregion

    #region Session Authentication

    #region UserInfo
    /// <summary>
    /// UserInfo is a property used to keep session information. This information is stored as 
    /// an instance of the UserObject structure. You can then access all the information needed
    /// from the returned UserObject
    /// </summary>
    /// <returns>A UserObject with the Role, UserID and UserName fields populated</returns>
    private Movement.Database.User UserInfo
    {
        get
        {
            object un = Session["UserInfo"];
            if (un != null) // There is session data availible
            {
                return (Movement.Database.User)un;
            }
            else // There is no session data availible. You will receive this exception if you have not logged in.
            {
                throw new NullReferenceException("There is no session data availible!");
            }
        }
        set
        {
            Session["UserInfo"] = value;
        }
    }
    #endregion

    #region Authenticated_AorC
    /// <summary>
    /// This is a private function that will be called before every WebMethod function 
    /// that will be availible to Clinicians and Administrators
    /// </summary>
    /// <returns>True if user role is A or C, else, false</returns>
    private bool Authenticated_AorC()
    {
        try
        {
            if (UserInfo.Role.Equals('A') || UserInfo.Role.Equals('C'))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch { return false; }
    }
    #endregion

    #region Authenticated_onlyA
    /// <summary>
    /// This is a private function that will be called before every WebMethod function 
    /// that will be availible to Clinicians and Administrators
    /// </summary>
    /// <returns>True if user role is A or C, else, false</returns>
    private bool Authenticated_onlyA()
    {
        try
        {
            if (UserInfo.Role.Equals('A'))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch { return false; }
    }
    #endregion

    #endregion

    #region FindAPatient
    /// <summary>
    /// This function is a wrapper to the FindPatient method
    /// </summary>
    /// <returns>A Movement.Database.Patient collection of all the patients found that match the criteria</returns>
    private ReadOnlyCollection<Movement.Database.Patient> FindAPatient(PatientObject patient)
    {
        return Movement.Database.Patient.FindPatient(patient.name, patient.dob, patient.ssn);
    }
    #endregion

    #region makeUser
    /// <summary>
    /// This function makes a new user
    /// </summary>
    /// <returns>True if the user created, else false</returns>
    [WebMethod(EnableSession = true)]
    public bool makeUser(UserObject user)
    {
        if (Authenticated_onlyA())
        {
            try
            {
                if (!user.userName.Equals(string.Empty) || !user.password.Equals(string.Empty))
                {
                    Movement.Database.User.CreateUser(user.userName,
                                                    user.password,
                                                    user.role,
                                                    user.name,
                                                    user.contactInfo);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Log(e); 
                return false;
            }
        }
        else
        {
            throw new UnauthorizedAccessException("You are not authorized to perform that action!");
        }
    }
    #endregion

    #region addPatientNote
    /// <summary>
    /// This function attaches a note to a patient which is given an author
    /// </summary>
    /// <returns>True if the note is succesfully added, else false</returns>
    [WebMethod(EnableSession = true)]
    public bool addPatientNote(PatientObject patient, UserObject user, String data)
    {
        if (Authenticated_AorC())
        {
            ReadOnlyCollection<Movement.Database.Patient> patientList = FindAPatient(patient);
            if (patientList.Count == 1)
            {
                try
                {
                    //Movement.Database.User filler = Movement.Database.User.Login(user.userName, user.password);
                    patientList[0].RecordNote(UserInfo, data);
                    return true;
                }
                catch (Exception e)
                {
                    Log(e); 
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            throw new UnauthorizedAccessException("You are not authorized to perform that action!");
        }
    }
    #endregion

    #region getPatientNotes
    /// <summary>
    /// This function gets all the notes about a patient
    /// </summary>
    /// <returns>True if the note is succesfully loaded, else false</returns>
    [WebMethod(EnableSession = true)]
    public List<Notes> getPatientNotes(PatientObject patient)
    {
        if (Authenticated_AorC())
        {
            ReadOnlyCollection<Movement.Database.Patient> patientList = FindAPatient(patient);
            Movement.Database.PatientNote newNote;
            List<Notes> allNotes = new List<Notes>();
            Notes filler = new Notes();
            if (patientList.Count == 1)
            {
                try
                {
                    for (int i = 0; i < patientList[0].Notes.Count; i++)
                    {
                        newNote = patientList[0].Notes[i];
                        filler.author = newNote.Author.Name;
                        filler.note = newNote.Data;
                        filler.time = newNote.Timestamp;
                        allNotes.Add(filler);

                    }
                    return allNotes;
                }
                catch (Exception e)
                {
                    Log(e); 
                    return null;
                }
            }
            return null;

        }
        else
        {
            throw new UnauthorizedAccessException("You are not authorized to perform that action!");
        }
    }
    #endregion

    #region logIn
    /// <summary>
    /// This function authenticates the user based on their username and password
    /// </summary>
    /// <returns>The User Object if a user is found, else a null object</returns>
    [WebMethod(EnableSession = true)]
    public UserObject logIn(string userName, string password)
    {
        //eventually this will start a session and populate session variables
        UserObject user = new UserObject();
        try
        {
            Movement.Database.User newUser = Movement.Database.User.Login(userName, password);

            if (newUser != null)
            {
                UserInfo = newUser;
                user.role = newUser.Role;
                user.userID = newUser.UserID;
                user.userName = newUser.Username;
                user.contactInfo = newUser.ContactInfo;
                user.name = newUser.Name;
                //UserInfo = user;        //---Set UserInfo session variable
                return user;
            }
            else
            {
                return user;
            }
        }
        catch (Exception e)
        {
            Log(e); 
            return user;
        }

    }
    #endregion

    #region logOut
    /// <summary>
    /// This function will remove all session data when called.
    /// </summary>
    /// <returns>True if function is successful, otherwise false.</returns>
    [WebMethod(EnableSession = true)]
    public bool logOut()
    {
        try
        {
            Session.Abandon();
            return true;
        }
        catch (Exception e)
        {
            Log(e); 
            return false;
        }
    }
    #endregion

    #region findPatient
    /// <summary>
    /// This function searches for a patient in the database 
    /// It looks for all patients that match 2 out of 3 of the relevant fields
    /// </summary>
    /// <returns>A collection of matching patients, or null</returns>
    [WebMethod(EnableSession = true)]
    public List<PatientObject> findPatient(PatientObject patient)
    {
        if (Authenticated_AorC())
        {
            try
            {
                bool flag = false;
                PatientObject filler;
                ReadOnlyCollection<Movement.Database.Patient> p1;
                ReadOnlyCollection<Movement.Database.Patient> p2;
                ReadOnlyCollection<Movement.Database.Patient> p3;
                List<PatientObject> finalCollection = new List<PatientObject>();
                p1 = Movement.Database.Patient.FindPatient(patient.name, patient.dob);
                p2 = Movement.Database.Patient.FindPatient(patient.name, patient.ssn);
                p3 = Movement.Database.Patient.FindPatient(patient.dob, patient.ssn);

                for (int i = 0; i < p1.Count; i++)
                {
                    filler.name = p1[i].Name;
                    filler.ssn = p1[i].SSN4;
                    filler.sex = p1[i].Sex;
                    filler.handedness = p1[i].Handedness;
                    filler.dob = p1[i].DOB;
                    filler.ContactInfo = p1[i].ContactInfo;
                    filler.address = p1[i].Address;
                    filler.ID = p1[i].PatientID;
                    finalCollection.Add(filler);
                }

                for (int i = 0; i < p2.Count; i++)
                {
                    flag = false;
                    for (int j = 0; j < finalCollection.Count; j++)
                    {
                        if (finalCollection[j].ID == p2[i].PatientID)
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (flag == false)
                    {
                        filler.name = p2[i].Name;
                        filler.ssn = p2[i].SSN4;
                        filler.sex = p2[i].Sex;
                        filler.handedness = p2[i].Handedness;
                        filler.dob = p2[i].DOB;
                        filler.ContactInfo = p2[i].ContactInfo;
                        filler.address = p2[i].Address;
                        filler.ID = p2[i].PatientID;
                        finalCollection.Add(filler);
                    }
                }
                for (int i = 0; i < p3.Count; i++)
                {
                    flag = false;
                    for (int j = 0; j < finalCollection.Count; j++)
                    {
                        if (finalCollection[j].ID == p3[i].PatientID)
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (flag == false)
                    {
                        filler.name = p3[i].Name;
                        filler.ssn = p3[i].SSN4;
                        filler.sex = p3[i].Sex;
                        filler.handedness = p3[i].Handedness;
                        filler.dob = p3[i].DOB;
                        filler.ContactInfo = p3[i].ContactInfo;
                        filler.address = p3[i].Address;
                        filler.ID = p3[i].PatientID;
                        finalCollection.Add(filler);
                    }
                }
                return finalCollection;
            }
            catch (Exception e)
            {
                Log(e); 
                return null;
            }
        }
        else
        {
            throw new UnauthorizedAccessException("You are not authorized to perform that action!");
        }
    }
    #endregion

    #region addPatient
    /// <summary>
    /// This function adds patient data to the database.  This data is passed in in the form of a PatientObject
    /// </summary>
    /// <returns>returns the patient ID if the patient was successfully added, 
    /// -1 if the patient exists already, and -5 if there is an error</returns>
    [WebMethod(EnableSession = true)]
    public int addPatient(PatientObject patient)
    {
        if (Authenticated_AorC())
        {
            Movement.Database.Patient filler;
            ReadOnlyCollection<Movement.Database.Patient> p1;
            try
            {
                p1 = FindAPatient(patient);

                if (!p1.Count.Equals(0))
                {
                    return -1;
                }
                else
                {
                    filler = Movement.Database.Patient.CreatePatient(patient.name,
                                                            patient.address,
                                                            patient.ContactInfo,
                                                            patient.sex,
                                                            patient.dob,
                                                            patient.handedness,
                                                            patient.ssn);

                    return filler.PatientID;
                }
            }
            catch (Exception e)
            {
                Log(e); 
                return -5;
            }
        }
        else
        {
            throw new UnauthorizedAccessException("You are not authorized to perform that action!");
        }
    }
    #endregion

    #region deletePatient
    /// <summary>
    /// This function deletes a patients data from the database
    /// </summary>
    /// <returns>True if the database was successfully modified, else false</returns>
    [WebMethod(EnableSession = true)]
    public bool deletePatient(PatientObject patient)
    {
        if (Authenticated_AorC())
        {
            ReadOnlyCollection<Movement.Database.Patient> patientList = FindAPatient(patient);
            if (patientList.Count == 1)
            {
                try
                {
                    Movement.Database.Patient.RemovePatient(patientList[0].PatientID);
                    return true;
                }
                catch (Exception e)
                {
                    Log(e); 
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            throw new UnauthorizedAccessException("You are not authorized to perform that action!");
        }
    }
    #endregion

    #region updatePatient
    /// <summary>
    /// This function finds a patient in the database and updates that patients data 
    /// based on the ID in a PatientObject
    /// </summary>
    /// <returns>True if the database was successfully modified, else false</returns>
    [WebMethod(EnableSession = true)]
    public bool updatePatient(PatientObject patient)
    {
        if (Authenticated_AorC())
        {
            Movement.Database.Patient tempPatient = new Movement.Database.Patient(patient.ID);
            try
            {
                tempPatient.Name = patient.name;
                tempPatient.Address = patient.address;
                tempPatient.ContactInfo = patient.ContactInfo;
                /* The following patient properties are READ-ONLY in the database */
                /*
                 * sex
                 * dob
                 * handedness
                 * ssn
                 */
                return true;
            }
            catch (Exception e)
            {
                Log(e); 
                return false;
            }
        }
        else
        {
            throw new UnauthorizedAccessException("You are not authorized to perform that action!");
        }
    }
    #endregion

    #region retrievePatientData
    /// <summary>
    /// This function searches a database for a patient based on the patially filled PatientObject.
    /// It then fills a new PatientObject with all the data about that patient in the database
    /// </summary>
    /// <returns>A filled PatientObject</returns>
    [WebMethod(EnableSession = true)]
    public PatientObject retrievePatientData(PatientObject patient)
    {
        if (Authenticated_AorC())
        {
            PatientObject newPatient = new PatientObject();
            ReadOnlyCollection<Movement.Database.Patient> patientList = FindAPatient(patient);
            if (patientList.Count == 1)
            {
                try
                {
                    newPatient.name = patientList[0].Name;
                    newPatient.address = patientList[0].Address;
                    newPatient.ContactInfo = patientList[0].ContactInfo;
                    newPatient.sex = patientList[0].Sex;
                    newPatient.dob = patientList[0].DOB;
                    newPatient.handedness = patientList[0].Handedness;
                    newPatient.ssn = patientList[0].SSN4;
                    return newPatient;
                }
                catch (Exception e)
                {
                    Log(e); 
                    return newPatient;
                }
            }
            else
            {
                return newPatient;
            }
        }
        else
        {
            throw new UnauthorizedAccessException("You are not authorized to perform that action!");
        }
    }
    #endregion

    #region getScript
    /// <summary>
    /// This function searches a database for a script that matches scriptName and retrieves that script
    /// </summary>
    /// <returns>The script corresponding to scriptName</returns>
    [WebMethod(EnableSession = true)]
    public Script getScript(ScriptInfo script)
    {
        if (Authenticated_AorC())
        {
            try
            {
                Movement.Database.TestScript test = new Movement.Database.TestScript(script.scriptID);
                Script newScript = new Script();
                newScript.scriptData = test.ScriptData;
                newScript.scriptID = test.ScriptID;
                newScript.type = test.ScriptType.Name;
                newScript.version = test.Version;


                return newScript;
            }
            catch (Exception e)
            {
                Log(e); 
                return new Script();
            }
        }
        else
        {
            throw new UnauthorizedAccessException("You are not authorized to perform that action!");
        }
    }
    #endregion

    #region getScriptData
    /// <summary>
    /// This function searches a database for a script that matches an ID and retrieves that script
    /// </summary>
    /// <returns>The script corresponding to ID</returns>
    private ScriptInfo getScriptData(int ID)
    {
        try
        {
            //using a test script ID not testScriptType ID
            Movement.Database.TestScript script = new Movement.Database.TestScript(ID);
            Movement.Database.TestScriptType test = script.ScriptType;
            ScriptInfo newScript = new ScriptInfo();
            newScript.name = test.Name;
            newScript.description = test.Description;
            newScript.scriptID = test.GetLatestScript().ScriptID;
            newScript.versionID = test.GetLatestScript().Version;
            return newScript;
        }
        catch (Exception e)
        {
            Log(e); 
            return new ScriptInfo();
        }
    }
    #endregion

    #region getBatch
    /// <summary>
    /// This function searches a database for a batch that matches batchID and retrieves that batch
    /// </summary>
    /// <returns>The batch corresponding to batchID</returns>
    [WebMethod(EnableSession = true)]
    public Batch getBatch(Batch batch)
    {
        if (Authenticated_AorC())
        {
            try
            {
                Movement.Database.TestBatch test = new Movement.Database.TestBatch(batch.batchID);
                Batch newBatch = new Batch();
                newBatch.batchID = test.TestBatchID;
                newBatch.description = test.Description;
                newBatch.name = test.Name;
                //newBatch.scripts = test.TestScripts;

                return newBatch;
            }
            catch (Exception e)
            {
                Log(e); 
                return new Batch();
            }
        }
        else
        {
            throw new UnauthorizedAccessException("You are not authorized to perform that action!");
        }
    }
    #endregion

    #region getScriptList
    /// <summary>
    /// This function is used to give the client all the possible script names it can retrieve
    /// </summary>
    /// <returns>A string of all scripts in the database</returns>
    [WebMethod]
    public List<ScriptInfo> getScriptList()
    {
        try
        {
            List<ScriptInfo> scripts = new List<ScriptInfo>();
            ScriptInfo filler = new ScriptInfo();
            ReadOnlyCollection<Movement.Database.TestScriptType> types = Movement.Database.TestScriptType.GetTestScriptTypes();
            for (int i = 0; i < types.Count; i++)
            {
                filler.name = types[i].Name;
                filler.scriptID = types[i].GetLatestScript().ScriptID;
                filler.description = types[i].Description;
                filler.versionID = types[i].GetLatestScript().Version;
                scripts.Add(filler);
            }
            return scripts;
        }
        catch { return null; }
    }
    #endregion

    #region getBatchList
    /// <summary>
    /// This function is used to give the client all the possible batch names it can retrieve
    /// </summary>
    /// <returns>A string of all batch names in the database</returns>
    [WebMethod]
    public List<Batch> getBatchList()
    {
        try
        {
            List<Batch> batches = new List<Batch>();
            Batch filler = new Batch();
            filler.scripts = new List<ScriptInfo>();
            ReadOnlyCollection<Movement.Database.TestBatch> types = Movement.Database.TestBatch.GetTestBatches();
            for (int i = 0; i < types.Count; i++)
            {
                filler = new Batch();
                filler.scripts = new List<ScriptInfo>();
                filler.name = types[i].Name;
                filler.batchID = types[i].TestBatchID;
                filler.description = types[i].Description;


                for (int j = 0; j < types[i].TestScripts.Count; j++)
                {

                    filler.scripts.Add(getScriptData(types[i].TestScripts[j].ScriptID));
                }
                filler.count = filler.scripts.Count;
                batches.Add(filler);

            }
            return batches;
        }
        catch { return null; }
    }
    #endregion

    #region storeTest
    /// <summary>
    /// This function allows the client to send data to the database
    /// This data will be stored in a RawData structure and entered into the database
    /// </summary>
    /// <returns>True if the database was modified successfully, else false</returns>
    [WebMethod(EnableSession = true)]
    public bool storeTest(Test test)
    {
        if (Authenticated_AorC())
        {
            try
            {
                Movement.Database.User newUser = new Movement.Database.User(test.user.userID);
                Movement.Database.Patient newPatient = new Movement.Database.Patient(test.patient.ID);
                Movement.Database.TestScript newScript = new Movement.Database.TestScript(test.script.scriptID);
                IEnumerable<Movement.Database.TestDataSample> data;
                data = test.data.ConvertAll<TestDataSample>(new Converter<Data, TestDataSample>(
                    delegate(Data filler) { return new TestDataSample(filler.time, filler.x, filler.y, filler.pressure); }));

                Movement.Database.Test.CreateTest(newUser, newPatient, test.hand, newScript, test.mode, data, test.avg_X, test.avg_Y, test.rotation);
                return true;
            }
            catch(Exception e) 
            {
                Log(e);
                return false; 
            }
        }
        else
        {
            throw new UnauthorizedAccessException("You are not authorized to perform that action!");
        }
    }
    #endregion

    #region makeBatch
    /// <summary>
    /// This function creates a test batch
    /// </summary>
    /// <returns>True if the batch was created, else false</returns>
    [WebMethod(EnableSession = true)]
    public bool makeBatch(Batch batch)
    {
        if (Authenticated_onlyA())
        {
            try
            {
                List<int> IDs = new List<int>();
                int filler;

                foreach (ScriptInfo x in batch.scripts)
                {

                    filler = x.scriptID;
                    IDs.Add(filler);
                }
                Movement.Database.TestBatch.CreateTestBatch(batch.name, batch.description, IDs);
                return true;
            }
            catch (Exception e)
            {
                Log(e); 
                return false;
            }
        }
        else
        {
            throw new UnauthorizedAccessException("You are not authorized to perform this action!");
        }
    }
    #endregion

    #region getTests
    /// <summary>
    /// This function gets n tests that a user has previously taken. if numTests = 0 then returns
    /// all past tests
    /// </summary>
    /// <returns>Returns a List of previously taken tests</returns>
    [WebMethod(EnableSession = true)]
    public List<Test> getTests(Int32 numTests, PatientObject patient)
    {
        if (Authenticated_AorC())
        {
            try
            {
                List<Test> tests = new List<Test>();
                Test filler = new Test();
                filler.anal = new List<AnalysisData>();
                Movement.Database.Patient p1 = new Movement.Database.Patient(patient.ID);
                ReadOnlyCollection<Movement.Database.Test> testList;
                testList = p1.GetAllTests();
                AnalysisData d1 = new AnalysisData();

                if (numTests == 0)
                {
                    for (int i = 0; i < testList.Count; i++)
                    {
                        filler.anal = new List<AnalysisData>();
                        filler.hand = testList[i].Hand;
                        filler.mode = testList[i].Mode;
                        filler.ID = testList[i].TestID;
                        filler.script.scriptID = testList[i].TestScript.ScriptID;
                        filler.timestamp = testList[i].Timestamp;
                        filler.rotation = testList[i].Rotation;
                        filler.script.type = testList[i].TestScript.ScriptType.Name;
                        foreach (KeyValuePair<Movement.Analysis.AnalysisMetric, Movement.Database.TestAnalysisComponent> data in testList[i].Analysis.Components)
                        {

                            d1.metric = data.Key;
                            d1.max = data.Value.Max;
                            d1.min = data.Value.Min;
                            d1.mean = data.Value.Mean;
                            d1.stdDev = data.Value.StdDev;

                            filler.anal.Add(d1);
                        }

                        tests.Add(filler);
                    }

                }
                else if (numTests < testList.Count)
                {
                    for (int j = 0; j < numTests; j++)
                    {
                        filler.anal = new List<AnalysisData>();
                        filler.hand = testList[j].Hand;
                        filler.mode = testList[j].Mode;
                        filler.ID = testList[j].TestID;
                        filler.script.scriptID = testList[j].TestScript.ScriptID;
                        filler.timestamp = testList[j].Timestamp;
                        filler.rotation = testList[j].Rotation;
                        filler.script.type = testList[j].TestScript.ScriptType.Name;
                        filler.isNormal = testList[j].AnalysisIsNormal;

                        foreach (KeyValuePair<Movement.Analysis.AnalysisMetric, Movement.Database.TestAnalysisComponent> data in testList[j].Analysis.Components)
                        {

                            d1.metric = data.Key;
                            d1.max = data.Value.Max;
                            d1.min = data.Value.Min;
                            d1.mean = data.Value.Mean;
                            d1.stdDev = data.Value.StdDev;

                            filler.anal.Add(d1);
                        }

                        tests.Add(filler);
                    }

                }
                else if (numTests > testList.Count)
                {
                    for (int i = 0; i < testList.Count; i++)
                    {
                        filler.anal = new List<AnalysisData>();
                        filler.hand = testList[i].Hand;
                        filler.mode = testList[i].Mode;
                        filler.ID = testList[i].TestID;
                        filler.script.scriptID = testList[i].TestScript.ScriptID;
                        filler.timestamp = testList[i].Timestamp;
                        filler.rotation = testList[i].Rotation;
                        filler.script.type = testList[i].TestScript.ScriptType.Name;
                        foreach (KeyValuePair<Movement.Analysis.AnalysisMetric, Movement.Database.TestAnalysisComponent> data in testList[i].Analysis.Components)
                        {

                            d1.metric = data.Key;
                            d1.max = data.Value.Max;
                            d1.min = data.Value.Min;
                            d1.mean = data.Value.Mean;
                            d1.stdDev = data.Value.StdDev;

                            filler.anal.Add(d1);
                        }

                        tests.Add(filler);
                    }
                }

                return tests;
            }
            catch (Exception e)
            {
                Log(e); 
                return new List<Test>();
            }
        }
        else
        {
            throw new UnauthorizedAccessException("You are not authorized to perform this action!");
        }
    }
    #endregion

    #region getTestData
    
    /// <summary>
    /// This function gets the raw data for a test
    /// </summary>
    /// <returns>Returns a collection of test data</returns>
    [WebMethod(EnableSession = true)]
    
    public List<Data> getTestData(Int32 testID)
    {
        
        if (Authenticated_AorC())
        {
            try
            {
                Movement.Database.Test filler = new Movement.Database.Test(testID);
                List<Data> rData = new List<Data>();
                Data dataSample;
                foreach (Movement.Database.TestDataSample sample in filler.Data)
                {
                    dataSample.pressure = sample.Pressure;
                    dataSample.time = sample.Time;
                    dataSample.x = sample.X;
                    dataSample.y = sample.Y;
                    rData.Add(dataSample);
                }
                return rData;
            }
            catch { return new List<Data>(); }
            
        }
        else
        {
            throw new UnauthorizedAccessException("You are not authorized to perform this action!");
        } 
    }
     
    #endregion

     #region Log
    
    /// <summary>
    /// This function logs any failures in the system
    /// </summary>
    /// <returns>void</returns>
    //[WebMethod(EnableSession = true)]
    public void Log(Exception e)
    {
        
        System.Diagnostics.EventLog log = new System.Diagnostics.EventLog("Application", ".", "Movement");
        string message = "";
        while (e != null)
        {
            message += e.Message;
            message += "\n";
            message += e.StackTrace;
            message += "\n";
            message += "\n";

            e = e.InnerException;
        }

        log.WriteEntry(message);

    }
     #endregion
    

}

