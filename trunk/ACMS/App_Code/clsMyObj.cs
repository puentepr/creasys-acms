using System;
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
            myDataTable.Columns.Add("limit_count11");
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
                        DR["registed_count"] = string.Format("正({0})+備({1})", limit_count, register_count - limit_count);
                    }
                    else
                    {
                        if (limit_count == 999999)
                        {
                            DR["registed_count"] = register_count;
                            DR["limit_count11"] = "無上限";
                        }
                        else
                        {
                            DR["limit_count11"] = limit_count;
                            DR["registed_count"] = string.Format("正({0})+備({1})", register_count, 0);
                        }
                    }

                }

                if (IsShowregisted_count == true)
                {
                    //剩餘名額
                    if (limit_count == 999999)
                    {
                        DR["registable_count"] = "";
                    }
                    else
                    {
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

    //個人報名成功寄信-update
    public static void RegistSuccessUpdate(string activity_id, string emp_id, string regist_by, string webPath, string path)
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
        ACMS.BO.ActivityRegistBO regBO = new ACMS.BO.ActivityRegistBO();
        ACMS.VO.ActivityRegistVO regVO = regBO.SelectActivityRegistByPK(id, emp_id);

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
        List<ACMS.VO.UpFileVO> listUpfileVo = uDAO.SELECT(path + "\\" + activity_id);
        Attachment data;
        foreach (ACMS.VO.UpFileVO UFvo in listUpfileVo)
        {
            data = new Attachment(UFvo.path);


            mail.Attachments.Add(data);

        }

        mail.Subject = vo.activity_name + "：個人報名成功通知";
        //寄件者
        mail.From = new System.Net.Mail.MailAddress(System.Configuration.ConfigurationManager.AppSettings["SMTPFrom"], "報名系統通知");
        mail.IsBodyHtml = true;

        ACMS.DAO.CustomFieldValueDAO myCustFieldValueDAO = new ACMS.DAO.CustomFieldValueDAO();
        List<ACMS.VO.CustomFieldValueVO> myCustomFieldValueVOList = myCustFieldValueDAO.SelectCustomFieldValue(id, emp_id);
        ACMS.BO.CustomFieldItemBO myCustFieldItemBO = new ACMS.BO.CustomFieldItemBO();
        List<ACMS.VO.CustomFieldItemVO> custFieldItemList;
        string custFieldSt = "";
        decimal ttl = 0;
        string[] FieldIDs;
        #region "自訂欄位"

        foreach (CustomFieldValueVO custFieldVO in myCustomFieldValueVOList)
        {
            if (custFieldVO.field_control.ToLower() == "textbox")
            {
                custFieldSt += "　　" + "<font color='Blue'><b>．" + custFieldVO.field_name + "：</b></font><br/>　　　" + "<font color='black'>" + custFieldVO.field_value + "</font><br/>";
            }
            if (custFieldVO.field_control.ToLower() == "textboxlist")
            {
                ttl = 0;
                custFieldSt += "　　" + "<font color='Blue'><b>．" + custFieldVO.field_name + "：</b></font><br/>";

                FieldIDs = custFieldVO.field_value.Split(',');
                custFieldItemList = myCustFieldItemBO.SelectByField_id(custFieldVO.field_id);
                foreach (string fieldID in FieldIDs)
                {
                    foreach (CustomFieldItemVO custFieldItem in custFieldItemList)
                        if (int.Parse(fieldID) == custFieldItem.field_item_id)
                        {
                            custFieldSt += "　　　" + "<font color='black'>" + custFieldItem.field_item_name + "　$" + custFieldItem.field_item_text + "</font><br/>";
                            ttl += decimal.Parse(custFieldItem.field_item_text);
                        }
                }

                custFieldSt += "　　　" + "<font color='Red'><b>合計：</b>　$" + ttl.ToString() + "</font><br/>";

            }
            if (custFieldVO.field_control.ToLower() == "checkboxlist")
            {


                custFieldSt += "　　" + "<font color='Blue'><b>．複選/</b>" + custFieldVO.field_name + "：</font><br/>";

                FieldIDs = custFieldVO.field_value.Split('、');
                custFieldItemList = myCustFieldItemBO.SelectByField_id(custFieldVO.field_id);
                foreach (string fieldID in FieldIDs)
                {
                    foreach (CustomFieldItemVO custFieldItem in custFieldItemList)
                        if (int.Parse(fieldID) == custFieldItem.field_item_id)
                        {
                            custFieldSt += "　　　" + "<font color='black'>" + custFieldItem.field_item_name + custFieldItem.field_item_text + "</font>,";

                        }
                }
                custFieldSt += "<br/>";

            }
            if (custFieldVO.field_control.ToLower() == "radiobuttonlist")
            {
                custFieldSt += "　　" + "<font color='Blue'><b>．單選/</b>" + custFieldVO.field_name + "：</font><br/>";

                FieldIDs = custFieldVO.field_value.Split(',');
                custFieldItemList = myCustFieldItemBO.SelectByField_id(custFieldVO.field_id);
                foreach (string fieldID in FieldIDs)
                {
                    foreach (CustomFieldItemVO custFieldItem in custFieldItemList)
                        if (int.Parse(fieldID) == custFieldItem.field_item_id)
                        {
                            custFieldSt += "　　　" + "<font color='black'>" + custFieldItem.field_item_name + custFieldItem.field_item_text + "</font>";

                        }
                }
                custFieldSt += "<br/>";
            }


        }
        #endregion

        ACMS.VO.EmployeeVO regByEmpVO = empDAO.getEmployee(regist_by);
        empVO = empDAO.getEmployee(emp_id);
        if (custFieldSt != "")//有自訂欄位
        {
            mail.Body = "<table border='1' width='400px'><tr><td style='background:#548DD4;Color:White' align='center'  ><b>個人報名成功通知 (資料已重新更新)</b></td> </tr><tr><td>"
                + "　　" + "<font color='Blue'><b>．姓名：</b></font><font color='black'>" + empVO.NATIVE_NAME + "</font><br/>"
                + "　　" + "<font color='Blue'><b>．工號：</b></font><font color='black'>" + empVO.WORK_ID + "</font><br/>"
                + "　　" + "<font color='Blue'><b>．活動名稱：</b></font><font color='black'>" + vo.activity_name + "</font><br/>"
                + "　　" + "<font color='Blue'><b>．報名日期：</b></font><font color='black'>" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "</font><br/>";

            //   + "　　" + "<font color='Blue'><b>．工號：</b></font>" + empVO.WORK_ID + "<br/>";
            if (vo.is_showperson_fix2.ToString().ToUpper() == "Y")
            {
                mail.Body += "　　" + "<font color='Blue'><b>．攜眷人數：</b></font><font color='black'>" + regVO.ext_people.ToString() + "<br/>";
            }

            mail.Body += "　　" + "<font color='Blue'><b>" + @"------------------------------------------------------</b></font><br/><br/>";

            mail.Body += "　　" + "<font color='Blue'><b>以下為此次您報名登入資料，敬請確認：</b></font><br/><br/>";

            mail.Body += "" + custFieldSt;
            //如果是他人代理報名,則顯示以下文字
            if (regist_by != emp_id)
            {
                mail.Body += "　　" + "<font color='Blue'><b>．此活動由</b></font><font color='Black'><u>" + regByEmpVO.NATIVE_NAME + "</u></font><font color='Blue'><b>代理您完成報名</b></font><br/>";
            }
            mail.Body += "　　" + "<font color='Blue'><b>．編號：</b></font><Font color='Red'>" + regBO.getSNByActivity(id, emp_id) + "</font></td></tr>" + "<tr><td align='center'> <a href='" + webPath + "?Type=1&ActID="
                + HttpUtility.UrlEncode(activity_id) + "&RegID=" + HttpUtility.UrlEncode(regist_by)
                + "'>" + vo.activity_name + "：報名成功連結</a></td></tr>" + "<tr><td style='background:#548DD4' align='center'  >   &nbsp; </td> </tr></table>";
        }
        else
        {
            mail.Body = "<table border='1' width='400px'><tr><td style='background:#548DD4;Color:White' align='center'  ><b>個人報名成功通知</b></td> </tr><tr><td>"
                + "　　" + "<font color='Blue'><b>．姓名：</b></font><font color='black'>" + empVO.NATIVE_NAME + "</font><br/>"
                + "　　" + "<font color='Blue'><b>．工號：</b></font><font color='black'>" + empVO.WORK_ID + "</font><br/>"
                + "　　" + "<font color='Blue'><b>．活動名稱：</b></font><font color='black'>" + vo.activity_name + "</font><br/>"
                + "　　" + "<font color='Blue'><b>．報名日期：</b></font><font color='black'>" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "</font><br/>";
            //   + "　　" + "<font color='Blue'><b>．工號：</b></font>" + empVO.WORK_ID + "<br/>";
            if (vo.is_showperson_fix2.ToString().ToUpper() == "Y")
            {
                mail.Body += "　　" + "<font color='Blue'><b>．攜眷人數：</b></font><font color='black'>" + regVO.ext_people.ToString() + "</font><br/>";
            }

            mail.Body += "　　" + "<font color='Blue'><b>" + @"--------------------------------------------</b></font><br/><br/>";

            mail.Body += "　　" + "<font color='Blue'><b>以下為此次您報名登入資料，敬請確認：</b></font><br/><br/>";


            //如果是他人代理報名,則顯示以下文字
            if (regist_by != emp_id)
            {
                mail.Body += "　　" + "<font color='Blue'><b>．此活動由</b></font><font color='Black'><u>" + regByEmpVO.NATIVE_NAME + "</u></font><font color='Blue'><b>代理您完成報名</b></font><br/>";
            }
            mail.Body += "　　" + "<font color='Blue'><b>．編號：</b></font><Font color='Red'>" + regBO.getSNByActivity(id, emp_id) + "</font></td></tr>" + "<tr><td align='center'> <a href='" + webPath + "?Type=1&ActID="
                + HttpUtility.UrlEncode(activity_id) + "&RegID=" + HttpUtility.UrlEncode(regist_by)
                + "'>" + vo.activity_name + "：報名成功連結</a></td></tr>" + "<tr><td style='background:#548DD4' align='center'  >   &nbsp; </td> </tr></table>";

        }
        SmtpClient smtp = new SmtpClient(System.Configuration.ConfigurationManager.AppSettings["SMTPServer"]);


        try
        {
            smtp.Send(mail);

        }
        catch (Exception ex)
        {
            LogMsg.Log(ex.Message, 5, false);

        }


    }

    //個人報名成功寄信
    public static void RegistSuccess(string activity_id, string emp_id, string regist_by, string webPath, string path)
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
        ACMS.BO.ActivityRegistBO regBO = new ACMS.BO.ActivityRegistBO();
        ACMS.VO.ActivityRegistVO regVO = regBO.SelectActivityRegistByPK(id, emp_id);

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
        List<ACMS.VO.UpFileVO> listUpfileVo = uDAO.SELECT(path + "\\" + activity_id);
        Attachment data;
        foreach (ACMS.VO.UpFileVO UFvo in listUpfileVo)
        {
            data = new Attachment(UFvo.path);


            mail.Attachments.Add(data);

        }

        mail.Subject = vo.activity_name + "：個人報名成功通知";
        //寄件者
        mail.From = new System.Net.Mail.MailAddress(System.Configuration.ConfigurationManager.AppSettings["SMTPFrom"], "報名系統通知");
        mail.IsBodyHtml = true;

        ACMS.DAO.CustomFieldValueDAO myCustFieldValueDAO = new ACMS.DAO.CustomFieldValueDAO();
        List<ACMS.VO.CustomFieldValueVO> myCustomFieldValueVOList = myCustFieldValueDAO.SelectCustomFieldValue(id, emp_id);
        ACMS.BO.CustomFieldItemBO myCustFieldItemBO = new ACMS.BO.CustomFieldItemBO();
        List<ACMS.VO.CustomFieldItemVO> custFieldItemList;
        string custFieldSt = "";
        decimal ttl = 0;
        string[] FieldIDs;
        #region "自訂欄位"

        foreach (CustomFieldValueVO custFieldVO in myCustomFieldValueVOList)
        {
            if (custFieldVO.field_control.ToLower() == "textbox")
            {
                custFieldSt += "　　" + "<font color='Blue'><b>．" + custFieldVO.field_name + "：</b></font><br/>　　　" + "<font color='black'>" + custFieldVO.field_value + "</font><br/>";
            }
            if (custFieldVO.field_control.ToLower() == "textboxlist")
            {
                ttl = 0;
                custFieldSt += "　　" + "<font color='Blue'><b>．" + custFieldVO.field_name + "：</b></font><br/>";

                FieldIDs = custFieldVO.field_value.Split(',');
                custFieldItemList = myCustFieldItemBO.SelectByField_id(custFieldVO.field_id);
                foreach (string fieldID in FieldIDs)
                {
                    foreach (CustomFieldItemVO custFieldItem in custFieldItemList)
                        if (int.Parse(fieldID) == custFieldItem.field_item_id)
                        {
                            custFieldSt += "　　　" + "<font color='black'>" + custFieldItem.field_item_name + "　$" + custFieldItem.field_item_text + "</font><br/>";
                            ttl += decimal.Parse(custFieldItem.field_item_text);
                        }
                }

                custFieldSt += "　　　" + "<font color='Red'><b>合計：</b>　$" + ttl.ToString() + "</font><br/>";

            }
            if (custFieldVO.field_control.ToLower() == "checkboxlist")
            {


                custFieldSt += "　　" + "<font color='Blue'><b>．複選/</b>" + custFieldVO.field_name + "：</font><br/>";

                FieldIDs = custFieldVO.field_value.Split('、');
                custFieldItemList = myCustFieldItemBO.SelectByField_id(custFieldVO.field_id);
                foreach (string fieldID in FieldIDs)
                {
                    foreach (CustomFieldItemVO custFieldItem in custFieldItemList)
                        if (int.Parse(fieldID) == custFieldItem.field_item_id)
                        {
                            custFieldSt += "　　　" + "<font color='black'>" + custFieldItem.field_item_name + custFieldItem.field_item_text + "</font>,";

                        }
                }
                custFieldSt += "<br/>";

            }
            if (custFieldVO.field_control.ToLower() == "radiobuttonlist")
            {
                custFieldSt += "　　" + "<font color='Blue'><b>．單選/</b>" + custFieldVO.field_name + "：</font><br/>";

                FieldIDs = custFieldVO.field_value.Split(',');
                custFieldItemList = myCustFieldItemBO.SelectByField_id(custFieldVO.field_id);
                foreach (string fieldID in FieldIDs)
                {
                    foreach (CustomFieldItemVO custFieldItem in custFieldItemList)
                        if (int.Parse(fieldID) == custFieldItem.field_item_id)
                        {
                            custFieldSt += "　　　" + "<font color='black'>" + custFieldItem.field_item_name + custFieldItem.field_item_text + "</font>";

                        }
                }
                custFieldSt += "<br/>";
            }


        }
        #endregion
        ACMS.VO.EmployeeVO regByEmpVO = empDAO.getEmployee(regist_by);
        empVO = empDAO.getEmployee(emp_id);
        if (custFieldSt != "")//有自訂欄位
        {
            mail.Body = "<table border='1' width='400px'><tr><td style='background:#548DD4;Color:White' align='center'  ><b>個人報名成功通知</b></td> </tr><tr><td>"
                + "　　" + "<font color='Blue'><b>．姓名：</b></font><font color='black'>" + empVO.NATIVE_NAME + "</font><br/>"
                + "　　" + "<font color='Blue'><b>．工號：</b></font><font color='black'>" + empVO.WORK_ID + "</font><br/>"
                + "　　" + "<font color='Blue'><b>．活動名稱：</b></font><font color='black'>" + vo.activity_name + "</font><br/>"
                + "　　" + "<font color='Blue'><b>．報名日期：</b></font><font color='black'>" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "</font><br/>";

            //   + "　　" + "<font color='Blue'><b>．工號：</b></font>" + empVO.WORK_ID + "<br/>";
            if (vo.is_showperson_fix2.ToString().ToUpper() == "Y")
            {
                mail.Body += "　　" + "<font color='Blue'><b>．攜眷人數：</b></font><font color='black'>" + regVO.ext_people.ToString() + "<br/>";
            }

            mail.Body += "　　" + "<font color='Blue'><b>" + @"------------------------------------------------------</b></font><br/><br/>";

            mail.Body += "　　" + "<font color='Blue'><b>以下為此次您報名登入資料，敬請確認：</b></font><br/><br/>";

            mail.Body += "" + custFieldSt;
            //如果是他人代理報名,則顯示以下文字
            if (regist_by != emp_id)
            {
                mail.Body += "　　" + "<font color='Blue'><b>．此活動由</b></font><font color='Black'><u>" + regByEmpVO.NATIVE_NAME + "</u></font><font color='Blue'><b>代理您完成報名</b></font><br/>";
            }
            mail.Body += "　　" + "<font color='Blue'><b>．編號：</b></font><Font color='Red'>" + regBO.getSNByActivity(id, emp_id) + "</font></td></tr>" + "<tr><td align='center'> <a href='" + webPath + "?Type=1&ActID="
                + HttpUtility.UrlEncode(activity_id) + "&RegID=" + HttpUtility.UrlEncode(regist_by)
                + "'>" + vo.activity_name + "：報名成功連結</a></td></tr>" + "<tr><td style='background:#548DD4' align='center'  >   &nbsp; </td> </tr></table>";
        }
        else
        {
            mail.Body = "<table border='1' width='400px'><tr><td style='background:#548DD4;Color:White' align='center'  ><b>個人報名成功通知</b></td> </tr><tr><td>"
                + "　　" + "<font color='Blue'><b>．姓名：</b></font><font color='black'>" + empVO.NATIVE_NAME + "</font><br/>"
                + "　　" + "<font color='Blue'><b>．工號：</b></font><font color='black'>" + empVO.WORK_ID + "</font><br/>"
                + "　　" + "<font color='Blue'><b>．活動名稱：</b></font><font color='black'>" + vo.activity_name + "</font><br/>"
                + "　　" + "<font color='Blue'><b>．報名日期：</b></font><font color='black'>" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "</font><br/>";
            //   + "　　" + "<font color='Blue'><b>．工號：</b></font>" + empVO.WORK_ID + "<br/>";
            if (vo.is_showperson_fix2.ToString().ToUpper() == "Y")
            {
                mail.Body += "　　" + "<font color='Blue'><b>．攜眷人數：</b></font><font color='black'>" + regVO.ext_people.ToString() + "</font><br/>";
            }

            mail.Body += "　　" + "<font color='Blue'><b>" + @"--------------------------------------------</b></font><br/><br/>";

            mail.Body += "　　" + "<font color='Blue'><b>以下為此次您報名登入資料，敬請確認：</b></font><br/><br/>";


            //如果是他人代理報名,則顯示以下文字
            if (regist_by != emp_id)
            {
                mail.Body += "　　" + "<font color='Blue'><b>．此活動由</b></font><font color='Black'><u>" + regByEmpVO.NATIVE_NAME + "</u></font><font color='Blue'><b>代理您完成報名</b></font><br/>";
            }
            mail.Body += "　　" + "<font color='Blue'><b>．編號：</b></font><Font color='Red'>" + regBO.getSNByActivity(id, emp_id) + "</font></td></tr>" + "<tr><td align='center'> <a href='" + webPath + "?Type=1&ActID="
                + HttpUtility.UrlEncode(activity_id) + "&RegID=" + HttpUtility.UrlEncode(regist_by)
                + "'>" + vo.activity_name + "：報名成功連結</a></td></tr>" + "<tr><td style='background:#548DD4' align='center'  >   &nbsp; </td> </tr></table>";

        }
        SmtpClient smtp = new SmtpClient(System.Configuration.ConfigurationManager.AppSettings["SMTPServer"]);


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
        mail.From = new System.Net.Mail.MailAddress(System.Configuration.ConfigurationManager.AppSettings["SMTPFrom"], "報名系統通知");
        mail.IsBodyHtml = true;
        mail.Body = "<a href='" + webPath + "?Type=2&ActID="
            + HttpUtility.UrlEncode(activity_id) + "&RegID=" + HttpUtility.UrlEncode(regist_by )
            + "'>" + vo.activity_name + ":個人報名失敗通知</a>";


       SmtpClient smtp = new SmtpClient(System.Configuration.ConfigurationManager.AppSettings["SMTPServer"]);

       
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
        EmployeeVO empCancelVO = new EmployeeVO();
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
        empVO = empDAO.getEmployee(emp_id);
        empCancelVO = empDAO.getEmployee(cancel_by);
        mail.Subject = vo.activity_name + "：取消報名通知";
        //寄件者
        mail.From = new System.Net.Mail.MailAddress(System.Configuration.ConfigurationManager.AppSettings["SMTPFrom"], "報名系統通知");
        mail.IsBodyHtml = true;
        mail.Body = "<table border='1'><tr><td style='background:#548DD4;Color:White' align='center'  ><b>取消個人報名通知</b></td> </tr>"
            + "<tr><td><br/><br/>" + "　　" + "<b><font color='Blue'>．姓名：</font></b>" + empVO.NATIVE_NAME
            + "<br/>" + "　　" + "<b><font color='Blue'>．工號：</font></b>" + empVO.WORK_ID
            + "<br/>" + "　　" + "<b><font color='Blue'>．活動名稱：</font></b>" + vo.activity_name
            + "<br/>" + "　　" + "<b><font color='Blue'>．取消日期：</font></b>" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") ;
            if(cancel_by!=emp_id)
            {
                mail.Body += "<br/>"+"　　" + "<b><font color='Blue'>．此活動由</font></b><font color='black'><u>" + empCancelVO.NATIVE_NAME + "</u></font><font color='Blue'><b>代理您完成取消</b></font>";
                
            }
            mail.Body += "<br/><br/><br/></td></tr><tr><td align='center'><a href='" + webPath + "?Type=1&ActID="
            + HttpUtility.UrlEncode(activity_id) + "&RegID=" + HttpUtility.UrlEncode(cancel_by)
            + "'>" + vo.activity_name + "：報名連結</a><br/></td></tr>"+"<tr><td style='background:#548DD4' align='center'  >  &nbsp;  </td> </tr></table>";



       SmtpClient smtp = new SmtpClient(System.Configuration.ConfigurationManager.AppSettings["SMTPServer"]);

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
        #region "自訂欄位"

        foreach (CustomFieldValueVO custFieldVO in myCustomFieldValueVOList)
        {
            if (custFieldVO.field_control.ToLower() == "textbox")
            {
                custFieldSt += "　　" + "<font color='Blue'><b>．" + custFieldVO.field_name + "：</b></font>" + custFieldVO.field_value + "<br/>";
            }
            if (custFieldVO.field_control.ToLower() == "textboxlist")
            {
                ttl = 0;
                custFieldSt += "　　" + "<font color='Blue'><b>．" + custFieldVO.field_name + "：</b></font><br/>";

                FieldIDs = custFieldVO.field_value.Split(',');
                custFieldItemList = myCustFieldItemBO.SelectByField_id(custFieldVO.field_id);
                foreach (string fieldID in FieldIDs)
                {
                    foreach (CustomFieldItemVO custFieldItem in custFieldItemList)
                        if (int.Parse(fieldID) == custFieldItem.field_item_id)
                        {
                            custFieldSt += "　　" + custFieldItem.field_item_name + "：$" + custFieldItem.field_item_text + "<br/>";
                            ttl += decimal.Parse(custFieldItem.field_item_text);
                        }
                }

                custFieldSt += "　　" + "<font color='Red'><b>合計：</b>$ " + ttl.ToString() + "</font><br/>";

            }
            if (custFieldVO.field_control.ToLower() == "checkboxlist")
            {


                custFieldSt += "　　" + "<font color='Blue'><b>．複選/</b>" + custFieldVO.field_name + "：</font><br/>";

                FieldIDs = custFieldVO.field_value.Split(',');
                custFieldItemList = myCustFieldItemBO.SelectByField_id(custFieldVO.field_id);
                foreach (string fieldID in FieldIDs)
                {
                    foreach (CustomFieldItemVO custFieldItem in custFieldItemList)
                        if (int.Parse(fieldID) == custFieldItem.field_item_id)
                        {
                            custFieldSt += "　　" + custFieldItem.field_item_name + custFieldItem.field_item_text + ",";

                        }
                }
                custFieldSt += "<br/>";

            }
            if (custFieldVO.field_control.ToLower() == "radiobuttonlist")
            {
                custFieldSt += "　　" + "<font color='Blue'><b>．單選/</b>" + custFieldVO.field_name + "：</font><br/>";

                FieldIDs = custFieldVO.field_value.Split(',');
                custFieldItemList = myCustFieldItemBO.SelectByField_id(custFieldVO.field_id);
                foreach (string fieldID in FieldIDs)
                {
                    foreach (CustomFieldItemVO custFieldItem in custFieldItemList)
                        if (int.Parse(fieldID) == custFieldItem.field_item_id)
                        {
                            custFieldSt += "　　" + custFieldItem.field_item_name + custFieldItem.field_item_text + ",";

                        }
                }
                custFieldSt += "<br/>";
            }


        }
        //foreach (CustomFieldValueVO custFieldVO in myCustomFieldValueVOList)
        //{
        //    if (custFieldVO.field_control.ToLower() == "textbox")
        //    {
        //        custFieldSt +=   "　　"+"<font color='Blue'><b>" + custFieldVO.field_name + ":</b></font>" + custFieldVO.field_value + "<br/>";
        //    }
        //    if (custFieldVO.field_control.ToLower() == "textboxlist")
        //    {
        //        ttl = 0;
        //        custFieldSt += "　　" + "<font color='Blue'><b>" + custFieldVO.field_name + ":</b></font><br/>";

        //        FieldIDs = custFieldVO.field_value.Split(',');
        //        custFieldItemList = myCustFieldItemBO.SelectByField_id(custFieldVO.field_id);
        //        foreach (string fieldID in FieldIDs)
        //        {
        //            foreach (CustomFieldItemVO custFieldItem in custFieldItemList)
        //                if (int.Parse(fieldID) == custFieldItem.field_item_id)
        //                {
        //                    custFieldSt += "　　" + custFieldItem.field_item_name + ":" + custFieldItem.field_item_text + "<br/>";
        //                    ttl += decimal.Parse(custFieldItem.field_item_text);
        //                }
        //        }

        //        custFieldSt += "　　" + "<font color='Red'><b>合計: " + ttl.ToString() + "</b></font><br/>";

        //    }
        //    if (custFieldVO.field_control.ToLower() == "checkboxlist")
        //    {


        //        custFieldSt += "　　" + " <font color='Blue'><b>．複選/" + custFieldVO.field_name + ":</b></font><br/>";

        //        FieldIDs = custFieldVO.field_value.Split(',');
        //        custFieldItemList = myCustFieldItemBO.SelectByField_id(custFieldVO.field_id);
        //        foreach (string fieldID in FieldIDs)
        //        {
        //            foreach (CustomFieldItemVO custFieldItem in custFieldItemList)
        //                if (int.Parse(fieldID) == custFieldItem.field_item_id)
        //                {
        //                    custFieldSt += "　　" + custFieldItem.field_item_name + custFieldItem.field_item_text + ",";

        //                }
        //        }
        //        custFieldSt += "<br/>";

        //    }
        //    if (custFieldVO.field_control.ToLower() == "radiobuttonlist")
        //    {
        //        custFieldSt += "　　" + " <font color='Blue'><b>．單選/" + custFieldVO.field_name + ":</b></font><br/>";

        //        FieldIDs = custFieldVO.field_value.Split(',');
        //        custFieldItemList = myCustFieldItemBO.SelectByField_id(custFieldVO.field_id);
        //        foreach (string fieldID in FieldIDs)
        //        {
        //            foreach (CustomFieldItemVO custFieldItem in custFieldItemList)
        //                if (int.Parse(fieldID) == custFieldItem.field_item_id)
        //                {
        //                    custFieldSt +=  "　　"+custFieldItem.field_item_name + custFieldItem.field_item_text + ",";

        //                }
        //        }
        //        custFieldSt += "<br/>";
        //    }


        //}
        #endregion
        mail.Subject = vo.activity_name + "：團隊報名成功通知";
        //寄件者
        mail.From = new System.Net.Mail.MailAddress(System.Configuration.ConfigurationManager.AppSettings["SMTPFrom"], "報名系統通知");
        mail.IsBodyHtml = true;

        ACMS.VO.EmployeeVO regByEmpVO = empDAO.getEmployee(regist_by);

        //string empList = ""; 
        //empList += "<table><b><tr><td>　　工號</td> <td>姓名</td></tr></b>";
        //foreach (string emp in emps)
        //{
        //    empList += "<tr><td>";
        //    empVO = empDAO.getEmployee(emp);
        //    empList += "　　" + empVO.WORK_ID + "</td><td>";
        //    empList += empVO.NATIVE_NAME + "</td></tr>";
        //}
        //empList +="</tr></table>";
        string empList = "";
        foreach (string emp in emps)
        {
            empVO = empDAO.getEmployee(emp);
            empList += empVO.NATIVE_NAME + "、";
        }
        empList = empList.TrimEnd('、');
       // empList += "</tr></table>";
        //+ "姓名:" + empVO.NATIVE_NAME + "<br/>"
        //       + "工號:" + empVO.WORK_ID + "<br/>"

       
        //if (custFieldSt != "")//有自訂欄位
        //{
        //    mail.Body = "<table border='1'<tr><td style='background:#548DD4;Color:White' align='center'  ><b>團隊報名成功通知</b></td> </tr><tr><td><b>親愛的同仁，恭喜您已完成此活動報名。</b></td> </tr><tr><td><a href='" + webPath + "?Type=2&ActID="
        //        + HttpUtility.UrlEncode(activity_id) + "&RegID=" + HttpUtility.UrlEncode(regist_by)
        //        + "'>" + vo.activity_name + ":團隊報名成功通知</a><br/>"

        //      + "　　" + "<font color='Blue'><b>.活動名稱:</b></font>" + vo.activity_name + "<br/>";
        //      if (regVO.team_name!="")
        //      {
        //          mail.Body += "　　" + "<font color='Blue'><b>." + regVO.team_name + "</b></font><br/>";
        //      }
        //      mail.Body += "　　" + "<font color='Blue'><b>.報名日期:</b></font>" + DateTime.Today.ToString("yyyy/MM/dd") + "<br/>"
        //        +  empList
        //       // + "　　" + "<font color='Blue'><b>.攜眷人數:</b></font>" + regVO.ext_people.ToString() + "<br/>"
        //        + "" + custFieldSt
        //         + "　　" + "<font color='Blue'><b>.報名人姓名:</b></font>" + regByEmpVO.NATIVE_NAME
        //         + "<br/>" + "　　<font color='Blue'><b>.編號:</b></font><font color='Red'>" + regBO.getSNByActivity(id, regist_by) + "</font></td></tr><tr><td style='background:#548DD4' align='center'  >  &nbsp;  </td> </tr></table>";
        //}
        //else
        //{
        //    mail.Body = "<table border='1'<tr><td style='background:#548DD4;Color:White' align='center'  ><b>團隊報名成功通知</b></td> </tr><tr><td><b>親愛的同仁，恭喜您已完成此活動報名。</b></td> </tr><tr><td><a href='" + webPath + "?Type=2&ActID="
        //        + HttpUtility.UrlEncode(activity_id) + "&RegID=" + HttpUtility.UrlEncode(regist_by)
        //        + "'>" + vo.activity_name + ":團隊報名成功通知</a><br/>"
        //         + "　　" + "<font color='Blue'><b>.活動名稱:</b></font>" + vo.activity_name + "<br/>";
        //    if (regVO.team_name != "")
        //    {
        //        mail.Body += "　　" + "<font color='Blue'><b>." + regVO.team_name + "</b></font><br/>";
        //    }
        //    mail.Body += "　　" + "<font color='Blue'><b>.報名日期:</b></font>" + DateTime.Today.ToString("yyyy/MM/dd") + "<br/>"
        //        + empList
        //      //  + "　　" + "<font color='Blue'><b>.攜眷人數:</b></font>" + regVO.ext_people.ToString() + "<br/>"
        //        + "　　" + "<font color='Blue'><b>.報名人姓名:</b></font>" + regByEmpVO.NATIVE_NAME
        //    + "<br/>" + "　　<font color='Blue'><b>.編號:</b></font><font color='Red'>" + regBO.getSNByActivity(id, regist_by) + "</font></td></tr><tr><td style='background:#548DD4' align='center'  >  &nbsp;  </td> </tr></table>";

        //}

        mail.Body = "<table border='1' width='400px'><tr><td style='background:#548DD4;Color:White' align='center'  ><b>團隊報名成功通知</b></td></tr><tr><td><br/><br/>"
                    + "　　" + "<font color='Blue'><b>．活動名稱：</b></font><font color='black'>" + vo.activity_name + "</font><br/>"
                    + "　　" + "<font color='Blue'><b>．報名日期：</b></font><font color='black'>" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "</font><br/>";

        if (regVO.team_name != "")
        {
            mail.Body += "　　" + "<font color='Blue'><b>．團隊名稱：</b></font><font color='black'>" + regVO.team_name + "</font><br/>";
        }
              mail.Body += "　　" + "<font color='Blue'><b>．團隊隊友：</b></font><font color='black'>" + empList + "</font><br/>" 
            //  + "　　" + "<font color='Blue'><b>.攜眷人數:</b></font>" + regVO.ext_people.ToString() + "<br/>"
           // + "　　" + "<font color='Blue'><b>．報名人姓名：</b></font>" + regByEmpVO.NATIVE_NAME
                   + "　　"+"<font color='Blue'><b>．報名狀態：</b></font><font color='Red'>" + regBO.getSNByActivity(id, regist_by).Replace(":", "第") + "隊</font><br/><br/><br/></td></tr>" + "<tr><td align='center'><a href='" + webPath + "?Type=2&ActID="
            + HttpUtility.UrlEncode(activity_id) + "&RegID=" + HttpUtility.UrlEncode(regist_by)
            + "'>" + vo.activity_name + "：團隊報名成功連結</a><br/></td></tr>" + "<tr><td style='background:#548DD4' align='center'  >  &nbsp;  </td> </tr></table>";

        SmtpClient smtp = new SmtpClient(System.Configuration.ConfigurationManager.AppSettings["SMTPServer"]);
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
        mail.From = new System.Net.Mail.MailAddress(System.Configuration.ConfigurationManager.AppSettings["SMTPFrom"], "報名系統通知");
        mail.IsBodyHtml = true;
        mail.Body = "<a href='" + webPath + "?Type=2&ActID="
            + HttpUtility.UrlEncode(activity_id) + "&RegID=" + HttpUtility.UrlEncode(regist_by)
            + "'>" + vo.activity_name + ":團隊報名失敗通知</a>";

        SmtpClient smtp = new SmtpClient(System.Configuration.ConfigurationManager.AppSettings["SMTPServer"]);
      
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
    public static void CancelRegist_Team(string activity_id, string emp_id, string cancel_by, string webPath, string bossid)
    {
        //andy 
        ACMS.VO.ActivatyVO vo = new ACMS.VO.ActivatyVO();
        ACMS.BO.ActivatyBO bo = new ACMS.BO.ActivatyBO();
        ACMS.DAO.ActivityRegistDAO regDAO = new ACMS.DAO.ActivityRegistDAO();



        string[] smtpto = System.Configuration.ConfigurationManager.AppSettings["SMTPTo"].Split(',');
        Guid id = new Guid(activity_id);
        vo = bo.SelectActivatyByActivatyID(id);
        MailMessage mail = new MailMessage();


        string[] emps = emp_id.Split(',');
        EmployeeVO empVO = new EmployeeVO();
        EmployeeVO empCancelVO = new EmployeeVO();
        EmployeeVO empBossVO = new EmployeeVO();
        EMPloyeeDAO empDAO = new EMPloyeeDAO();


        string mailtype="";
        foreach (string emp in emps)
        {
            //收件者
            mail.To.Clear();
            mailtype = System.Configuration.ConfigurationManager.AppSettings["MailType"].ToLower();
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


                if (emp != cancel_by)
                {
                    empVO = empDAO.getEmployee(emp);
                    mail.To.Add(empVO.OFFICE_MAIL);
                }
            }

        }

        string empList = "";
        empList += "";
        foreach (string emp in emps)
        {



            empVO = empDAO.getEmployee(emp);
            empList = "<font color='blue'><b>．姓名：</b></font>";
            empList += empVO.NATIVE_NAME + "<br/>";
            empList += " <font color='blue'><b>．工號：</b></font>";
            empList += empVO.WORK_ID + "<br/>";

            empCancelVO = empDAO.getEmployee(cancel_by);

            if ((string.Compare(mailtype, "online") == 0))
            {
                empBossVO = empDAO.getEmployee(bossid);
                if (empBossVO.ID != cancel_by)
                {

                    empVO = empDAO.getEmployee(empBossVO.ID);
                    mail.To.Add(empVO.OFFICE_MAIL);
                }
            }
            mail.Subject = vo.activity_name + "：團隊取消報名通知";
            //寄件者
            mail.From = new System.Net.Mail.MailAddress(System.Configuration.ConfigurationManager.AppSettings["SMTPFrom"], "報名系統通知");
            mail.IsBodyHtml = true;
            mail.Body = "<table  border='1' style='padding-left:30px' width='400px'><tr><td style='background:#548DD4;Color:White' align='center'  ><b>團隊取消報名通知</b></td> </tr>"

                + "<tr><td><br/><br/><font color='Blue'><b>．活動名稱：</b></font>" + vo.activity_name + "<br/>"
                + "<font color='Blue'><b>．取消日期：</b></font>" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "<br/>"
                + empList;
            if (cancel_by != emp)
            {
                mail.Body += "<font color='Blue'><b>．此活動由</b></font>" + empCancelVO.NATIVE_NAME + "<font color='Blue'><b>代理您完成取消</b></font>";
            }

            mail.Body += "<br/><br/><br/></td></tr>" + "<tr><td align='center'>※報名人數未低於下限，其餘隊友仍具有活動參加資格<br/><a href='" + webPath + "?Type=2&ActID="
            + HttpUtility.UrlEncode(activity_id) + "&RegID=" + HttpUtility.UrlEncode(cancel_by)
            + "'>" + vo.activity_name + "：報名連結</a><br/>" + "<tr><td style='background:#548DD4' align='center'  >  &nbsp;  </td> </tr></table>";



            SmtpClient smtp = new SmtpClient(System.Configuration.ConfigurationManager.AppSettings["SMTPServer"]);

            try
            {
                smtp.Send(mail);

            }
            catch (Exception ex)
            {
                LogMsg.Log(ex.Message, 5, false);
            }


        }


    }

    //團隊取消報名寄信已達下限
    public static void CancelRegist_TeamUnderLimit(string activity_id, string emp_id, string cancel_by, string webPath)
    {
        //andy 
        ACMS.VO.ActivatyVO vo = new ACMS.VO.ActivatyVO();
        ACMS.BO.ActivatyBO bo = new ACMS.BO.ActivatyBO();
        ACMS.DAO.ActivityRegistDAO regDAO = new ACMS.DAO.ActivityRegistDAO();



        string[] smtpto = System.Configuration.ConfigurationManager.AppSettings["SMTPTo"].Split(',');
        Guid id = new Guid(activity_id);
        vo = bo.SelectActivatyByActivatyID(id);
        MailMessage mail = new MailMessage();


        string[] emps = emp_id.Split(',');
        EmployeeVO empVO = new EmployeeVO();
        EmployeeVO empCancelVO = new EmployeeVO();
        EmployeeVO empBossVO = new EmployeeVO();
        EMPloyeeDAO empDAO = new EMPloyeeDAO();


        string mailtype = "";
        foreach (string emp in emps)
        {
            //收件者
            mail.To.Clear();
            mailtype = System.Configuration.ConfigurationManager.AppSettings["MailType"].ToLower();
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


                if (emp != cancel_by)
                {
                    empVO = empDAO.getEmployee(emp);
                    mail.To.Add(empVO.OFFICE_MAIL);
                }
            }

        }

        string empList = "";
        empList += "";
        foreach (string emp in emps)
        {



            empVO = empDAO.getEmployee(emp);
            empList = "<font color='blue'><b>．姓名：</b></font>";
            empList += empVO.NATIVE_NAME + "<br/>";
            empList += " <font color='blue'><b>．工號：</b></font>";
            empList += empVO.WORK_ID + "<br/>";

            empCancelVO = empDAO.getEmployee(cancel_by);

            //if ((string.Compare(mailtype, "online") == 0))
            //{
            //    empBossVO = empDAO.getEmployee(bossid);
            //    if (empBossVO.ID != cancel_by)
            //    {

            //        empVO = empDAO.getEmployee(empBossVO.ID);
            //        mail.To.Add(empVO.OFFICE_MAIL);
            //    }
            //}
            mail.Subject = vo.activity_name + "：團隊取消報名通知";
            //寄件者
            mail.From = new System.Net.Mail.MailAddress(System.Configuration.ConfigurationManager.AppSettings["SMTPFrom"], "報名系統通知");
            mail.IsBodyHtml = true;
            mail.Body = "<table  border='1' style='padding-left:30px'><tr><td style='background:#548DD4;Color:White' align='center'  ><b>團隊取消報名通知</b></td> </tr>"

                + "<tr><td><br/><br/><font color='Blue'><b>．活動名稱：</b></font>" + vo.activity_name + "<br/>"
                + "<font color='Blue'><b>．取消日期：</b></font>" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "<br/>"
                + empList;
            if (cancel_by != emp)
            {
                mail.Body += "<font color='Blue'><b>．此活動由</b></font>" + empCancelVO.NATIVE_NAME + "<font color='Blue'><b>代理您完成取消</b></font>";
            }

            mail.Body += "<br/><br/><br/></td></tr>" + "<tr><td align='center'><font color='red'>※團隊報名人數已低於下限，系統已取消此隊伍參加資格</font><br/><a href='" + webPath + "?Type=2&ActID="
            + HttpUtility.UrlEncode(activity_id) + "&RegID=" + HttpUtility.UrlEncode(cancel_by)
            + "'>" + vo.activity_name + "：報名連結</a><br/>" + "<tr><td style='background:#548DD4' align='center'  >  &nbsp;  </td> </tr></table>";



            SmtpClient smtp = new SmtpClient(System.Configuration.ConfigurationManager.AppSettings["SMTPServer"]);

            try
            {
                smtp.Send(mail);

            }
            catch (Exception ex)
            {
                LogMsg.Log(ex.Message, 5, false);
            }


        }

        //ACMS.VO.ActivatyVO vo = new ACMS.VO.ActivatyVO();
        //ACMS.BO.ActivatyBO bo = new ACMS.BO.ActivatyBO();
        //string[] smtpto = System.Configuration.ConfigurationManager.AppSettings["SMTPTo"].Split(',');
        //Guid id = new Guid(activity_id);
        //vo = bo.SelectActivatyByActivatyID(id);
        //MailMessage mail = new MailMessage();


        //string[] emps = emp_id.Split(',');
        //EmployeeVO empVO = new EmployeeVO();
        //EMPloyeeDAO empDAO = new EMPloyeeDAO();

        ////收件者
        //string mailtype = System.Configuration.ConfigurationManager.AppSettings["MailType"].ToLower();
        //if ((string.Compare(mailtype, "online") != 0))
        //{
        //    foreach (string st1 in smtpto)
        //    {
        //        mail.To.Add(st1);
        //    }
        //}
        //else
        //{

        //    if (string.Compare(cancel_by, "") != 0)
        //    {
        //        empVO = empDAO.getEmployee(cancel_by);
        //        mail.To.Add(empVO.OFFICE_MAIL);
        //    }



        //    foreach (string emp in emps)
        //    {
        //        if (emp != cancel_by)
        //        {
        //            empVO = empDAO.getEmployee(emp);
        //            mail.To.Add(empVO.OFFICE_MAIL);
        //        }
        //    }

        //}

        //string empList = "";
        //empList += "<table><tr><td>工號</td> <td>姓名</td></tr>";
        //foreach (string emp in emps)
        //{


        //    empList += "<tr><td>";
        //    empVO = empDAO.getEmployee(emp);
        //    empList += empVO.WORK_ID + "</td><td>";
        //    empList += empVO.NATIVE_NAME + "</td></tr>";


        //}
        //empList += "</tr></table>";


        //mail.Subject = vo.activity_name + "：團隊取消報名通知(已達每隊人數下限,已全隊取消報名)";
        ////寄件者
        //mail.From = new System.Net.Mail.MailAddress(System.Configuration.ConfigurationManager.AppSettings["SMTPFrom"], "報名系統通知");
        //mail.IsBodyHtml = true;
        //mail.Body = "<table  border='1'><tr><td style='background:#548DD4;Color:White' align='center'  ><b>團隊取消報名通知</b></td> </tr><tr><td><b>親愛的同仁，您取消活動報名。</b></td> </tr> <tr><td><a href='" + webPath + "?Type=2&ActID="
        //    + HttpUtility.UrlEncode(activity_id) + "&RegID=" + HttpUtility.UrlEncode(cancel_by)
        //    + "'>" + vo.activity_name + ":團隊取消報名通知(已達每隊人數下限,已全隊取消報名)</a><br/>"
        //    + "<font color='Blue'><b>活動名稱:</b></font>" + vo.activity_name + "<br/>"
        //    + empList + "</td></tr><tr><td style='background:#548DD4' align='center'  >  &nbsp;  </td> </tr></table>";



        //SmtpClient smtp = new SmtpClient(System.Configuration.ConfigurationManager.AppSettings["SMTPServer"]);

      
        //try
        //{
        //    smtp.Send(mail);

        //}
        //catch (Exception ex)
        //{
        //    LogMsg.Log(ex.Message, 5, false);
        //}


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
                        //andy-個人修改成功寄信
                        clsMyObj.RegistSuccessUpdate(myActivityRegistVO.activity_id.ToString(), myActivityRegistVO.emp_id, myActivityRegistVO.regist_by, webPath, path);

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

                // andy add  2011/3/28 取消前需將資料加到 ActivityRegistCancel中
                myActivityRegistDAO.InsertActivityRegistCancel(activity_id, emp_id, "1", clsAuth.ID);

                if (Convert.ToDateTime(regist_deadline) >=DateTime.Today)
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
                else if (Convert.ToDateTime(regist_deadline) < DateTime.Today)
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
                else if (Convert.ToDateTime(cancelregist_deadline) < DateTime.Today)
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
                        //團隊修改寄信
                        clsMyObj.RegistSuccess_Team(myActivityRegistVO.activity_id.ToString(), strEmp_id, myActivityRegistVO.regist_by, webPath, path);
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
               // myActivityRegistDAO.InsertActivityRegistCancel(activity_id, emp_id, "2", clsAuth.ID);


                if (Convert.ToDateTime(regist_deadline) >= DateTime.Today)
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
                else if (Convert.ToDateTime(regist_deadline) < DateTime.Today)
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
                else if (Convert.ToDateTime(cancelregist_deadline) < DateTime.Today)
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