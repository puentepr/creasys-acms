﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using ACMS.VO;

/// <summary>
/// clsMyObj 的摘要描述
/// </summary>
public class clsMyObj
{
    public clsMyObj()
    {
        //
        // TODO: 在此加入建構函式的程式碼
        //
    }

    public static void ShowMessage(string message)
    {
        string js = null;

        js = string.Format("alert('{0}');", message);
        ScriptManager.RegisterClientScriptBlock(HttpContext.Current.Handler as Page, typeof(string), string.Format("alert_{0}", DateTime.Now.ToString("hhmmss")), js, true);
    }

    public static void ShowMessage(string format, params object[] args)
    {
        clsMyObj.ShowMessage(string.Format(format, args));
    }

    public static DataTable GetDataTable(DataSet DS)
    {
        if (DS == null || DS.Tables.Count == 0 || DS.Tables[0] == null)
        {
            return null;
        }
        else
        {
            return DS.Tables[0];
        }
    }

    public static string GetStringObject(object myObj)
    {
        return (string)(myObj == DBNull.Value ? null : myObj);
    }

    public static void CheckFull(ref DataTable myDataTable, bool IsShowregisted_count, bool IsShowregistable_count)
    {
        if (myDataTable != null)
        {
            myDataTable.Columns.Add("registed_count");
            myDataTable.Columns.Add("registable_count");

            foreach (DataRow DR in myDataTable.Rows)
            {
                int limit_count = Convert.ToInt32(DR["limit_count"]);
                int limit2_count = Convert.ToInt32(DR["limit2_count"]);
                int register_count = Convert.ToInt32(DR["register_count"]);

                if (IsShowregisted_count == true)
                {
                    //已報名人數
                    if (register_count > limit_count)
                    {
                        DR["registed_count"] = string.Format("正({0})+備({1})", limit_count, limit_count - limit_count);
                    }
                    else
                    {
                        DR["registed_count"] = string.Format("正({0})+備({1})", register_count, 0);
                    }

                }

                if (IsShowregisted_count == true)
                {
                    //剩餘名額
                    if (register_count < (limit_count + limit2_count))
                    {
                        int registableA = 0;
                        int registableB = 0;

                        if (register_count >= limit_count)
                        {
                            registableA = 0;
                            registableB = (limit_count + limit2_count) - register_count;
                        }
                        else
                        {
                            registableA = limit_count - register_count;
                            registableB = limit2_count;
                        }

                        DR["registable_count"] = string.Format("正({0})+備({1})", registableA, registableB);
                    }
                    else
                    {
                        DR["registable_count"] = "額滿";
                    }
                }



            }

            myDataTable.AcceptChanges();
        }
    }

    //回傳1 代表字數不到10   
    //回傳2代表第二碼非1,2   
    //回傳3 代表首碼有誤   
    //回傳4代表檢查碼不對   
    public static string IDChk(string vid)
    {
        string[] a = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "U", "V", "X", "Y", "W", "Z", "I", "O" }; 

        List<string> FirstEng = new List<string>(a);// { "A", "B", "C", "D", "E", "F", "G", "H", "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "U", "V", "X", "Y", "W", "Z", "I", "O" };
        string aa = vid.ToUpper();
        bool chackFirstEnd = false;
        if (aa.Trim().Length == 10)
        {
            byte firstNo = Convert.ToByte(aa.Trim().Substring(1, 1));
            if (firstNo > 2 || firstNo < 1)
            {
                return "2";
            }
            else
            {
                int x;
                for (x = 0; x < FirstEng.Count; x++)
                {
                    if (aa.Substring(0, 1) == FirstEng[x])
                    {
                        aa = string.Format("{0}{1}", x + 10, aa.Substring(1, 9));
                        chackFirstEnd = true;
                        break;
                    }

                }
                if (!chackFirstEnd)
                    return "3";

                int i = 1;
                int ss = int.Parse(aa.Substring(0, 1));
                while (aa.Length > i)
                {
                    ss = ss + (int.Parse(aa.Substring(i, 1)) * (10 - i));
                    i++;
                }
                aa = ss.ToString();
                if (vid.Substring(9, 1) == "0")
                {
                    if (aa.Substring(aa.Length - 1, 1) == "0")
                    {
                        return "0";
                    }
                    else
                    {
                        return "4";
                    }
                }
                else
                {
                    if (vid.Substring(9, 1) == (10 - int.Parse(aa.Substring(aa.Length - 1, 1))).ToString())
                    {

                        return "0";
                    }
                    else
                    {
                        return "4";
                    }
                }
            }
        }
        else
        {

            return "1";
        }
    }


}



public class MySingleton
{
    private static MySingleton _singleton = null;

    public enum AlterRegistType
    {
        RegistInsert,
        RegistUpdate,
        CancelRegist
    }

    public enum AlterRegistResult
    {
        RegistSucess,
        RegistFail,
        RegistFail_Already,
        RegistFail_Full,
        CancelRegistSucess,
        CancelRegistFail,
        CancelRegistFail_DayOver,
    }


    public static MySingleton GetMySingleton()
    {
        lock (typeof(MySingleton))
        {
            if (_singleton == null)
            {
                _singleton = new MySingleton();
            }
        }

        return _singleton;
    }

    
    public AlterRegistResult AlterRegist(ActivityRegistVO myActivityRegistVO, List<CustomFieldValueVO> myCustomFieldValueVOList, AlterRegistType myAlterRegistType, Guid activity_id, string emp_id,string regist_deadline, string cancelregist_deadline)
    {
        lock (this)
        {
            if (myAlterRegistType == AlterRegistType.RegistInsert || myAlterRegistType == AlterRegistType.RegistUpdate)
            {
                ACMS.DAO.ActivityRegistDAO myActivityRegistDAO = new ACMS.DAO.ActivityRegistDAO();

                if (myAlterRegistType == AlterRegistType.RegistInsert)
                {
                    //是否重複報名
                    int RegistCount = myActivityRegistDAO.IsPersonRegisted(myActivityRegistVO.activity_id, myActivityRegistVO.emp_id);

                    if (RegistCount > 0)
                    {
                        return AlterRegistResult.RegistFail_Already;
                    }
                    else
                    {
                        //是否已額滿
                        int RegistableCount = myActivityRegistDAO.RegistableCount(myActivityRegistVO.activity_id);

                        if (RegistableCount <= 0)
                        {
                            return AlterRegistResult.RegistFail_Full;
                        }
                        else
                        {
                            int intSaveResult = myActivityRegistDAO.UpdateActivityRegist(myActivityRegistVO, myCustomFieldValueVOList,null, "insert","1");
                            if (intSaveResult == 1)
                            {
                                return AlterRegistResult.RegistSucess;
                            }
                            else
                            {
                                return AlterRegistResult.RegistFail;
                            }
                        }

                    }
                }
                else
                {
                    int intSaveResult = myActivityRegistDAO.UpdateActivityRegist(myActivityRegistVO, myCustomFieldValueVOList,null, "update","1");
                    if (intSaveResult == 1)
                    {
                        return AlterRegistResult.RegistSucess;
                    }
                    else
                    {
                        return AlterRegistResult.RegistFail;
                    }
                }           


            }
            else
            {
                //報名截止日之前-刪除
                //報名截止日之後-狀態改取消
                //取消報名截止日之後-不可以取消

                ACMS.DAO.ActivityRegistDAO myActivityRegistDAO = new ACMS.DAO.ActivityRegistDAO();


                if (Convert.ToDateTime(cancelregist_deadline) > DateTime.Today)
                {
                    //取消報名截止日之前-刪除
                    if (myActivityRegistDAO.DeleteRegist(activity_id, emp_id,"1") > 0)
                    {
                        return AlterRegistResult.CancelRegistSucess;
                    }
                    else
                    {
                        return AlterRegistResult.CancelRegistFail;
                    }
                }
                else if (Convert.ToDateTime(cancelregist_deadline) <= DateTime.Today)
                {
                    //取消報名截止日之後-狀態改取消
                    if (myActivityRegistDAO.CancelRegist(activity_id, emp_id,"1") > 0)
                    {
                        return AlterRegistResult.CancelRegistSucess;
                    }
                    else
                    {
                        return AlterRegistResult.CancelRegistFail;
                    }
                }
                else if (Convert.ToDateTime(cancelregist_deadline) <= DateTime.Today)
                {
                    return AlterRegistResult.CancelRegistFail_DayOver;
                }

                return AlterRegistResult.CancelRegistFail;

            }

        }

    }

    public AlterRegistResult AlterRegist_Team(ActivityRegistVO myActivityRegistVO, List<CustomFieldValueVO> myCustomFieldValueVOList, List<ActivityTeamMemberVO> myActivityTeamMemberVOList, AlterRegistType myAlterRegistType, Guid activity_id, string emp_id, string regist_deadline, string cancelregist_deadline)
    {
        lock (this)
        {
            if (myAlterRegistType == AlterRegistType.RegistInsert || myAlterRegistType == AlterRegistType.RegistUpdate)
            {
                ACMS.DAO.ActivityRegistDAO myActivityRegistDAO = new ACMS.DAO.ActivityRegistDAO();

                if (myAlterRegistType == AlterRegistType.RegistInsert)
                {
                    //是否重複報名
                    int RegistCount = myActivityRegistDAO.IsPersonRegisted(myActivityRegistVO.activity_id, myActivityRegistVO.emp_id);

                    if (RegistCount > 0)
                    {
                        return AlterRegistResult.RegistFail_Already;
                    }
                    else
                    {
                        //是否已額滿
                        int RegistableCount = myActivityRegistDAO.RegistableCount(myActivityRegistVO.activity_id);

                        if (RegistableCount <= 0)
                        {
                            return AlterRegistResult.RegistFail_Full;
                        }
                        else
                        {                          
                            int intSaveResult = myActivityRegistDAO.UpdateActivityRegist(myActivityRegistVO, myCustomFieldValueVOList, myActivityTeamMemberVOList, "insert","2");

                            if (intSaveResult == 1)
                            {
                                return AlterRegistResult.RegistSucess;
                            }
                            else
                            {
                                return AlterRegistResult.RegistFail;
                            }
                        
                        }

                    }
                }
                else
                {
                    int intSaveResult = myActivityRegistDAO.UpdateActivityRegist(myActivityRegistVO, myCustomFieldValueVOList, myActivityTeamMemberVOList, "update","2");

                    if (intSaveResult == 1)
                    {
                        return AlterRegistResult.RegistSucess;
                    }
                    else
                    {
                        return AlterRegistResult.RegistFail;
                    }
                }


            }
            else
            {
                //報名截止日之前-刪除
                //報名截止日之後-狀態改取消
                //取消報名截止日之後-不可以取消

                ACMS.DAO.ActivityRegistDAO myActivityRegistDAO = new ACMS.DAO.ActivityRegistDAO();


                if (Convert.ToDateTime(cancelregist_deadline) > DateTime.Today)
                {
                    //取消報名截止日之前-刪除
                    if (myActivityRegistDAO.DeleteRegist(activity_id, emp_id,"2") > 0)
                    {
                        return AlterRegistResult.CancelRegistSucess;
                    }
                    else
                    {
                        return AlterRegistResult.CancelRegistFail;
                    }
                }
                else if (Convert.ToDateTime(cancelregist_deadline) <= DateTime.Today)
                {
                    //取消報名截止日之後-狀態改取消
                    if (myActivityRegistDAO.CancelRegist(activity_id, emp_id,"2") > 0)
                    {
                        return AlterRegistResult.CancelRegistSucess;
                    }
                    else
                    {
                        return AlterRegistResult.CancelRegistFail;
                    }
                }
                else if (Convert.ToDateTime(cancelregist_deadline) <= DateTime.Today)
                {
                    return AlterRegistResult.CancelRegistFail_DayOver;
                }

                return AlterRegistResult.CancelRegistFail;

            }

        }

    }

}

public class RegistGoSecondEventArgs : EventArgs
{
    private Guid _id;
    public Guid activity_id
    {
        get { return _id; }
    }

    public RegistGoSecondEventArgs(Guid activity_id)
    {
        this._id = activity_id;
    }

}

public class GetEmployeeEventArgs : EventArgs
{
    private string _id;
    public string id
    {
        get { return _id; }
    }

    public GetEmployeeEventArgs(string emp_id)
    {
        this._id = emp_id;
    }

}