using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// ActivatyVo 的摘要描述
/// </summary>
namespace ACMS.VO
{
    public partial class ActivatyVO : BaseVO
    {
        public ActivatyVO()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        private Guid _id;
        private string _activity_type;
        private string _activity_info;
        private string _org_id;
        private string _activity_name;
        private string _people_type;
        private DateTime? _activity_startdate;
        private DateTime? _activity_enddate;
        private int? _limit_count;
        private int? _limit2_count;
        private int? _team_member_max;
        private int? _team_member_min;
        private DateTime? _regist_deadline;
        private DateTime? _regist_startdate;
        private DateTime? _cancelregist_deadline;
        private string _is_showfile;
        private string _is_showprogress;
        private string _is_showperson_fix1;
        private string _is_showperson_fix2;
        private int? _personextcount_max;
        private int? _personextcount_min;
        private string _is_showidno;
        private string _is_showremark;
        private string _remark_name;
        private string _is_showteam_fix1;
        private string _is_showteam_fix2;
        private int? _teamextcount_max;
        private int? _teamextcount_min;
        private string _is_grouplimit;
        private string _notice;
        private string _active;
        private string _emp_id;


        public string emp_id { get { return _emp_id; } set { _emp_id = value; } }
        public Guid id { get { return _id; } set { _id = value; } }
        public string activity_type { get { return _activity_type; } set { _activity_type = value; } }
        public string activity_info { get { return _activity_info; } set { _activity_info = value; } }
        public string org_id { get { return _org_id; } set { _org_id = value; } }
        public string activity_name { get { return _activity_name; } set { _activity_name = value; } }
        public string people_type { get { return _people_type; } set { _people_type = value; } }
        public DateTime? activity_startdate { get { return _activity_startdate; } set { _activity_startdate = value; } }
        public DateTime? activity_enddate { get { return _activity_enddate; } set { _activity_enddate = value; } }
        public int? limit_count { get { return _limit_count; } set { _limit_count = value; } }
        public int? limit2_count { get { return _limit2_count; } set { _limit2_count = value; } }
        public int? team_member_max { get { return _team_member_max; } set { _team_member_max = value; } }
        public int? team_member_min { get { return _team_member_min; } set { _team_member_min = value; } }
        public DateTime? regist_deadline { get { return _regist_deadline; } set { _regist_deadline = value; } }
        public DateTime? regist_startdate { get { return _regist_startdate; } set { _regist_startdate = value; } }
        public DateTime? cancelregist_deadline { get { return _cancelregist_deadline; } set { _cancelregist_deadline = value; } }
        public string is_showfile { get { return _is_showfile; } set { _is_showfile = value; } }
        public string is_showprogress { get { return _is_showprogress; } set { _is_showprogress = value; } }
        public string is_showperson_fix1 { get { return _is_showperson_fix1; } set { _is_showperson_fix1 = value; } }
        public string is_showperson_fix2 { get { return _is_showperson_fix2; } set { _is_showperson_fix2 = value; } }
        public int? personextcount_max { get { return _personextcount_max; } set { _personextcount_max = value; } }
        public int? personextcount_min { get { return _personextcount_min; } set { _personextcount_min = value; } }
        public string is_showidno { get { return _is_showidno; } set { _is_showidno = value; } }
        public string is_showremark { get { return _is_showremark; } set { _is_showremark = value; } }
        public string remark_name { get { return _remark_name; } set { _remark_name = value; } }
        public string is_showteam_fix1 { get { return _is_showteam_fix1; } set { _is_showteam_fix1 = value; } }
        public string is_showteam_fix2 { get { return _is_showteam_fix2; } set { _is_showteam_fix2 = value; } }
        public int? teamextcount_max { get { return _teamextcount_max; } set { _teamextcount_max = value; } }
        public int? teamextcount_min { get { return _teamextcount_min; } set { _teamextcount_min = value; } }
        public string is_grouplimit { get { return _is_grouplimit; } set { _is_grouplimit = value; } }
        public string notice { get { return _notice; } set { _notice = value; } }
        public string active { get { return _active; } set { _active = value; } }

    }


}
