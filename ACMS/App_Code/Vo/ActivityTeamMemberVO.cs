using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// ActivatyVo 的摘要描述
/// </summary>
namespace ACMS.VO
{
    [Serializable]
    public partial class ActivityTeamMemberVO : BaseVO
    {
        private Guid _activity_id;
        private string _emp_id;
        private string _boss_id;
        private int _idno_type;
        private string _idno;
        private string _remark;
        private int _check_status;


        private string _WORK_ID;
        private string _NATIVE_NAME;
        private string _C_DEPT_ABBR;

        private string _WritePersonInfo;



        public Guid activity_id { get { return _activity_id; } set { _activity_id = value; } }
        public string emp_id { get { return _emp_id; } set { _emp_id = value; } }
        public string boss_id { get { return _boss_id; } set { _boss_id = value; } }
        public string idno { get { return _idno; } set { _idno = value; } }
        public int idno_type { get { return _idno_type; } set { _idno_type = value; } }
        public string remark { get { return _remark; } set { _remark = value; } }
        public int check_status { get { return _check_status; } set { _check_status = value; } }

        public string WORK_ID { get { return _WORK_ID; } set { _WORK_ID = value; } }
        public string NATIVE_NAME { get { return _NATIVE_NAME; } set { _NATIVE_NAME = value; } }
        public string C_DEPT_ABBR { get { return _C_DEPT_ABBR; } set { _C_DEPT_ABBR = value; } }

        public string WritePersonInfo { get { return _WritePersonInfo; } set { _WritePersonInfo = value; } }





    }


}
