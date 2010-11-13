using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// ActivatyVo 的摘要描述
/// </summary>
namespace ACMS.VO
{
    public partial class RoleUserMappingVO : BaseVO
    {
        public RoleUserMappingVO()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        private int _id;
        private int _role_id;
        private int _unit_id;
        private string _emp_id;

        private string _role_name;
        private string _unit_name;
        private string _C_DEPT_ABBR;
        private string _WORK_ID;
        private string _NATIVE_NAME;

        public int id { get { return _id; } set { _id = value; } }
        public int role_id { get { return _role_id; } set { _role_id = value; } }
        public int unit_id { get { return _unit_id; } set { _unit_id = value; } }
        public string emp_id { get { return _emp_id; } set { _emp_id = value; } }

        public string role_name { get { return _role_name; } set { _role_name = value; } }
        public string unit_name { get { return _unit_name; } set { _unit_name = value; } }
        public string C_DEPT_ABBR { get { return _C_DEPT_ABBR; } set { _C_DEPT_ABBR = value; } }
        public string WORK_ID { get { return _WORK_ID; } set { _WORK_ID = value; } }
        public string NATIVE_NAME { get { return _NATIVE_NAME; } set { _NATIVE_NAME = value; } }

    }

}
