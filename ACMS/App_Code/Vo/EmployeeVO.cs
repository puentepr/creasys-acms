using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// ActivatyVo 的摘要描述
/// </summary>
namespace ACMS.VO
{
    public partial class EmployeeVO : BaseVO
    {
        private int _keyID;
        private bool _keyValue;
        private string _ID;
        private string _NATIVE_NAME;
        private string _ENGLISH_NAME;
        private string _WORK_ID;
        private string _OFFICE_MAIL;
        private string _DEPT_ID;
        private string _C_DEPT_NAME;
        private string _C_DEPT_ABBR;
        private string _OFFICE_PHONE;
        private DateTime? _EXPERIENCE_START_DATE;
        private DateTime? _BIRTHDAY;
        private string _SEX;
        private string _JOB_CNAME;
        private string _STATUS;
        private DateTime? _WORK_END_DATE;
        private string _COMPANY_CODE;
        private string _C_NAME;

        public int keyID { get { return _keyID; } set { _keyID = value; } }
        public bool keyValue { get { return _keyValue; } set { _keyValue = value; } }
        public string ID { get { return _ID; } set { _ID = value; } }
        public string NATIVE_NAME { get { return _NATIVE_NAME; } set { _NATIVE_NAME = value; } }
        public string ENGLISH_NAME { get { return _ENGLISH_NAME; } set { _ENGLISH_NAME = value; } }
        public string WORK_ID { get { return _WORK_ID; } set { _WORK_ID = value; } }
        public string OFFICE_MAIL { get { return _OFFICE_MAIL; } set { _OFFICE_MAIL = value; } }
        public string DEPT_ID { get { return _DEPT_ID; } set { _DEPT_ID = value; } }
        public string C_DEPT_NAME { get { return _C_DEPT_NAME; } set { _C_DEPT_NAME = value; } }
        public string C_DEPT_ABBR { get { return _C_DEPT_ABBR; } set { _C_DEPT_ABBR = value; } }
        public string OFFICE_PHONE { get { return _OFFICE_PHONE; } set { _OFFICE_PHONE = value; } }
        public DateTime? EXPERIENCE_START_DATE { get { return _EXPERIENCE_START_DATE; } set { _EXPERIENCE_START_DATE = value; } }
        public DateTime? BIRTHDAY { get { return _BIRTHDAY; } set { _BIRTHDAY = value; } }
        public string SEX { get { return _SEX; } set { _SEX = value; } }
        public string JOB_CNAME { get { return _JOB_CNAME; } set { _JOB_CNAME = value; } }
        public string STATUS { get { return _STATUS; } set { _STATUS = value; } }
        public DateTime? WORK_END_DATE { get { return _WORK_END_DATE; } set { _WORK_END_DATE = value; } }
        public string COMPANY_CODE { get { return _COMPANY_CODE; } set { _COMPANY_CODE = value; } }
        public string C_NAME { get { return _C_NAME; } set { _C_NAME = value; } }

    }

}
