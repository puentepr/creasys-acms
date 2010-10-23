using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// ActivatyVo 的摘要描述
/// </summary>
namespace ACMS.VO
{
    public partial class ActivityRegistVO : BaseVO
    {
        public ActivityRegistVO()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        private int _id;
        private Guid _activity_id;
        private string _emp_id;
        private string _regist_by;
        private string _idno;
        private string _team_name;
        private int? _ext_people;
        private DateTime _createat;
        private int _check_status;
        

        public int id { get { return _id; } set { _id = value; } }
        public Guid activity_id { get { return _activity_id; } set { _activity_id = value; } }
        public string emp_id { get { return _emp_id; } set { _emp_id = value; } }
        public string regist_by { get { return _regist_by; } set { _regist_by = value; } }
        public string idno { get { return _idno; } set { _idno = value; } }
        public string team_name { get { return _team_name; } set { _team_name = value; } } 
        public int? ext_people { get { return _ext_people; } set { _ext_people = value; } }
        public DateTime createat { get { return _createat; } set { _createat = value; } }
        public int check_status { get { return _check_status; } set { _check_status = value; } }

    }


}
