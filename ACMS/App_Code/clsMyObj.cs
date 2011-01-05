﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using ACMS.VO;
using System.Net.Mail;
using System.Transactions;
using PDFPlatform;
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

    //個人報名成功寄信
    public static void RegistSuccess(string activity_id, string emp_id, string regist_by,string webPath,string path)
    {//andy 

       
        ACMS.VO.ActivatyVO vo = new ACMS.VO.ActivatyVO();
        ACMS.BO.ActivatyBO bo = new ACMS.BO.ActivatyBO();
        string[] smtpto = System.Configuration.ConfigurationManager.AppSettings["SMTPTo"].Split(',');
        Guid id = new Guid(activity_id);
        vo = bo.SelectActivatyByActivatyID(id);

        MailMessage mail = new MailMessage();
        
      


        //收件者
        string mailtype = System.Configuration.ConfigurationManager.AppSettings["MailType"].ToLower();

        EmployeeVO empVO = new EmployeeVO();
        EMPloyeeDAO empDAO = new EMPloyeeDAO();
        empVO = empDAO.getEmployee(emp_id);
        ACMS.BO.ActivityRegistBO regBO = new ACMS.BO.ActivityRegistBO ();
        ACMS.VO.ActivityRegistVO  regVO = regBO .SelectActivityRegistByPK(id,emp_id);

        if ((string.Compare(mailtype, "online") != 0))
        {
            foreach (string st1 in smtpto)
            {
                mail.To.Add(st1);
            }

        }
        else
        {
           
            mail.To.Add(empVO.OFFICE_MAIL);
            if (emp_id != regist_by)
            {
                empVO = empDAO.getEmployee(regist_by);
                mail.To.Add(empVO.OFFICE_MAIL);
            }
        }

        //取得附加檔案
        ACMS.DAO.UpFilestDAO uDAO = new ACMS.DAO.UpFilestDAO();
        List<ACMS.VO.UpFileVO> listUpfileVo = uDAO.SELECT(path+"\\"+activity_id );
        Attachment data;
        foreach (ACMS.VO.UpFileVO UFvo in listUpfileVo)
        {
            data=new Attachment(UFvo.path  );


            mail.Attachments .Add(data);

        }

        mail.Subject = vo.activity_name +":個人報名成功通知";
        //寄件者
        mail.From = new System.Net.Mail.MailAddress(System.Configuration.ConfigurationManager.AppSettings["SMTPFrom"]);
        mail.IsBodyHtml = true;

        ACMS.DAO.CustomFieldValueDAO myCustFieldValueDAO = new ACMS.DAO.CustomFieldValueDAO();
        List<ACMS.VO.CustomFieldValueVO> myCustomFieldValueVOList = myCustFieldValueDAO.SelectCustomFieldValue(id, emp_id);
        ACMS.BO.CustomFieldItemBO myCustFieldItemBO = new ACMS.BO.CustomFieldItemBO();
        List<ACMS.VO.CustomFieldItemVO> custFieldItemList;
        string custFieldSt = "";
        decimal ttl = 0;
        string[]   FieldIDs;

        foreach (CustomFieldValueVO custFieldVO in myCustomFieldValueVOList)
        {
            if (custFieldVO.field_control.ToLower() == "textbox")
            {
                custFieldSt += custFieldVO.field_name + ":" + custFieldVO.field_value + "<br/>";
            }
            if (custFieldVO.field_control.ToLower() == "textboxlist")
            {
                ttl=0;
                custFieldSt += custFieldVO.field_name + ":<br/>";

                FieldIDs=custFieldVO.field_value.Split (',');
                custFieldItemList = myCustFieldItemBO.SelectByField_id(custFieldVO .field_id );
                foreach (string fieldID in FieldIDs)
                {
                    foreach (CustomFieldItemVO custFieldItem in custFieldItemList)
                        if (int.Parse(fieldID) == custFieldItem.field_item_id)
                        {
                            custFieldSt += custFieldItem.field_item_name + ":" + custFieldItem.field_item_text + "<br/>";
                            ttl += decimal.Parse (custFieldItem.field_item_text);
                        }
                }

                custFieldSt +="合計: "+ttl.ToString ()+ "<br/>";

            }
            if (custFieldVO.field_control.ToLower() == "checkboxlist")
            {
               

                custFieldSt += custFieldVO.field_name + ":<br/>";

                FieldIDs = custFieldVO.field_value.Split(',');
                custFieldItemList = myCustFieldItemBO.SelectByField_id(custFieldVO.field_id);
                foreach (string fieldID in FieldIDs)
                {
                    foreach (CustomFieldItemVO custFieldItem in custFieldItemList)
                        if (int.Parse(fieldID) == custFieldItem.field_item_id)
                        {
                            custFieldSt += custFieldItem.field_item_name +  custFieldItem.field_item_text + ",";
                            
                        }
                }
                custFieldSt += "<br/>";

            }
            if (custFieldVO.field_control.ToLower() == "radiobuttonlist")
            {
                custFieldSt += custFieldVO.field_name + ":<br/>";

                FieldIDs = custFieldVO.field_value.Split(',');
                custFieldItemList = myCustFieldItemBO.SelectByField_id(custFieldVO.field_id);
                foreach (string fieldID in FieldIDs)
                {
                    foreach (CustomFieldItemVO custFieldItem in custFieldItemList)
                        if (int.Parse(fieldID) == custFieldItem.field_item_id)
                        {
                            custFieldSt += custFieldItem.field_item_name  + custFieldItem.field_item_text + ",";

                        }
                }
                custFieldSt += "<br/>";
            }


        }
        ACMS.VO.EmployeeVO regByEmpVO = empDAO.getEmployee(regist_by );
        if (custFieldSt != "")//有自訂欄位
        {
            mail.Body = "<a href='" + webPath + "?ActID="
                + HttpUtility.UrlEncode(activity_id) + "&RegID=" + HttpUtility.UrlEncode(regist_by)
                + "'>" + vo.activity_name + ":個人報名成功通知</a><br/>"
                + "活動名稱:" + vo.activity_name + "<br/>"
                + "報名日期:" + DateTime.Today.ToString("yyyy/MM/dd") + "<br/>"
                + "姓名:" + empVO.NATIVE_NAME + "<br/>"
                + "工號:" + empVO.WORK_ID + "<br/>"
                + "攜眷人數:" + regVO.ext_people.ToString() + "<br/>"
                + custFieldSt
                +"報名人姓名:"+regByEmpVO .NATIVE_NAME
                + "<br/>編號:" + regBO.getSNByActivity(id, emp_id);
        }
        else
        {
            mail.Body = "<a href='" + webPath + "?ActID="
                + HttpUtility.UrlEncode(activity_id) + "&RegID=" + HttpUtility.UrlEncode(regist_by)
                + "'>" + vo.activity_name + ":個人報名成功通知</a><br/>"
                + "活動名稱:" + vo.activity_name + "<br/>"
                + "報名日期:" + DateTime.Today.ToString("yyyy/MM/dd") + "<br/>"
                + "姓名:" + empVO.NATIVE_NAME + "<br/>"
                + "工號:" + empVO.WORK_ID + "<br/>"
                + "攜眷人數:" + regVO.ext_people.ToString() + "<br/>"
                + "報名人姓名:" + regByEmpVO.NATIVE_NAME
            + "<br/>編號:" + regBO.getSNByActivity(id, emp_id);

        }
        SmtpClient smtp = new SmtpClient(System.Configuration.ConfigurationManager.AppSettings["SMTPServer"]);
       // smtp.EnableSsl = true;

        try
        {
            smtp.Send(mail);

        }
        catch (Exception ex)
        {
            LogMsg.Log(ex.Message, 5, false);

        }
     

    }

    //個人報名失敗寄信
    public static void RegistFail(string activity_id, string emp_id, string regist_by, string webPath)
    {

        //andy
        ACMS.VO.ActivatyVO vo = new ACMS.VO.ActivatyVO();
        ACMS.BO.ActivatyBO bo = new ACMS.BO.ActivatyBO();
        string[] smtpto = System.Configuration.ConfigurationManager.AppSettings["SMTPTo"].Split(',');
        Guid id = new Guid(activity_id);
        vo = bo.SelectActivatyByActivatyID(id);

        MailMessage mail = new MailMessage();

        //收件者
        string mailtype = System.Configuration.ConfigurationManager.AppSettings["MailType"].ToLower();
        if ((string.Compare(mailtype, "online") != 0))
        {
            foreach (string st1 in smtpto)
            {
                mail.To.Add(st1);
            }
        }
        else
        {
            EmployeeVO empVO = new EmployeeVO();
            EMPloyeeDAO empDAO = new EMPloyeeDAO();
            empVO = empDAO.getEmployee(emp_id);
            mail.To.Add(empVO.OFFICE_MAIL);
            if (emp_id != regist_by)
            {
                empVO = empDAO.getEmployee(regist_by);
                mail.To.Add(empVO.OFFICE_MAIL);
            }
        }

        mail.Subject = vo.activity_name + ":報名失敗通知";
        //寄件者
        mail.From = new System.Net.Mail.MailAddress(System.Configuration.ConfigurationManager.AppSettings["SMTPFrom"]);
        mail.IsBodyHtml = true;
        mail.Body = "<a href='" + webPath + "?ActID="
            + HttpUtility.UrlEncode(activity_id) + "&RegID=" + HttpUtility.UrlEncode(regist_by )
            + "'>" + vo.activity_name + ":個人報名失敗通知</a>";


        SmtpClient smtp = new SmtpClient(System.Configuration.ConfigurationManager.AppSettings["SMTPServer"]);
        //smtp.EnableSsl = true;

        try
        {
            smtp.Send(mail);

        }
        catch(Exception ex)
        {
            LogMsg.Log(ex.Message, 5, false);
        }
     


    }

    //個人取消報名寄信
    public static void CancelRegist(string activity_id, string emp_id, string cancel_by, string webPath)
    {//andy 

        ACMS.VO.ActivatyVO vo = new ACMS.VO.ActivatyVO();
        ACMS.BO.ActivatyBO bo = new ACMS.BO.ActivatyBO();
        string[] smtpto = System.Configuration.ConfigurationManager.AppSettings["SMTPTo"].Split(',');
        Guid id = new Guid(activity_id);
        vo = bo.SelectActivatyByActivatyID(id);
        MailMessage mail = new MailMessage();

        //收件者
        string mailtype = System.Configuration.ConfigurationManager.AppSettings["MailType"].ToLower();

        EmployeeVO empVO = new EmployeeVO();
        EMPloyeeDAO empDAO = new EMPloyeeDAO();
        empVO = empDAO.getEmployee(emp_id);

        if ((string.Compare(mailtype, "online") != 0))
        {
            foreach (string st1 in smtpto)
            {
                mail.To.Add(st1);
            }
        }
        else
        {
           
            mail.To.Add(empVO.OFFICE_MAIL);
            if (emp_id != cancel_by)
            {
                empVO = empDAO.getEmployee(cancel_by);
                mail.To.Add(empVO.OFFICE_MAIL);
            }
        }
        mail.Subject = vo.activity_name + ":取消報名通知";
        //寄件者
        mail.From = new System.Net.Mail.MailAddress(System.Configuration.ConfigurationManager.AppSettings["SMTPFrom"]);
        mail.IsBodyHtml = true;
        mail.Body = mail.Body = "<a href='" + webPath + "?ActID="
            + HttpUtility.UrlEncode(activity_id) + "&RegID=" + HttpUtility.UrlEncode(cancel_by)
            + "'>"+ vo.activity_name + ":取消報名通知</a><br/>"
            +"活動名稱:"+vo.activity_name
               + "<br/>工號:" + empVO.WORK_ID
            +"<br/>姓名:" +empVO.NATIVE_NAME ;
         


        SmtpClient smtp = new SmtpClient(System.Configuration.ConfigurationManager.AppSettings["SMTPServer"]);
        //smtp.EnableSsl = true;

        try
        {
            smtp.Send(mail);

        }
        catch (Exception ex)
        {
            LogMsg.Log(ex.Message, 5, false);

        }
     

    }


    //團隊報名成功寄信
    public static void RegistSuccess_Team(string activity_id, string emp_id, string regist_by, string webPath, string path)
    {

        if (emp_id == "")
        {
            return;
        }

        //andy 
        ACMS.VO.ActivatyVO vo = new ACMS.VO.ActivatyVO();
        ACMS.BO.ActivatyBO bo = new ACMS.BO.ActivatyBO();
        string[] smtpto = System.Configuration.ConfigurationManager.AppSettings["SMTPTo"].Split(',');
        Guid id = new Guid(activity_id);
        vo = bo.SelectActivatyByActivatyID(id);
        MailMessage mail = new MailMessage();

        //收件者
        string mailtype = System.Configuration.ConfigurationManager.AppSettings["MailType"].ToLower();
        EmployeeVO empVO = new EmployeeVO();
        EMPloyeeDAO empDAO = new EMPloyeeDAO();
        ACMS.BO.ActivityRegistBO regBO = new ACMS.BO.ActivityRegistBO();
        ACMS.VO.ActivityRegistVO regVO = regBO.SelectActivityRegistByPK(id, regist_by);
        string[] emps = emp_id.Split(',');

        if ((string.Compare(mailtype, "online") != 0))
        {
            foreach (string st1 in smtpto)
            {
                mail.To.Add(st1);
            }
        }

        else
        {

            if (string.Compare(regist_by, "") != 0)
            {
                empVO = empDAO.getEmployee(regist_by);
                mail.To.Add(empVO.OFFICE_MAIL);
            }



            foreach (string emp in emps)
            {
                if (emp != regist_by)
                {
                    empVO = empDAO.getEmployee(emp);
                    mail.To.Add(empVO.OFFICE_MAIL);
                }
            }

        }

        //取得附加檔案
        ACMS.DAO.UpFilestDAO uDAO = new ACMS.DAO.UpFilestDAO();
        List<ACMS.VO.UpFileVO> listUpfileVo = uDAO.SELECT(path + "\\" + activity_id);
        Attachment data;
        foreach (ACMS.VO.UpFileVO UFvo in listUpfileVo)
        {
            data = new Attachment(UFvo.path);


            mail.Attachments.Add(data);

        }



        ACMS.DAO.CustomFieldValueDAO myCustFieldValueDAO = new ACMS.DAO.CustomFieldValueDAO();
        List<ACMS.VO.CustomFieldValueVO> myCustomFieldValueVOList = myCustFieldValueDAO.SelectCustomFieldValue(id,regist_by);
        ACMS.BO.CustomFieldItemBO myCustFieldItemBO = new ACMS.BO.CustomFieldItemBO();
        List<ACMS.VO.CustomFieldItemVO> custFieldItemList;
        string custFieldSt = "";
        decimal ttl = 0;
        string[] FieldIDs;

        foreach (CustomFieldValueVO custFieldVO in myCustomFieldValueVOList)
        {
            if (custFieldVO.field_control.ToLower() == "textbox")
            {
                custFieldSt += custFieldVO.field_name + ":" + custFieldVO.field_value + "<br/>";
            }
            if (custFieldVO.field_control.ToLower() == "textboxlist")
            {
                ttl = 0;
                custFieldSt += custFieldVO.field_name + ":<br/>";

                FieldIDs = custFieldVO.field_value.Split(',');
                custFieldItemList = myCustFieldItemBO.SelectByField_id(custFieldVO.field_id);
                foreach (string fieldID in FieldIDs)
                {
                    foreach (CustomFieldItemVO custFieldItem in custFieldItemList)
                        if (int.Parse(fieldID) == custFieldItem.field_item_id)
                        {
                            custFieldSt += custFieldItem.field_item_name + ":" + custFieldItem.field_item_text + "<br/>";
                            ttl += decimal.Parse(custFieldItem.field_item_text);
                        }
                }

                custFieldSt += "合計: " + ttl.ToString() + "<br/>";

            }
            if (custFieldVO.field_control.ToLower() == "checkboxlist")
            {


                custFieldSt += custFieldVO.field_name + ":<br/>";

                FieldIDs = custFieldVO.field_value.Split(',');
                custFieldItemList = myCustFieldItemBO.SelectByField_id(custFieldVO.field_id);
                foreach (string fieldID in FieldIDs)
                {
                    foreach (CustomFieldItemVO custFieldItem in custFieldItemList)
                        if (int.Parse(fieldID) == custFieldItem.field_item_id)
                        {
                            custFieldSt += custFieldItem.field_item_name + custFieldItem.field_item_text + ",";

                        }
                }
                custFieldSt += "<br/>";

            }
            if (custFieldVO.field_control.ToLower() == "radiobuttonlist")
            {
                custFieldSt += custFieldVO.field_name + ":<br/>";

                FieldIDs = custFieldVO.field_value.Split(',');
                custFieldItemList = myCustFieldItemBO.SelectByField_id(custFieldVO.field_id);
                foreach (string fieldID in FieldIDs)
                {
                    foreach (CustomFieldItemVO custFieldItem in custFieldItemList)
                        if (int.Parse(fieldID) == custFieldItem.field_item_id)
                        {
                            custFieldSt += custFieldItem.field_item_name + custFieldItem.field_item_text + ",";

                        }
                }
                custFieldSt += "<br/>";
            }


        }

        mail.Subject = vo.activity_name + ":團隊報名成功通知";
        //寄件者
        mail.From = new System.Net.Mail.MailAddress(System.Configuration.ConfigurationManager.AppSettings["SMTPFrom"]);
        mail.IsBodyHtml = true;

        ACMS.VO.EmployeeVO regByEmpVO = empDAO.getEmployee(regist_by);

        string empList = ""; 
        empList += "<table><tr><td>工號</td> <td>姓名</td></tr>";
        foreach (string emp in emps)
        {
           
           
                 empList +="<tr><td>";
                empVO = empDAO.getEmployee(emp);
                  empList +=  empVO.WORK_ID + "</td><td>";
                empList +=  empVO.NATIVE_NAME +"</td></tr>";
            
             
        }
        empList +="</tr></table>";
        //+ "姓名:" + empVO.NATIVE_NAME + "<br/>"
        //       + "工號:" + empVO.WORK_ID + "<br/>"


        if (custFieldSt != "")//有自訂欄位
        {
            mail.Body = "<a href='" + webPath + "?ActID="
                + HttpUtility.UrlEncode(activity_id) + "&RegID=" + HttpUtility.UrlEncode(regist_by)
                + "'>" + vo.activity_name + ":團隊報名成功通知</a><br/>"
                + "活動名稱:" + vo.activity_name + "<br/>"
                + "報名日期:" + DateTime.Today.ToString("yyyy/MM/dd") + "<br/>"
               + empList
                + "攜眷人數:" + regVO.ext_people.ToString() + "<br/>"
                + custFieldSt
                + "報名人姓名:" + regByEmpVO.NATIVE_NAME
                + "<br/>編號:" + regBO.getSNByActivity(id, regist_by);
        }
        else
        {
            mail.Body = "<a href='" + webPath + "?ActID="
                + HttpUtility.UrlEncode(activity_id) + "&RegID=" + HttpUtility.UrlEncode(regist_by)
                + "'>" + vo.activity_name + ":團隊報名成功通知</a><br/>"
                + "活動名稱:" + vo.activity_name + "<br/>"
                + "報名日期:" + DateTime.Today.ToString("yyyy/MM/dd") + "<br/>"
              + empList
                + "攜眷人數:" + regVO.ext_people.ToString() + "<br/>"
                + "報名人姓名:" + regByEmpVO.NATIVE_NAME
            + "<br/>編號:" + regBO.getSNByActivity(id, regist_by);

        }


        SmtpClient smtp = new SmtpClient(System.Configuration.ConfigurationManager.AppSettings["SMTPServer"]);
        //smtp.EnableSsl = true;

        try
        {
            smtp.Send(mail);

        }
        catch (Exception ex)
        {
            //clsMyObj.ShowMessage(ex.Message );
            
            LogMsg.Log (ex.Message,5,false);
        }


    }

    //團隊報名失敗寄信
    public static void RegistFail_Team(string activity_id, string emp_id, string regist_by, string webPath)
    {//andy 
        ACMS.VO.ActivatyVO vo = new ACMS.VO.ActivatyVO();
        ACMS.BO.ActivatyBO bo = new ACMS.BO.ActivatyBO();
        string[] smtpto = System.Configuration.ConfigurationManager.AppSettings["SMTPTo"].Split(',');
        Guid id = new Guid(activity_id);
        vo = bo.SelectActivatyByActivatyID(id);
        MailMessage mail = new MailMessage();



        //收件者
        string mailtype = System.Configuration.ConfigurationManager.AppSettings["MailType"].ToLower();
        if ((string.Compare(mailtype, "online") != 0))
        {
            foreach (string st1 in smtpto)
            {
                mail.To.Add(st1);
            }
        }

        else
        {
            EmployeeVO empVO = new EmployeeVO();
            EMPloyeeDAO empDAO = new EMPloyeeDAO();
            if (string.Compare(regist_by, "") != 0)
            {
                empVO = empDAO.getEmployee(regist_by);
                mail.To.Add(empVO.OFFICE_MAIL);
            }
            string[] emps = emp_id.Split(',');

            foreach (string emp in emps)
            {
                if (emp != regist_by)
                {
                    empVO = empDAO.getEmployee(emp);
                    mail.To.Add(empVO.OFFICE_MAIL);
                }
            }

        }
        mail.Subject = vo.activity_name + ":團隊報名失敗通知";
        //寄件者
        mail.From = new System.Net.Mail.MailAddress(System.Configuration.ConfigurationManager.AppSettings["SMTPFrom"]);
        mail.IsBodyHtml = true;
        mail.Body = "<a href='" + webPath + "?ActID="
            + HttpUtility.UrlEncode(activity_id) + "&RegID=" + HttpUtility.UrlEncode(regist_by)
            + "'>" + vo.activity_name + ":團隊報名失敗通知</a>";

        SmtpClient smtp = new SmtpClient(System.Configuration.ConfigurationManager.AppSettings["SMTPServer"]);
        //smtp.EnableSsl = true;

        try
        {
            smtp.Send(mail);

        }
        catch(Exception ex)
        {
            LogMsg.Log(ex.Message, 5, false);

        }



    }

    //團隊取消報名寄信
    public static void CancelRegist_Team(string activity_id, string emp_id, string cancel_by, string webPath)
    {
        //andy 
        ACMS.VO.ActivatyVO vo = new ACMS.VO.ActivatyVO();
        ACMS.BO.ActivatyBO bo = new ACMS.BO.ActivatyBO();
        string[] smtpto = System.Configuration.ConfigurationManager.AppSettings["SMTPTo"].Split(',');
        Guid id = new Guid(activity_id);
        vo = bo.SelectActivatyByActivatyID(id);
        MailMessage mail = new MailMessage();


        string[] emps = emp_id.Split(',');
        EmployeeVO empVO = new EmployeeVO();
        EMPloyeeDAO empDAO = new EMPloyeeDAO();

        //收件者
      string mailtype = System.Configuration.ConfigurationManager.AppSettings["MailType"].ToLower();
        if ((string.Compare(mailtype, "online") != 0))
        {
            foreach (string st1 in smtpto)
            {
                mail.To.Add(st1);
            }
        }
        else
        {
           
            if (string.Compare(cancel_by, "") != 0)
            {
                empVO = empDAO.getEmployee(cancel_by);
                mail.To.Add(empVO.OFFICE_MAIL);
            }
         
           

            foreach (string emp in emps)
            {
                if (emp != cancel_by)
                {
                    empVO = empDAO.getEmployee(emp);
                    mail.To.Add(empVO.OFFICE_MAIL);
                }
            }

        }

        string empList = "";
        empList += "<table><tr><td>工號</td> <td>姓名</td></tr>";
        foreach (string emp in emps)
        {


            empList += "<tr><td>";
            empVO = empDAO.getEmployee(emp);
            empList += empVO.WORK_ID + "</td><td>";
            empList += empVO.NATIVE_NAME + "</td></tr>";


        }
        empList += "</tr></table>";


        mail.Subject = vo.activity_name + ":團隊取消報名通知";
        //寄件者
        mail.From = new System.Net.Mail.MailAddress(System.Configuration.ConfigurationManager.AppSettings["SMTPFrom"]);
        mail.IsBodyHtml = true;
        mail.Body = "<a href='" + webPath + "?ActID="
            + HttpUtility.UrlEncode(activity_id) + "&RegID=" + HttpUtility.UrlEncode(cancel_by)
            + "'>" + vo.activity_name + ":團隊取消報名通知</a><br/>"
            +"活動名稱:"+vo.activity_name +"<br/>"
            + empList;



        SmtpClient smtp = new SmtpClient(System.Configuration.ConfigurationManager.AppSettings ["SMTPServer"]);
        //smtp.EnableSsl = true;
        try
        {
            smtp.Send(mail);

        }
        catch(Exception ex)
        {
            LogMsg.Log(ex.Message, 5, false);
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
        UpdateRegistSucess,
        UpdateRegistFail,
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

    //個人活動報名或取消報名
    public AlterRegistResult AlterRegist(ActivityRegistVO myActivityRegistVO, List<CustomFieldValueVO> myCustomFieldValueVOList, AlterRegistType myAlterRegistType, Guid activity_id, string emp_id,string regist_deadline, string cancelregist_deadline, string webPath ,string path)
    {
        lock (this)
        {
            if (myAlterRegistType == AlterRegistType.RegistInsert || myAlterRegistType == AlterRegistType.RegistUpdate)
            {
                ACMS.DAO.ActivityRegistDAO myActivityRegistDAO = new ACMS.DAO.ActivityRegistDAO();

                if (myAlterRegistType == AlterRegistType.RegistInsert)
                {
                    //先Insert報名資訊看是否成功
                    int intSaveResult = myActivityRegistDAO.UpdateActivityRegist(myActivityRegistVO, myCustomFieldValueVOList, null, "insert", "1",webPath ,path);
                    if (intSaveResult == 1)
                    {
                        //andy-報名成功寄信
                        clsMyObj.RegistSuccess(myActivityRegistVO.activity_id.ToString(), myActivityRegistVO.emp_id, myActivityRegistVO.regist_by,webPath ,path );

                        return AlterRegistResult.RegistSucess;
                    }
                    else
                    {
                        //若失敗可能是重複報名或額滿

                        //是否重複報名
                        int RegistCount = myActivityRegistDAO.IsPersonRegisted(myActivityRegistVO.activity_id, myActivityRegistVO.emp_id,"", "1");

                        if (RegistCount > 0)
                        {
                            //andy-報名失敗寄信
                            clsMyObj.RegistFail(myActivityRegistVO.activity_id.ToString(), myActivityRegistVO.emp_id, myActivityRegistVO.regist_by,webPath );

                            return AlterRegistResult.RegistFail_Already;
                        }

                        //是否已額滿
                        int RegistableCount = myActivityRegistDAO.RegistableCount(myActivityRegistVO.activity_id);

                        if (RegistableCount <= 0)
                        {
                            //andy-報名失敗寄信
                            clsMyObj.RegistFail(myActivityRegistVO.activity_id.ToString(), myActivityRegistVO.emp_id, myActivityRegistVO.regist_by,webPath );
                            return AlterRegistResult.RegistFail_Full;
                        }

                        //andy-報名失敗寄信
                        clsMyObj.RegistFail(myActivityRegistVO.activity_id.ToString(), myActivityRegistVO.emp_id, myActivityRegistVO.regist_by,webPath );

                        return AlterRegistResult.RegistFail;
                    }

                }
                else
                {
                    int intSaveResult = myActivityRegistDAO.UpdateActivityRegist(myActivityRegistVO, myCustomFieldValueVOList, null, "update", "1",webPath,path);
                    if (intSaveResult == 1)
                    {
                        return AlterRegistResult.UpdateRegistSucess;
                    }
                    else
                    {
                        return AlterRegistResult.UpdateRegistFail;
                    }
                }


            }
            else
            {
                //報名截止日之前-刪除
                //報名截止日之後-狀態改取消
                //取消報名截止日之後-不可以取消

                ACMS.DAO.ActivityRegistDAO myActivityRegistDAO = new ACMS.DAO.ActivityRegistDAO();

                if (Convert.ToDateTime(regist_deadline) > DateTime.Today)
                {
                    //報名截止日之前-刪除
                    if (myActivityRegistDAO.DeleteRegist(activity_id, emp_id, "1",webPath ) > 0)
                    {
                        //andy-取消報名寄信
                        clsMyObj.CancelRegist( activity_id.ToString(),  emp_id, clsAuth.ID,webPath );

                        return AlterRegistResult.CancelRegistSucess;
                    }
                    else
                    {
                        return AlterRegistResult.CancelRegistFail;
                    }
                }
                else if (Convert.ToDateTime(regist_deadline) <= DateTime.Today)
                {
                    //報名截止日之後-狀態改取消
                    if (myActivityRegistDAO.CancelRegist(activity_id, emp_id, "1",webPath) > 0)
                    {
                        //andy-取消報名寄信
                        clsMyObj.CancelRegist(activity_id.ToString(), emp_id, clsAuth.ID,webPath);

                        return AlterRegistResult.CancelRegistSucess;
                    }
                    else
                    {
                        return AlterRegistResult.CancelRegistFail;
                    }
                }
                else if (Convert.ToDateTime(cancelregist_deadline) <= DateTime.Today)
                {
                    //取消報名截止日之後-不可以取消
                    return AlterRegistResult.CancelRegistFail_DayOver;
                }

                return AlterRegistResult.CancelRegistFail;

            }


        }
    }

    //團隊活動報名或取消報名
    public AlterRegistResult AlterRegist_Team(ActivityRegistVO myActivityRegistVO, List<CustomFieldValueVO> myCustomFieldValueVOList, List<ActivityTeamMemberVO> myActivityTeamMemberVOList, AlterRegistType myAlterRegistType, Guid activity_id, string emp_id, string regist_deadline, string cancelregist_deadline ,string webPath,string path)
    {
        lock (this)
        {
            if (myAlterRegistType == AlterRegistType.RegistInsert || myAlterRegistType == AlterRegistType.RegistUpdate)
            {
                string strEmp_id = "";
                foreach (ACMS.VO.ActivityTeamMemberVO myActivityTeamMemberVO in myActivityTeamMemberVOList)
                {
                    strEmp_id += string.Format("{0},", myActivityTeamMemberVO.emp_id);
                }

                if (strEmp_id.EndsWith(","))
                {
                    strEmp_id = strEmp_id.Substring(0, strEmp_id.Length - 1);
                }

                ACMS.DAO.ActivityRegistDAO myActivityRegistDAO = new ACMS.DAO.ActivityRegistDAO();

                if (myAlterRegistType == AlterRegistType.RegistInsert)
                {
                    //先Insert報名資訊看是否成功
                    int intSaveResult = myActivityRegistDAO.UpdateActivityRegist(myActivityRegistVO, myCustomFieldValueVOList, myActivityTeamMemberVOList, "insert", "2",webPath,path);

                    if (intSaveResult == 1)
                    {
                        //andy-報名成功寄信
                        clsMyObj.RegistSuccess_Team(myActivityRegistVO.activity_id.ToString(), strEmp_id, myActivityRegistVO.regist_by,webPath,path);

                        return AlterRegistResult.RegistSucess;
                    }
                    else
                    {
                        //若失敗可能是重複報名或額滿

                        //是否重複報名

                        int RegistCount = myActivityRegistDAO.IsPersonRegisted(myActivityRegistVO.activity_id, strEmp_id, myActivityRegistVO.regist_by, "2");

                        if (RegistCount > 0)
                        {
                            //andy-報名失敗寄信
                            clsMyObj.RegistFail_Team(myActivityRegistVO.activity_id.ToString(), strEmp_id, myActivityRegistVO.regist_by,webPath);

                            return AlterRegistResult.RegistFail_Already;
                        }

                        //是否已額滿
                        int RegistableCount = myActivityRegistDAO.RegistableCount(myActivityRegistVO.activity_id);

                        if (RegistableCount <= 0)
                        {
                            //andy-報名失敗寄信
                            clsMyObj.RegistFail_Team(myActivityRegistVO.activity_id.ToString(), strEmp_id, myActivityRegistVO.regist_by,webPath);

                            return AlterRegistResult.RegistFail_Full;
                        }

                        //andy-報名失敗寄信
                        clsMyObj.RegistFail_Team(myActivityRegistVO.activity_id.ToString(), strEmp_id, myActivityRegistVO.regist_by,webPath);

                        return AlterRegistResult.RegistFail;
                    }
          
                }
                else
                {
                    int intSaveResult = myActivityRegistDAO.UpdateActivityRegist(myActivityRegistVO, myCustomFieldValueVOList, myActivityTeamMemberVOList, "update","2",webPath,path);

                    if (intSaveResult == 1)
                    {
                        return AlterRegistResult.UpdateRegistSucess;
                    }
                    else
                    {
                        //因為團長會異動報名的人，所以要檢查是否選到重複的人
                        if (myActivityRegistVO.emp_id == myActivityRegistVO.regist_by)
                        {
                            //是否重複報名

                            int RegistCount = myActivityRegistDAO.IsPersonRegisted(myActivityRegistVO.activity_id, strEmp_id, myActivityRegistVO.regist_by, "2");

                            if (RegistCount > 0)
                            {
                                //andy-報名失敗寄信
                                clsMyObj.RegistFail_Team(myActivityRegistVO.activity_id.ToString(), strEmp_id, myActivityRegistVO.regist_by,webPath);

                                return AlterRegistResult.RegistFail_Already;
                            }
                        }                   

                        return AlterRegistResult.UpdateRegistFail;
                    }
                }


            }
            else
            {
                //報名截止日之前-刪除
                //報名截止日之後-狀態改取消
                //取消報名截止日之後-不可以取消

                ACMS.DAO.ActivityRegistDAO myActivityRegistDAO = new ACMS.DAO.ActivityRegistDAO();

                if (Convert.ToDateTime(regist_deadline) > DateTime.Today)
                {
                    //取消報名截止日之前-刪除
                    if (myActivityRegistDAO.DeleteRegist(activity_id, emp_id,"2",webPath ) > 0)
                    {
                        //寄信
                       // clsMyObj.RegistSuccess_Team(activity_id.ToString (), emp_id, "", webPath);

                        return AlterRegistResult.CancelRegistSucess;
                    }
                    else
                    {
                        return AlterRegistResult.CancelRegistFail;
                    }
                }
                else if (Convert.ToDateTime(regist_deadline) <= DateTime.Today)
                {
                    //取消報名截止日之後-狀態改取消
                    if (myActivityRegistDAO.CancelRegist(activity_id, emp_id,"2",webPath) > 0)
                    {
                        //寄信
                        //clsMyObj.RegistSuccess_Team(activity_id.ToString(), emp_id,"", webPath,path);
                       
                        return AlterRegistResult.CancelRegistSucess;
                    }
                    else
                    {
                        return AlterRegistResult.CancelRegistFail;
                    }
                }
                else if (Convert.ToDateTime(cancelregist_deadline) <= DateTime.Today)
                {
                    //取消報名截止日之後-不可以取消
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